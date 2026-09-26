using System.Text;

namespace CodingGuide.Core.Search;

/// <summary>
/// 한국어/영어가 섞인 텍스트를 검색용 토큰으로 자른다.
/// <list type="bullet">
/// <item>한글 연속 구간은 2글자씩 겹쳐 자른다(bigram). "리스트에서" → 리스, 스트, 트에, 에서.
///   형태소 분석기 없이도 조사가 붙은 단어를 찾을 수 있다.</item>
/// <item>영문/숫자 구간은 소문자 단어 하나로 두고, camelCase면 부분 단어도 함께 넣는다.
///   "SelectMany" → selectmany, select, many.</item>
/// </list>
/// </summary>
public static class Tokenizer
{
    public static List<string> Tokenize(string text)
    {
        var tokens = new List<string>();
        int i = 0;
        while (i < text.Length)
        {
            char c = text[i];
            if (IsHangul(c))
            {
                int start = i;
                while (i < text.Length && IsHangul(text[i])) i++;
                AddHangul(text.AsSpan(start, i - start), tokens);
            }
            else if (IsLatinOrDigit(c))
            {
                int start = i;
                while (i < text.Length && IsLatinOrDigit(text[i])) i++;
                AddLatin(text.Substring(start, i - start), tokens);
            }
            else
            {
                i++;
            }
        }
        return tokens;
    }

    /// <summary>공백/기호를 없애고 소문자로 만든다. 구(phrase) 단위 포함 여부를 비교할 때 쓴다.</summary>
    public static string Compact(string text)
    {
        var sb = new StringBuilder(text.Length);
        foreach (char c in text)
        {
            if (IsHangul(c) || IsLatinOrDigit(c))
                sb.Append(char.ToLowerInvariant(c));
        }
        return sb.ToString();
    }

    public static bool IsHangul(char c) => c is >= '가' and <= '힣';

    static bool IsLatinOrDigit(char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or >= '0' and <= '9';

    public static bool IsLatinToken(string token) => token.Length > 0 && IsLatinOrDigit(token[0]);

    static void AddHangul(ReadOnlySpan<char> run, List<string> tokens)
    {
        if (run.Length == 1)
        {
            tokens.Add(run.ToString());
            return;
        }
        for (int j = 0; j + 1 < run.Length; j++)
            tokens.Add(run.Slice(j, 2).ToString());
    }

    static void AddLatin(string word, List<string> tokens)
    {
        string lower = word.ToLowerInvariant();
        // 한 글자 영문(a, b, i 같은 변수명)은 검색에 도움이 안 되므로 버린다. 숫자는 남긴다(예: "0").
        if (lower.Length < 2 && !char.IsDigit(lower[0])) return;
        tokens.Add(lower);

        var parts = SplitCamelCase(word);
        if (parts.Count > 1)
        {
            foreach (var part in parts)
            {
                if (part.Length >= 2) tokens.Add(part.ToLowerInvariant());
            }
        }
    }

    /// <summary>"GetValueOrDefault" → Get, Value, Or, Default / "HTTPClient" → HTTP, Client / "CS0103" → CS, 0103</summary>
    static List<string> SplitCamelCase(string word)
    {
        var parts = new List<string>();
        int start = 0;
        for (int k = 1; k < word.Length; k++)
        {
            char prev = word[k - 1], cur = word[k];
            bool boundary =
                (char.IsLower(prev) && char.IsUpper(cur)) ||
                (char.IsLetter(prev) != char.IsLetter(cur)) ||
                (char.IsUpper(prev) && char.IsUpper(cur) && k + 1 < word.Length && char.IsLower(word[k + 1]));
            if (boundary)
            {
                parts.Add(word[start..k]);
                start = k;
            }
        }
        parts.Add(word[start..]);
        return parts;
    }
}
