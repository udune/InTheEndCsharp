---
id: int-types
title: 정수형 타입 (byte, short, int, long)
category: 값 타입과 변수
order: 101
summary: 정수를 저장하는 타입들과 각 타입의 범위, 어떤 타입을 골라야 하는지 정리합니다.
keywords: 정수, 정수형, int, long, byte, sbyte, short, ushort, uint, ulong, 범위, 오버플로, overflow, MaxValue, MinValue
lesson: 정수형타입
related: float-types, var-declaration, arithmetic-operators, recipe-parse-number
---
## 핵심
정수형 타입은 **크기(바이트 수)** 와 **부호 유무**로 나뉩니다. 특별한 이유가 없으면 `int`를 쓰고, 21억을 넘을 수 있으면 `long`을 씁니다.

| 타입 | 크기 | 범위 |
|---|---|---|
| byte | 1바이트 | 0 ~ 255 |
| sbyte | 1바이트 | -128 ~ 127 |
| short | 2바이트 | -32,768 ~ 32,767 |
| int | 4바이트 | 약 -21억 ~ 21억 |
| long | 8바이트 | 약 ±922경 |
| uint / ulong | 4 / 8바이트 | 0 이상만 (부호 없음) |

```csharp
byte age = 255;          // 0~255
sbyte temperature = -128; // -128~127
int count = 2_000_000;   // 숫자 사이에 _ 를 넣어 읽기 쉽게 쓸 수 있음
long big = 10_000_000_000L;

Console.WriteLine(int.MaxValue); // 2147483647
```

## 주의할 점
- 범위를 넘으면 **오류 없이 값이 뒤집힙니다**(오버플로). `int.MaxValue + 1`은 `int.MinValue`가 됩니다.
- 오버플로를 예외로 잡고 싶다면 `checked { ... }` 블록을 씁니다.

```csharp
int x = int.MaxValue;
int y = unchecked(x + 1); // -2147483648
checked { int z = x + 1; } // OverflowException 발생
```
- 정수끼리 나누면 소수점이 버려집니다. `10 / 20` 은 `0` 입니다. (→ 산술 연산자)
