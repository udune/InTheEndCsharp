namespace CodingGuide.Core.Search;

/// <summary>
/// 동의어 사전. 한 줄이 한 그룹이며, 질문에 그룹의 어떤 표현이 들어 있으면 나머지 표현도 함께 찾는다.
/// <code>
/// # 주석
/// 걸러내기, 거르기, 필터, 골라내기, 조건에 맞는, where, filter
/// </code>
/// 사람이 쓰는 말("걸러내고 싶어")과 문서에 쓰인 용어("Where")를 이어 주는 역할을 한다.
/// </summary>
public sealed class SynonymDictionary
{
    readonly List<Group> _groups = [];

    sealed record Group(IReadOnlyList<string> Terms, IReadOnlyList<string> CompactTerms);

    public int GroupCount => _groups.Count;

    public static SynonymDictionary Parse(string text)
    {
        var dict = new SynonymDictionary();
        foreach (var raw in text.Replace("\r\n", "\n").Split('\n'))
        {
            var line = raw.Trim();
            if (line.Length == 0 || line.StartsWith('#')) continue;
            var terms = line.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (terms.Length < 2) continue;
            dict._groups.Add(new Group(terms, terms.Select(Tokenizer.Compact).ToList()));
        }
        return dict;
    }

    /// <summary>
    /// 질문과 맞닿은 그룹들의 "다른" 표현들을 돌려준다.
    /// 한글 표현은 질문에 부분 문자열로 들어 있으면 일치로 보고(조사 대응),
    /// 영문 표현은 질문의 영문 단어와 정확히 같아야 일치로 본다("where"가 "nowhere"에 걸리지 않도록).
    /// </summary>
    public IEnumerable<string> Expand(string query)
    {
        string compactQuery = Tokenizer.Compact(query);
        var latinWords = Tokenizer.Tokenize(query).Where(Tokenizer.IsLatinToken).ToHashSet();
        var result = new List<string>();

        foreach (var group in _groups)
        {
            var matched = new HashSet<int>();
            for (int i = 0; i < group.Terms.Count; i++)
            {
                string term = group.CompactTerms[i];
                if (term.Length == 0) continue;
                bool hit = Tokenizer.IsLatinToken(term)
                    ? latinWords.Contains(term)
                    : term.Length >= 2 && compactQuery.Contains(term, StringComparison.Ordinal);
                if (hit) matched.Add(i);
            }
            if (matched.Count == 0) continue;

            for (int i = 0; i < group.Terms.Count; i++)
            {
                if (!matched.Contains(i)) result.Add(group.Terms[i]);
            }
        }
        return result.Distinct();
    }
}
