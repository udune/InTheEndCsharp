---
id: task-run
title: Task.Run - 무거운 계산을 백그라운드로
category: 비동기
order: 1805
summary: CPU를 많이 쓰는 계산을 Task.Run으로 스레드 풀에 보내 UI나 호출 스레드를 막지 않게 하는 방법과, 쓰지 말아야 할 경우입니다.
keywords: Task.Run, 백그라운드 작업, 백그라운드 스레드, 무거운 계산, CPU 작업, 스레드 풀, 오래 걸리는 작업, 병렬, Parallel.For, Parallel.ForEach, LongRunning
source: AsyncUI/Form1.cs
related: async-ui, threadpool, task-whenall, async-await
---
## 핵심
```csharp
// UI 버튼 클릭
private async void btnCalc_Click(object sender, EventArgs e)
{
    double result = await Task.Run(() =>
    {
        double sum = 0;
        for (long i = 0; i < 2_000_000_000; i++)
            sum += Math.Sqrt(i);          // 수십 초 걸리는 계산
        return sum;
    });
    lbLog.Items.Add(result);              // 다시 UI 스레드
}
```

## I/O vs CPU 구분
| 작업 종류 | 예 | 방법 |
|---|---|---|
| I/O 대기 | 파일, HTTP, DB | 비동기 API를 그대로 `await` (`ReadAllTextAsync`, `GetStringAsync`) |
| CPU 계산 | 이미지 처리, 대량 계산, 압축 | `await Task.Run(...)` |

```csharp
// 불필요한 Task.Run (I/O 는 이미 비동기)
var text = await Task.Run(() => File.ReadAllTextAsync("a.txt"));   // X
var text2 = await File.ReadAllTextAsync("a.txt");                  // O
```

## 여러 코어로 병렬 계산
```csharp
Parallel.For(0, 100, i => Process(i));
Parallel.ForEach(files, file => Compress(file));

await Parallel.ForEachAsync(urls, new ParallelOptions { MaxDegreeOfParallelism = 4 },
    async (url, ct) => await DownloadAsync(url, ct));
```

## 주의할 점
- ASP.NET Core 서버 코드에서는 `Task.Run`으로 감싸도 이득이 거의 없습니다(이미 스레드 풀에서 실행 중).
- Task.Run 안에서는 UI 컨트롤을 직접 만지지 마세요.
