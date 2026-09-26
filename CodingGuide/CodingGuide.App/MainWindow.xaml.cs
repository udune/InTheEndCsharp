using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CodingGuide.App.Rendering;
using CodingGuide.App.Running;
using CodingGuide.Core.Knowledge;
using CodingGuide.Core.Search;

namespace CodingGuide.App;

public sealed record ResultItem(string Id, string Title, string Category, string Snippet);

public partial class MainWindow : Window
{
    const string HomeDocId = "guide-howto";
    const int MaxOutputChars = 400_000;

    private readonly KnowledgeBase _kb;
    private readonly SearchEngine _engine;
    private readonly LessonRunner _runner = new();
    private readonly ConsoleRouter _console = new();
    private readonly DispatcherTimer _searchDebounce;
    private readonly DispatcherTimer _outputPump;
    private readonly Stack<string> _history = new();
    private readonly Stopwatch _runWatch = new();

    private KnowledgeDoc? _current;
    private List<(string Path, string Text)> _sources = [];
    private bool _running;
    private bool _suppressSelection;
    private int _outputChars;
    private double _bottomHeight = 300;

    public MainWindow()
    {
        InitializeComponent();

        var loadWatch = Stopwatch.StartNew();
        _kb = KnowledgeBase.LoadEmbedded();
        _engine = new SearchEngine(_kb);
        StatsText.Text = $"문서 {_kb.Docs.Count}개 · 실행 가능한 예제 {_runner.Count}개 · 색인 {_engine.TermCount:N0}단어 · 준비 {loadWatch.ElapsedMilliseconds}ms";

        // 예제의 Console 출력을 화면으로 가져온다. 입력은 없음(null)으로 둔다.
        Console.SetOut(_console);
        Console.SetError(_console);
        Console.SetIn(TextReader.Null);

        _searchDebounce = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(120) };
        _searchDebounce.Tick += (_, _) => RunSearch();
        _outputPump = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(80) };
        _outputPump.Tick += (_, _) => PumpOutput();
        _outputPump.Start();

        if (Environment.GetCommandLineArgs().Contains("--selftest"))
        {
            RunSelfTest();
            return;
        }

        BuildToc();
        ShowToc();
        Open(_kb.ById[HomeDocId], addHistory: false);
        Loaded += (_, _) => SearchBox.Focus();
    }

    // ── 목차 ───────────────────────────────────────────────

    private void BuildToc()
    {
        foreach (var category in _kb.Categories)
        {
            var docs = _kb.Docs.Where(d => d.Category == category).ToList();
            var categoryItem = new TreeViewItem
            {
                Header = new TextBlock
                {
                    Text = $"{category}  ({docs.Count})",
                    FontWeight = FontWeights.SemiBold,
                    Margin = new Thickness(0, 3, 0, 3),
                },
                IsExpanded = category == _kb.ById[HomeDocId].Category,
            };

            foreach (var doc in docs)
            {
                var header = new TextBlock { Margin = new Thickness(0, 2, 0, 2), TextTrimming = TextTrimming.CharacterEllipsis };
                if (IsRunnable(doc))
                    header.Inlines.Add(new System.Windows.Documents.Run("▶ ") { Foreground = (System.Windows.Media.Brush)FindResource("AccentBrush"), FontSize = 10 });
                header.Inlines.Add(new System.Windows.Documents.Run(doc.Title));
                categoryItem.Items.Add(new TreeViewItem { Header = header, Tag = doc.Id, ToolTip = doc.Summary });
            }
            TocTree.Items.Add(categoryItem);
        }
    }

    private void TocTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue is TreeViewItem { Tag: string id })
            Open(_kb.ById[id]);
    }

    private void ShowToc()
    {
        ResultList.Visibility = Visibility.Collapsed;
        TocTree.Visibility = Visibility.Visible;
        ResultInfo.Text = $"전체 목차 · {_kb.Categories.Count}개 분류 · ▶ 표시는 실행 가능한 예제";
    }

    // ── 검색 ───────────────────────────────────────────────

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        bool empty = SearchBox.Text.Length == 0;
        SearchPlaceholder.Visibility = empty ? Visibility.Visible : Visibility.Collapsed;
        ClearButton.Visibility = empty ? Visibility.Collapsed : Visibility.Visible;
        _searchDebounce.Stop();
        _searchDebounce.Start();
    }

    private void RunSearch()
    {
        _searchDebounce.Stop();
        string query = SearchBox.Text.Trim();
        if (query.Length == 0)
        {
            ShowToc();
            return;
        }

        var watch = Stopwatch.StartNew();
        var hits = _engine.Search(query, limit: 40);
        var items = hits.Select(h => new ResultItem(h.Doc.Id, h.Doc.Title, h.Doc.Category, h.Snippet)).ToList();

        _suppressSelection = true;
        ResultList.ItemsSource = items;
        ResultList.SelectedIndex = items.Count > 0 ? 0 : -1;
        _suppressSelection = false;

        TocTree.Visibility = Visibility.Collapsed;
        ResultList.Visibility = Visibility.Visible;
        ResultInfo.Text = items.Count == 0
            ? "검색 결과가 없습니다. 다른 표현이나 영어 키워드로 물어보세요."
            : $"검색 결과 {items.Count}건 · {watch.ElapsedMilliseconds}ms · ↑↓ 로 이동";

        // 입력하는 대로 가장 관련 있는 문서를 바로 보여준다. (타이핑마다 기록이 쌓이지 않도록 기록은 남기지 않음)
        if (items.Count > 0)
        {
            ResultList.ScrollIntoView(items[0]);
            Open(_kb.ById[items[0].Id], addHistory: false);
        }
    }

    private void ResultList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!_suppressSelection && ResultList.SelectedItem is ResultItem item)
            Open(_kb.ById[item.Id]);
    }

    private void SearchBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Down when ResultList.Visibility == Visibility.Visible:
                MoveResultSelection(+1);
                e.Handled = true;
                break;
            case Key.Up when ResultList.Visibility == Visibility.Visible:
                MoveResultSelection(-1);
                e.Handled = true;
                break;
            case Key.Enter:
                RunSearch();
                e.Handled = true;
                break;
            case Key.Escape:
                SearchBox.Clear();
                e.Handled = true;
                break;
        }
    }

    private void MoveResultSelection(int delta)
    {
        if (ResultList.Items.Count == 0) return;
        int index = Math.Clamp(ResultList.SelectedIndex + delta, 0, ResultList.Items.Count - 1);
        ResultList.SelectedIndex = index;
        ResultList.ScrollIntoView(ResultList.SelectedItem);
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        SearchBox.Clear();
        SearchBox.Focus();
    }

    // ── 문서 열기 ───────────────────────────────────────────

    private void Open(KnowledgeDoc doc, bool addHistory = true)
    {
        if (addHistory && _current != null && _current.Id != doc.Id)
            _history.Push(_current.Id);
        _current = doc;
        BackButton.IsEnabled = _history.Count > 0;
        Breadcrumb.Text = $"{doc.Category}  ›  {doc.Title}";
        DocViewer.Document = MarkdownRenderer.Render(doc, _kb, id => Open(_kb.ById[id]));

        _sources = _kb.GetSources(doc).ToList();
        SourcePicker.ItemsSource = _sources.Select(s => s.Path).ToList();
        SourceBar.Visibility = _sources.Count > 1 ? Visibility.Visible : Visibility.Collapsed;
        if (_sources.Count > 0)
            SourcePicker.SelectedIndex = 0;
        else
            CodeEditor.Text = "";

        bool hasLesson = doc.Lessons.Any(_runner.Exists);
        UpdateRunButton();
        SetBottomPanelVisible(_sources.Count > 0 || hasLesson);
        if (!hasLesson && !_running)
            BottomTabs.SelectedIndex = 0;
    }

    private bool IsRunnable(KnowledgeDoc doc) => doc.Runnable && doc.Lessons.Any(_runner.Exists);

    private void UpdateRunButton()
    {
        bool runnable = _current != null && IsRunnable(_current);
        RunButton.IsEnabled = runnable && !_running;
        RunButton.ToolTip = _current switch
        {
            _ when _running => "다른 예제를 실행하는 중입니다",
            { Runnable: false } => "키보드 입력이 필요한 예제라 여기서는 실행할 수 없습니다. 원래 콘솔 프로젝트에서 실행하세요.",
            _ when !runnable => "이 문서에는 실행할 예제가 없습니다",
            _ => "이 문서의 학습 예제를 실행합니다 (F5)",
        };
        ToolTipService.SetShowOnDisabled(RunButton, true);
    }

    private void SetBottomPanelVisible(bool visible)
    {
        if (!visible && BottomRow.ActualHeight > 60)
            _bottomHeight = BottomRow.ActualHeight;
        BottomRow.Height = visible ? new GridLength(_bottomHeight) : new GridLength(0);
        SplitterRow.Height = new GridLength(visible ? 5 : 0);
        BottomTabs.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        BottomSplitter.Visibility = BottomTabs.Visibility;
    }

    private void SourcePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int index = SourcePicker.SelectedIndex;
        if (index < 0 || index >= _sources.Count) return;
        CodeEditor.Text = _sources[index].Text;
        CodeEditor.ScrollToHome();
    }

    private void BackButton_Click(object sender, RoutedEventArgs e) => GoBack();

    private void GoBack()
    {
        if (_history.Count == 0) return;
        Open(_kb.ById[_history.Pop()], addHistory: false);
    }

    // ── 예제 실행 ───────────────────────────────────────────

    private async void RunButton_Click(object sender, RoutedEventArgs e)
    {
        if (_current is null || _running || !IsRunnable(_current)) return;
        var lessons = _current.Lessons.Where(_runner.Exists).ToList();

        _running = true;
        UpdateRunButton();
        SetBottomPanelVisible(true);
        BottomTabs.SelectedItem = OutputTab;

        PumpOutput();                // 이전 실행에서 늦게 도착한 출력은 먼저 비운다
        OutputBox.Clear();
        _outputChars = 0;

        foreach (var lesson in lessons)
        {
            AppendOutput($"▶ {lesson}.실행()\n\n");
            _runWatch.Restart();
            try
            {
                await _runner.RunAsync(lesson);
                PumpOutput();
                AppendOutput($"\n■ 완료 · {_runWatch.Elapsed.TotalSeconds:F2}초\n");
            }
            catch (Exception ex)
            {
                PumpOutput();
                AppendOutput($"\n✖ 예외 발생: {ex.GetType().Name}\n  {ex.Message}\n");
            }
        }

        _running = false;
        RunStatus.Text = $"완료 ({_runWatch.Elapsed.TotalSeconds:F2}초) · 스레드 풀, 소멸자처럼 늦게 출력되는 내용은 이어서 표시됩니다.";
        UpdateRunButton();
    }

    private void PumpOutput()
    {
        string text = _console.Drain();
        if (text.Length > 0)
            AppendOutput(text);
        if (_running)
            RunStatus.Text = $"실행 중…  {_runWatch.Elapsed.TotalSeconds:F1}초";
    }

    private void AppendOutput(string text)
    {
        if (_outputChars >= MaxOutputChars) return;
        if (_outputChars + text.Length > MaxOutputChars)
            text = text[..(MaxOutputChars - _outputChars)] + "\n… 출력이 너무 많아 이후는 생략합니다.\n";
        _outputChars += text.Length;
        OutputBox.AppendText(text);
        OutputBox.ScrollToEnd();
    }

    private void ClearOutput_Click(object sender, RoutedEventArgs e)
    {
        _console.Drain();
        OutputBox.Clear();
        _outputChars = 0;
    }

    /// <summary>
    /// 배포 전 점검용: 모든 문서를 실제로 렌더링해 보고 실패한 문서를 selftest.txt에 기록한 뒤 종료한다.
    /// 사용법: CodingGuide.App.exe --selftest
    /// </summary>
    private void RunSelfTest()
    {
        var failures = new List<string>();
        foreach (var doc in _kb.Docs)
        {
            try { MarkdownRenderer.Render(doc, _kb, _ => { }); }
            catch (Exception ex) { failures.Add($"{doc.Id}: {ex.GetType().Name} {ex.Message}"); }
        }
        File.WriteAllLines(Path.Combine(AppContext.BaseDirectory, "selftest.txt"),
            [$"rendered={_kb.Docs.Count} failed={failures.Count}", .. failures]);
        Application.Current.Shutdown(failures.Count == 0 ? 0 : 1);
    }

    // ── 단축키 ──────────────────────────────────────────────

    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        bool ctrl = Keyboard.Modifiers.HasFlag(ModifierKeys.Control);
        if (ctrl && e.Key is Key.F or Key.L)
        {
            SearchBox.Focus();
            SearchBox.SelectAll();
            e.Handled = true;
        }
        else if (e.Key == Key.F5)
        {
            if (RunButton.IsEnabled) RunButton_Click(RunButton, new RoutedEventArgs());
            e.Handled = true;
        }
        else if (e.Key == Key.System && e.SystemKey == Key.Left)
        {
            GoBack();
            e.Handled = true;
        }
    }
}
