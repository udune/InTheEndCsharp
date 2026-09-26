---
id: linq-query-join
title: LINQ 쿼리 join - 두 컬렉션 연결
category: LINQ
order: 1508
summary: 쿼리 구문의 join ~ on ~ equals로 학생 목록과 점수 목록처럼 두 컬렉션을 공통 키로 연결하는 방법입니다.
keywords: join, 조인, on, equals, 두 리스트 합치기, 연결, 관계, 외래키, 내부 조인, inner join, left join, group join
lesson: 린큐_쿼리_join
source: 린큐/Student.cs, 린큐/Score.cs
related: linq-method-join, linq-query-group
---
## 핵심
```csharp
var result = from student in students
             join score in studentScores on student.Id equals score.StudentId
             select (student, score);

foreach (var (student, score) in result)
    Console.WriteLine($"{student.Name}: {score.ScoreValue} - {score.Subject}");
```
- `on A equals B` : 양쪽 키가 같은 쌍만 결과로 나옵니다(**내부 조인**).
- `==`가 아니라 반드시 `equals` 키워드를 씁니다. 왼쪽에는 앞 컬렉션, 오른쪽에는 join한 컬렉션의 키가 와야 합니다.

## 학생별 점수 묶기 (group join)
```csharp
var perStudent = from st in students
                 join sc in studentScores on st.Id equals sc.StudentId into scores
                 select new { st.Name, Total = scores.Sum(x => x.ScoreValue) };
```

## 왼쪽 외부 조인 (점수가 없는 학생도 포함)
```csharp
var left = from st in students
           join sc in studentScores on st.Id equals sc.StudentId into g
           from sc in g.DefaultIfEmpty()
           select new { st.Name, Score = sc?.ScoreValue ?? 0 };
```
.NET 10부터는 메서드 구문 `LeftJoin`도 제공됩니다.
