---
id: task-whenall
title: 여러 작업 동시에 기다리기 - Task.WhenAll, Task.WhenAny
category: 비동기
order: 1806
summary: 여러 비동기 작업을 동시에 시작해 모두 끝날 때까지(WhenAll) 또는 가장 먼저 끝난 것(WhenAny)을 기다리는 방법과 타임아웃 처리입니다.
keywords: WhenAll, WhenAny, 동시 실행, 병렬 요청, 여러 작업 기다리기, 모두 완료, 먼저 끝난 것, 타임아웃, timeout, WaitAsync, 동시에 여러 API 호출
source: AsyncUI/Form1.cs
related: async-await, thread-semaphore, cancellation-token, async-ui
---
## WhenAll - 모두 끝날 때까지
```csharp
// 순서대로 기다리면: 3초 + 1.5초 = 4.5초
await TaskAsync1();
await TaskAsync2();

// 동시에 시작하고 한꺼번에 기다리면: 약 3초
Task t1 = TaskAsync1();
Task t2 = TaskAsync2();
await Task.WhenAll(t1, t2);

// 결과 받기
Task<string>[] tasks = urls.Select(u => http.GetStringAsync(u)).ToArray();
string[] pages = await Task.WhenAll(tasks);     // 입력 순서대로 결과 배열
```

## WhenAny - 먼저 끝난 순서대로 처리
```csharp
List<Task<string>> tasks = [TaskAsync3(), TaskAsync4()];
while (tasks.Count > 0)
{
    Task<string> done = await Task.WhenAny(tasks);
    Console.WriteLine(await done);    // 끝난 것부터 처리
    tasks.Remove(done);
}
```

## 타임아웃 걸기
```csharp
try
{
    string data = await http.GetStringAsync(url).WaitAsync(TimeSpan.FromSeconds(5));   // .NET 6+
}
catch (TimeoutException)
{
    Console.WriteLine("5초 초과");
}
```

## 주의할 점
- WhenAll에서 여러 작업이 실패해도 `await`는 **첫 번째 예외만** 던집니다. 모두 보려면 `whenAllTask.Exception.InnerExceptions`를 확인하세요.
- 수백 개를 한꺼번에 시작하면 서버나 네트워크에 부담이 됩니다. SemaphoreSlim이나 `Parallel.ForEachAsync`로 동시 개수를 제한하세요.
