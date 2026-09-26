---
id: anonymous-type
title: 익명 타입 (new { Name, Age })
category: C# 문법 보강
order: 2504
summary: 클래스를 선언하지 않고 new { ... }로 즉석에서 만드는 읽기 전용 익명 타입과, LINQ select에서의 활용, 한계입니다.
keywords: 익명 타입, anonymous type, new { }, 이름 없는 클래스, var, LINQ select, 임시 객체, 투영
related: linq-query-select, tuples, var-declaration, record-type
---
## 핵심
```csharp
var person = new { Name = "홍길동", Age = 30 };
Console.WriteLine(person.Name);    // 홍길동
Console.WriteLine(person);         // { Name = 홍길동, Age = 30 }
// person.Age = 31;                // 오류: 읽기 전용
```
- 컴파일러가 이름 없는 클래스를 자동으로 만듭니다. 그래서 **`var`로만** 받을 수 있습니다.
- 속성 이름을 생략하면 변수/속성 이름을 그대로 씁니다: `new { s.Name, s.Age }`
- 같은 속성 구성이면 `Equals`가 값으로 비교됩니다.

## LINQ에서 가장 많이 사용
```csharp
var infos = students.Select(s => new { s.Name, Average = s.Scores.Average() });
foreach (var x in infos)
    Console.WriteLine($"{x.Name}: {x.Average}");

// 여러 키로 그룹화
students.GroupBy(s => new { s.Gender, s.Age });
```

## 한계
- 메서드의 **반환 타입이나 매개변수**로 쓸 수 없습니다(이름이 없으니까). 밖으로 내보내야 하면 record나 튜플을 쓰세요.
- `with` 식으로 복사본을 만들 수는 있습니다: `person with { Age = 31 }` (C# 10+)
