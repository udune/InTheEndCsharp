---
id: linq-take-skip
title: 일부만 가져오기와 페이징 - Take, Skip, Chunk
category: LINQ
order: 1552
summary: 앞에서 N개(Take), N개 건너뛰기(Skip), 조건이 맞는 동안(TakeWhile), N개씩 나누기(Chunk)와 페이지 처리 방법입니다.
keywords: Take, Skip, TakeWhile, SkipWhile, TakeLast, Chunk, 페이징, paging, 페이지, 상위 N개, top 10, 앞에서 몇 개, 나누기, 분할, 배치
related: linq-method-orderby, linq-first-single
---
## 핵심
```csharp
int[] nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

nums.Take(3);                   // 1, 2, 3
nums.Skip(7);                   // 8, 9, 10
nums.TakeLast(2);               // 9, 10
nums.Take(2..5);                // 3, 4, 5 (범위, .NET 6+)
nums.TakeWhile(n => n < 4);     // 1, 2, 3 (조건이 처음 거짓이 되면 멈춤)
nums.SkipWhile(n => n < 8);     // 8, 9, 10
```

## 상위 N개
```csharp
var top5 = students.OrderByDescending(s => s.Score).Take(5).ToList();
```

## 페이징
```csharp
int page = 2, pageSize = 10;           // 2페이지 (1부터 시작)
var items = all
    .OrderBy(x => x.Id)                // 페이징 전에는 반드시 정렬
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToList();
int totalPages = (int)Math.Ceiling(all.Count / (double)pageSize);
```

## N개씩 묶어서 처리 (Chunk, .NET 6+)
```csharp
foreach (int[] batch in ids.Chunk(100))   // 100개씩 나눠서 API 호출 등
    await SendAsync(batch);
```
