---
id: linq-query-where
title: LINQ 쿼리 where - 조건 걸기
category: LINQ
order: 1504
summary: 쿼리 구문의 where 절로 조건에 맞는 요소만 남기고, &&와 ||로 여러 조건을 조합하는 방법입니다.
keywords: where, 쿼리 where, 조건, 필터, 거르기, 여러 조건, and, or, 쿼리 구문 조건
lesson: 린큐_쿼리_where
related: linq-method-where, linq-query-let, logical-operators
---
## 핵심
```csharp
var result = from s in students
             where s.Age >= 21 || s.Name == "Alice"     // 21살 이상이거나 이름이 Alice
             select new { MyName = s.Name, s.Age };

foreach (var r in result)
    Console.WriteLine($"{r.MyName} {r.Age}");
```

## where 여러 번
where를 여러 번 쓰면 **AND**로 이어집니다.
```csharp
var result = from s in students
             where s.Age >= 20
             where s.Name.StartsWith("A")
             select s;
```

## 메서드 구문으로는
```csharp
var result = students.Where(s => s.Age >= 21 || s.Name == "Alice")
                     .Select(s => new { MyName = s.Name, s.Age });
```
