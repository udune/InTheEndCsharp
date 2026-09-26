---
id: hashset
title: HashSet<T> - 중복 없는 집합, 빠른 포함 검사
category: 열거자와 컬렉션
order: 1413
summary: 중복을 허용하지 않고 포함 여부를 매우 빠르게 확인하는 HashSet의 사용법과 합집합/교집합, 그리고 어떤 컬렉션을 골라야 하는지 정리합니다.
keywords: HashSet, 해시셋, 집합, set, 중복 없음, 중복 제거, 빠른 검색, 포함 여부, Contains 빠르게, UnionWith, IntersectWith, ExceptWith, 방문 체크, 컬렉션 선택, 어떤 컬렉션
related: linq-set-operations, list-search, dictionary, list-create
---
## 핵심
```csharp
var set = new HashSet<string>();
set.Add("apple");      // true
set.Add("banana");     // true
set.Add("apple");      // false ← 이미 있음 (예외 없이 무시)

Console.WriteLine(set.Count);             // 2
Console.WriteLine(set.Contains("apple")); // True (매우 빠름)
set.Remove("banana");

var unique = new HashSet<int>([1, 2, 2, 3]);   // 리스트 중복 제거 → {1, 2, 3}
var ignoreCase = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
```

## 집합 연산 (원본을 바꿈)
```csharp
var a = new HashSet<int> { 1, 2, 3 };
var b = new HashSet<int> { 2, 3, 4 };
a.UnionWith(b);        // a = {1,2,3,4}
a.IntersectWith(b);    // 공통만 남김
a.ExceptWith(b);       // b 에 있는 것 제거
bool sub = a.IsSubsetOf(b);
```

## List.Contains vs HashSet.Contains
- `List.Contains`: 처음부터 하나씩 비교 → 요소가 많을수록 느림 (O(n))
- `HashSet.Contains`: 해시로 바로 찾음 → 개수와 상관없이 빠름 (O(1))
```csharp
var seen = new HashSet<int>();
foreach (var id in ids)
    if (!seen.Add(id)) Console.WriteLine($"중복: {id}");   // Add 가 false 면 이미 있던 것
```

## 어떤 컬렉션을 쓸까?
| 상황 | 컬렉션 |
|---|---|
| 순서대로 모아두고 인덱스로 접근 | `List<T>` |
| 크기가 고정 | 배열 `T[]` |
| 키로 값을 찾기 | `Dictionary<TKey, TValue>` |
| 중복 없이, 포함 여부를 자주 확인 | `HashSet<T>` |
| 먼저 넣은 것부터 처리 | `Queue<T>` |
| 마지막에 넣은 것부터 처리 | `Stack<T>` |
| 항상 정렬된 상태 유지 | `SortedDictionary`, `SortedSet` |
| 우선순위가 높은 것부터 | `PriorityQueue<TElement, TPriority>` |
| 여러 스레드가 동시에 사용 | `ConcurrentDictionary`, `ConcurrentQueue` |
| 외부에 읽기 전용으로 공개 | `IReadOnlyList<T>`, `IReadOnlyDictionary` |
