---
id: delegate-as-parameter
title: 델리게이트를 메서드 매개변수로 넘기기 (콜백)
category: 델리게이트와 이벤트
order: 1205
summary: 메서드에 "어떤 계산을 할지"를 델리게이트로 넘겨, 동작을 바꿔 끼울 수 있게 하는 방법입니다.
keywords: 함수를 매개변수로, 메서드를 인자로, 콜백, callback, 전략, 고차 함수, higher-order function, 동작 주입
lesson: 델리게이트_함수매개변수
related: delegate-func, lambda-expression, linq-method-where
---
## 핵심
```csharp
delegate int Operation(int a, int b);

void ApplyOperation(int a, int b, Operation operation)
{
    int result = operation(a, b);     // 무엇을 할지는 호출한 쪽이 결정
    Console.WriteLine(result);
}

int Plus(int a, int b) => a + b;
int Minus(int a, int b) => a - b;

ApplyOperation(5, 10, Plus);            // 15
ApplyOperation(5, 10, Minus);           // -5
ApplyOperation(5, 10, (a, b) => a * b); // 50 (람다로 즉석에서)
```

## 이 패턴이 쓰이는 곳
- LINQ: `list.Where(x => x > 0)` — "어떤 조건으로 거를지"를 넘김
- 정렬: `list.Sort((a, b) => a.Age.CompareTo(b.Age))` — "어떤 기준으로 정렬할지"
- 재시도, 시간 측정 같은 공통 로직으로 감싸기
```csharp
T Measure<T>(Func<T> work)
{
    var sw = System.Diagnostics.Stopwatch.StartNew();
    T result = work();
    Console.WriteLine($"{sw.ElapsedMilliseconds}ms");
    return result;
}
var data = Measure(() => LoadData());
```
