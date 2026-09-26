---
id: error-cross-thread
title: 크로스 스레드 오류 - 다른 스레드에서 UI 컨트롤 접근 (WinForms/WPF)
category: 흔한 예외
order: 2708
summary: Task.Run, Thread, 타이머 콜백 등 UI 스레드가 아닌 곳에서 컨트롤을 바꿀 때 나는 예외와, await 구조/Invoke/Dispatcher/IProgress로 해결하는 방법입니다.
keywords: 크로스 스레드, cross-thread, 크로스 스레드 작업이 잘못되었습니다, Cross-thread operation not valid, 컨트롤이 자신이 만들어진 스레드가 아닌 스레드에서 액세스되었습니다, 다른 스레드가 이 개체를 소유하고 있어 호출한 스레드가 해당 개체에 액세스할 수 없습니다, The calling thread cannot access this object because a different thread owns it, Invoke, BeginInvoke, Dispatcher.Invoke, InvokeRequired, UI 스레드
related: async-ui, async-file-progress, task-run, recipe-timer
---
## 메시지
- WinForms: 크로스 스레드 작업이 잘못되었습니다. 'label1' 컨트롤이 자신이 만들어진 스레드가 아닌 스레드에서 액세스되었습니다. / Cross-thread operation not valid.
- WPF: 다른 스레드가 이 개체를 소유하고 있어 호출한 스레드가 해당 개체에 액세스할 수 없습니다. / The calling thread cannot access this object because a different thread owns it.

## 원인
UI 컨트롤은 **자신을 만든 UI 스레드에서만** 만질 수 있습니다.
```csharp
await Task.Run(() =>
{
    var data = Load();
    listBox.Items.Add(data);    // ← 예외! 스레드 풀 스레드에서 UI 접근
});
```

## 해결 1 (가장 좋음): 결과를 돌려받아 await 뒤에서 갱신
```csharp
var data = await Task.Run(() => Load());   // 백그라운드에서 계산
listBox.Items.Add(data);                    // await 이후는 UI 스레드
```

## 해결 2: UI 스레드에 부탁하기
```csharp
// WinForms
this.Invoke(() => label1.Text = "완료");
if (label1.InvokeRequired) label1.BeginInvoke(() => label1.Text = "완료");

// WPF
Dispatcher.Invoke(() => textBlock.Text = "완료");
Application.Current.Dispatcher.BeginInvoke(() => textBlock.Text = "완료");
```

## 해결 3: 진행률은 IProgress<T>
```csharp
var progress = new Progress<int>(p => progressBar.Value = p);   // UI 스레드에서 생성
await Task.Run(() => Work(progress));                          // 어디서 Report 해도 UI 스레드에서 실행
```

## 주의할 점
- `ConfigureAwait(false)` 이후 코드도 UI 스레드가 아닙니다.
- `System.Threading.Timer`, `System.Timers.Timer`의 콜백도 UI 스레드가 아닙니다 → `DispatcherTimer`/`Forms.Timer` 사용.
