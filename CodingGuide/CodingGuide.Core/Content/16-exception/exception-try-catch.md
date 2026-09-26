---
id: exception-try-catch
title: 예외 처리 기초 (try-catch)
category: 예외 처리
order: 1601
summary: 실행 중 발생하는 오류(예외)로 프로그램이 죽지 않도록 try-catch로 잡아 처리하는 기본 방법입니다.
keywords: 예외, exception, 예외 처리, try, catch, 오류 처리, 에러 처리, 프로그램이 죽음, 강제 종료, 크래시, crash, 에러 잡기
lesson: 예외처리_정의및trycatch
related: exception-object, exception-finally, custom-exception, error-index-out-of-range
---
## 핵심
예외가 발생하면 그 아래 코드는 실행되지 않고 **catch 블록으로 점프**합니다. 잡지 않으면 프로그램이 종료됩니다.

```csharp
int[] ints = [1, 2, 3];

try
{
    int i = ints[5];                        // IndexOutOfRangeException 발생
    Console.WriteLine("여기는 실행 안 됨");
}
catch
{
    Console.WriteLine("예외가 발생되었습니다.");
}

Console.WriteLine("Hello World!");          // 프로그램은 계속 진행
```

## 예외 정보 받기
```csharp
try
{
    int n = int.Parse("abc");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);          // 오류 메시지
    Console.WriteLine(ex.GetType().Name);   // FormatException
    Console.WriteLine(ex.StackTrace);       // 어디서 났는지
}
```

## 좋은 예외 처리 습관
- **예측 가능한 상황은 예외 대신 조건문으로** 처리합니다. (`int.TryParse`, `dict.TryGetValue`, `File.Exists`)
- **처리할 수 있는 곳에서만** catch 합니다. 아무것도 안 하는 빈 catch는 버그를 숨깁니다.
- 로그를 남기고 다시 던지려면 `throw;`를 씁니다(`throw ex;`는 스택 정보가 사라짐).
```csharp
catch (Exception ex)
{
    logger.Log(ex.ToString());
    throw;
}
```
