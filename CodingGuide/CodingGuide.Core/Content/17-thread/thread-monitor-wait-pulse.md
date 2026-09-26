---
id: thread-monitor-wait-pulse
title: Monitor.Wait와 Pulse - 생산자/소비자 신호 보내기
category: 스레드
order: 1707
summary: 한 스레드가 데이터가 준비될 때까지 기다리고(Wait), 다른 스레드가 준비 완료를 알리는(Pulse) 생산자-소비자 패턴입니다.
keywords: Monitor.Wait, Monitor.Pulse, PulseAll, 생산자 소비자, producer consumer, 신호, 대기, 알림, 스레드 간 통신, BlockingCollection, Channel
lesson: 스레드_임계영역_Monitor_2
related: thread-monitor, thread-lock, concurrent-collections
---
## 핵심
```csharp
static readonly object _lock = new();
static bool _isReady = false;

static void Producer()
{
    lock (_lock)
    {
        Console.WriteLine("생산자: 준비 중...");
        Thread.Sleep(2000);
        _isReady = true;
        Monitor.Pulse(_lock);          // 기다리는 스레드 하나를 깨움
    }
}

static void Consumer()
{
    lock (_lock)
    {
        while (!_isReady)              // if 가 아니라 while (깨어난 뒤 다시 확인)
        {
            Console.WriteLine("소비자: 기다리는 중...");
            Monitor.Wait(_lock);       // 잠금을 "놓고" 신호가 올 때까지 대기
        }
        Console.WriteLine("소비자: 데이터를 받았습니다.");
    }
}
```

## 동작 순서
1. 소비자가 lock을 잡고 `_isReady`가 false라 `Wait` → **잠금을 반납하고** 잠듦
2. 생산자가 lock을 잡고 준비 후 `Pulse` → 소비자를 깨움
3. 생산자가 lock을 빠져나가면 소비자가 다시 잠금을 얻고 진행

## 규칙
- `Wait`/`Pulse`는 반드시 **같은 객체의 lock 안에서** 호출해야 합니다(아니면 SynchronizationLockException).
- 조건은 `while`로 검사합니다(가짜 깨어남, 여러 소비자 대비).

## 실무에서는
직접 구현하기보다 이미 만들어진 도구를 씁니다.
```csharp
using System.Threading.Channels;
var channel = Channel.CreateUnbounded<int>();
await channel.Writer.WriteAsync(42);           // 생산자
await foreach (var item in channel.Reader.ReadAllAsync()) { }  // 소비자
```
