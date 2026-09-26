---
id: logical-operators
title: 논리 연산자 (&&, ||, !)
category: 연산자
order: 204
summary: 여러 조건을 조합하는 AND, OR, NOT 연산자와 단락 평가(short-circuit)를 설명합니다.
keywords: 논리, 그리고, 또는, 아니다, and, or, not, &&, ||, !, 조건 조합, 여러 조건, 단락 평가, short circuit
lesson: 논리연산자
related: comparison-operators, if-ternary, bit-operators, pattern-matching
---
## 핵심
- `a && b` : 그리고(AND) — 둘 다 true 일 때만 true
- `a || b` : 또는(OR) — 하나라도 true 이면 true
- `!a` : 부정(NOT) — true ↔ false

```csharp
bool a = true, b = false;
Console.WriteLine(a && b);    // False
Console.WriteLine(a || b);    // True
Console.WriteLine(!(a || b)); // False
```

## 단락 평가 (Short-circuit)
`&&`는 왼쪽이 false면, `||`는 왼쪽이 true면 **오른쪽을 아예 실행하지 않습니다.** null 검사에 자주 씁니다.

```csharp
if (user != null && user.Age > 19)   // user 가 null 이면 user.Age 를 평가하지 않음
    Console.WriteLine("성인");
```

## 패턴 매칭의 and / or / not
```csharp
if (score is >= 80 and < 90) { }
if (obj is not null) { }
if (ch is 'a' or 'e' or 'i' or 'o' or 'u') { }
```
