using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using CodingGuide.Core.Knowledge;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;

namespace CodingGuide.App.Rendering;

/// <summary>
/// 지식 문서의 마크다운(이 프로젝트에서 쓰는 부분 집합)을 WPF FlowDocument로 그린다.
/// 지원: #/##/### 제목, 문단, - 목록, 1. 목록, > 인용, | 표 |, ``` 코드 블록(C# 문법 강조), `인라인 코드`, **굵게**
/// </summary>
internal static partial class MarkdownRenderer
{
    static readonly FontFamily BodyFont = new("Malgun Gothic, Segoe UI");
    static readonly FontFamily MonoFont = new("D2Coding, Consolas, Malgun Gothic");

    public static FlowDocument Render(KnowledgeDoc doc, KnowledgeBase kb, Action<string> navigate)
    {
        var fd = new FlowDocument
        {
            FontFamily = BodyFont,
            FontSize = 14.5,
            PagePadding = new Thickness(32, 22, 32, 36),
            Foreground = Brush("TextBrush"),
            TextAlignment = TextAlignment.Left,
        };

        fd.Blocks.Add(new Paragraph(new Run(doc.Category))
        {
            Foreground = Brush("AccentBrush"),
            FontSize = 12.5,
            FontWeight = FontWeights.SemiBold,
            Margin = new Thickness(0, 0, 0, 2),
        });
        fd.Blocks.Add(Heading(doc.Title, 1));
        if (doc.Summary.Length > 0)
        {
            fd.Blocks.Add(new Paragraph(new Run(doc.Summary))
            {
                Foreground = Brush("SubtleTextBrush"),
                Margin = new Thickness(0, 0, 0, 14),
            });
        }

        foreach (var block in ParseBlocks(doc.Body))
            fd.Blocks.Add(block);

        var related = doc.Related.Where(kb.ById.ContainsKey).Select(id => kb.ById[id]).ToList();
        if (related.Count > 0)
        {
            fd.Blocks.Add(Heading("관련 문서", 2));
            var list = new List { MarkerStyle = TextMarkerStyle.None, Padding = new Thickness(0), Margin = new Thickness(0) };
            foreach (var r in related)
            {
                var link = new Hyperlink(new Run(r.Title)) { Foreground = Brush("AccentBrush"), TextDecorations = null, Cursor = System.Windows.Input.Cursors.Hand };
                string id = r.Id;
                link.Click += (_, _) => navigate(id);
                var p = new Paragraph { Margin = new Thickness(0, 2, 0, 2) };
                p.Inlines.Add(new Run("→ ") { Foreground = Brush("SubtleTextBrush") });
                p.Inlines.Add(link);
                p.Inlines.Add(new Run($"  · {r.Category}") { Foreground = Brush("SubtleTextBrush"), FontSize = 12 });
                list.ListItems.Add(new ListItem(p));
            }
            fd.Blocks.Add(list);
        }

        return fd;
    }

