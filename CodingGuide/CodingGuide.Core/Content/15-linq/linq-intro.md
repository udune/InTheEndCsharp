---
id: linq-intro
title: LINQ란? - 컬렉션을 질의하는 문법
category: LINQ
order: 1501
summary: 컬렉션에서 원하는 데이터를 거르고, 바꾸고, 정렬하고, 묶는 작업을 SQL처럼 선언적으로 쓰는 LINQ의 개념과 두 가지 문법(쿼리/메서드)입니다.
keywords: LINQ, 린큐, 링크, 쿼리, query, 질의, 데이터 처리, 컬렉션 가공, 쿼리 구문, 메서드 구문, System.Linq, 필터링, 변환
lesson: 린큐_정의
related: linq-query-basics, linq-method-where, linq-method-select, linq-deferred-execution, linq-cheatsheet
---
## 핵심
LINQ(Language INtegrated Query)는 **"어떻게"가 아니라 "무엇을" 원하는지** 적는 방식으로 컬렉션을 다룹니다.

```csharp
List<Student> students =
[
    new Student { ID = 1, Name = "Alice", Age = 20 },
    new Student { ID = 2, Name = "Bob", Age = 21 },
    new Student { ID = 3, Name = "Charlie", Age = 18 },
];

// foreach 로 직접 작성
var adults1 = new List<Student>();
foreach (var s in students)
    if (s.Age >= 20) adults1.Add(s);

// LINQ 메서드 구문 (실무에서 가장 많이 씀)
var adults2 = students.Where(s => s.Age >= 20).ToList();

// LINQ 쿼리 구문 (SQL 과 비슷)
var adults3 = (from s in students where s.Age >= 20 select s).ToList();
```

## 두 가지 문법
| 쿼리 구문 | 메서드 구문 |
|---|---|
| `from s in list` | `list` |
| `where s.Age > 20` | `.Where(s => s.Age > 20)` |
| `orderby s.Name` | `.OrderBy(s => s.Name)` |
| `select s.Name` | `.Select(s => s.Name)` |
| `group s by s.Gender` | `.GroupBy(s => s.Gender)` |

둘은 결과가 같습니다. `join`, `let`이 많으면 쿼리 구문이 읽기 쉽고, 그 외에는 메서드 구문이 짧습니다. `Count()`, `First()`, `Distinct()` 등은 메서드 구문에만 있습니다.

## 사용 조건
- `using System.Linq;` (최신 프로젝트는 ImplicitUsings로 자동 포함)
- `IEnumerable<T>`이면 무엇이든: 배열, List, Dictionary, 문자열(`"abc".Where(char.IsLetter)`)
