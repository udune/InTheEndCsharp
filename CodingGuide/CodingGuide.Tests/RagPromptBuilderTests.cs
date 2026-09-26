using CodingGuide.Core.Knowledge;
using CodingGuide.Core.Search;

namespace CodingGuide.Tests;

public class RagPromptBuilderTests
{
    static readonly SearchEngine Engine = new(KnowledgeBase.LoadEmbedded());

    [Fact]
    public void 질문과_관련된_문서를_참고_자료로_붙인다()
    {
        var prompt = RagPromptBuilder.Build("리스트에서 중복 제거하는 방법", Engine);

        Assert.InRange(prompt.Sources.Count, 1, 3);
        Assert.Contains("[참고 자료]", prompt.UserPrompt);
        Assert.EndsWith("리스트에서 중복 제거하는 방법", prompt.UserPrompt);
        Assert.Contains(prompt.Sources, d => d.Id is "linq-set-operations" or "hashset");
    }

    [Fact]
    public void 프롬프트_길이에_상한이_있다()
    {
        // CPU 추론은 입력이 길면 첫 응답까지 오래 걸리므로 프롬프트가 과도하게 커지지 않아야 한다.
        var prompt = RagPromptBuilder.Build("스레드 lock Monitor Mutex 세마포어 차이", Engine);
        Assert.True(prompt.UserPrompt.Length < 3 * 1400 + 600, $"길이 {prompt.UserPrompt.Length}");
    }

    [Fact]
    public void 잘린_코드_블록은_닫아준다()
    {
        string body = "설명\n```csharp\nint a = 1;\nint b = 2;\nint c = 3;\n```\n끝";
        string cut = RagPromptBuilder.Truncate(body, 30);
        Assert.Equal(0, cut.Split('\n').Count(l => l.TrimStart().StartsWith("```")) % 2);
    }

    [Fact]
    public void 관련_문서가_없어도_질문은_포함된다()
    {
        var prompt = RagPromptBuilder.Build("qwxzv", Engine, maxDocs: 3);
        Assert.EndsWith("qwxzv", prompt.UserPrompt);
    }
}
