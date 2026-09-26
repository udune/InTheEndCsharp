---
id: async-stream
title: 비동기 스트림 - IAsyncEnumerable과 await foreach
category: 비동기
order: 1808
summary: 데이터를 하나씩 비동기로 만들어 내보내는 IAsyncEnumerable<T>와 await foreach로 받아 처리하는 방법, 취소 토큰 연결입니다.
keywords: 비동기 스트림, IAsyncEnumerable, await foreach, yield return 비동기, async yield, EnumeratorCancellation, 스트리밍, 하나씩 받기
source: AsyncUI/Form1.cs
related: enumerator, cancellation-token, async-await
---
## 핵심
`async` + `yield return`을 함께 쓰면, 값이 준비될 때마다 **하나씩** 비동기로 내보낼 수 있습니다.

```csharp
async IAsyncEnumerable<int> GetIntsAsync(
    [EnumeratorCancellation] CancellationToken token = default)
{
    for (int i = 0; i < 10; i++)
    {
        token.ThrowIfCancellationRequested();
        await Task.Delay(1000, token);   // 1초마다
        yield return i;                  // 하나씩 내보냄
    }
}

await foreach (int i in GetIntsAsync(cts.Token))
{
    lbLog.Items.Add(i);                  // 도착하는 대로 화면에 추가
}
```

## List로 한꺼번에 받는 것과의 차이
- `Task<List<int>>`: 10초 뒤에 10개가 **한꺼번에** 도착
- `IAsyncEnumerable<int>`: 1초마다 **하나씩** 도착 → 사용자가 바로바로 볼 수 있고 메모리도 적게 씀

## 활용
- 페이지 단위 API를 끝까지 이어서 읽기
- 큰 파일을 줄 단위로 읽기: `File.ReadLinesAsync(path)` (.NET 7+)
- DB 결과를 한 행씩 처리

## 주의할 점
- 취소 토큰을 받는 매개변수에는 `[EnumeratorCancellation]`을 붙여야 `WithCancellation(token)`으로 전달한 토큰도 연결됩니다.
- `using System.Runtime.CompilerServices;`가 필요합니다.
