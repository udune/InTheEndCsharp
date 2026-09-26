---
id: async-deadlock
title: 비동기 교착 상태(.Result, .Wait())와 ConfigureAwait(false)
category: 비동기
order: 1804
summary: UI 스레드에서 .Result나 .Wait()로 비동기 작업을 기다리면 프로그램이 영원히 멈추는 이유와 해결책(await, ConfigureAwait(false))입니다.
keywords: 교착 상태, 데드락, deadlock, .Result, .Wait(), GetAwaiter().GetResult(), ConfigureAwait, ConfigureAwait(false), 프로그램 멈춤, 무한 대기, 동기로 비동기 호출
source: AsyncUI/Form1.cs
related: async-ui, async-await, async-flow-console, async-common-mistakes
---
## 문제 상황
```csharp
private void button1_Click(object sender, EventArgs e)
{
    string test = LoadAsync().Result;   // UI 가 영원히 멈춤!
}

async Task<string> LoadAsync()
{
    await Task.Delay(1000);   // 끝나면 "UI 스레드로 돌아가서" 다음 줄을 실행하려 함
    return "test";
}
```
1. UI 스레드가 `.Result`에서 LoadAsync가 끝나기를 **막고 기다림**
2. LoadAsync는 await 이후 코드를 **UI 스레드에서** 실행하려고 UI 스레드가 비기를 기다림
3. 서로가 서로를 기다림 → **교착 상태**

## 해결 1 (정답): 끝까지 await
```csharp
private async void button1_Click(object sender, EventArgs e)
{
    string test = await LoadAsync();
    MessageBox.Show(test);
}
```

## 해결 2: 라이브러리 코드에서 ConfigureAwait(false)
"await 이후에 원래 컨텍스트(UI 스레드)로 돌아올 필요 없다"는 뜻입니다.
```csharp
async Task<string> LoadAsync()
{
    await Task.Delay(1000).ConfigureAwait(false);   // 이후 코드는 스레드 풀에서 실행
    return "test";
}
```
- UI를 만지지 않는 **라이브러리/서비스 코드**에서 쓰면 교착 상태 예방과 약간의 성능 이득이 있습니다.
- ConfigureAwait(false) 이후에는 **UI 컨트롤을 만지면 안 됩니다**.
- ConfigureAwait(false)는 근본 해결이 아닙니다. 호출하는 쪽이 `.Result`를 쓰지 않는 것이 먼저입니다.

## 요약
- UI/ASP.NET 코드에서 `.Result`, `.Wait()`, `.GetAwaiter().GetResult()`를 쓰지 마세요.
- 콘솔 앱은 SynchronizationContext가 없어서 교착 상태가 나지 않습니다.
