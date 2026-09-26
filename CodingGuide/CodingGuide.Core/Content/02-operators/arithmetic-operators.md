---
id: arithmetic-operators
title: 산술 연산자와 증감 연산자 (+ - * / % ++ --)
category: 연산자
order: 202
summary: 사칙연산, 나머지 연산, 정수 나눗셈의 함정, 전위/후위 증감 연산자의 차이를 설명합니다.
keywords: 더하기, 빼기, 곱하기, 나누기, 나머지, 몫, %, 증가, 감소, ++, --, 전위, 후위, 정수 나눗셈, 짝수, 홀수
lesson: 산술연산자
related: assignment-operators, int-types, float-types
---
## 핵심
```csharp
int a = 10, b = 20;
Console.WriteLine(a + b); // 30
Console.WriteLine(a - b); // -10
Console.WriteLine(a * b); // 200
Console.WriteLine(a / b); // 0   ← 정수 ÷ 정수 = 정수 (소수점 버림)
Console.WriteLine(a % b); // 10  ← 나머지
```

## 정수 나눗셈의 함정
둘 중 하나라도 실수여야 소수점까지 계산됩니다.
```csharp
Console.WriteLine(10 / 20);          // 0
Console.WriteLine(10 / 20.0);        // 0.5
Console.WriteLine((double)10 / 20);  // 0.5
```

## 전위(++x)와 후위(x++)
```csharp
decimal c = 10;
Console.WriteLine(c++); // 10 을 먼저 쓰고, 그 다음 11 로 증가
Console.WriteLine(c);   // 11
Console.WriteLine(++c); // 먼저 12 로 증가시키고 12 를 씀
```

## 자주 쓰는 패턴
```csharp
bool isEven = n % 2 == 0;       // 짝수 판별
int lastDigit = n % 10;         // 일의 자리
int next = (i + 1) % length;    // 순환 인덱스 (마지막 다음은 처음)
```

## 주의할 점
- 정수를 0으로 나누면 `DivideByZeroException`, 실수를 0으로 나누면 `Infinity`가 됩니다.
