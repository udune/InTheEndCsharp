---
id: linq-cheatsheet
title: LINQ 치트시트 - 하고 싶은 일별 메서드 찾기
category: LINQ
order: 1555
summary: "~하고 싶다"에서 바로 LINQ 메서드를 찾을 수 있도록 목적별로 정리한 요약표입니다.
keywords: LINQ 요약, 치트시트, cheatsheet, 정리, 목록, 메서드 목록, 무엇을 써야, Any, All, Contains, Zip, ToDictionary, ToHashSet, Reverse, Concat, 합치기
related: linq-intro, linq-method-where, linq-aggregate, linq-first-single, linq-set-operations
---
## 목적별 LINQ 메서드
| 하고 싶은 일 | 메서드 | 예 |
|---|---|---|
| 조건으로 거르기 | `Where` | `.Where(x => x.Age > 20)` |
| 모양 바꾸기 | `Select` | `.Select(x => x.Name)` |
| 리스트의 리스트 펼치기 | `SelectMany` | `.SelectMany(x => x.Tags)` |
| 정렬 | `OrderBy`, `ThenBy`, `...Descending` | `.OrderBy(x => x.Name)` |
| 하나 찾기 | `FirstOrDefault`, `SingleOrDefault` | `.FirstOrDefault(x => x.Id == 3)` |
| 있는지 확인 | `Any` | `.Any(x => x.IsAdmin)` |
| 모두 만족하는지 | `All` | `.All(x => x.Age >= 0)` |
| 특정 값 포함 | `Contains` | `.Contains(5)` |
| 개수 | `Count` | `.Count(x => x.Done)` |
| 합계/평균/최대/최소 | `Sum`, `Average`, `Max`, `Min` | `.Sum(x => x.Price)` |
| 최대값을 가진 요소 | `MaxBy`, `MinBy` | `.MaxBy(x => x.Score)` |
| 그룹으로 묶기 | `GroupBy` | `.GroupBy(x => x.Dept)` |
| 개수 세기(그룹별) | `CountBy` (.NET 9+) | `.CountBy(x => x.Dept)` |
| 중복 제거 | `Distinct`, `DistinctBy` | `.DistinctBy(x => x.Email)` |
| 앞에서 N개 | `Take` | `.Take(10)` |
| N개 건너뛰기 | `Skip` | `.Skip(20)` |
| N개씩 나누기 | `Chunk` | `.Chunk(100)` |
| 두 목록 이어 붙이기 | `Concat` | `a.Concat(b)` |
| 합/교/차집합 | `Union`, `Intersect`, `Except` | `a.Except(b)` |
| 두 목록 짝짓기 | `Zip` | `names.Zip(ages)` |
| 키로 연결 | `Join`, `GroupJoin` | |
| 순서 뒤집기 | `Reverse` | `.Reverse()` |
| 리스트/배열로 확정 | `ToList`, `ToArray` | |
| Dictionary로 | `ToDictionary` | `.ToDictionary(x => x.Id)` |
| 집합으로 | `ToHashSet` | |
| 타입으로 거르기 | `OfType<T>` | `objs.OfType<string>()` |
| 인덱스와 함께 | `Select((x, i) => ...)`, `Index()` (.NET 9+) | |

## 예: 여러 메서드 조합
```csharp
// 부서별 평균 급여 상위 3개 부서
var top3 = employees
    .GroupBy(e => e.Dept)
    .Select(g => new { Dept = g.Key, Avg = g.Average(e => e.Salary) })
    .OrderByDescending(x => x.Avg)
    .Take(3)
    .ToList();
```
