using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Text;
using CodingGuide.App.Ai;
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

    // 로컬 AI
    const string AnswerHistoryId = "__answer__";
    private readonly LocalLlm _llm = new();
    private readonly Stopwatch _askWatch = new();
    private readonly StringBuilder _answerText = new();
    private readonly Lock _answerLock = new();
    private AnswerDocument? _answer;
    private CancellationTokenSource? _askCts;
    private bool _showingAnswer;
    private bool _answerDirty;
    private int _answerTokens;
    private int _answerSourceCount;
    private double _firstTokenSeconds = -1;

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
        _outputPump.Tick += (_, _) =>
        {
            PumpOutput();
            RefreshAnswer();
        };
        _outputPump.Start();

        if (Environment.GetCommandLineArgs().Contains("--selftest"))
        {
            RunSelfTest();
            return;
        }

        BuildToc();
        ShowToc();
        Open(_kb.ById[HomeDocId], addHistory: false);
        Loaded += async (_, _) =>
        {
            SearchBox.Focus();
            await LoadLlmAsync();
        };
        Closed += (_, _) => _askCts?.Cancel();
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
            case Key.Enter when Keyboard.Modifiers.HasFlag(ModifierKeys.Control):
                _ = AskAsync();
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
        if (addHistory && _showingAnswer)
            _history.Push(AnswerHistoryId);
        else if (addHistory && _current != null && _current.Id != doc.Id)
            _history.Push(_current.Id);
        _showingAnswer = false;
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
        string id = _history.Pop();
        if (id == AnswerHistoryId)
        {
            if (_answer != null) ShowAnswer();
            else GoBack();
            return;
        }
        Open(_kb.ById[id], addHistory: false);
    }

    // ── 로컬 AI 질문 ────────────────────────────────────────

    private async Task LoadLlmAsync()
    {
        string? path = LocalLlm.FindModel();
        if (path is null)
        {
            AiStatus.Text = "AI 꺼짐 · models 폴더에 모델(.gguf)이 없습니다";
            AskButton.ToolTip = "실행 폴더의 models 폴더에 GGUF 모델 파일을 넣으면 AI 답변을 쓸 수 있습니다.";
            return;
        }

        AiStatus.Text = "AI 모델 불러오는 중…";
        var progress = new Progress<float>(p => AiStatus.Text = $"AI 모델 불러오는 중… {p:P0}");
        try
        {
            await Task.Run(() => _llm.LoadAsync(path, progress));
            AiStatus.Text = $"AI 준비됨 · {_llm.ModelName} · CPU · 오프라인";
            AskButton.IsEnabled = true;
        }
        catch (Exception ex)
        {
            AiStatus.Text = "AI 모델을 불러오지 못했습니다 (메모리 부족 또는 파일 손상)";
            AskButton.ToolTip = ex.Message;
        }
    }

    private async void AskButton_Click(object sender, RoutedEventArgs e) => await AskAsync();

    private void StopButton_Click(object sender, RoutedEventArgs e) => _askCts?.Cancel();

    private async Task AskAsync()
    {
        string question = SearchBox.Text.Trim();
        if (!_llm.IsLoaded || _askCts != null) return;
        if (question.Length == 0)
        {
            SearchBox.Focus();
            return;
        }

        // 질문으로 가이드 문서를 찾아 근거 자료로 붙인다 (RAG).
        var rag = RagPromptBuilder.Build(question, _engine);

        if (_showingAnswer) { }
        else if (_current != null) _history.Push(_current.Id);

        _answer = new AnswerDocument(question, rag.Sources, id => Open(_kb.ById[id]));
        lock (_answerLock)
        {
            _answerText.Clear();
            _answerTokens = 0;
            _firstTokenSeconds = -1;
            _answerDirty = false;
        }
        _answerSourceCount = rag.Sources.Count;
        ShowAnswer();

        _askCts = new CancellationTokenSource();
        var token = _askCts.Token;
        AskButton.IsEnabled = false;
        StopButton.Visibility = Visibility.Visible;
        _askWatch.Restart();
        RefreshAnswer();

        string result;
        try
        {
            // 추론은 CPU를 오래 쓰므로 UI 스레드가 아닌 곳에서 돌리고, 결과는 타이머가 주기적으로 화면에 옮긴다.
            await Task.Run(async () =>
            {
                await foreach (var piece in _llm.AskAsync(rag.SystemPrompt, rag.UserPrompt, token))
                {
                    lock (_answerLock)
                    {
                        if (_firstTokenSeconds < 0) _firstTokenSeconds = _askWatch.Elapsed.TotalSeconds;
                        _answerText.Append(piece);
                        _answerTokens++;
                        _answerDirty = true;
                    }
                }
            }, token);
            result = "완료";
        }
        catch (OperationCanceledException)
        {
            result = "중지됨";
        }
        catch (Exception ex)
        {
            result = $"오류: {ex.Message}";
        }

        _askWatch.Stop();
        _askCts.Dispose();
        _askCts = null;
        AskButton.IsEnabled = true;
        StopButton.Visibility = Visibility.Collapsed;
        RefreshAnswer();

        double first = _firstTokenSeconds < 0 ? _askWatch.Elapsed.TotalSeconds : _firstTokenSeconds;
        _answer.SetStatus($"{result} · {_answerTokens}토큰 · 첫 응답 {first:F0}초, 전체 {_askWatch.Elapsed.TotalSeconds:F0}초 · " +
                          "AI가 만든 답변은 틀릴 수 있으니 아래 참고 문서로 확인하세요.");
    }

    private void ShowAnswer()
    {
        if (_answer is null) return;
        _showingAnswer = true;
        _current = null;
        lock (_answerLock) _answerDirty = _answerText.Length > 0;   // 다른 문서를 보는 동안 쌓인 내용을 다시 그린다
        DocViewer.Document = _answer.Document;
        Breadcrumb.Text = "AI 답변";
        BackButton.IsEnabled = _history.Count > 0;
        UpdateRunButton();
        SetBottomPanelVisible(false);
    }

    /// <summary>생성 중인 답변을 화면에 반영한다 (80ms 타이머에서 호출).</summary>
    private void RefreshAnswer()
    {
        if (_answer is null || _askCts is null && !_answerDirty) return;

        string? text = null;
        int tokens;
        double first;
        lock (_answerLock)
        {
            if (_answerDirty && _showingAnswer)
            {
                text = _answerText.ToString();
                _answerDirty = false;
            }
            tokens = _answerTokens;
            first = _firstTokenSeconds;
        }

        double elapsed = _askWatch.Elapsed.TotalSeconds;
        if (_askCts is not null)
        {
            _answer.SetStatus(first < 0
                ? $"AI가 참고 문서 {_answerSourceCount}개를 읽는 중… {elapsed:F0}초  (이 PC에서는 보통 30초 안팎 · 그동안 아래 참고 문서를 먼저 보셔도 됩니다)"
                : $"답변 작성 중… {tokens}토큰 · 초당 {Math.Max(0, tokens - 1) / Math.Max(0.1, elapsed - first):F1}토큰");
        }

        if (text is null) return;
        var scroll = DocViewer.Template?.FindName("PART_ContentHost", DocViewer) as ScrollViewer;
        bool atBottom = scroll is null || scroll.VerticalOffset >= scroll.ScrollableHeight - 40;
        _answer.SetMarkdown(text);
        if (atBottom) scroll?.ScrollToEnd();
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
