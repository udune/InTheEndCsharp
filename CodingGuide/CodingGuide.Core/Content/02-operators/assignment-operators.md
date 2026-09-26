---
id: assignment-operators
title: 할당(복합 대입) 연산자 (+=, -=, *=, /=, %=, ??=)
category: 연산자
order: 203
summary: 연산과 대입을 한 번에 하는 복합 대입 연산자를 설명합니다.
keywords: 대입, 할당, +=, -=, *=, /=, %=, ??=, 누적, 합계
lesson: 할당연산자
related: arithmetic-operators, null-coalescing, delegate-multicast
---
## 핵심
`a = a + 10`을 `a += 10`으로 줄여 씁니다.

```csharp
decimal a = 10;
a += 10; // 20
a -= 10; // 10
a *= 2;  // 20
a /= 2;  // 10
a %= 2;  // 0
```

## 그 밖의 대입 연산자
```csharp
string? name = null;
name ??= "기본값";   // null 일 때만 대입

string s = "a";
s += "b";           // 문자열 이어 붙이기 → "ab"

Action act = A;
act += B;           // 델리게이트/이벤트 구독 추가 (→ 델리게이트 멀티캐스트)
```
