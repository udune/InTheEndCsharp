---
id: async-flow-console
title: 비동기 실행 흐름 - UI가 없는 환경(콘솔)
category: 비동기
order: 1802
summary: 콘솔처럼 UI(SynchronizationContext)가 없는 환경에서 await 이후 코드가 스레드 풀 스레드에서 이어 실행되는 흐름을 설명합니다.
keywords: 비동기 흐름, 실행 순서, 콘솔 비동기, SynchronizationContext, await 이후 스레드, 스레드 풀, 컨텍스트, continuation
lesson: 비동기_진행흐름_UI가없는환경
related: async-await, async-ui, async-deadlock
---
## 실행 순서
```csharp
async Task TaskAsync()
{
    Console.WriteLine("Task 1 Started");     // ① 호출한 스레드에서 바로 실행
    await Task.Delay(3000);                  // ② 여기서 호출자에게 제어를 돌려줌
    Console.WriteLine("Task 1 Finished");    // ⑤ 3초 후, 스레드 풀 스레드에서 이어서 실행
}

Task t1 = TaskAsync();     // ① ② 까지 실행하고 돌아옴
Task t2 = TaskAsync2();    // ③ "Task 2 Started" 출력 후 돌아옴
Task.WhenAll(t1, t2).Wait(); // ④ 대기 → 1.5초 후 "Task 2 Finished", 3초 후 "Task 1 Finished"
```
출력:
```
Task Async Started
Task Async 2 Started
Task Async 2 Finished
Task Async Finished
```

## 어느 스레드에서 이어지나?
`await` 뒤의 코드가 어디서 실행될지는 **SynchronizationContext**가 결정합니다.

| 환경 | await 이후 실행 스레드 |
|---|---|
| 콘솔, ASP.NET Core | 스레드 풀의 아무 스레드 (원래 스레드가 아닐 수 있음) |
| WinForms, WPF | **원래의 UI 스레드**로 돌아옴 (그래서 화면 컨트롤을 만질 수 있음) |

스레드 ID를 찍어보면 확인할 수 있습니다.
```csharp
Console.WriteLine($"before: {Environment.CurrentManagedThreadId}");
await Task.Delay(100);
Console.WriteLine($"after : {Environment.CurrentManagedThreadId}");   // 콘솔에서는 다를 수 있음
```

## 콘솔에서 .Wait()가 괜찮은 이유
돌아갈 UI 스레드가 없으니 await 이후 코드가 다른 스레드에서 실행되어 막히지 않습니다. UI 앱에서는 교착 상태가 날 수 있습니다(→ 비동기 교착 상태).
