---
id: recipe-rounding
title: 반올림, 올림, 내림, 버림 (Math.Round, Ceiling, Floor, Truncate)
category: 실무 레시피
order: 2603
summary: Math.Round의 기본 동작(은행가 반올림) 함정과 일반적인 사사오입(MidpointRounding.AwayFromZero), 올림/내림/버림, 소수점 N자리 처리입니다.
keywords: 반올림, 올림, 내림, 버림, 절사, 사사오입, Math.Round, Math.Ceiling, Math.Floor, Math.Truncate, MidpointRounding, AwayFromZero, 은행가 반올림, banker's rounding, 2.5 반올림이 2, 소수점 자리
related: recipe-number-format, explicit-cast, float-types
---
## 한눈에
| 값 | Round | Ceiling(올림) | Floor(내림) | Truncate(버림) |
|---|---|---|---|---|
| 2.5 | **2** (!) | 3 | 2 | 2 |
| 3.5 | 4 | 4 | 3 | 3 |
| -2.7 | -3 | -2 | -3 | -2 |

## 함정: Math.Round(2.5) == 2
.NET의 기본 반올림은 **은행가 반올림**(0.5일 때 가까운 짝수로)입니다. 학교에서 배운 사사오입을 원하면:
```csharp
Math.Round(2.5);                                    // 2
Math.Round(2.5, MidpointRounding.AwayFromZero);     // 3  ← 일반적인 반올림
```

## 소수점 N자리
```csharp
Math.Round(3.14159, 2);                                   // 3.14
Math.Round(1.005m, 2, MidpointRounding.AwayFromZero);     // 1.01 (decimal 이라 정확)
Math.Round(1.005, 2, MidpointRounding.AwayFromZero);      // 1    (double 오차 때문! 1.005 가 실제로는 1.00499...)

// N자리 올림/내림
double CeilTo(double v, int digits) => Math.Ceiling(v * Math.Pow(10, digits)) / Math.Pow(10, digits);
```

## 정수 나눗셈 올림 (페이지 수 계산 등)
```csharp
int pages = (total + pageSize - 1) / pageSize;             // 정수만으로 올림
int pages2 = (int)Math.Ceiling(total / (double)pageSize);
```

## 주의할 점
- 금액은 **decimal**로 계산하고 반올림하세요. double은 1.005, 1.255 같은 값을 정확히 저장하지 못해 반올림 결과가 달라집니다.
- `(int)3.9`는 3(버림)입니다. `Convert.ToInt32(3.5)`는 4, `Convert.ToInt32(2.5)`는 2(은행가 반올림)입니다.
