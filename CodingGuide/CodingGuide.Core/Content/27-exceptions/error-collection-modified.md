---
id: error-collection-modified
title: 컬렉션이 수정되었습니다. 열거 작업이 실행되지 않을 수도 있습니다
category: 흔한 예외
order: 2705
summary: foreach로 List나 Dictionary를 순회하는 도중에 요소를 추가/삭제하면 발생하는 InvalidOperationException의 원인과 4가지 해결법입니다.
keywords: 컬렉션이 수정되었습니다, 열거 작업이 실행되지 않을 수도 있습니다, Collection was modified, enumeration operation may not execute, foreach 중 삭제, foreach 중 추가, 순회 중 수정, 반복 중 삭제, InvalidOperationException, RemoveAll
related: list-remove, loops, concurrent-collections, error-invalid-operation
---
## 메시지
- 컬렉션이 수정되었습니다. 열거 작업이 실행되지 않을 수도 있습니다.
- Collection was modified; enumeration operation may not execute.

## 원인
```csharp
foreach (var item in list)
{
    if (item.IsExpired)
        list.Remove(item);      // ← 예외! 순회 중인 컬렉션을 수정
}

foreach (var key in dict.Keys)
    dict[key] = 0;              // ← 예외! (Dictionary 값 수정도 .NET 버전에 따라 문제)
```

## 해결
```csharp
// 1) RemoveAll (가장 간단)
list.RemoveAll(item => item.IsExpired);

// 2) 뒤에서부터 for
for (int i = list.Count - 1; i >= 0; i--)
    if (list[i].IsExpired) list.RemoveAt(i);

// 3) 복사본을 순회
foreach (var item in list.ToList())
    if (item.IsExpired) list.Remove(item);

// 4) 새 컬렉션을 만든다 (함수형)
list = list.Where(item => !item.IsExpired).ToList();

// Dictionary 삭제
foreach (var key in dict.Where(kv => kv.Value == 0).Select(kv => kv.Key).ToList())
    dict.Remove(key);
```

## 멀티스레드에서 나는 경우
한 스레드가 foreach 하는 동안 **다른 스레드가** Add 하면 같은 예외가 납니다. lock으로 보호하거나 `ConcurrentDictionary` 같은 동시성 컬렉션을 쓰세요.
