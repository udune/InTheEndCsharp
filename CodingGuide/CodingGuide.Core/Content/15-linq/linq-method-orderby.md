---
id: linq-method-orderby
title: OrderBy, ThenBy와 메서드 체이닝
category: LINQ
order: 1512
summary: OrderBy/OrderByDescending으로 정렬하고 ThenBy로 2차 기준을 두는 방법, 여러 LINQ 메서드를 점(.)으로 이어 쓰는 메서드 체이닝입니다.
keywords: OrderBy, OrderByDescending, ThenBy, ThenByDescending, 정렬, 내림차순, 오름차순, 메서드 체이닝, method chaining, 여러 기준, 상위 N개, Top N
lesson: 린큐_메소드_OrderBy_메소드체이닝
source: 린큐/Student.cs
related: linq-query-orderby, list-sort, linq-take-skip
---
## 핵심
```csharp
var byAge = students.OrderBy(s => s.Age);                 // 나이 오름차순
var byAgeDesc = students.OrderByDescending(s => s.Age);   // 나이 내림차순

var sorted = students.OrderBy(s => s.Gender)              // 1차: 성별
                     .ThenByDescending(s => s.Age)        // 2차: 나이 내림차순
                     .ThenBy(s => s.Name);                // 3차: 이름
```
> 예제 코드의 `OrderBy(Age).ThenByDescending(Age)`는 같은 키를 두 번 써서 두 번째 기준이 의미가 없습니다. ThenBy에는 **다른** 속성을 주세요.

## 메서드 체이닝
각 LINQ 메서드가 IEnumerable을 돌려주므로 이어서 호출할 수 있습니다. **위에서 아래로 읽히게** 줄을 나눠 쓰세요.
```csharp
var top3 = students
    .Where(s => s.Age >= 20)
    .OrderByDescending(s => s.Scores.Average())
    .Take(3)
    .Select(s => s.Name)
    .ToList();
```

## 주의할 점
- `OrderBy(...).OrderBy(...)`처럼 OrderBy를 두 번 쓰면 **앞의 정렬이 무시**됩니다. 두 번째부터는 `ThenBy`.
- 문자열 정렬 기준을 명확히 하려면 `OrderBy(s => s.Name, StringComparer.Ordinal)`.
