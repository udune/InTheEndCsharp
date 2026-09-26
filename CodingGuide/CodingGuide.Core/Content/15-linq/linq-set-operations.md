---
id: linq-set-operations
title: 중복 제거와 집합 연산 - Distinct, DistinctBy, Union, Intersect, Except
category: LINQ
order: 1553
summary: 중복 값 제거(Distinct), 속성 기준 중복 제거(DistinctBy), 합집합/교집합/차집합을 구하는 LINQ 메서드입니다.
keywords: 중복 제거, 중복제거, 중복 없애기, 고유값, unique, Distinct, DistinctBy, 합집합, Union, 교집합, Intersect, 차집합, Except, 두 리스트 비교, 공통 요소, 없는 요소, HashSet
related: hashset, linq-method-groupby, list-search
---
## 중복 제거
```csharp
int[] nums = [1, 2, 2, 3, 3, 3];
var unique = nums.Distinct().ToList();                 // [1, 2, 3]

var names = new[] { "Kim", "kim", "Lee" };
names.Distinct(StringComparer.OrdinalIgnoreCase);      // 대소문자 무시 → Kim, Lee

// 객체를 특정 속성 기준으로 중복 제거 (.NET 6+)
var uniqueByEmail = users.DistinctBy(u => u.Email).ToList();
```
> 클래스 객체에 그냥 `Distinct()`를 쓰면 **같은 인스턴스인지**로 비교해서 중복이 안 지워질 수 있습니다. `DistinctBy`를 쓰거나 `record`를 사용하세요.

## 두 리스트 비교
```csharp
int[] a = [1, 2, 3, 4];
int[] b = [3, 4, 5];

a.Union(b);       // 합집합: 1, 2, 3, 4, 5
a.Intersect(b);   // 교집합(공통): 3, 4
a.Except(b);      // 차집합(a 에만 있는 것): 1, 2
b.Except(a);      // b 에만 있는 것: 5

// 속성 기준 (.NET 6+)
var newUsers = incoming.ExceptBy(existing.Select(e => e.Id), u => u.Id);
```

## 두 리스트가 같은지
```csharp
bool sameOrder = a.SequenceEqual(b);                   // 순서까지 같은지
bool sameSet = a.ToHashSet().SetEquals(b);             // 순서 무관, 같은 원소들인지
```
