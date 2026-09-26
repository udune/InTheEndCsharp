---
id: recipe-using-dispose
title: using과 IDisposable - 파일, DB, 네트워크 자원 확실히 닫기
category: 실무 레시피
order: 2609
summary: 파일/DB 연결/스트림처럼 사용 후 반드시 닫아야 하는 자원을 using 문과 using 선언으로 자동 해제하는 방법과 IDisposable 직접 구현입니다.
keywords: using, IDisposable, Dispose, 자원 해제, 리소스 해제, 파일 닫기, 연결 닫기, using 선언, using var, await using, IAsyncDisposable, 파일이 다른 프로세스에서 사용 중, 메모리 누수
related: destructor, exception-finally, recipe-file-io
---
## 핵심
`IDisposable`을 구현한 객체는 다 쓰고 나서 `Dispose()`를 불러야 합니다. `using`이 자동으로 불러 줍니다(예외가 나도).

```csharp
// using 문: 블록이 끝나면 Dispose
using (var reader = new StreamReader("data.txt"))
{
    Console.WriteLine(reader.ReadToEnd());
}

// using 선언 (C# 8+): 현재 범위(메서드)가 끝날 때 Dispose
using var writer = new StreamWriter("out.txt");
writer.WriteLine("hello");

// 비동기 자원
await using var stream = File.OpenRead("big.bin");
```

## Dispose가 필요한 대표 타입
`FileStream`, `StreamReader/Writer`, `SqlConnection`, `HttpResponseMessage`, `CancellationTokenSource`, `Timer`, `Bitmap`, `Process`, `SemaphoreSlim`

## 직접 구현하기
```csharp
public class LogWriter : IDisposable
{
    private readonly StreamWriter _writer = new("app.log", append: true);
    public void Write(string msg) => _writer.WriteLine(msg);

    public void Dispose()
    {
        _writer.Dispose();          // 가지고 있는 자원도 해제
        GC.SuppressFinalize(this);
    }
}

using var log = new LogWriter();
log.Write("시작");
```

## 주의할 점
- Dispose하지 않으면 "파일이 다른 프로세스에서 사용 중이므로 액세스할 수 없습니다" 오류가 나거나 연결이 고갈됩니다.
- 예외: `HttpClient`는 매번 using으로 버리지 말고 **재사용**합니다.
- DI 컨테이너가 만든 객체는 컨테이너(스코프)가 Dispose해 주므로 직접 하지 않습니다.
