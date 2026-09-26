---
id: delegate-params-return
title: 매개변수와 반환값이 있는 델리게이트
category: 델리게이트와 이벤트
order: 1202
summary: 매개변수와 반환 타입을 가진 델리게이트를 정의하고, 모양(시그니처)이 같은 메서드만 담을 수 있다는 규칙을 설명합니다.
keywords: 델리게이트 매개변수, 델리게이트 반환값, 시그니처, signature, 메서드 모양, delegate int, CS0123
lesson: 델리게이트_매개변수가있는_반환값이있는
related: delegate-basics, delegate-as-parameter, delegate-func
---
## 핵심
```csharp
delegate void Operation(int a, int b);     // int 2개 받고 반환 없음
delegate int OperationRet(int a, int b);   // int 2개 받고 int 반환

void Print(int a, int b) => Console.WriteLine(a + b);
int Plus(int a, int b) => a + b;

Operation op = Print;
OperationRet opRet = Plus;

op(1, 3);                    // 4 출력
int result = opRet(1, 3);    // 4
```

## 시그니처가 맞아야 합니다
델리게이트에 담을 메서드는 **매개변수 타입/개수, 반환 타입**이 같아야 합니다. 이름은 상관없습니다.
```csharp
// OperationRet x = Print;   // 오류 CS0123: 반환 타입이 다름 (void vs int)
```

## Func로 바꿔 쓰면
```csharp
Action<int, int> op2 = Print;       // delegate void Operation(int, int) 와 같은 모양
Func<int, int, int> opRet2 = Plus;  // delegate int OperationRet(int, int) 와 같은 모양
```
