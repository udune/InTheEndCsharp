using CodingGuide.Core.Knowledge;

namespace CodingGuide.Tests;

/// <summary>
/// 지식베이스 자체의 무결성 검사. 문서를 추가/수정한 뒤 이 테스트가 통과하면
/// 링크 깨짐, 중복 id, 예제 누락 없이 배포할 수 있다.
/// </summary>
public class KnowledgeBaseTests
{
    static readonly KnowledgeBase Kb = KnowledgeBase.LoadEmbedded();

    static readonly IReadOnlyDictionary<string, System.Reflection.MethodInfo> Lessons =
        LessonLocator.Find(typeof(InTheEndCsharp.값타입및선언.정수형타입).Assembly);

    [Fact]
    public void 문서가_로드된다()
    {
        Assert.True(Kb.Docs.Count >= 200, $"문서 수: {Kb.Docs.Count}");
        Assert.NotEmpty(Kb.SynonymText);
    }

    [Fact]
    public void 모든_문서에_요약과_본문이_있다()
    {
        var empty = Kb.Docs.Where(d => d.Summary.Length < 10 || d.Body.Length < 50).Select(d => d.Id).ToList();
        Assert.Empty(empty);
    }

    [Fact]
    public void 관련_문서_링크가_모두_존재한다()
    {
        var broken = Kb.Docs
            .SelectMany(d => d.Related.Where(r => !Kb.ById.ContainsKey(r)).Select(r => $"{d.Id} → {r}"))
            .ToList();
        Assert.Empty(broken);
    }

    [Fact]
    public void 추가_소스_경로가_모두_존재한다()
    {
        var missing = Kb.Docs
            .SelectMany(d => d.Sources.Where(s => !Kb.SourceFiles.ContainsKey(s)).Select(s => $"{d.Id} → {s}"))
            .ToList();
        Assert.Empty(missing);
    }

    [Fact]
    public void 문서의_레슨은_실제_예제_클래스다()
    {
        var unknown = Kb.Docs
            .SelectMany(d => d.Lessons.Where(l => !Lessons.ContainsKey(l)).Select(l => $"{d.Id} → {l}"))
            .ToList();
        Assert.Empty(unknown);
    }

    [Fact]
    public void 모든_예제에_문서가_있다()
    {
        var documented = Kb.Docs.SelectMany(d => d.Lessons).ToHashSet();
        var undocumented = Lessons.Keys.Where(l => !documented.Contains(l)).Order().ToList();
        Assert.Empty(undocumented);
    }

    [Fact]
    public void 모든_레슨의_소스를_찾을_수_있다()
    {
        var noSource = Kb.Docs.SelectMany(d => d.Lessons).Where(l => !Kb.HasLessonSource(l)).ToList();
        Assert.Empty(noSource);
    }

    [Fact]
    public void 코드_블록이_모두_닫혀_있다()
    {
        var unclosed = Kb.Docs
            .Where(d => d.Body.Split('\n').Count(l => l.TrimStart().StartsWith("```")) % 2 != 0)
            .Select(d => d.Id)
            .ToList();
        Assert.Empty(unclosed);
    }
}
