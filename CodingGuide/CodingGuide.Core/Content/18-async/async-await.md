---
id: async-await
title: async / await 기초 - 기다리는 동안 다른 일 하기
category: 비동기
order: 1801
summary: async 메서드와 await로 시간이 걸리는 작업(파일, 네트워크, 대기)을 스레드를 막지 않고 기다리는 비동기 프로그래밍의 기본입니다.
keywords: 비동기, async, await, Task, Task<T>, 비동기 메서드, 논블로킹, non-blocking, Task.Delay, 동시에 실행, asynchronous, 기다리기
lesson: 비동기_async_await
related: async-flow-console, task-whenall, async-ui, task-run, async-common-mistakes
---
## 핵심
- `async`: 이 메서드 안에서 `await`를 쓰겠다는 표시
- `await`: 작업이 끝날 때까지 **스레드를 붙잡지 않고** 기다렸다가, 끝나면 그 다음 줄부터 이어서 실행
- 반환 타입: 값이 없으면 `Task`, 있으면 `Task<T>`

```csharp
async Task TaskAsync()
{
    Console.WriteLine("Task 1 Started");
    await Task.Delay(3000);           // 3초 기다림 (스레드는 놀지 않고 다른 일 가능)
    Console.WriteLine("Task 1 Finished");
}

async Task TaskAsync2()
{
    Console.WriteLine("Task 2 Started");
    await Task.Delay(1500);
    Console.WriteLine("Task 2 Finished");
}

Task t1 = TaskAsync();       // 시작만 하고 바로 다음 줄로
Task t2 = TaskAsync2();      // 둘이 동시에 진행
await Task.WhenAll(t1, t2);  // 둘 다 끝날 때까지 기다림 → 총 약 3초 (4.5초 아님)
```
> 예제는 `실행()`이 async가 아니라서 `Task.WhenAll(...).Wait()`로 기다립니다. 콘솔에서는 괜찮지만 **UI 프로그램에서 `.Wait()`/`.Result`는 교착 상태**를 일으킬 수 있습니다.

## 값을 돌려주는 비동기 메서드
```csharp
async Task<string> LoadAsync(string path)
{
    string text = await File.ReadAllTextAsync(path);
    return text.ToUpper();          // Task<string> 이지만 return 은 string
}

string result = await LoadAsync("a.txt");
```

## 규칙
- 비동기 메서드 이름은 `~Async`로 끝내는 것이 관례입니다.
- `await`를 쓰려면 호출하는 메서드도 `async`여야 합니다. (위로 전파됨: "async all the way")
- 콘솔 `Main`도 `static async Task Main()` 으로 만들 수 있습니다(최상위 문에서는 그냥 await 사용 가능).
- `async void`는 **이벤트 핸들러에서만** 씁니다(예외를 잡을 수 없음).