    static IEnumerable<Block> ParseBlocks(string markdown)
    {
        var lines = markdown.Replace("\r\n", "\n").Split('\n');
        int i = 0;
        while (i < lines.Length)
        {
            string t = lines[i].Trim();
            if (t.Length == 0) { i++; continue; }

            if (t.StartsWith("```"))
            {
                string lang = t[3..].Trim().ToLowerInvariant();
                var code = new List<string>();
                i++;
                while (i < lines.Length && !lines[i].TrimStart().StartsWith("```"))
                    code.Add(lines[i++]);
                i++; // 닫는 ```
                foreach (var b in CodeBlock(string.Join("\n", code), lang)) yield return b;
                continue;
            }

            if (t.StartsWith('#'))
            {
                int level = t.TakeWhile(c => c == '#').Count();
                yield return Heading(t[level..].Trim(), level);
                i++;
                continue;
            }

            if (t.StartsWith('>'))
            {
                var quote = new List<string>();
                while (i < lines.Length && lines[i].TrimStart().StartsWith('>'))
                    quote.Add(lines[i++].TrimStart()[1..].Trim());
                yield return Quote(string.Join(" ", quote));
                continue;
            }

            if (t.StartsWith('|'))
            {
                var rows = new List<string>();
                while (i < lines.Length && lines[i].TrimStart().StartsWith('|'))
                    rows.Add(lines[i++].Trim());
                yield return Table(rows);
                continue;
            }

            if (IsBullet(t) || IsNumbered(t))
            {
                bool numbered = IsNumbered(t);
                var items = new List<string>();
                while (i < lines.Length && (numbered ? IsNumbered(lines[i].Trim()) : IsBullet(lines[i].Trim())))
                {
                    string item = lines[i++].Trim();
                    items.Add(numbered ? NumberedPrefix().Replace(item, "") : item[2..]);
                }
                yield return ListBlock(items, numbered);
                continue;
            }

            var para = new List<string>();
            while (i < lines.Length && lines[i].Trim() is { Length: > 0 } l && !IsSpecial(l))
            {
                para.Add(l);
                i++;
            }
            var p = new Paragraph { Margin = new Thickness(0, 4, 0, 8), LineHeight = 23 };
            AddInlines(p.Inlines, string.Join(" ", para));
            yield return p;
        }
    }

    static bool IsBullet(string t) => t.StartsWith("- ") || t.StartsWith("* ");
    static bool IsNumbered(string t) => NumberedPrefix().IsMatch(t);
    static bool IsSpecial(string t) =>
        t.StartsWith("```") || t.StartsWith('#') || t.StartsWith('>') || t.StartsWith('|') || IsBullet(t) || IsNumbered(t);

    static Paragraph Heading(string text, int level)
    {
        var p = new Paragraph
        {
            FontWeight = FontWeights.Bold,
            FontSize = level switch { 1 => 25, 2 => 18.5, _ => 15.5 },
            Margin = level switch { 1 => new Thickness(0, 0, 0, 6), 2 => new Thickness(0, 20, 0, 6), _ => new Thickness(0, 14, 0, 4) },
            Foreground = Brush(level == 1 ? "TextBrush" : "HeadingBrush"),
        };
        AddInlines(p.Inlines, text);
        return p;
    }

    static Paragraph Quote(string text)
    {
        var p = new Paragraph
        {
            BorderBrush = Brush("AccentBrush"),
            BorderThickness = new Thickness(3, 0, 0, 0),
            Background = Brush("QuoteBackgroundBrush"),
            Padding = new Thickness(12, 8, 12, 8),
            Margin = new Thickness(0, 6, 0, 10),
            FontSize = 13.5,
        };
        AddInlines(p.Inlines, text);
        return p;
    }

    static List ListBlock(List<string> items, bool numbered)
    {
        var list = new List
        {
            MarkerStyle = numbered ? TextMarkerStyle.Decimal : TextMarkerStyle.Disc,
            Margin = new Thickness(0, 2, 0, 8),
            Padding = new Thickness(22, 0, 0, 0),
        };
        foreach (var item in items)
        {
            var p = new Paragraph { Margin = new Thickness(0, 2, 0, 2), LineHeight = 22 };
            AddInlines(p.Inlines, item);
            list.ListItems.Add(new ListItem(p));
        }
        return list;
    }

    static Table Table(List<string> rows)
    {
        var cells = rows
            .Where(r => !TableSeparator().IsMatch(r))
            .Select(r => PipeSplit().Split(r.Trim().Trim('|')).Select(c => c.Replace("\\|", "|").Trim()).ToArray())
            .ToList();
        int columns = cells.Max(c => c.Length);

        var table = new Table
        {
            CellSpacing = 0,
            Margin = new Thickness(0, 6, 0, 12),
            BorderBrush = Brush("BorderBrush"),
            BorderThickness = new Thickness(1, 1, 0, 0),
            FontSize = 13.5,
        };
        for (int c = 0; c < columns; c++)
            table.Columns.Add(new TableColumn());

        var group = new TableRowGroup();
        for (int r = 0; r < cells.Count; r++)
        {
            var row = new TableRow();
            if (r == 0) { row.Background = Brush("TableHeaderBrush"); row.FontWeight = FontWeights.SemiBold; }
            for (int c = 0; c < columns; c++)
            {
                var p = new Paragraph { Margin = new Thickness(0) };
                AddInlines(p.Inlines, c < cells[r].Length ? cells[r][c] : "");
                row.Cells.Add(new TableCell(p)
                {
                    BorderBrush = Brush("BorderBrush"),
                    BorderThickness = new Thickness(0, 0, 1, 1),
                    Padding = new Thickness(8, 5, 8, 5),
                });
            }
            group.Rows.Add(row);
        }
        table.RowGroups.Add(group);
        return table;
    }

