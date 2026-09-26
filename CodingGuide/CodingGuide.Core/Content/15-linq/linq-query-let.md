---
id: linq-query-let
title: LINQ 쿼리 let - 중간 계산값 저장
category: LINQ
order: 1505
summary: 쿼리 중간에 let으로 계산 결과(평균 등)를 변수에 담아 where/select/orderby에서 재사용하는 방법입니다.
keywords: let, 쿼리 변수, 중간 값, 계산 재사용, 평균, Average, 쿼리 안 변수
lesson: 린큐_쿼리_let
related: linq-query-where, linq-query-orderby, linq-aggregate
---
## 핵심
같은 계산을 where와 select에서 반복하지 않도록 `let`에 담습니다.

```csharp
var results = from s in students
              let gender = s.Gender
              where gender == "M"
              let average = s.Scores.Average()          // 점수 평균을 한 번만 계산
              where s.Age >= 21 || s.Name == "Alice"
              select (MyName: s.Name, s.Age, average);  // 튜플로 결과

foreach (var r in results)
    Console.WriteLine($"{r.MyName} {r.Age} Average : {r.average}");
```

## 메서드 구문으로 표현하면
let은 메서드 구문에 없으므로 `Select`로 중간 객체를 만듭니다.
```csharp
var results = students
    .Where(s => s.Gender == "M")
    .Select(s => new { s, average = s.Scores.Average() })
    .Where(x => x.s.Age >= 21 || x.s.Name == "Alice")
    .Select(x => (MyName: x.s.Name, x.s.Age, x.average));
```
이런 경우는 쿼리 구문이 더 읽기 쉽습니다.

## 주의할 점
- 빈 리스트에 `Average()`를 쓰면 `InvalidOperationException`이 납니다. 빈 값이 있을 수 있으면 `DefaultIfEmpty().Average()`를 씁니다.
