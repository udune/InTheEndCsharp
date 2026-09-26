---
id: recipe-stopwatch
title: 실행 시간 측정 (Stopwatch)과 성능 비교
category: 실무 레시피
order: 2610
summary: Stopwatch로 코드 실행 시간을 밀리초 단위로 재는 방법과, 정확한 성능 비교를 위한 주의점(워밍업, 반복, BenchmarkDotNet)입니다.
keywords: 실행 시간, 시간 측정, 성능 측정, 속도 측정, 걸린 시간, 경과 시간, Stopwatch, ElapsedMilliseconds, Elapsed, 벤치마크, benchmark, BenchmarkDotNet, 프로파일링, 느린 코드 찾기
related: aop-proxy, recipe-datetime, delegate-as-parameter
---
## 핵심
```csharp
using System.Diagnostics;

var sw = Stopwatch.StartNew();
DoWork();
sw.Stop();

Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
Console.WriteLine(sw.Elapsed);                  // 00:00:01.2345678
Console.WriteLine(sw.Elapsed.TotalSeconds);

sw.Restart();                                   // 0 부터 다시
```

## 재사용 가능한 측정 도우미
```csharp
static T Measure<T>(string name, Func<T> func)
{
    var sw = Stopwatch.StartNew();
    T result = func();
    Console.WriteLine($"[{name}] {sw.ElapsedMilliseconds} ms");
    return result;
}

var list = Measure("로드", () => LoadData());
```
여러 메서드에 한꺼번에 적용하려면 AOP 인터셉터(TimingInterceptor)를 참고하세요.

## 두 방법 비교할 때 주의
- 처음 한 번은 JIT 컴파일 때문에 느립니다. **한 번 실행(워밍업) 후** 측정하세요.
- 한 번만 재지 말고 **여러 번 반복한 평균**을 보세요.
- **Release 빌드**로 측정하세요(`dotnet run -c Release`). Debug는 최적화가 꺼져 있습니다.
- 정밀한 비교는 BenchmarkDotNet 라이브러리를 씁니다.

## 경과 시간만 간단히 (할당 없이)
```csharp
long start = Stopwatch.GetTimestamp();
DoWork();
TimeSpan elapsed = Stopwatch.GetElapsedTime(start);   // .NET 7+
```
