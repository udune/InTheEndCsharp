---
id: cancellation-token
title: 작업 취소 - CancellationToken
category: 비동기
order: 1807
summary: CancellationTokenSource와 CancellationToken으로 진행 중인 비동기 작업을 중지 버튼 등으로 취소하는 방법입니다.
keywords: 취소, cancel, 작업 중지, 중지 버튼, 멈추기, CancellationToken, CancellationTokenSource, Cancel, ThrowIfCancellationRequested, OperationCanceledException, TaskCanceledException, 타임아웃 취소, CancelAfter
source: AsyncUI/Form1.cs
related: async-stream, task-whenall, thread-practice, async-ui
---
## 핵심
- **CancellationTokenSource**: 취소를 "요청하는" 쪽 (중지 버튼)
- **CancellationToken**: 작업에 전달되어 취소 요청을 "확인하는" 쪽

```csharp
private CancellationTokenSource? cts;

private async void btnStart_Click(object sender, EventArgs e)
{
    if (cts != null) return;                  // 이미 실행 중
    cts = new CancellationTokenSource();
    try
    {
        await DoWorkAsync(cts.Token);
    }
    catch (OperationCanceledException)        // TaskCanceledException 도 여기 포함
    {
        MessageBox.Show("작업이 취소되었습니다");
    }
    finally
    {
        cts.Dispose();
        cts = null;
    }
}

private void btnStop_Click(object sender, EventArgs e) => cts?.Cancel();

async Task DoWorkAsync(CancellationToken token)
{
    for (int i = 0; i < 10; i++)
    {
        token.ThrowIfCancellationRequested();   // 취소됐으면 예외로 빠져나감
        await Task.Delay(1000, token);          // 대기 중에도 즉시 취소됨
    }
}
```

## 규칙
- 취소는 **협조적**입니다. 작업 쪽에서 토큰을 확인하지 않으면 멈추지 않습니다.
- 비동기 API 대부분이 마지막 매개변수로 `CancellationToken`을 받습니다. 꼭 전달하세요. (`ReadAllTextAsync(path, token)`, `GetAsync(url, token)`)
- `TaskCanceledException`은 `OperationCanceledException`의 자식이므로 `OperationCanceledException` 하나로 잡으면 됩니다.

## 시간 제한으로 취소
```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));   // 10초 후 자동 취소
await DownloadAsync(url, cts.Token);
```
