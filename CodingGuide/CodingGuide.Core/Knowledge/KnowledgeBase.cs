using System.Reflection;
using System.Text.RegularExpressions;

namespace CodingGuide.Core.Knowledge;

/// <summary>
/// 어셈블리에 포함된 지식 문서, 동의어 사전, 학습 예제 소스를 모두 읽어 들인 저장소.
/// </summary>
public sealed partial class KnowledgeBase
{
    public IReadOnlyList<KnowledgeDoc> Docs { get; }
    public IReadOnlyDictionary<string, KnowledgeDoc> ById { get; }

    /// <summary>카테고리 목록. 각 카테고리 문서들의 최소 order 순서.</summary>
    public IReadOnlyList<string> Categories { get; }

    /// <summary>"폴더/파일.cs" 형태의 경로 → 소스 텍스트.</summary>
    public IReadOnlyDictionary<string, string> SourceFiles { get; }

    public string SynonymText { get; }

    readonly Dictionary<string, string> _lessonToSourcePath;

    public KnowledgeBase(IEnumerable<KnowledgeDoc> docs, IReadOnlyDictionary<string, string> sourceFiles, string synonymText)
    {
        Docs = docs.OrderBy(d => d.Order).ThenBy(d => d.Id, StringComparer.Ordinal).ToList();
        ById = Docs.ToDictionary(d => d.Id);
        Categories = Docs.GroupBy(d => d.Category).OrderBy(g => g.Min(d => d.Order)).Select(g => g.Key).ToList();
        SourceFiles = sourceFiles;
        SynonymText = synonymText;
        _lessonToSourcePath = IndexLessonSources(sourceFiles);
    }

    public static KnowledgeBase LoadEmbedded()
    {
        var asm = typeof(KnowledgeBase).Assembly;
        var docs = new List<KnowledgeDoc>();
        var sources = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        string synonyms = "";

        foreach (var name in asm.GetManifestResourceNames())
        {
            if (name.StartsWith("kb/") && name.EndsWith(".md"))
                docs.Add(DocParser.Parse(ReadResource(asm, name), name));
            else if (name.StartsWith("src/"))
                sources[name[4..].Replace('\\', '/')] = ReadResource(asm, name);
            else if (name == "synonyms.txt")
                synonyms = ReadResource(asm, name);
        }

        return new KnowledgeBase(docs, sources, synonyms);
    }

    /// <summary>문서에 연결된 소스 파일들 (레슨 소스 → 추가 소스 순).</summary>
    public IEnumerable<(string Path, string Text)> GetSources(KnowledgeDoc doc)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var lesson in doc.Lessons)
        {
            if (_lessonToSourcePath.TryGetValue(lesson, out var path) && seen.Add(path))
                yield return (path, SourceFiles[path]);
        }
        foreach (var path in doc.Sources)
        {
            if (SourceFiles.TryGetValue(path, out var text) && seen.Add(path))
                yield return (path, text);
        }
    }

    public bool HasLessonSource(string lesson) => _lessonToSourcePath.ContainsKey(lesson);

    /// <summary>레슨 클래스 이름 → 해당 클래스를 선언한 소스 경로. 파일 이름이 같으면 우선한다.</summary>
    static Dictionary<string, string> IndexLessonSources(IReadOnlyDictionary<string, string> sources)
    {
        var map = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var (path, text) in sources)
        {
            foreach (Match m in ClassDeclaration().Matches(text))
                map.TryAdd(m.Groups[1].Value, path);
        }
        foreach (var path in sources.Keys)
            map[Path.GetFileNameWithoutExtension(path)] = path;
        return map;
    }

    static string ReadResource(Assembly asm, string name)
    {
        using var stream = asm.GetManifestResourceStream(name)!;
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    [GeneratedRegex(@"\bclass\s+([\p{L}_][\p{L}\p{N}_]*)")]
    private static partial Regex ClassDeclaration();
}
