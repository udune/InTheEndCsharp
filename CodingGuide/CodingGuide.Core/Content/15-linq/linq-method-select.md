---
id: linq-method-select
title: Select - 각 요소를 다른 모양으로 바꾸기 (map)
category: LINQ
order: 1509
summary: LINQ 메서드 Select로 각 요소에서 필요한 값만 뽑거나 새 객체로 변환하는 방법(다른 언어의 map)입니다.
keywords: Select, 변환, 매핑, map, 투영, 속성만 뽑기, 이름 목록, 리스트 변환, DTO 변환, 인덱스와 함께
lesson: 린큐_메소드_Select
source: 린큐/Student.cs
related: linq-query-select, linq-method-selectmany, linq-method-where
---
## 핵심
```csharp
var ages = students.Select(s => s.Age);                     // IEnumerable<int>
var names = students.Select(s => s.Name).ToList();          // List<string>
var infos = students.Select(s => new { MyName = s.Name, MyAge = s.Age });

foreach (var x in infos)
    Console.WriteLine($"{x.MyName} {x.MyAge}");
```

## 자주 쓰는 형태
```csharp
// 인덱스와 함께
var numbered = names.Select((name, i) => $"{i + 1}. {name}");

// 문자열 목록 → 숫자 목록
List<int> nums = "1,2,3".Split(',').Select(int.Parse).ToList();

// 엔티티 → DTO
var dtos = users.Select(u => new UserDto { Id = u.Id, Name = u.Name }).ToList();
```

## 주의할 점
- Select는 개수를 바꾸지 않습니다(입력 N개 → 출력 N개). 거르려면 `Where`, 펼치려면 `SelectMany`.
- 결과는 지연 실행입니다. 리스트로 확정하려면 `.ToList()`.
