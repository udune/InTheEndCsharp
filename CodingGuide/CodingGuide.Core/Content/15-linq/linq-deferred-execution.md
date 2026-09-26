---
id: linq-deferred-execution
title: LINQ 지연 실행과 ToList()를 언제 해야 하나
category: LINQ
order: 1554
summary: LINQ 쿼리는 만들 때가 아니라 꺼낼 때 실행된다는 지연 실행의 원리와, 여러 번 실행되는 문제, ToList/ToArray로 확정하는 시점입니다.
keywords: 지연 실행, deferred execution, lazy, 즉시 실행, ToList, ToArray, 여러 번 실행, 다중 열거, multiple enumeration, IEnumerable 경고, 결과가 바뀜
related: enumerable, linq-intro, linq-method-where
---
## 핵심
`Where`, `Select`, `OrderBy` 등은 **실행 계획만 만들고** 실제 계산은 foreach/ToList/Count 등으로 **꺼낼 때** 합니다.

```csharp
var list = new List<int> { 1, 2, 3 };
var query = list.Where(n => n > 1);   // 아직 아무것도 계산 안 함

list.Add(4);                           // 쿼리를 만든 "뒤"에 추가
Console.WriteLine(string.Join(",", query));   // 2,3,4 ← 4 도 포함됨!
```

## 문제 1: 여러 번 계산된다
```csharp
var expensive = users.Where(u => CheckSlowly(u));   // 느린 조건
if (expensive.Any())                                 // 1번 실행
    foreach (var u in expensive) { }                 // 또 실행
Console.WriteLine(expensive.Count());                // 또 실행
```
해결: 한 번 확정해서 재사용
```csharp
var result = users.Where(u => CheckSlowly(u)).ToList();
```

## 문제 2: 캡처한 변수가 바뀌면 결과도 바뀐다
```csharp
int min = 1;
var q = nums.Where(n => n > min);
min = 100;
q.ToList();   // min = 100 기준으로 계산됨
```

## 즉시 실행되는 메서드
`ToList`, `ToArray`, `ToDictionary`, `ToHashSet`, `Count`, `Sum`, `Max`, `First`, `Any`, `All`, `Contains` 등 **하나의 값이나 컬렉션을 돌려주는 것**은 바로 실행됩니다.

## 규칙
- 결과를 **두 번 이상** 쓰거나, 원본이 **바뀔 수 있으면** `.ToList()`로 확정합니다.
- 메서드에서 반환할 때도 대부분 `ToList()`해서 돌려주는 것이 안전합니다.
