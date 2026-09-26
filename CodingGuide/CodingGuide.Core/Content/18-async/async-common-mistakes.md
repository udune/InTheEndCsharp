---
id: async-common-mistakes
title: 비동기 흔한 실수 모음 (async void, await 빠뜨림, 순차 await)
category: 비동기
order: 1810
summary: await를 빠뜨려 경고가 나거나, async void로 예외를 놓치거나, 병렬로 할 수 있는 작업을 순서대로 기다리는 등 자주 하는 비동기 실수와 고치는 법입니다.
keywords: 비동기 실수, async void, await 누락, CS4014, CS1998, 경고, 이 호출이 대기되지 않으므로 호출이 완료되기 전에 현재 메서드가 계속 실행됩니다, Because this call is not awaited, 호출 결과에 await 연산자를 적용해 보세요, 이 비동기 메서드에는 await 연산자가 없으며, 예외가 사라짐, fire and forget, 순차 실행, 느린 비동기, Task 반환
related: async-await, async-deadlock, task-whenall, cancellation-token
---
## 1. await를 빠뜨림 (경고 CS4014)
> 이 호출이 대기되지 않으므로 호출이 완료되기 전에 현재 메서드가 계속 실행됩니다. 호출 결과에 'await' 연산자를 적용해 보세요.

```csharp
SaveAsync();              // 기다리지 않음 → 저장 전에 다음 코드 실행, 예외도 사라짐
await SaveAsync();        // O
```

## 2. async void
```csharp
async void Load() { throw new Exception(); }   // 호출한 쪽에서 try-catch 로 잡을 수 없음, 프로그램이 죽을 수 있음
async Task Load() { ... }                       // O
```
`async void`는 **이벤트 핸들러에서만** 씁니다. 그 안에서는 try-catch로 직접 예외를 처리하세요.

## 3. await가 없는 async 메서드 (경고 CS1998)
```csharp
async Task<int> GetAsync() { return 1; }       // 비동기가 아닌데 async 를 붙임
Task<int> GetAsync() => Task.FromResult(1);    // O
```

## 4. 병렬로 할 수 있는데 하나씩 기다림
```csharp
var a = await GetUserAsync();      // 1초
var b = await GetOrdersAsync();    // 1초 → 총 2초

var ta = GetUserAsync();           // 동시에 시작
var tb = GetOrdersAsync();
await Task.WhenAll(ta, tb);        // 총 1초
var user = ta.Result; var orders = tb.Result;   // 이미 끝났으므로 .Result 안전
```

## 5. .Result / .Wait()로 동기 대기
UI/웹 앱에서 교착 상태의 원인입니다. 끝까지 `await`하세요.

## 6. 반복문 안의 await가 필요 이상으로 느림
```csharp
foreach (var id in ids) results.Add(await LoadAsync(id));        // 하나씩
var results2 = await Task.WhenAll(ids.Select(LoadAsync));         // 동시에
```

## 7. Thread.Sleep 사용
비동기 메서드 안에서는 `await Task.Delay(ms)`를 씁니다. `Thread.Sleep`은 스레드를 붙잡습니다.
