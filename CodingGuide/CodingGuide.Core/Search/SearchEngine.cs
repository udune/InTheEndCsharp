using System.Text.RegularExpressions;
using CodingGuide.Core.Knowledge;

namespace CodingGuide.Core.Search;

public sealed record SearchHit(KnowledgeDoc Doc, double Score, string Snippet, IReadOnlyList<string> MatchedTokens);

/// <summary>
/// 지식베이스 전체를 메모리에 색인하고 BM25F 방식으로 순위를 매긴다.
/// 인터넷/외부 라이브러리 없이 동작한다.
///
/// 점수 = Σ(질문 토큰 가중치 × IDF × 필드가중 TF 포화값) × 커버리지 보정 + 키워드 구문 일치 보너스
/// - 필드 가중치: 제목·키워드 &gt; 요약 &gt; 본문 &gt; 코드 &gt; 예제 소스
/// - 질문 토큰: 원래 질문(1.0), 동의어 확장(0.6), 영문 오타 보정(0.7)
/// </summary>
public sealed partial class SearchEngine
{
    const double K1 = 1.2;

    enum Field { Title, Keywords, Summary, Prose, Code, Source }

    static readonly double[] FieldWeight = [4.0, 4.0, 2.0, 1.0, 0.6, 0.3];
    static readonly double[] FieldB = [0.3, 0.3, 0.5, 0.75, 0.75, 0.75];

    /// <summary>질문에서 검색에 도움이 안 되는 표현. 토큰화 전에 지운다.</summary>
    static readonly string[] StopPhrases =
    [
        "어떻게", "하는법", "하는 법", "하는 방법", "방법", "싶어요", "싶어", "싶다", "알려줘", "알려주세요",
        "뭐야", "뭔가요", "무엇인가요", "무엇", "하려면", "사용법", "쓰는법", "쓰는 법", "해줘", "주세요",
        "있나요", "있어", "하나요", "되나요", "인가요", "예제", "궁금", "c#", "C#", "씨샵",
        "구하기", "구하는", "언제 써", "언제", "뭔지", "이란", "란?",
    ];

    readonly KnowledgeBase _kb;
    readonly SynonymDictionary _synonyms;
    readonly int _fieldCount = FieldWeight.Length;

    // term → (docIndex → field별 등장 횟수)
    readonly Dictionary<string, Dictionary<int, int[]>> _postings = new(StringComparer.Ordinal);
    readonly int[][] _fieldLength;      // [doc][field]
    readonly double[] _avgFieldLength;  // [field]
    readonly string[][] _compactKeywords; // [doc][keyword]
    readonly HashSet<string> _latinVocabulary = new(StringComparer.Ordinal);

    public SearchEngine(KnowledgeBase kb)
    {
        _kb = kb;
        _synonyms = SynonymDictionary.Parse(kb.SynonymText);
        int n = kb.Docs.Count;
        _fieldLength = new int[n][];
        _avgFieldLength = new double[_fieldCount];
        _compactKeywords = new string[n][];

        for (int d = 0; d < n; d++)
        {
            var doc = kb.Docs[d];
            string sourceText = string.Join('\n', kb.GetSources(doc).Select(s => s.Text));
            string[] fieldTexts =
            [
                doc.Title,
                string.Join(' ', doc.Keywords),
                doc.Summary,
                doc.ProseText,
                doc.CodeText,
                sourceText,
            ];

            _fieldLength[d] = new int[_fieldCount];
            for (int f = 0; f < _fieldCount; f++)
            {
                var tokens = Tokenizer.Tokenize(fieldTexts[f]);
                _fieldLength[d][f] = tokens.Count;
                _avgFieldLength[f] += tokens.Count;
                foreach (var t in tokens)
                {
                    if (!_postings.TryGetValue(t, out var byDoc))
                        _postings[t] = byDoc = [];
                    if (!byDoc.TryGetValue(d, out var tf))
                        byDoc[d] = tf = new int[_fieldCount];
                    tf[f]++;
                    // 오타 보정 후보는 사람이 쓴 설명·제목에 나온 영문 단어만 쓴다(예제 소스의 변수명은 제외).
                    if (f != (int)Field.Source && Tokenizer.IsLatinToken(t) && t.Length >= 3)
                        _latinVocabulary.Add(t);
                }
            }

            _compactKeywords[d] = doc.Keywords.Append(doc.Title)
                .Select(Tokenizer.Compact)
                .Where(k => k.Length >= 2)
                .ToArray();
        }

        for (int f = 0; f < _fieldCount; f++)
            _avgFieldLength[f] = n == 0 ? 1 : Math.Max(1, _avgFieldLength[f] / n);
    }

