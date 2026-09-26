---
id: is-operator
title: is 연산자와 타입 패턴 (obj is string s)
category: 타입 변환
order: 905
summary: 타입을 검사하면서 동시에 변환된 변수를 만드는 is 패턴 매칭을 설명합니다.
keywords: is, is 연산자, 타입 검사, 타입 패턴, 패턴 매칭, pattern matching, is not null, 타입 확인, 형 확인
lesson: 타입변환_is
related: as-operator, pattern-matching, switch-statement
---
## 핵심
```csharp
object obj = "C# Programming";

if (obj is string str)             // string 이면 str 변수에 담아 줌
    Console.WriteLine($"변환 성공: {str}");

if (obj is int n && n > 10) { }    // 조건과 함께
if (obj is not null) { }           // null 검사 (권장 스타일)
if (obj is null) { }
```

## 여러 타입 분기
```csharp
string Describe(object o)
{
    if (o is int i) return $"정수 {i}";
    if (o is string s) return $"문자열 {s}";
    return "기타";
}

// switch 식으로 더 간결하게
string Describe2(object o) => o switch
{
    int i => $"정수 {i}",
    string s => $"문자열 {s}",
    _ => "기타"
};
```
더 다양한 패턴은 "패턴 매칭" 문서를 보세요.
