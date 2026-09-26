---
id: thread-monitor
title: Monitor - TryEnter로 시간 제한 잠금
category: 스레드
order: 1706
summary: lock의 실제 구현인 Monitor의 Enter/Exit, 시간 제한을 두고 잠금을 시도하는 TryEnter와 finally로 반드시 해제하는 패턴입니다.
keywords: Monitor, Monitor.Enter, Monitor.Exit, TryEnter, 타임아웃, 시간 제한 잠금, 잠금 시도, lock 내부 구현
lesson: 스레드_임계영역_Monitor_1
related: thread-lock, thread-monitor-wait-pulse, exception-finally
---
## 핵심
`lock` 문은 컴파일러가 `Monitor.Enter` / `try` / `finally { Monitor.Exit }`로 바꿔 줍니다. Monitor를 직접 쓰면 **"일정 시간 안에 못 잡으면 포기"** 같은 제어가 가능합니다.

```csharp
static readonly object _lock = new();

static void DoWork()
{
    if (Monitor.TryEnter(_lock, TimeSpan.FromSeconds(5)))   // 최대 5초 기다림
    {
        try
        {
            Console.WriteLine($"{Environment.CurrentManagedThreadId}번 잠금 획득");
            Thread.Sleep(1000);
        }
        finally
        {
            Monitor.Exit(_lock);        // 반드시 finally 에서 해제
        }
    }
    else
    {
        Console.WriteLine("잠금을 얻지 못했습니다.");
    }
}
```

## 참고: Lock 타입을 쓸 때
예제는 `System.Threading.Lock` 객체를 `Monitor`에 넘깁니다. 이렇게 하면 Lock 타입의 전용 잠금이 아니라 일반 object 잠금으로 동작하고 컴파일러 경고(CS9216)가 납니다. Lock 타입은 자체 메서드를 씁니다.
```csharp
static readonly Lock _lk = new();
if (_lk.TryEnter(TimeSpan.FromSeconds(5)))
{
    try { /* 작업 */ }
    finally { _lk.Exit(); }
}
```

## 언제 쓰나요?
대부분은 `lock`으로 충분합니다. 잠금 대기 시간 제한이 필요하거나, 생산자-소비자 신호(Wait/Pulse)가 필요할 때 Monitor를 씁니다.
