---
id: lambda-expression
title: 람다 표현식 (=>)
category: 람다
order: 1301
summary: 이름 없는 메서드를 (매개변수) => 식 형태로 즉석에서 만드는 람다 표현식과 캡처(클로저) 개념을 설명합니다.
keywords: 람다, lambda, =>, 화살표 함수, 익명 함수, 익명 메서드, 식 람다, 문 람다, 클로저, closure, 캡처, 변수 캡처
lesson: 람다표현식
related: delegate-func, delegate-action, linq-method-where, delegate-as-parameter
---
## 핵심
```csharp
Func<int, int, int> add = (a, b) => a + b;              // 식 람다: 결과가 곧 반환값
Action<int, int> print = (a, b) => { Console.WriteLine(a + b); }; // 문 람다: { } 블록

Console.WriteLine(add(1, 2));   // 3
print(1, 2);                    // 3
```

## 문법 모음
```csharp
x => x * 2                  // 매개변수 1개: 괄호 생략 가능
(x, y) => x + y             // 2개 이상: 괄호 필수
() => Console.WriteLine()   // 매개변수 없음
(int x) => x * 2            // 타입 명시
async () => await Task.Delay(100)  // 비동기 람다
_ => true                   // 매개변수를 안 쓸 때
var square = (int x) => x * x;     // C# 10+: var 로 추론 가능
```

## 가장 많이 쓰는 곳: LINQ와 정렬
```csharp
var adults = people.Where(p => p.Age >= 20);
var names = people.Select(p => p.Name);
people.Sort((a, b) => a.Age.CompareTo(b.Age));
button.Click += (s, e) => MessageBox.Show("클릭");
```

## 클로저 - 바깥 변수 캡처
람다는 바깥의 지역 변수를 **값이 아니라 변수 자체**로 기억합니다.
```csharp
int count = 0;
Action inc = () => count++;
inc(); inc();
Console.WriteLine(count);   // 2

var actions = new List<Action>();
for (int i = 0; i < 3; i++)
{
    int copy = i;                           // 반복 변수를 복사해서 캡처하는 습관
    actions.Add(() => Console.WriteLine(copy));
}
```
