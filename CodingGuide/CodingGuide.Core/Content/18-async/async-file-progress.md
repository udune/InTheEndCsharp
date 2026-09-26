---
id: async-file-progress
title: 큰 파일 비동기 쓰기/복사와 진행률 표시 (IProgress)
category: 비동기
order: 1809
summary: FileStream의 WriteAsync/ReadAsync로 큰 파일을 UI 멈춤 없이 만들고 복사하면서, 콜백이나 IProgress<T>로 진행률을 표시하는 방법입니다.
keywords: 파일 복사, 큰 파일, 대용량 파일, 진행률, 진행 상황, progress, 프로그레스바, ProgressBar, IProgress, Progress<T>, FileStream, WriteAsync, ReadAsync, CopyToAsync, 버퍼
source: AsyncUI/Form2.cs
related: async-ui, recipe-file-io, cancellation-token
---
## 비동기로 파일 복사하면서 진행률 알리기
```csharp
async Task CopyFileAsync(string src, string dst, Action<long, long> onProgress)
{
    const int bufferSize = 1024 * 1024;   // 1MB 씩
    using var source = new FileStream(src, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, useAsync: true);
    using var dest = new FileStream(dst, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, useAsync: true);

    long total = source.Length, copied = 0;
    byte[] buffer = new byte[bufferSize];
    int read;
    while ((read = await source.ReadAsync(buffer)) > 0)
    {
        await dest.WriteAsync(buffer.AsMemory(0, read));
        copied += read;
        onProgress(copied, total);
    }
}

// 버튼 클릭
await CopyFileAsync(txtSource.Text, txtDestination.Text, (cur, total) =>
{
    lblCopyProgress.Text = $"{cur / 1048576.0:F0}MB / {total / 1048576.0:F0}MB ({(double)cur / total:P})";
});
```
- `useAsync: true` : 운영체제의 비동기 I/O를 사용합니다.
- UI 이벤트에서 시작했으므로 await 이후 콜백이 UI 스레드에서 실행되어 라벨을 바로 바꿀 수 있습니다.

## 권장: IProgress<T>
어느 스레드에서 보고하든 **만든 곳(UI 스레드)에서** 콜백이 실행되도록 보장합니다.
```csharp
var progress = new Progress<double>(p => progressBar.Value = p * 100);
await Task.Run(() => Work(progress));

void Work(IProgress<double> progress)
{
    for (int i = 0; i <= 100; i++)
    {
        Thread.Sleep(50);
        progress.Report(i / 100.0);
    }
}
```

## 진행률이 필요 없으면
```csharp
await using var src = File.OpenRead(a);
await using var dst = File.Create(b);
await src.CopyToAsync(dst);
```

## 주의할 점
- 매 1MB마다 UI를 갱신하면 너무 잦을 수 있습니다. 퍼센트가 바뀔 때만 갱신하면 더 부드럽습니다.
