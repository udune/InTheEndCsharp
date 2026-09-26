---
id: var-declaration
title: 명시적 선언과 암시적 선언 (var)
category: 값 타입과 변수
order: 105
summary: 타입을 직접 쓰는 명시적 선언과 컴파일러가 타입을 추론하는 var의 차이, 언제 var를 쓰면 좋은지 설명합니다.
keywords: var, 변수 선언, 타입 추론, 암시적, 명시적, 변수, 초기화, 지역 변수, target-typed new
lesson: 명시적_암시적선언
related: int-types, generic-basics, anonymous-type
---
## 핵심
`var`는 **오른쪽 값을 보고 컴파일러가 타입을 정해 주는 것**입니다. 동적 타입이 아니므로, 한 번 정해진 타입은 바뀌지 않습니다.

```csharp
// 명시적 선언
int number;
string text = "hi";
double pi = 3.14;

// 암시적 선언 (var)
var number2 = 10;   // int
var text2 = "hi";   // string
var pi2 = 3.14;     // double

// 긴 타입 이름을 반복하지 않아도 됨
var dict = new Dictionary<string, List<Tuple<int, string>>>();

// C# 9 이상: 왼쪽에 타입을 쓰고 오른쪽을 new() 로 줄이는 방법
Dictionary<string, int> scores = new();
```

## var를 쓸 수 없는 경우
- 초기값이 없을 때: `var x;` → 컴파일 오류
- `null`로 초기화할 때: `var x = null;` → 컴파일 오류
- 필드(클래스 멤버 변수)에는 쓸 수 없습니다. 지역 변수에서만 가능합니다.

## 언제 var를 쓰나요?
- 오른쪽만 봐도 타입이 명확할 때 (`new`, 캐스팅, 리터럴)
- LINQ 결과나 익명 타입처럼 타입 이름이 길거나 없을 때
- 반대로 `var result = Calculate();` 처럼 타입이 안 보이면 명시적으로 쓰는 편이 읽기 쉽습니다.
