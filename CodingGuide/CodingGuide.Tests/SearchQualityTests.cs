using CodingGuide.Core.Knowledge;
using CodingGuide.Core.Search;

namespace CodingGuide.Tests;

/// <summary>
/// 사람이 실제로 입력할 법한 질문으로 검색 품질을 검사한다.
/// 기대 문서(여러 개면 | 로 구분) 중 하나가 상위 3개 안에 있어야 통과한다.
/// 문서나 동의어를 고친 뒤 품질이 떨어지지 않았는지 확인하는 회귀 테스트 역할을 한다.
/// </summary>
public class SearchQualityTests
{
    static readonly SearchEngine Engine = new(KnowledgeBase.LoadEmbedded());

    [Theory]
    // 하고 싶은 일(자연어)
    [InlineData("리스트에서 조건에 맞는 것만 뽑고 싶어", "linq-method-where|linq-query-where")]
    [InlineData("여러 스레드가 같은 변수를 바꾸면 값이 이상해", "thread-race-condition")]
    [InlineData("천 단위 콤마 찍기", "recipe-number-format")]
    [InlineData("리스트 중복 제거", "linq-set-operations|hashset")]
    [InlineData("버튼 누르면 화면이 멈춰요", "async-ui")]
    [InlineData("텍스트 파일 읽기", "recipe-file-io")]
    [InlineData("객체를 json 으로 저장", "recipe-json")]
    [InlineData("소수점 반올림", "recipe-rounding")]
    [InlineData("몇 초마다 실행하고 싶다", "recipe-timer")]
    [InlineData("문자열을 숫자로 바꾸기", "recipe-parse-number|convert-class")]
    [InlineData("진행 중인 작업 취소하기", "cancellation-token")]
    [InlineData("동시에 3개까지만 실행되게 제한", "thread-semaphore")]
    [InlineData("메서드를 변수에 담기", "delegate-basics")]
    [InlineData("값 타입 참조 타입 차이", "struct-vs-class|method-param-semantics")]
    [InlineData("private 필드 값 읽기", "reflection-field")]
    [InlineData("로그 파일 남기기", "recipe-logging")]
    [InlineData("리스트 내림차순 정렬", "list-sort|linq-method-orderby|linq-query-orderby")]
    [InlineData("날짜 포맷 yyyy-MM-dd", "recipe-datetime")]
    [InlineData("이메일 형식 검사", "recipe-regex")]
    [InlineData("가장 점수가 높은 학생 찾기", "linq-aggregate")]
    [InlineData("두 리스트 합치기", "list-insert|linq-set-operations")]
    [InlineData("여러 값을 반환하고 싶어", "tuples")]
    [InlineData("부모 클래스 메서드 재정의", "inheritance-override")]
    [InlineData("서버에 데이터 보내기 post", "api-post-headers")]
    [InlineData("단위 테스트 작성법", "xunit-basics|test-importance")]
    // 개념/키워드
    [InlineData("override new 차이", "override-vs-new")]
    [InlineData("싱글톤 스코프 트랜지언트 차이", "di-lifetime")]
    [InlineData("SelectMany", "linq-method-selectmany")]
    [InlineData("appsettings.json 읽기", "di-appsettings")]
    [InlineData("제네릭 제약 조건", "generic-constraint-struct-class|generic-constraint-new|generic-constraint-classtype|generic-constraint-interface")]
    [InlineData("상속", "inheritance")]
    [InlineData("lock", "thread-lock")]
    [InlineData("yield return", "enumerator|enumerable")]
    [InlineData("AOP 인터셉터", "aop-proxy")]
    // 오타
    [InlineData("Dictonary", "dictionary")]
    [InlineData("Semaphor", "thread-semaphore")]
    // 오류 메시지 붙여넣기
    [InlineData("CS0103", "error-cs0103")]
    [InlineData("개체 참조가 개체의 인스턴스로 설정되지 않았습니다", "error-null-reference")]
    [InlineData("컬렉션이 수정되었습니다. 열거 작업이 실행되지 않을 수도 있습니다", "error-collection-modified")]
    [InlineData("Sequence contains no elements", "error-invalid-operation|linq-first-single")]
    [InlineData("암시적으로 'double' 형식을 'int' 형식으로 변환할 수 없습니다", "error-cs0266")]
    [InlineData("다른 스레드가 이 개체를 소유하고 있어 호출한 스레드가 해당 개체에 액세스할 수 없습니다", "error-cross-thread")]
    [InlineData("지정한 키가 사전에 없습니다", "error-key-not-found")]
    [InlineData("입력 문자열의 형식이 잘못되었습니다", "error-format")]
    public void 기대한_문서가_상위_3개_안에_나온다(string query, string expectedIds)
    {
        var expected = expectedIds.Split('|');
        var top = Engine.Search(query, limit: 3).Select(h => h.Doc.Id).ToList();
        Assert.True(top.Any(expected.Contains),
            $"'{query}' → 기대: {expectedIds}, 실제 상위 3개: {string.Join(", ", top)}");
    }

    [Fact]
    public void 빈_검색어는_결과가_없다()
    {
        Assert.Empty(Engine.Search("   "));
    }

    [Fact]
    public void 전혀_관계없는_말은_결과가_적다()
    {
        Assert.True(Engine.Search("qwxzv").Count <= 3);
    }

    [Fact]
    public void 검색은_충분히_빠르다()
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 50; i++)
            Engine.Search("리스트에서 조건에 맞는 것만 뽑고 싶어");
        Assert.True(sw.ElapsedMilliseconds < 2000, $"50회 검색 {sw.ElapsedMilliseconds}ms");
    }
}