    public int DocumentCount => _kb.Docs.Count;
    public int TermCount => _postings.Count;

    public IReadOnlyList<SearchHit> Search(string query, int limit = 30)
    {
        if (string.IsNullOrWhiteSpace(query)) return [];

        var weights = BuildQueryWeights(query);
        if (weights.Count == 0) return [];

        double totalWeight = weights.Values.Sum();
        var scores = new Dictionary<int, double>();
        var matchedWeight = new Dictionary<int, double>();
        var matchedTokens = new Dictionary<int, List<string>>();
        int n = _kb.Docs.Count;

        foreach (var (term, qWeight) in weights)
        {
            if (!_postings.TryGetValue(term, out var byDoc)) continue;
            double idf = Math.Log(1 + (n - byDoc.Count + 0.5) / (byDoc.Count + 0.5));

            foreach (var (d, tf) in byDoc)
            {
                double weightedTf = 0;
                for (int f = 0; f < _fieldCount; f++)
                {
                    if (tf[f] == 0) continue;
                    double norm = 1 - FieldB[f] + FieldB[f] * _fieldLength[d][f] / _avgFieldLength[f];
                    weightedTf += FieldWeight[f] * tf[f] / norm;
                }
                double s = qWeight * idf * weightedTf / (K1 + weightedTf);
                scores[d] = scores.GetValueOrDefault(d) + s;
                matchedWeight[d] = matchedWeight.GetValueOrDefault(d) + qWeight;
                if (!matchedTokens.TryGetValue(d, out var list))
                    matchedTokens[d] = list = [];
                list.Add(term);
            }
        }

        // 질문 토큰을 골고루 많이 포함한 문서를 우대한다.
        foreach (var d in scores.Keys.ToList())
        {
            double coverage = Math.Min(1, matchedWeight[d] / totalWeight);
            scores[d] *= 0.4 + 0.6 * coverage;
        }

        // 문서 키워드(또는 제목)가 질문에 구문 그대로 들어 있으면 큰 보너스. 예: "중복 제거", "CS0103"
        string compactQuery = Tokenizer.Compact(StripStopPhrases(query));
        for (int d = 0; d < n; d++)
        {
            double bonus = 0;
            foreach (var kw in _compactKeywords[d])
            {
                if (kw.Length >= 2 && compactQuery.Contains(kw, StringComparison.Ordinal))
                    bonus += 1.0 + Math.Min(kw.Length, 8) * 0.25;
            }
            if (bonus > 0)
                scores[d] = scores.GetValueOrDefault(d) + Math.Min(bonus, 6);
        }

        return scores
            .Where(kv => kv.Value > 0)
            .OrderByDescending(kv => kv.Value)
            .ThenBy(kv => _kb.Docs[kv.Key].Order)
            .Take(limit)
            .Select(kv =>
            {
                var doc = _kb.Docs[kv.Key];
                var tokens = matchedTokens.GetValueOrDefault(kv.Key) ?? [];
                return new SearchHit(doc, kv.Value, MakeSnippet(doc, tokens), tokens);
            })
            .ToList();
    }

