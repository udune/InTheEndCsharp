using System.Windows;
using System.Windows.Documents;
using CodingGuide.Core.Knowledge;

namespace CodingGuide.App.Rendering;

/// <summary>
/// AI 답변 화면. 질문, 진행 상태, (스트리밍으로 계속 갱신되는) 답변 본문, 참고한 문서 링크로 구성된다.
/// 같은 FlowDocument 안에서 본문 Section만 교체하므로 갱신 중에도 스크롤 위치가 유지된다.
/// </summary>
internal sealed class AnswerDocument
{
    private readonly Section _body = new();
    private readonly Run _status = new();

    public FlowDocument Document { get; }

    public AnswerDocument(string question, IReadOnlyList<KnowledgeDoc> sources, Action<string> navigate)
    {
        Document = new FlowDocument
        {
            FontFamily = new System.Windows.Media.FontFamily("Malgun Gothic, Segoe UI"),
            FontSize = 14.5,
            PagePadding = new Thickness(32, 22, 32, 36),
            Foreground = MarkdownRenderer.Brush("TextBrush"),
            TextAlignment = TextAlignment.Left,
        };

        Document.Blocks.Add(new Paragraph(new Run("AI 답변 · 로컬 모델 (인터넷 사용 안 함)"))
        {
            Foreground = MarkdownRenderer.Brush("AccentBrush"),
            FontSize = 12.5,
            FontWeight = FontWeights.SemiBold,
            Margin = new Thickness(0, 0, 0, 2),
        });
        // 긴 질문은 첫 줄만 제목으로 쓰고, 전체 질문은 아래 인용 상자에 줄바꿈을 살려 보여준다.
        string firstLine = question.Split('\n').Select(l => l.Trim()).FirstOrDefault(l => l.Length > 0) ?? question;
        string title = firstLine.Length > 80 ? firstLine[..80] + "…" : firstLine;
        Document.Blocks.Add(MarkdownRenderer.Heading(title, 1));
        if (title != question.Trim())
        {
            var full = new Paragraph
            {
                FontFamily = new System.Windows.Media.FontFamily("D2Coding, Consolas, Malgun Gothic"),
                FontSize = 13,
                Background = MarkdownRenderer.Brush("CodeBackgroundBrush"),
                BorderBrush = MarkdownRenderer.Brush("BorderBrush"),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(12, 8, 12, 8),
                Margin = new Thickness(0, 0, 0, 6),
            };
            var lines = question.Replace("\r\n", "\n").Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                full.Inlines.Add(new Run(lines[i]));
                if (i < lines.Length - 1) full.Inlines.Add(new LineBreak());
            }
            Document.Blocks.Add(full);
        }
        Document.Blocks.Add(new Paragraph(_status)
        {
            Foreground = MarkdownRenderer.Brush("SubtleTextBrush"),
            Background = MarkdownRenderer.Brush("QuoteBackgroundBrush"),
            Padding = new Thickness(12, 6, 12, 6),
            Margin = new Thickness(0, 4, 0, 12),
            FontSize = 13,
        });
        Document.Blocks.Add(_body);

        if (sources.Count > 0)
        {
            Document.Blocks.Add(MarkdownRenderer.Heading("참고한 가이드 문서", 2));
            var list = new List { MarkerStyle = TextMarkerStyle.None, Padding = new Thickness(0), Margin = new Thickness(0) };
            foreach (var doc in sources)
            {
                var link = new Hyperlink(new Run(doc.Title))
                {
                    Foreground = MarkdownRenderer.Brush("AccentBrush"),
                    TextDecorations = null,
                    Cursor = System.Windows.Input.Cursors.Hand,
                };
                string id = doc.Id;
                link.Click += (_, _) => navigate(id);
                var p = new Paragraph { Margin = new Thickness(0, 2, 0, 2) };
                p.Inlines.Add(new Run("→ ") { Foreground = MarkdownRenderer.Brush("SubtleTextBrush") });
                p.Inlines.Add(link);
                p.Inlines.Add(new Run($"  · {doc.Summary}") { Foreground = MarkdownRenderer.Brush("SubtleTextBrush"), FontSize = 12 });
                list.ListItems.Add(new ListItem(p));
            }
            Document.Blocks.Add(list);
        }
    }

    public void SetStatus(string text) => _status.Text = text;

    public void SetMarkdown(string markdown)
    {
        _body.Blocks.Clear();
        foreach (var block in MarkdownRenderer.ParseBlocks(markdown))
            _body.Blocks.Add(block);
    }
}
