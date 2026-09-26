---
id: queue
title: Queue<T> - 선입선출(FIFO)
category: 열거자와 컬렉션
order: 1410
summary: 먼저 넣은 것이 먼저 나오는 Queue의 Enqueue, Dequeue, Peek, TryDequeue 사용법과 활용 예입니다.
keywords: 큐, Queue, 선입선출, FIFO, Enqueue, Dequeue, Peek, TryDequeue, 대기열, 작업 대기열, 줄 서기, ConcurrentQueue, BFS
lesson: 컬렉션_Queue
related: stack, list-create, thread-race-condition
---
## 핵심
줄 서기와 같습니다. **먼저 들어온 것이 먼저 나갑니다.**

```csharp
var q = new Queue<string>();
q.Enqueue("apple");      // 넣기
q.Enqueue("orange");
q.Enqueue("grape");

Console.WriteLine(q.Dequeue());  // apple  (꺼내면서 제거)
Console.WriteLine(q.Dequeue());  // orange
Console.WriteLine(q.Peek());     // grape  (보기만, 제거 안 함)

if (q.TryDequeue(out string? item))  // 비어 있어도 예외 없음
    Console.WriteLine(item);         // grape
Console.WriteLine(q.Count);          // 0
```

## 언제 쓰나요?
- 작업 대기열(들어온 순서대로 처리), 메시지 처리
- 너비 우선 탐색(BFS)

## 주의할 점
- 비어 있을 때 `Dequeue()`/`Peek()`는 `InvalidOperationException`을 던집니다. `TryDequeue`/`TryPeek` 또는 `Count` 확인을 쓰세요.
- 여러 스레드가 함께 쓰면 `System.Collections.Concurrent.ConcurrentQueue<T>`를 씁니다.
