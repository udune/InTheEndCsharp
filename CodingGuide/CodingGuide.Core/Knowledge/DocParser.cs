using System.Text;

namespace CodingGuide.Core.Knowledge;

/// <summary>
/// "---"로 감싼 front matter + 마크다운 본문 형식의 문서를 읽는다.
/// <code>
/// ---
/// id: linq-where
/// title: Where로 걸러내기
/// category: LINQ
/// keywords: 필터, 거르기, filter
/// ---
/// 본문...
/// </code>
/// </summary>
public static class DocParser
{
    public static KnowledgeDoc Parse(string text, string sourceName)
    {
        text = text.Replace("\r\n", "\n").TrimStart('﻿');
        if (!text.StartsWith("---\n"))
            throw new FormatException($"{sourceName}: front matter(---)로 시작해야 합니다.");

        int end = text.IndexOf("\n---", 4, StringComparison.Ordinal);
        if (end < 0)
            throw new FormatException($"{sourceName}: front matter가 닫히지 않았습니다.");

        var meta = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var rawLine in text[4..end].Split('\n'))
        {
            var line = rawLine.Trim();
            if (line.Length == 0 || line.StartsWith('#')) continue;
            int colon = line.IndexOf(':');
            if (colon <= 0)
                throw new FormatException($"{sourceName}: 잘못된 front matter 줄 '{line}'");
            meta[line[..colon].Trim()] = line[(colon + 1)..].Trim();
        }

        int bodyStart = text.IndexOf('\n', end + 1);
        string body = bodyStart < 0 ? "" : text[(bodyStart + 1)..].Trim();
        var (prose, code) = SplitCode(body);

        string Required(string key) =>
            meta.TryGetValue(key, out var v) && v.Length > 0
                ? v
                : throw new FormatException($"{sourceName}: '{key}' 항목이 필요합니다.");

        return new KnowledgeDoc
        {
            Id = Required("id"),
            Title = Required("title"),
            Category = Required("category"),
            Order = meta.TryGetValue("order", out var o) && int.TryParse(o, out var n) ? n : int.MaxValue,
            Summary = meta.GetValueOrDefault("summary", ""),
            Keywords = SplitList(meta.GetValueOrDefault("keywords")),
            Lessons = SplitList(meta.GetValueOrDefault("lesson")),
            Sources = SplitList(meta.GetValueOrDefault("source")),
            Related = SplitList(meta.GetValueOrDefault("related")),
            Runnable = !string.Equals(meta.GetValueOrDefault("runnable"), "false", StringComparison.OrdinalIgnoreCase),
            Body = body,
            ProseText = prose,
            CodeText = code,
        };
    }

    static IReadOnlyList<string> SplitList(string? value) =>
        string.IsNullOrWhiteSpace(value)
            ? []
            : value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    static (string Prose, string Code) SplitCode(string body)
    {
        var prose = new StringBuilder();
        var code = new StringBuilder();
        bool inCode = false;
        foreach (var line in body.Split('\n'))
        {
            if (line.TrimStart().StartsWith("```"))
            {
                inCode = !inCode;
                continue;
            }
            (inCode ? code : prose).AppendLine(line);
        }
        return (prose.ToString(), code.ToString());
    }
}
