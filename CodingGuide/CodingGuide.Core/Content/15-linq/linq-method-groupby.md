---
id: linq-method-groupby
title: GroupBy - 키별로 묶고 집계하기
category: LINQ
order: 1513
summary: GroupBy로 같은 키의 요소를 묶고, 그룹별 개수/합계/평균을 구하거나 Dictionary로 바꾸는 방법입니다.
keywords: GroupBy, 그룹, 그룹화, 묶기, 분류, 카테고리별 합계, 그룹별 개수, 그룹별 평균, 집계, 통계, ToDictionary, ToLookup, 중복 찾기
lesson: 린큐_메소드_GroupBy
source: 린큐/Student.cs
related: linq-query-group, linq-aggregate, dictionary
---
## 핵심
```csharp
var groups = students.GroupBy(s => s.Gender)
                     .OrderByDescending(g => g.Key);

foreach (var g in groups)
{
    Console.WriteLine(g.Key);           // "M", "F"
    foreach (var s in g)                // 그룹 안의 학생들
        Console.WriteLine($"  {s}");
}
```

## 그룹별 집계 (가장 많이 쓰는 형태)
```csharp
var stats = students
    .GroupBy(s => s.Gender)
    .Select(g => new
    {
        Gender = g.Key,
        Count = g.Count(),
        AvgAge = g.Average(s => s.Age),
        Oldest = g.MaxBy(s => s.Age)?.Name,
    });

// 개수만 Dictionary 로
Dictionary<string, int> countByGender =
    students.GroupBy(s => s.Gender).ToDictionary(g => g.Key, g => g.Count());

// .NET 9+: 개수 세기 전용
foreach (var (gender, count) in students.CountBy(s => s.Gender))
    Console.WriteLine($"{gender}: {count}");
```

## 중복된 값 찾기
```csharp
var duplicates = emails.GroupBy(e => e)
                       .Where(g => g.Count() > 1)
                       .Select(g => g.Key);
```

## 여러 키로 묶기
```csharp
students.GroupBy(s => new { s.Gender, s.Age });
```
