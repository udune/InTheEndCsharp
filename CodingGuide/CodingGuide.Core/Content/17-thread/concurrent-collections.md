---
id: concurrent-collections
title: 스레드 안전 컬렉션 (ConcurrentDictionary, ConcurrentQueue, Interlocked)
category: 스레드
order: 1711
summary: 여러 스레드가 동시에 읽고 써도 안전한 System.Collections.Concurrent 컬렉션들과 Interlocked 원자 연산입니다.
keywords: 스레드 안전, thread safe, 동시성 컬렉션, ConcurrentDictionary, ConcurrentQueue, ConcurrentBag, BlockingCollection, Interlocked, 원자적, 동시에 추가, 멀티스레드 리스트
related: thread-race-condition, thread-lock, dictionary, queue
---
## 왜 필요한가요?
`List<T>`, `Dictionary<K,V>`는 여러 스레드가 동시에 수정하면 데이터가 깨지거나 예외가 납니다. 매번 lock을 거는 대신 스레드 안전 컬렉션을 쓸 수 있습니다.

## 종류
```csharp
using System.Collections.Concurrent;

var dict = new ConcurrentDictionary<string, int>();
dict.TryAdd("a", 1);
dict.AddOrUpdate("a", 1, (key, old) => old + 1);    // 없으면 1, 있으면 +1
int v = dict.GetOrAdd("b", key => ExpensiveCreate(key));

var queue = new ConcurrentQueue<int>();
queue.Enqueue(1);
if (queue.TryDequeue(out int item)) { }

var bag = new ConcurrentBag<int>();                  // 순서 상관없는 모음
Parallel.For(0, 1000, i => bag.Add(i));
```

## 숫자 하나만 안전하게: Interlocked
```csharp
int count = 0;
Parallel.For(0, 1000, _ => Interlocked.Increment(ref count));   // 항상 1000
Interlocked.Add(ref total, 5);
Interlocked.Exchange(ref flag, 1);
```

## 주의할 점
- 개별 연산은 안전해도, **"확인 후 추가"처럼 두 연산을 조합**하면 여전히 경쟁 상태가 될 수 있습니다. `GetOrAdd`, `AddOrUpdate`처럼 한 번에 하는 메서드를 쓰세요.
- 단일 스레드에서는 일반 컬렉션보다 느립니다. 정말 공유할 때만 쓰세요.
