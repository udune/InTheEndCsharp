---
id: linq-query-group
title: LINQ 쿼리 group by ~ into - 그룹으로 묶기
category: LINQ
order: 1507
summary: 쿼리 구문의 group ~ by ~ into로 같은 키(성별 등)를 가진 요소들을 그룹으로 묶고, 그룹별로 처리하는 방법입니다.
keywords: group, group by, into, 그룹, 그룹화, 묶기, 분류, 카테고리별, 성별로, IGrouping, Key
lesson: 린큐_쿼리_group
related: linq-method-groupby, linq-aggregate, dictionary
---
## 핵심
```csharp
var genderGroups = from s in students
                   group s by s.Gender into g          // 성별로 묶어서 g 라고 부름
                   select (Gender: g.Key, Group: g);

foreach (var gg in genderGroups)
{
    Console.WriteLine($"Gender: {gg.Gender}");
    foreach (var s in gg.Group)
        Console.WriteLine($"  {s.Name} {s.Age}");
}
```

## 그룹별 집계
```csharp
var stats = from s in students
            group s by s.Gender into g
            select new
            {
                Gender = g.Key,
                Count = g.Count(),
                AvgAge = g.Average(x => x.Age),
            };
```

## 알아둘 것
- 그룹 `g`는 `IGrouping<TKey, TElement>` 타입이며, `g.Key`가 묶은 기준값이고 `g` 자체를 foreach 하면 그룹 안의 요소가 나옵니다.
- `into` 없이 `group s by s.Gender`로 쿼리를 끝낼 수도 있습니다.
