namespace CodingGuide.Core.Knowledge;

/// <summary>
/// 지식베이스의 문서 한 건. 마크다운 파일 하나에 대응한다.
/// </summary>
public sealed record KnowledgeDoc
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Category { get; init; }
    public int Order { get; init; }
    public string Summary { get; init; } = "";
    public IReadOnlyList<string> Keywords { get; init; } = [];

    /// <summary>이 문서가 설명하는 학습 예제 클래스 이름들 (예: "린큐_메소드_Where"). 실행 버튼과 예제 코드 탭에 쓰인다.</summary>
    public IReadOnlyList<string> Lessons { get; init; } = [];

    /// <summary>예제 코드 탭에 함께 보여줄 추가 소스 경로들 (예: "AsyncUI/Form1.cs").</summary>
    public IReadOnlyList<string> Sources { get; init; } = [];

    public IReadOnlyList<string> Related { get; init; } = [];

    /// <summary>false면 이 프로그램 안에서 실행할 수 없는 예제(키보드 입력 필요 등). front matter의 "runnable: false".</summary>
    public bool Runnable { get; init; } = true;

    /// <summary>front matter를 뺀 마크다운 본문.</summary>
    public string Body { get; init; } = "";

    /// <summary>본문 안의 ``` 코드 블록만 모은 텍스트. 검색 가중치를 따로 주기 위해 분리한다.</summary>
    public string CodeText { get; init; } = "";

    /// <summary>본문에서 코드 블록을 뺀 텍스트.</summary>
    public string ProseText { get; init; } = "";
}
