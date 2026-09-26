---
id: comparison-operators
title: 비교 연산자 (==, !=, <, >)
category: 연산자
order: 201
summary: 두 값을 비교해 bool을 돌려주는 연산자들과, 문자열/객체 비교 시 주의할 점입니다.
keywords: 비교, 같다, 다르다, 크다, 작다, ==, !=, <, >, <=, >=, 동등, Equals, 문자열 비교
lesson: 비교연산자
related: logical-operators, if-ternary, string-basics, struct-vs-class
---
## 핵심
비교 연산자의 결과는 항상 `bool`입니다.

```csharp
Console.WriteLine(1 == 1); // True
Console.WriteLine(1 != 2); // True
Console.WriteLine(3 >= 5); // False

string a = "안녕";
string b = "안녕";
Console.WriteLine(a == b); // True - string 은 내용(값)을 비교
```

## 주의할 점
- `=` 는 대입, `==` 는 비교입니다.
- **string의 `==`는 내용을 비교**합니다(Java와 달리 안전).
- 일반 **클래스 객체의 `==`는 같은 인스턴스인지(참조)** 를 비교합니다. 내용 비교가 필요하면 `Equals`를 재정의하거나 `record`를 씁니다.
- 대소문자 무시 비교: `string.Equals(a, b, StringComparison.OrdinalIgnoreCase)`
- double 비교는 오차 때문에 `==` 대신 `Math.Abs(a - b) < 1e-9` 방식을 씁니다.
