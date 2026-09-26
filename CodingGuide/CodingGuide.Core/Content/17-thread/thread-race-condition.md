---
id: thread-race-condition
title: 경쟁 상태 (Race Condition) - 여러 스레드가 같은 값을 바꿀 때
category: 스레드
order: 1704
summary: 여러 스레드가 동시에 같은 변수를 수정하면 결과가 틀어지는 경쟁 상태의 원인(data++이 원자적이지 않음)과 해결 방법입니다.
keywords: 경쟁 상태, race condition, 동시성 문제, 값이 이상함, 결과가 매번 다름, 숫자가 모자람, 카운트 틀림, 스레드 안전, thread safe, 원자성, atomic, Interlocked, 공유 변수
lesson: 스레드_경쟁상태
related: thread-lock, thread-monitor, concurrent-collections, thread-intro
---
## 증상
10개 스레드가 각각 `data++`를 10번씩 하면 100이 나와야 하는데, **100보다 작은 값이 나오고 실행할 때마다 다릅니다.**

```csharp
int data = 0;

void DoWork()
{
    for (int i = 0; i < 10; i++)
    {
        Thread.Sleep(1);
        data++;          // 위험!
    }
}

var threads = new List<Thread>();
for (int i = 0; i < 10; i++)
{
    var t = new Thread(DoWork);
    threads.Add(t);
    t.Start();
}
threads.ForEach(t => t.Join());
Console.WriteLine(data);    // 100 이 아닐 수 있음 (예: 93)
```

## 원인
`data++`는 한 번에 끝나는 동작이 아니라 **읽기 → 1 더하기 → 쓰기** 3단계입니다.
1. 스레드 A가 data(=5)를 읽음
2. 스레드 B도 data(=5)를 읽음
3. A가 6을 씀, B도 6을 씀 → 한 번의 증가가 사라짐

## 해결 방법
```csharp
// 1) lock - 한 번에 한 스레드만
private static readonly Lock _lock = new();
lock (_lock) { data++; }

// 2) Interlocked - 단순 숫자 증감은 가장 빠르고 간단
Interlocked.Increment(ref data);
Interlocked.Add(ref total, amount);

// 3) 스레드 안전 컬렉션
var dict = new ConcurrentDictionary<string, int>();
dict.AddOrUpdate("key", 1, (_, old) => old + 1);
```

## 예방
- 가능하면 **공유 상태를 없애세요**. 각 스레드가 자기 결과를 만들고 마지막에 합치는 방식이 가장 안전합니다.
- `List<T>`, `Dictionary<K,V>`는 스레드 안전하지 않습니다. 동시에 Add 하면 데이터가 깨지거나 예외가 납니다.
