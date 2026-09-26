---
id: if-ternary
title: if 문과 삼항 연산자
category: 조건문
order: 301
summary: 조건에 따라 코드를 나누는 if / else if / else와 한 줄 조건식인 삼항 연산자(? :)를 설명합니다.
keywords: if, else, else if, 조건, 분기, 삼항, 삼항 연산자, ? :, 짝수 홀수, 조건문, 조기 반환
lesson: if_삼항연산자
related: switch-statement, comparison-operators, logical-operators, pattern-matching
---
## 핵심
```csharp
int a = 1, b = 2;

if (a == b)
{
    Console.WriteLine("같다");
}
else if (a > b)
{
    Console.WriteLine("a가 크다");
}
else
{
    Console.WriteLine("b가 크다");
}
```

## 삼항 연산자
`조건 ? 참일 때 값 : 거짓일 때 값` — 둘 중 하나의 값을 고를 때 씁니다.
```csharp
int number = 7;
string kind = number % 2 == 0 ? "짝수" : "홀수";
```

## 사용자 입력과 함께 쓰기
```csharp
string? input = Console.ReadLine();
if (int.TryParse(input, out int n))
    Console.WriteLine(n % 2 == 0 ? "짝수" : "홀수");
else
    Console.WriteLine("숫자가 아닙니다");
```
> `int.Parse`는 숫자가 아니면 예외를 던지므로, 사용자 입력에는 `int.TryParse`가 안전합니다.

## 팁
- 중괄호 `{ }`는 한 줄이어도 쓰는 습관이 버그를 줄입니다.
- 조건이 깊게 중첩되면 **조기 반환(early return)** 으로 평평하게 만드세요.
```csharp
if (user == null) return;
if (!user.IsActive) return;
// 본 로직
```
