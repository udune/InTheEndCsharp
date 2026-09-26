---
id: linq-method-where
title: Where - 조건에 맞는 요소만 걸러내기 (filter)
category: LINQ
order: 1511
summary: LINQ 메서드 Where로 리스트에서 조건에 맞는 요소만 골라내는 방법(다른 언어의 filter)입니다.
keywords: Where, 필터, filter, 거르기, 걸러내기, 골라내기, 조건에 맞는, 조건으로 찾기, 검색, 추출, 특정 조건
lesson: 린큐_메소드_Where
source: 린큐/Student.cs
related: linq-query-where, list-search, delegate-predicate, linq-first-single
---
## 핵심
```csharp
var result = students.Where(s => s.Name.EndsWith("e"));   // 이름이 e 로 끝나는 학생

foreach (var s in result)
    Console.WriteLine(s.Name);    // Alice, Charlie, Eve
```

## 자주 쓰는 형태
```csharp
var adults = people.Where(p => p.Age >= 20).ToList();
var active = users.Where(u => u.IsActive && u.Email != null);
var evens = numbers.Where(n => n % 2 == 0);
var notEmpty = lines.Where(l => !string.IsNullOrWhiteSpace(l));
var withIndex = items.Where((item, i) => i % 2 == 0);   // 짝수 번째만

// 조건을 동적으로 이어 붙이기
IEnumerable<Product> q = products;
if (minPrice != null) q = q.Where(p => p.Price >= minPrice);
if (keyword != null) q = q.Where(p => p.Name.Contains(keyword));
var list = q.ToList();
```

## 하나만 필요하면
Where 후 첫 번째를 꺼내기보다 바로 `FirstOrDefault(조건)`을 씁니다.
```csharp
var bob = students.FirstOrDefault(s => s.Name == "Bob");
bool any = students.Any(s => s.Age > 20);   // 존재 여부만 필요하면 Any
int count = students.Count(s => s.Age > 20);
```

## 주의할 점
- Where는 원본을 바꾸지 않습니다. 원본에서 삭제하려면 `list.RemoveAll(조건)`.
- 결과는 지연 실행이라 **foreach나 ToList() 할 때 계산**됩니다.
