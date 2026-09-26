---
id: recipe-timer
title: 일정 간격으로 반복 실행 - PeriodicTimer, DispatcherTimer, Timer
category: 실무 레시피
order: 2615
summary: 몇 초마다 작업을 반복하는 방법. 비동기 루프용 PeriodicTimer, WPF/WinForms 화면 갱신용 타이머, 백그라운드 System.Threading.Timer의 차이입니다.
keywords: 타이머, timer, 주기적 실행, 반복 실행, 몇 초마다, 1초마다, 일정 간격, 스케줄, PeriodicTimer, DispatcherTimer, System.Threading.Timer, System.Timers.Timer, Windows Forms Timer, 시계, 폴링, polling
related: cancellation-token, async-ui, task-run
---
## 비동기 루프: PeriodicTimer (.NET 6+, 권장)
```csharp
using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
while (await timer.WaitForNextTickAsync(cancellationToken))
{
    Console.WriteLine(DateTime.Now);   // 1초마다
}
```
- 작업이 겹치지 않습니다(이전 작업이 끝나야 다음 대기).
- CancellationToken으로 깔끔하게 멈출 수 있습니다.

## WPF 화면 갱신: DispatcherTimer
```csharp
var timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
timer.Tick += (s, e) => clockText.Text = DateTime.Now.ToString("HH:mm:ss");   // UI 스레드에서 실행
timer.Start();
```
WinForms는 `System.Windows.Forms.Timer`(Tick 이벤트, UI 스레드)를 씁니다.

## 백그라운드: System.Threading.Timer
```csharp
var timer = new Timer(_ => Console.WriteLine("tick"), null, dueTime: 0, period: 5000);
// ... 사용이 끝나면
timer.Dispose();
```
- 스레드 풀에서 실행되므로 UI 컨트롤을 직접 만지면 안 됩니다.
- 변수가 사라지면 GC가 타이머를 수거해 멈출 수 있으니 **필드로 보관**하세요.

## 선택 기준
| 상황 | 타이머 |
|---|---|
| async 코드, 서비스, 콘솔 | `PeriodicTimer` |
| WPF 화면 갱신 | `DispatcherTimer` |
| WinForms 화면 갱신 | `System.Windows.Forms.Timer` |
| 가벼운 백그라운드 콜백 | `System.Threading.Timer` |
