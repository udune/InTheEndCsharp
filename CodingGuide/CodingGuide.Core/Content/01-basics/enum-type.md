---
id: enum-type
title: 열거형 enum
category: 값 타입과 변수
order: 104
summary: 관련된 상수들을 이름 있는 타입으로 묶는 enum의 선언, 값 지정, 문자열/정수 변환 방법입니다.
keywords: enum, 열거형, 상수, 요일, 상태값, Enum.Parse, Enum.GetValues, Flags, 문자열을 enum으로
lesson: Enum타입
related: const-readonly, switch-statement, recipe-parse-number
---
## 핵심
`const int SUNDAY = 0;` 같은 상수를 여러 개 두는 대신, **의미 있는 이름의 집합**을 하나의 타입으로 만듭니다. 잘못된 값이 들어오는 것을 컴파일러가 막아 줍니다.

```csharp
enum Days
{
    Sunday = 1,   // 값을 지정하지 않으면 0부터 1씩 증가
    Monday = 2,
    Tuesday = 3,
}

Days day = Days.Sunday;
if (day == Days.Sunday)
    Console.WriteLine("It's sunday");

Console.WriteLine(day);       // "Sunday" (이름이 출력됨)
Console.WriteLine((int)day);  // 1
```

## 변환
```csharp
Days d1 = (Days)2;                        // 정수 → enum
Days d2 = Enum.Parse<Days>("Monday");     // 문자열 → enum
bool ok = Enum.TryParse("Tue", out Days d3); // 실패해도 예외 없음
string name = Days.Monday.ToString();     // enum → 문자열

foreach (Days v in Enum.GetValues<Days>()) // 모든 값 순회
    Console.WriteLine(v);
```

## 비트 플래그 [Flags]
여러 값을 동시에 가질 수 있을 때 씁니다.
```csharp
[Flags]
enum Permission { None = 0, Read = 1, Write = 2, Delete = 4 }

var p = Permission.Read | Permission.Write;
Console.WriteLine(p.HasFlag(Permission.Write)); // True
```

## 주의할 점
- `(Days)100`처럼 정의되지 않은 정수도 캐스팅은 됩니다. 검증하려면 `Enum.IsDefined(typeof(Days), 100)`을 씁니다.