    static IEnumerable<Block> CodeBlock(string code, string lang)
    {
        // 복사 링크
        var copy = new Hyperlink(new Run("복사")) { Foreground = Brush("SubtleTextBrush"), TextDecorations = null, FontSize = 11.5 };
        copy.Click += (_, _) =>
        {
            try { Clipboard.SetText(code); } catch { /* 클립보드가 잠겨 있으면 무시 */ }
        };
        yield return new Paragraph(copy) { TextAlignment = TextAlignment.Right, Margin = new Thickness(0, 6, 0, 0) };

        var p = new Paragraph
        {
            FontFamily = MonoFont,
            FontSize = 13,
            Background = Brush("CodeBackgroundBrush"),
            BorderBrush = Brush("BorderBrush"),
            BorderThickness = new Thickness(1),
            Padding = new Thickness(14, 10, 14, 10),
            Margin = new Thickness(0, 0, 0, 12),
            LineHeight = 19,
        };

        var definition = lang switch
        {
            "csharp" or "cs" or "c#" => HighlightingManager.Instance.GetDefinition("C#"),
            "xml" => HighlightingManager.Instance.GetDefinition("XML"),
            "json" => HighlightingManager.Instance.GetDefinition("JavaScript"),
            _ => null,
        };

        if (definition == null)
        {
            var lines = code.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                p.Inlines.Add(new Run(lines[i]));
                if (i < lines.Length - 1) p.Inlines.Add(new LineBreak());
            }
        }
        else
        {
            var document = new TextDocument(code);
            var highlighter = new DocumentHighlighter(document, definition);
            for (int line = 1; line <= document.LineCount; line++)
            {
                foreach (var run in highlighter.HighlightLine(line).ToRichText().CreateRuns())
                    p.Inlines.Add(run);
                if (line < document.LineCount) p.Inlines.Add(new LineBreak());
            }
        }
        yield return p;
    }

    /// <summary>`코드` 와 **굵게** 를 인라인 요소로 바꾼다. 굵게 안의 `코드`도 처리한다.</summary>
    static void AddInlines(InlineCollection inlines, string text)
    {
        int pos = 0;
        foreach (Match m in InlineToken().Matches(text))
        {
            if (m.Index > pos) inlines.Add(new Run(text[pos..m.Index]));
            if (m.Groups[1].Success)
            {
                inlines.Add(new Run(m.Groups[1].Value)
                {
                    FontFamily = MonoFont,
                    Background = Brush("InlineCodeBackgroundBrush"),
                    Foreground = Brush("InlineCodeBrush"),
                    FontSize = 13.5,
                });
            }
            else
            {
                var bold = new Bold();
                AddInlines(bold.Inlines, m.Groups[2].Value);
                inlines.Add(bold);
            }
            pos = m.Index + m.Length;
        }
        if (pos < text.Length) inlines.Add(new Run(text[pos..]));
    }

    static Brush Brush(string key) => (Brush)Application.Current.Resources[key];

    [GeneratedRegex(@"`([^`]+)`|\*\*(.+?)\*\*")]
    private static partial Regex InlineToken();

    [GeneratedRegex(@"^\d+\.\s+")]
    private static partial Regex NumberedPrefix();

    [GeneratedRegex(@"^\|?\s*:?-{2,}")]
    private static partial Regex TableSeparator();

    [GeneratedRegex(@"(?<!\\)\|")]
    private static partial Regex PipeSplit();
}
