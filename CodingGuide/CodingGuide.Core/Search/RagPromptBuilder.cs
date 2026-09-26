using System.Text;
using CodingGuide.Core.Knowledge;

namespace CodingGuide.Core.Search;

public sealed record RagPrompt(string SystemPrompt, string UserPrompt, IReadOnlyList<KnowledgeDoc> Sources);

/// <summary>
/// 로컬 LLM에 보낼 프롬프트를 만든다(RAG: 검색 증강 생성).
/// 질문으로 지식베이스를 검색해 상위 문서를 "참고 자료"로 붙이고,
/// 그 자료를 근거로만 답하도록 지시한다. 작은 모델이 지어내는 답을 줄이는 핵심 장치다.
///
/// CPU에서는 입력(프롬프트)이 길수록 첫 글자가 나오기까지 오래 걸리므로
/// 문서 수와 문서별 길이에 상한을 둔다.
/// </summary>
public static class RagPromptBuilder
{
    public const string SystemPrompt =
        """
        당신은 C# 프로그래밍을 가르치는 친절한 한국어 도우미입니다.
        - 반드시 한국어로 답합니다.
        - 아래 [참고 자료]를 우선 근거로 삼아 정확하게 답합니다. 자료에 없는 내용은 일반적인 C# 지식으로 보충하되, 확실하지 않으면 모른다고 말합니다.
        - 코드는 ```csharp 코드 블록으로 짧고 실행 가능한 예제를 보여줍니다.
        - 핵심부터 간결하게 설명하고, 필요하면 주의할 점을 덧붙입니다.
        """;

    /// <remarks>
    /// 기본값은 Ryzen 5 3600 + 7B(Q4_K_M) CPU 측정 기준이다. 프롬프트 읽기가 초당 약 34토큰이라
    /// 문서당 1400자(약 1600토큰)면 첫 글자까지 약 50초, 700자면 약 30초 걸린다.
    /// </remarks>
    public static RagPrompt Build(string question, SearchEngine engine, int maxDocs = 3, int maxCharsPerDoc = 700)
    {
        var sources = engine.Search(question, limit: maxDocs).Select(h => h.Doc).ToList();

        var user = new StringBuilder();
        if (sources.Count > 0)
        {
            user.AppendLine("[참고 자료]");
            for (int i = 0; i < sources.Count; i++)
            {
                var doc = sources[i];
                user.AppendLine($"### 자료 {i + 1}: {doc.Title}");
                user.AppendLine(doc.Summary);
                user.AppendLine(Truncate(doc.Body, maxCharsPerDoc));
                user.AppendLine();
            }
        }
        user.AppendLine("[질문]");
        user.Append(question.Trim());

        return new RagPrompt(SystemPrompt, user.ToString(), sources);
    }

    /// <summary>줄 단위로 잘라 길이를 맞추고, 잘린 코드 블록은 닫아 준다.</summary>
    internal static string Truncate(string body, int maxChars)
    {
        if (body.Length <= maxChars) return body;

        var sb = new StringBuilder();
        bool inCode = false;
        foreach (var line in body.Split('\n'))
        {
            if (sb.Length + line.Length + 1 > maxChars) break;
            sb.AppendLine(line);
            if (line.TrimStart().StartsWith("```")) inCode = !inCode;
        }
        if (inCode) sb.AppendLine("```");
        sb.AppendLine("…(이하 생략)");
        return sb.ToString();
    }
}
