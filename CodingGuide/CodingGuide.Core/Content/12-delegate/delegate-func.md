---
id: delegate-func
title: Func 델리게이트 - 반환값이 있는 메서드
category: 델리게이트와 이벤트
order: 1206
summary: 미리 정의된 제네릭 델리게이트 Func<..., TResult>로 반환값이 있는 메서드를 담는 방법입니다.
keywords: Func, Func<T, TResult>, 반환값 있는 델리게이트, 내장 델리게이트, 마지막 타입이 반환 타입
lesson: 델리게이트_Func
related: delegate-action, delegate-predicate, lambda-expression, delegate-as-parameter
---
## 핵심
`Func<입력1, 입력2, ..., 반환>` — **마지막 타입 인자가 반환 타입**입니다. 델리게이트를 따로 선언할 필요가 없습니다.

```csharp
void ApplyOperation(int a, int b, Func<int, int, int> operation)
{
    Console.WriteLine(operation(a, b));
}

int Plus(int a, int b) => a + b;
ApplyOperation(5, 10, Plus);             // 15
ApplyOperation(5, 10, (a, b) => a - b);  // -5
```

## 모양별 예
```csharp
Func<int> getNumber = () => 42;                  // 입력 없음, int 반환
Func<string, int> length = s => s.Length;        // string → int
Func<int, int, bool> isGreater = (a, b) => a > b;
Func<Task<string>> loadAsync = async () => await File.ReadAllTextAsync("a.txt");
```

## Action / Func / Predicate 한눈에
| 델리게이트 | 반환 | 예 |
|---|---|---|
| `Action<T...>` | 없음(void) | `Action<string> log = Console.WriteLine;` |
| `Func<T..., TResult>` | TResult | `Func<int, int> square = x => x * x;` |
| `Predicate<T>` | bool | `Predicate<int> isPositive = x => x > 0;` |
