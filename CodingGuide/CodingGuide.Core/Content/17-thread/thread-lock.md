---
id: thread-lock
title: lock으로 임계 영역 보호하기 (Lock 타입)
category: 스레드
order: 1705
summary: lock 문으로 한 번에 한 스레드만 코드 블록(임계 영역)을 실행하게 해 경쟁 상태를 막는 방법과 C# 13의 Lock 타입입니다.
keywords: lock, 락, 잠금, 임계 영역, critical section, 동기화, synchronization, Lock 타입, System.Threading.Lock, 상호 배제, mutual exclusion, 교착 상태, deadlock
lesson: 스레드_임계영역_lock
related: thread-race-condition, thread-monitor, thread-mutex, thread-semaphore
---
## 핵심
`lock (잠금객체) { ... }` 블록은 **한 번에 한 스레드만** 들어갈 수 있습니다. 다른 스레드는 앞 스레드가 나올 때까지 기다립니다.

```csharp
static int data = 0;
static readonly Lock lockObject = new Lock();   // C# 13 / .NET 9+ 전용 잠금 타입

void DoWork()
{
    for (int i = 0; i < 10; i++)
    {
        lock (lockObject)       // 임계 영역 시작
        {
            Thread.Sleep(1);
            data++;
        }                       // 자동으로 잠금 해제 (예외가 나도)
    }
}
// 10개 스레드 실행 후 data 는 항상 100
```

## 잠금 객체 규칙
- 전용 객체를 `private static readonly`로 만들어 씁니다. (.NET 9 이전: `private static readonly object _lock = new();`)
- `lock (this)`, `lock (typeof(X))`, `lock ("문자열")`은 **외부에서도 같은 객체를 잠글 수 있어** 위험합니다.
- 값 타입(int 등)은 lock 할 수 없습니다.

## 주의할 점
- 잠금 범위는 **최대한 짧게**. 잠금 안에서 파일/네트워크 작업을 하면 모든 스레드가 줄을 섭니다.
- **교착 상태(Deadlock)**: 스레드 A가 락1을 잡고 락2를 기다리고, B가 락2를 잡고 락1을 기다리면 둘 다 영원히 멈춥니다. 여러 락은 **항상 같은 순서로** 잡으세요.
- `lock` 블록 안에서는 `await`를 쓸 수 없습니다(CS1996). 비동기 코드에는 `SemaphoreSlim.WaitAsync()`를 씁니다.
