---
id: exception-object
title: 예외 타입별로 catch 하기 (여러 catch, when 필터)
category: 예외 처리
order: 1602
summary: InvalidCastException, IndexOutOfRangeException 등 예외 종류별로 catch를 나누는 방법, 순서 규칙, when 필터와 자주 보는 예외 목록입니다.
keywords: 예외 종류, 예외 타입, 여러 catch, catch 순서, when, 예외 필터, InvalidCastException, IndexOutOfRangeException, FormatException, ArgumentException, 예외 목록, Exception 클래스
lesson: 예외처리_예외객체
related: exception-try-catch, custom-exception, error-null-reference, error-invalid-cast
---
## 핵심
```csharp
int[] ints = [1, 2, 3];
object obj = "abc";

try
{
    double d = (double)obj;   // 여기서 InvalidCastException → 아래 줄은 실행 안 됨
    int i = ints[5];
}
catch (InvalidCastException)
{
    Console.WriteLine("형변환 실패");
}
catch (IndexOutOfRangeException)
{
    Console.WriteLine("인덱스 범위 초과");
}
catch (Exception e)           // 그 밖의 모든 예외 (마지막에!)
{
    Console.WriteLine(e.Message);
}
```

## 규칙
- catch는 **위에서부터** 검사하므로 **구체적인 예외를 먼저**, `Exception`은 마지막에 둡니다. 순서가 반대면 컴파일 오류(CS0160)입니다.
- 변수가 필요 없으면 `catch (InvalidCastException)`처럼 이름을 생략할 수 있습니다.

## when 필터
```csharp
catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
{
    Console.WriteLine("없는 주소");
}
```

## 자주 만나는 예외
| 예외 | 원인 |
|---|---|
| `NullReferenceException` | null 인 변수의 멤버 사용 |
| `IndexOutOfRangeException` | 배열 인덱스 범위 초과 |
| `ArgumentOutOfRangeException` | List 인덱스 범위 초과, 잘못된 범위 인자 |
| `InvalidCastException` | 잘못된 형변환 |
| `FormatException` | `int.Parse("abc")` 처럼 형식 오류 |
| `KeyNotFoundException` | Dictionary에 없는 키 조회 |
| `InvalidOperationException` | 순회 중 컬렉션 수정, 빈 시퀀스의 First() 등 |
| `DivideByZeroException` | 정수를 0으로 나눔 |
| `FileNotFoundException` | 없는 파일 열기 |
| `ArgumentNullException` | null 이면 안 되는 인자에 null |
