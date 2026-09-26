---
id: linq-query-select
title: LINQ 쿼리 select와 익명 타입 (new { })
category: LINQ
order: 1503
summary: 쿼리 구문의 select로 필요한 값만 뽑거나, 익명 타입 new { ... }으로 새로운 모양의 결과를 만드는 방법입니다.
keywords: select, 투영, projection, 익명 타입, anonymous type, new { }, 특정 속성만, 이름 바꾸기, 필드 추출
lesson: 린큐_쿼리_select
related: linq-method-select, anonymous-type, tuples, linq-query-basics
---
## 핵심
`select`는 각 요소를 **원하는 모양으로 바꿔서** 내보냅니다.

```csharp
// 1) 속성 하나만
var names = from s in students select s.Name;              // IEnumerable<string>

// 2) 익명 타입으로 여러 값
var infos = from s in students select new { s.Name, s.Age };
foreach (var x in infos) Console.WriteLine($"{x.Name} {x.Age}");

// 3) 속성 이름 바꾸기
var renamed = from s in students select new { MyName = s.Name, s.Age };
foreach (var x in renamed) Console.WriteLine($"{x.MyName} {x.Age}");

// 4) 튜플로
var tuples = from s in students select (s.Name, s.Age);
```

## 익명 타입
- `new { A = 1, B = "x" }` 처럼 **이름 없는 클래스**를 즉석에서 만듭니다. 변수는 `var`로만 받을 수 있습니다.
- 속성은 읽기 전용이고, 같은 메서드 안에서만 쓰기 좋습니다. 메서드 밖으로 돌려주려면 **record나 튜플**을 쓰세요.
