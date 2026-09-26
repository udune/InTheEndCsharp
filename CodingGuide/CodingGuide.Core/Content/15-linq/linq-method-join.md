---
id: linq-method-join
title: Join 메서드 - 두 컬렉션을 키로 연결
category: LINQ
order: 1514
summary: LINQ 메서드 Join(내부 조인)과 GroupJoin으로 두 컬렉션을 공통 키로 연결하는 방법입니다.
keywords: Join, 조인, GroupJoin, LeftJoin, 연결, 두 리스트, 키로 연결, inner join, 관계형 데이터
lesson: 린큐_메소드_join
source: 린큐/Student.cs, 린큐/Score.cs
related: linq-query-join, linq-method-groupby
---
## 핵심
`Join(상대 컬렉션, 내 키, 상대 키, 결과 만들기)`

```csharp
var result = students.Join(
    studentScores,                    // 연결할 컬렉션
    student => student.Id,            // 내 쪽 키
    score => score.StudentId,         // 상대 쪽 키
    (student, score) => (student, score));   // 결과

foreach (var (student, score) in result)
    Console.WriteLine($"{student.Name}: {score.ScoreValue} - {score.Subject}");
```

## 학생별 점수 목록 (GroupJoin)
```csharp
var perStudent = students.GroupJoin(
    studentScores,
    st => st.Id,
    sc => sc.StudentId,
    (st, scores) => new { st.Name, Avg = scores.Average(x => x.ScoreValue) });
```

## 키가 많고 자주 찾는다면
조인 대신 Dictionary로 미리 만들어 두는 것도 좋은 방법입니다.
```csharp
var studentById = students.ToDictionary(s => s.Id);
foreach (var sc in studentScores)
    Console.WriteLine($"{studentById[sc.StudentId].Name}: {sc.ScoreValue}");
```
