---
id: linq-query-orderby
title: LINQ 쿼리 orderby - 정렬 (여러 기준, descending)
category: LINQ
order: 1506
summary: 쿼리 구문의 orderby로 하나 또는 여러 기준으로 정렬하고, descending으로 내림차순 정렬하는 방법입니다.
keywords: orderby, 정렬, 오름차순, ascending, 내림차순, descending, 여러 기준 정렬, 다중 정렬
lesson: 린큐_쿼리_orderby
related: linq-method-orderby, list-sort, linq-query-let
---
## 핵심
```csharp
var results = from s in students
              let average = s.Scores.Average()
              where average > 3
              orderby s.Age, s.Name, average descending    // 나이 → 이름 → 평균(내림차순)
              select (MyName: s.Name, s.Age, average);
```
- 쉼표로 기준을 나열하면 **앞 기준이 같을 때** 다음 기준으로 정렬합니다.
- 기본은 오름차순(`ascending` 생략 가능), 내림차순은 `descending`.

## 메서드 구문으로는
```csharp
students.OrderBy(s => s.Age)
        .ThenBy(s => s.Name)
        .ThenByDescending(s => s.Scores.Average());
```
