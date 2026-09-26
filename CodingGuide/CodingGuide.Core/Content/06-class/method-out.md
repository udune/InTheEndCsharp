---
id: method-out
title: out 키워드와 TryParse 패턴
category: 클래스
order: 611
summary: 메서드가 반환값 외에 추가 결과를 돌려주는 out 매개변수와, 실패해도 예외가 없는 TryParse 패턴을 설명합니다.
keywords: out, TryParse, 변환 실패, 문자열을 숫자로, 여러 값 반환, 출력 매개변수, TryGetValue, out var
lesson: 메서드_out키워드
related: method-ref, recipe-parse-number, tuples, dictionary
---
## 핵심
- `out` 매개변수는 호출 전에 초기화할 필요가 없고, 메서드는 **반드시 값을 대입**해야 합니다.
- 가장 많이 보는 곳은 `TryParse` 입니다.

```csharp
string text = "123";
if (int.TryParse(text, out int result))   // 선언과 동시에 out 변수 생성
    Console.WriteLine(result + 1);        // 124
else
    Console.WriteLine("숫자가 아님");     // 실패 시 result 는 0
```

## Try 패턴 (예외 대신 bool 반환)
```csharp
double.TryParse("3.14", out var d);
DateTime.TryParse("2026-01-01", out var date);
dict.TryGetValue("key", out var value);     // Dictionary 조회
Enum.TryParse<Days>("Monday", out var day);
```

## 직접 만들기
```csharp
bool TryDivide(int a, int b, out int quotient)
{
    if (b == 0) { quotient = 0; return false; }
    quotient = a / b;
    return true;
}

if (TryDivide(10, 3, out var q)) Console.WriteLine(q); // 3
```

## 값이 필요 없으면 _ (discard)
```csharp
bool isNumber = int.TryParse(input, out _);
```

## 여러 값을 돌려줄 때 대안
out 대신 튜플 반환이 더 깔끔할 때가 많습니다: `(int Min, int Max) GetRange() => (1, 10);`
