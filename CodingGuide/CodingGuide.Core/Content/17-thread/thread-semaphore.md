---
id: thread-semaphore
title: SemaphoreSlim - 동시에 N개까지만 허용
category: 스레드
order: 1709
summary: 입장권 개수만큼만 동시에 들어갈 수 있게 제한하는 세마포어(SemaphoreSlim)와, 비동기 코드에서 WaitAsync로 동시 요청 수를 제한하는 방법입니다.
keywords: 세마포어, Semaphore, SemaphoreSlim, 동시 실행 개수 제한, 동시성 제한, throttling, 병렬 제한, 최대 N개, WaitAsync, Release, 비동기 잠금, async lock
lesson: 스레드_semaphore
related: thread-lock, thread-mutex, async-await, task-whenall
---
## 핵심
lock은 1명만, 세마포어는 **정해진 N명까지** 동시에 들어갈 수 있습니다.

```csharp
// 처음 입장권 2장, 최대 6장
static SemaphoreSlim semaphore = new SemaphoreSlim(2, 6);

semaphore.Release(2);   // 입장권 2장 추가 → 동시에 4개까지

static void Access(object? id)
{
    Console.WriteLine($"스레드 {id} - 접근 시도");
    semaphore.Wait();              // 입장권 받기 (없으면 대기)
    try
    {
        Console.WriteLine($"스레드 {id} - 접근!");
        Thread.Sleep(2000);
    }
    finally
    {
        semaphore.Release();       // 입장권 반납 (반드시 finally)
    }
}
// 10개 스레드를 실행하면 4개씩 차례로 들어감
```

## 비동기에서 동시 요청 수 제한 (실무에서 가장 많이 쓰는 형태)
```csharp
var throttle = new SemaphoreSlim(3);          // 동시에 3개까지만

var tasks = urls.Select(async url =>
{
    await throttle.WaitAsync();
    try
    {
        return await http.GetStringAsync(url);
    }
    finally
    {
        throttle.Release();
    }
});
string[] pages = await Task.WhenAll(tasks);
```

## 비동기 잠금으로 쓰기
`lock` 안에서는 await를 못 쓰므로, 입장권 1장짜리 SemaphoreSlim을 비동기 잠금으로 씁니다.
```csharp
private readonly SemaphoreSlim _mutex = new(1, 1);
await _mutex.WaitAsync();
try { await SaveAsync(); }
finally { _mutex.Release(); }
```

## 주의할 점
- 최대값을 넘겨 `Release`하면 `SemaphoreFullException`이 납니다.
- `Semaphore`(Slim 아님)는 프로세스 간 공유용이고 더 무겁습니다. 대부분 `SemaphoreSlim`이면 됩니다.
