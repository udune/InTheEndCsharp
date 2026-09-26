---
id: pattern-matching
title: 패턴 매칭 총정리 (is, switch, 관계/논리/속성/리스트 패턴)
category: C# 문법 보강
order: 2501
summary: is와 switch에서 쓰는 타입 패턴, 상수 패턴, 관계 패턴(>=), 논리 패턴(and/or/not), 속성 패턴({ Age: > 20 }), 리스트 패턴([1, ..])을 한곳에 정리합니다.
keywords: 패턴 매칭, pattern matching, 타입 패턴, 관계 패턴, 논리 패턴, and, or, not, 속성 패턴, property pattern, 리스트 패턴, list pattern, 위치 패턴, is not null, switch 식, 범위 검사
related: is-operator, switch-statement, record-type, logical-operators
---
## 타입 패턴
```csharp
if (obj is string s) Console.WriteLine(s.Length);
if (obj is not null) { }
```

## 관계 + 논리 패턴
```csharp
string Grade(int score) => score switch
{
    >= 90 => "A",
    >= 80 and < 90 => "B",
    < 0 or > 100 => "잘못된 점수",
    _ => "C"
};

bool IsVowel(char c) => c is 'a' or 'e' or 'i' or 'o' or 'u';
bool IsDigit(char c) => c is >= '0' and <= '9';
```

## 속성 패턴 - 객체의 속성 값으로 검사
```csharp
if (user is { Age: >= 20, IsActive: true })
    Console.WriteLine("활성 성인 사용자");

decimal Discount(Order o) => o switch
{
    { Total: > 100_000, Customer.IsVip: true } => 0.2m,   // 중첩 속성 (C# 10+)
    { Total: > 100_000 } => 0.1m,
    _ => 0m
};

if (text is { Length: > 0 }) { }   // null 도 아니고 빈 문자열도 아님
```

## 위치 패턴 - 튜플/record 분해
```csharp
string Quadrant((int X, int Y) p) => p switch
{
    (0, 0) => "원점",
    ( > 0, > 0) => "1사분면",
    ( < 0, > 0) => "2사분면",
    _ => "기타"
};
```

## 리스트 패턴 (C# 11+)
```csharp
int[] arr = [1, 2, 3, 4];
if (arr is [1, ..]) { }                // 1 로 시작
if (arr is [.., 4]) { }                // 4 로 끝남
if (arr is [var first, _, ..]) { }     // 첫 요소 꺼내기, 최소 2개
if (args is []) Console.WriteLine("인자 없음");
```

## 장점
- `if-else` 사슬보다 짧고, 빠뜨린 경우를 컴파일러가 경고(CS8509)해 줍니다.
- null 검사와 타입 변환이 한 번에 됩니다.
