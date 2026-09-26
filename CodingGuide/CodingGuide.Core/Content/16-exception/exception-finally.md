---
id: exception-finally
title: finally - 예외가 나도 반드시 실행
category: 예외 처리
order: 1603
summary: 예외 발생 여부, return 여부와 상관없이 항상 실행되는 finally 블록과, 자원 정리에 쓰는 using과의 관계입니다.
keywords: finally, 항상 실행, 정리, 뒷정리, cleanup, 자원 해제, return 후에도, try finally, using
lesson: 예외처리_finally
related: exception-try-catch, recipe-using-dispose, thread-monitor
---
## 핵심
`finally`는 try가 정상 종료되든, 예외가 나든, **catch 안에서 return 하든** 항상 실행됩니다.

```csharp
object? obj = "abc";

string Error()
{
    try
    {
        double d = (double)obj;        // InvalidCastException
    }
    catch (Exception)
    {
        return "Exception 에러가 발생했습니다.";   // return 하기 "직전에" finally 실행
    }
    finally
    {
        obj = null;                    // 반드시 실행됨
    }
    return "";
}

string msg = Error();
Console.WriteLine($"errorMessage: {msg}, obj: {obj}");   // obj 는 null
```

## 대표 용도: 자원 정리
```csharp
var file = File.OpenWrite("a.txt");
try
{
    // 파일 쓰기
}
finally
{
    file.Dispose();   // 예외가 나도 파일이 닫힘
}
```
위 코드는 `using`으로 똑같이 줄일 수 있습니다(컴파일러가 try-finally로 바꿔 줌).
```csharp
using var file = File.OpenWrite("a.txt");
```

## 그 밖의 예
- 락 해제: `Monitor.Exit`, `semaphore.Release()`
- UI 상태 복구: 버튼 다시 활성화, 로딩 표시 끄기
```csharp
button.IsEnabled = false;
try { await LoadAsync(); }
finally { button.IsEnabled = true; }
```
