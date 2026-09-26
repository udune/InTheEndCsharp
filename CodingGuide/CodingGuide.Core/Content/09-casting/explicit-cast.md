---
id: explicit-cast
title: 암시적 변환과 명시적 변환(캐스팅)
category: 타입 변환
order: 901
summary: 작은 타입에서 큰 타입으로는 자동 변환되고, 큰 타입에서 작은 타입으로는 (타입) 캐스팅이 필요한 이유와 데이터 손실을 설명합니다.
keywords: 형변환, 타입 변환, 캐스팅, cast, 암시적 변환, 명시적 변환, (int), 소수점 버림, 데이터 손실, double to int, CS0266
lesson: 타입명시적변환
related: convert-class, as-operator, is-operator, recipe-rounding, error-cs0266
---
## 핵심
- **암시적 변환**: 손실 없이 넓은 타입으로 → 자동 (`int → long → double`)
- **명시적 변환(캐스팅)**: 손실 가능성이 있는 좁은 타입으로 → `(타입)`을 직접 써야 함

```csharp
int intNum = 100;
double doubleNum = intNum;          // 암시적: OK

double d = 123.456;
// int i = d;                       // 오류 CS0266: 명시적 변환이 필요
int i = (int)d;                     // 123 ← 소수점 "버림" (반올림 아님)
float f = (float)d;                 // 123.456 (정밀도 일부 손실 가능)
```

## 캐스팅할 때 조심할 것
```csharp
(int)3.99        // 3   (버림)
(int)-3.99       // -3  (0 쪽으로 버림)
Math.Round(3.5)  // 4   반올림이 필요하면 Math.Round
(byte)300        // 44  범위를 넘으면 값이 잘림 (오류 없음!)
checked((byte)300) // OverflowException
```

## 참조 타입의 캐스팅
```csharp
object o = "hello";
string s = (string)o;       // OK
int n = (int)o;             // InvalidCastException (실제 타입이 아님)
```
실패 가능성이 있으면 `as` 또는 `is` 패턴을 쓰세요.
