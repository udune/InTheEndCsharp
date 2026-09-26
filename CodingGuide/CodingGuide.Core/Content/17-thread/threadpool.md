---
id: threadpool
title: 스레드 풀 (ThreadPool)과 Task
category: 스레드
order: 1710
summary: 스레드를 매번 새로 만들지 않고 미리 만들어 둔 스레드를 재사용하는 스레드 풀과, 이를 편하게 쓰는 Task.Run입니다.
keywords: 스레드 풀, ThreadPool, QueueUserWorkItem, 스레드 재사용, Task.Run, Task, 백그라운드 작업, 작업 큐
lesson: 스레드풀
related: task-run, thread-create, async-await
---
## 핵심
스레드를 만들고 없애는 데는 비용이 큽니다. 스레드 풀은 스레드를 **미리 만들어 두고 작업만 넘겨서** 재사용합니다.

```csharp
for (int i = 1; i <= 5; i++)
{
    ThreadPool.QueueUserWorkItem(state => DoWork());   // 작업을 풀에 맡김
}

static void DoWork()
{
    Console.WriteLine($"[ThreadPool] {Environment.CurrentManagedThreadId} 작업 시작");
    Thread.Sleep(2000);
    Console.WriteLine($"[ThreadPool] {Environment.CurrentManagedThreadId} 작업 완료");
}
```
> 스레드 풀 스레드는 **백그라운드 스레드**입니다. 콘솔 프로그램에서 메인이 먼저 끝나면 작업이 끝나기 전에 프로그램이 종료될 수 있습니다. 이 가이드 프로그램에서 실행하면 프로그램이 계속 살아 있으므로 2초 뒤 "작업 완료"가 이어서 출력됩니다.

## 요즘은 Task.Run을 씁니다
Task.Run도 스레드 풀을 쓰지만, **완료를 기다리고(await), 결과를 받고, 예외를 전달**받을 수 있습니다.
```csharp
Task t = Task.Run(() => DoWork());
await t;                                   // 완료 대기

int result = await Task.Run(() => HeavyCalculation());   // 결과 받기

Task[] tasks = Enumerable.Range(1, 5).Select(_ => Task.Run(DoWork)).ToArray();
await Task.WhenAll(tasks);                 // 모두 끝날 때까지
```

## 주의할 점
- 스레드 풀 스레드를 `Thread.Sleep`이나 `.Wait()`로 오래 붙잡으면 풀이 고갈되어 전체가 느려집니다. 대기는 `await Task.Delay`로 하세요.
