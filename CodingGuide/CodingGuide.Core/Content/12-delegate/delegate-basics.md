---
id: delegate-basics
title: 델리게이트 정의 - 메서드를 변수에 담기
category: 델리게이트와 이벤트
order: 1201
summary: 메서드를 가리키는 타입인 델리게이트(delegate)를 정의하고, 메서드를 변수에 담아 호출하는 기본 개념입니다.
keywords: 델리게이트, delegate, 대리자, 메서드 참조, 함수 포인터, 콜백, callback, 메서드를 변수에
lesson: 델리게이트_정의및매개변수가없는
related: delegate-params-return, delegate-as-parameter, delegate-func, lambda-expression
---
## 핵심
델리게이트는 **"이런 모양(매개변수/반환 타입)의 메서드를 담는 타입"** 입니다. 메서드를 값처럼 변수에 넣고, 나중에 호출할 수 있습니다.

```csharp
delegate void MyDelegate();          // 1) 모양 정의: 매개변수 없음, 반환 없음

void Hello() => Console.WriteLine("안녕하세요");

MyDelegate d = Hello;                // 2) 메서드를 담기 (괄호 없이 이름만!)
d();                                 // 3) 호출
d.Invoke();                          // 같은 의미
```

## 왜 필요한가요?
"무엇을 할지"를 **나중에, 밖에서** 정할 수 있게 해줍니다.
- 버튼 클릭 시 실행할 코드 (이벤트)
- 정렬 기준, 필터 조건 (LINQ의 `Where(x => ...)`)
- 작업이 끝나면 호출할 코드 (콜백)

## 주의할 점
- `d = Hello;` (메서드 담기) vs `d = Hello();` (메서드 **실행 결과**를 담기 → 오류)
- 요즘은 직접 `delegate`를 선언하기보다 **`Action`, `Func`** 을 주로 씁니다.
