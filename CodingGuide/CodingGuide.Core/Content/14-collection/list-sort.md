---
id: list-sort
title: List 정렬과 역순 (Sort, Reverse)
category: 열거자와 컬렉션
order: 1408
summary: List를 제자리에서 정렬하는 Sort, 기준을 주는 비교 람다, 뒤집는 Reverse, 그리고 LINQ OrderBy와의 차이입니다.
keywords: 정렬, sort, 오름차순, 내림차순, 역순, 뒤집기, Reverse, Sort, 비교 람다, 객체 정렬, 속성 기준 정렬
lesson: 컬렉션_List_역순및정렬
related: delegate-comparison, linq-method-orderby, multidim-array
---
## 핵심
```csharp
var nums = new List<int> { 3, 100, 5, -1, 20 };

nums.Reverse();                          // 순서 뒤집기 → 20, -1, 5, 100, 3
nums.Sort();                             // 오름차순 → -1, 3, 5, 20, 100
nums.Sort((a, b) => a.CompareTo(b));     // 같은 의미 (기준을 직접 지정)
nums.Sort((a, b) => b.CompareTo(a));     // 내림차순
```

## 객체 리스트 정렬
```csharp
people.Sort((a, b) => a.Age.CompareTo(b.Age));        // 나이 오름차순
people.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
```

## Sort vs OrderBy
| | `list.Sort()` | `list.OrderBy(...)` (LINQ) |
|---|---|---|
| 원본 | **원본을 바꿈** | 원본 그대로, 새 시퀀스 반환 |
| 반환 | void | `IOrderedEnumerable<T>` |
| 여러 기준 | 비교 람다를 직접 작성 | `.ThenBy()`로 간단 |

```csharp
var sorted = people.OrderBy(p => p.Age).ThenBy(p => p.Name).ToList();
var desc = people.OrderByDescending(p => p.Score).ToList();
```

## 주의할 점
- `nums.Sort()`는 반환값이 없습니다. `var x = nums.Sort();`는 컴파일 오류입니다.
- 클래스 객체를 기준 없이 `Sort()`하면 `InvalidOperationException`(IComparable 미구현)이 납니다.
