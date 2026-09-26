---
id: tuples
title: 튜플 - 여러 값을 한 번에 반환하기 ((int, string))
category: C# 문법 보강
order: 2503
summary: 클래스를 만들지 않고 여러 값을 묶어 반환하거나 전달하는 ValueTuple, 이름 있는 요소, 분해(deconstruction), 값 교환입니다.
keywords: 튜플, tuple, ValueTuple, 여러 값 반환, 다중 반환, 반환값 여러 개, 분해, deconstruction, 이름 있는 튜플, 값 교환, swap, Item1
related: method-out, record-type, anonymous-type, linq-query-let
---
## 핵심
```csharp
(int Min, int Max) GetRange(int[] nums) => (nums.Min(), nums.Max());

var range = GetRange([3, 7, 1]);
Console.WriteLine($"{range.Min} ~ {range.Max}");   // 1 ~ 7

var (min, max) = GetRange([3, 7, 1]);               // 분해해서 변수로 받기
var (_, onlyMax) = GetRange([3, 7, 1]);             // 필요 없는 값은 _
```

## 만들기
```csharp
var t1 = (1, "a");                      // 요소 이름: Item1, Item2
var t2 = (Id: 1, Name: "a");            // 이름 있는 튜플
(int Id, string Name) t3 = (1, "a");

string name = "홍"; int age = 3;
var t4 = (name, age);                   // 변수 이름이 요소 이름이 됨 → t4.name
```

## 활용
```csharp
(a, b) = (b, a);                                    // 값 교환

foreach (var (key, value) in dict) { }              // Dictionary 분해

var results = students.Select(s => (s.Name, Avg: s.Scores.Average()));   // LINQ 결과

bool TryFind(int id, out User? user) { ... }        // out 대신
(bool Found, User? User) Find(int id) { ... }       // 튜플 반환
```

## 튜플 vs 클래스/record
- 메서드 **안이나 private 범위**에서 잠깐 묶을 때: 튜플
- 여러 곳에서 쓰는 **공개 API**의 반환 타입: 의미가 분명한 record/class
