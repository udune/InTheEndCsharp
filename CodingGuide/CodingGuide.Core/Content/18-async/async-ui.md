---
id: async-ui
title: UI 프로그램의 비동기 - 화면이 멈추지 않게 (WinForms/WPF)
category: 비동기
order: 1803
summary: 버튼 클릭에서 오래 걸리는 작업을 동기로 하면 화면이 멈추는 이유와, async 이벤트 핸들러로 UI를 멈추지 않게 하는 방법을 AsyncUI 예제로 설명합니다.
keywords: UI 멈춤, 화면 멈춤, 응답 없음, 프리징, freezing, not responding, WinForms 비동기, WPF 비동기, async void, 이벤트 핸들러, UI 스레드, Invoke, Dispatcher, 크로스 스레드, cross-thread
source: AsyncUI/Form1.cs
related: async-await, async-flow-console, async-deadlock, task-run, error-cross-thread
---
## 동기 버전: 화면이 멈춘다
```csharp
private void btnSync_Click(object sender, EventArgs e)
{
    TaskSync1();   // Thread.Sleep(3000) → 3초 동안 UI 스레드가 잠듦
    TaskSync2();   // Thread.Sleep(1500)
}
```
버튼 이벤트는 **UI 스레드**에서 실행됩니다. UI 스레드가 잠들면 화면 그리기, 클릭 처리가 모두 멈춰 "응답 없음"이 됩니다. 로그도 작업이 모두 끝난 뒤에 한꺼번에 나타납니다.

## 비동기 버전: 화면이 살아 있다
```csharp
private async void btnAsync_Click(object sender, EventArgs e)   // 이벤트 핸들러는 async void 허용
{
    Task<string> task3 = TaskAsync3();   // 3초 걸림
    Task<string> task4 = TaskAsync4();   // 1.5초 걸림

    // 끝나는 순서대로 처리 (WhenAny)
    List<Task<string>> tasks = [task3, task4];
    while (tasks.Count > 0)
    {
        var done = await Task.WhenAny(tasks);
        lbLog.Items.Add(await done);      // await 이후엔 다시 UI 스레드 → 컨트롤 접근 OK
        tasks.Remove(done);
    }
}

async Task<string> TaskAsync3()
{
    await Task.Delay(3000);
    return "TaskAsync3 Finished";
}
```

## 무거운 "계산"은 Task.Run으로
`await`는 I/O 대기를 해결해 주지만, CPU 계산 자체는 여전히 스레드를 씁니다. 계산은 백그라운드로 보냅니다.
```csharp
double result = await Task.Run(GetBigCalculateData);   // 계산은 스레드 풀에서
lbLog.Items.Add(result);                                // 결과 표시는 UI 스레드에서
```

## 다른 스레드에서 UI 만지기
Task.Run 안이나 다른 스레드에서 컨트롤을 직접 바꾸면 **크로스 스레드 예외**가 납니다.
```csharp
// WinForms
this.Invoke(() => lbLog.Items.Add("완료"));
// WPF
Dispatcher.Invoke(() => listBox.Items.Add("완료"));
```
가능하면 결과를 `return` 해서 `await` 이후에 UI를 갱신하는 구조가 가장 깔끔합니다.

## 그 밖의 예제 (Form1.cs)
- `TaskCreationOptions.LongRunning`: 아주 오래 도는 작업을 스레드 풀 대신 전용 스레드에서 실행
- `TaskCreationOptions.AttachedToParent`: 부모 Task가 자식 Task 완료까지 기다리게 함
