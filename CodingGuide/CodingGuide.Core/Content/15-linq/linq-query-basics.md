---
id: linq-query-basics
title: LINQ 쿼리 구문의 구조 (from, where, select)
category: LINQ
order: 1502
summary: from ~ in ~ where ~ select 로 이루어진 LINQ 쿼리 구문의 기본 구조와, foreach 코드와의 대응 관계입니다.
keywords: 쿼리 구문, query syntax, from, in, where, select, 쿼리식, 범위 변수, SQL 같은 문법
lesson: 린큐_쿼리_구조및기초
related: linq-intro, linq-query-select, linq-query-where
---
## 핵심
```csharp
var result = from student in students     // 1) 데이터 소스와 범위 변수
             where student.Age >= 21      // 2) 조건
             select student;              // 3) 결과로 무엇을 낼지

foreach (Student s in result)
    Console.WriteLine(s);
```
위 쿼리는 아래 foreach 코드와 같은 일을 합니다.
```csharp
var result = new List<Student>();
foreach (Student student in students)
    if (student.Age >= 21)
        result.Add(student);
```

## 규칙
- 쿼리는 반드시 `from`으로 시작하고 `select` 또는 `group`으로 끝납니다.
- `student`는 **범위 변수**로, 각 요소를 차례로 가리킵니다.
- 쿼리 결과는 실행을 미룬 `IEnumerable<T>`입니다. 리스트가 필요하면 괄호로 감싸 `.ToList()`를 붙입니다.
```csharp
List<Student> list = (from s in students where s.Age >= 21 select s).ToList();
```
