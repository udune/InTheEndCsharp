---
id: delegate-multicast
title: 델리게이트 멀티캐스트 (+=, -=)
category: 델리게이트와 이벤트
order: 1203
summary: 하나의 델리게이트에 여러 메서드를 +=로 연결해 한 번에 호출하는 멀티캐스트와, 반환값이 마지막 메서드의 값이 되는 점을 설명합니다.
keywords: 멀티캐스트, multicast, +=, -=, 여러 메서드 연결, 체인, 구독, 해제, 호출 목록
lesson: 델리게이트_멀티캐스팅
related: delegate-event, delegate-basics, assignment-operators
---
## 핵심
```csharp
delegate int Operation(int a, int b);

int Plus(int a, int b)  { Console.WriteLine($"{a} + {b} = {a + b}"); return a + b; }
int Minus(int a, int b) { Console.WriteLine($"{a} - {b} = {a - b}"); return a - b; }

Operation op = Plus;
op += Minus;              // 메서드 추가

int result = op(1, 3);    // Plus → Minus 순서로 둘 다 실행
Console.WriteLine(result); // -2 ← 반환값은 "마지막" 메서드의 것

op -= Plus;               // 메서드 제거
```

## 주의할 점
- 반환값이 있는 델리게이트를 멀티캐스트하면 **앞선 메서드들의 반환값은 버려집니다.** 그래서 멀티캐스트는 보통 `void`(Action, 이벤트)에 씁니다.
- 중간 메서드에서 예외가 나면 **뒤의 메서드는 실행되지 않습니다.**
- 모두 제거되면 델리게이트는 `null`이 됩니다. 호출 전에 `op?.Invoke(...)`로 확인하세요.