    /// <summary>질문 → (토큰, 가중치). 원문 토큰, 동의어 확장, 오타 보정 순으로 채운다.</summary>
    internal Dictionary<string, double> BuildQueryWeights(string query)
    {
        var weights = new Dictionary<string, double>(StringComparer.Ordinal);
        void Add(string token, double w)
        {
            if (!weights.TryGetValue(token, out var old) || old < w)
                weights[token] = w;
        }

        string cleaned = StripStopPhrases(query);
        var original = Tokenizer.Tokenize(cleaned);
        foreach (var t in original) Add(t, 1.0);

        foreach (var term in _synonyms.Expand(cleaned))
        {
            var tokens = Tokenizer.Tokenize(term);
            if (tokens.Count == 0) continue;
            // 긴 한글 표현은 bigram이 여러 개 나오므로 가중치를 나눠 과대평가를 막는다.
            double w = 0.6 / Math.Sqrt(tokens.Count);
            foreach (var t in tokens) Add(t, w);
        }

        foreach (var t in original)
        {
            if (!Tokenizer.IsLatinToken(t) || t.Length < 4 || _postings.ContainsKey(t)) continue;
            foreach (var candidate in FuzzyCandidates(t))
                Add(candidate, 0.7);
        }

        return weights;
    }

    IEnumerable<string> FuzzyCandidates(string token)
    {
        int maxDistance = token.Length >= 8 ? 2 : 1;
        return _latinVocabulary
            .Where(v => Math.Abs(v.Length - token.Length) <= maxDistance)
            .Select(v => (Word: v, Distance: EditDistance(token, v)))
            .Where(x => x.Distance <= maxDistance)
            .OrderBy(x => x.Distance)
            .Take(3)
            .Select(x => x.Word);
    }

    /// <summary>인접 문자 뒤바뀜까지 1로 세는 편집 거리(Damerau–Levenshtein, OSA).</summary>
    internal static int EditDistance(string a, string b)
    {
        var dp = new int[a.Length + 1, b.Length + 1];
        for (int i = 0; i <= a.Length; i++) dp[i, 0] = i;
        for (int j = 0; j <= b.Length; j++) dp[0, j] = j;
        for (int i = 1; i <= a.Length; i++)
        {
            for (int j = 1; j <= b.Length; j++)
            {
                int cost = a[i - 1] == b[j - 1] ? 0 : 1;
                dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
                if (i > 1 && j > 1 && a[i - 1] == b[j - 2] && a[i - 2] == b[j - 1])
                    dp[i, j] = Math.Min(dp[i, j], dp[i - 2, j - 2] + 1);
            }
        }
        return dp[a.Length, b.Length];
    }

    static string StripStopPhrases(string query)
    {
        foreach (var p in StopPhrases)
            query = query.Replace(p, " ", StringComparison.OrdinalIgnoreCase);
        return query;
    }

    /// <summary>본문에서 일치 토큰이 가장 많이 들어 있는 줄을 골라 미리보기로 쓴다.</summary>
    static string MakeSnippet(KnowledgeDoc doc, IReadOnlyList<string> tokens)
    {
        string best = doc.Summary;
        int bestHits = 0;
        foreach (var raw in doc.ProseText.Split('\n'))
        {
            string line = CleanMarkdown(raw);
            if (line.Length < 8) continue;
            var lineTokens = Tokenizer.Tokenize(line).ToHashSet();
            int hits = tokens.Count(lineTokens.Contains);
            if (hits > bestHits)
            {
                bestHits = hits;
                best = line;
            }
        }
        return best.Length > 140 ? best[..140] + "…" : best;
    }

    static string CleanMarkdown(string line) =>
        MarkdownSymbols().Replace(line, "").Trim();

    [GeneratedRegex(@"^\s*(#+|[-*>]|\d+\.)\s*|\*\*|`|\|")]
    private static partial Regex MarkdownSymbols();
}
