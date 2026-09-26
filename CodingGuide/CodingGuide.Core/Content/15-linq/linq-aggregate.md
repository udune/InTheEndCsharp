---
id: linq-aggregate
title: 집계 - Count, Sum, Average, Min, Max, MaxBy, Aggregate
category: LINQ
order: 1550
summary: 합계, 평균, 개수, 최댓값/최솟값, 최댓값을 가진 요소 찾기 등 LINQ 집계 메서드 모음입니다.
keywords: 합계, 총합, sum, 평균, average, 개수, count, 최댓값, 최솟값, max, min, MaxBy, MinBy, 가장 큰, 가장 작은, 가장 나이 많은, Aggregate, 누적, 통계
related: linq-method-groupby, linq-query-let, linq-cheatsheet
---
## 기본 집계
```csharp
int[] nums = [3, 7, 1, 9, 4];

nums.Count();              // 5
nums.Count(n => n > 3);    // 3 (조건에 맞는 개수)
nums.Sum();                // 24
nums.Average();            // 4.8 (double)
nums.Min();                // 1
nums.Max();                // 9
```

## 객체 리스트에서
```csharp
decimal total = orders.Sum(o => o.Price * o.Quantity);
double avgAge = people.Average(p => p.Age);
int maxScore = students.Max(s => s.Score);            // 최댓값 "값"

Student? best = students.MaxBy(s => s.Score);         // 최댓값을 가진 "요소" (.NET 6+)
Student? youngest = students.MinBy(s => s.Age);
```
> `Max(s => s.Score)`는 **점수(숫자)**, `MaxBy(s => s.Score)`는 **그 학생(객체)** 을 돌려줍니다.

## Aggregate - 직접 누적하기
```csharp
int product = nums.Aggregate((acc, n) => acc * n);           // 모두 곱하기
string csv = names.Aggregate((a, b) => $"{a},{b}");          // string.Join 이 더 간단
```

## 주의할 점
- **빈 컬렉션**에서 `Average()`, `Min()`, `Max()`는 `InvalidOperationException`("시퀀스에 요소가 없습니다")을 던집니다.
```csharp
double avg = nums.Any() ? nums.Average() : 0;
double avg2 = nums.DefaultIfEmpty(0).Average();
int? max = nums.Cast<int?>().Max();   // 비어 있으면 null
```
- 빈 컬렉션의 `Sum()`은 0, `Count()`는 0 이라 안전합니다.
- `int` 합계가 매우 크면 오버플로 예외가 날 수 있습니다. `Sum(x => (long)x)`로 계산하세요.
