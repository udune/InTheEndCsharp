---
id: float-types
title: 소수형 타입 (float, double, decimal)
category: 값 타입과 변수
order: 102
summary: 실수를 저장하는 float, double, decimal의 차이와 돈 계산에 decimal을 써야 하는 이유를 설명합니다.
keywords: 실수, 소수, 소수점, float, double, decimal, 부동소수점, 정밀도, 오차, 돈 계산, 금액, 접미사, f, m
lesson: 소수형타입
related: int-types, recipe-number-format, recipe-rounding
---
## 핵심
| 타입 | 크기 | 유효 자릿수 | 리터럴 접미사 | 용도 |
|---|---|---|---|---|
| float | 4바이트 | 약 7자리 | `f` | 그래픽, 게임 좌표 |
| double | 8바이트 | 약 15~16자리 | 없음(기본) | 일반 계산, 과학 계산 |
| decimal | 16바이트 | 28~29자리 | `m` | **돈, 금액, 정확한 10진 계산** |

```csharp
float f = 1.1f;      // f 가 없으면 double 로 보고 컴파일 오류
double d = 1.51;
decimal m = 1.11m;   // m 이 없으면 컴파일 오류

Console.WriteLine(f + 1.2f);   // 2.3000002 처럼 오차가 보일 수 있음
Console.WriteLine(d + 1.61);   // 3.12
Console.WriteLine(m + 1.21m);  // 2.32 (정확)
```

## 왜 오차가 생기나요?
float/double은 2진수로 값을 저장하기 때문에 0.1 같은 10진 소수를 정확히 표현하지 못합니다.

```csharp
Console.WriteLine(0.1 + 0.2 == 0.3);     // False
Console.WriteLine(0.1m + 0.2m == 0.3m);  // True
```

## 주의할 점
- **금액 계산에는 반드시 decimal**을 씁니다.
- double 값을 비교할 때는 `==` 대신 오차 범위를 둡니다: `Math.Abs(a - b) < 1e-9`
- decimal은 double보다 느리므로, 대량 수치 계산에는 double을 씁니다.
