---
id: custom-exception-benefit
title: 사용자 정의 예외의 장점 - 깊은 호출 단계를 한 번에 빠져나오기
category: 예외 처리
order: 1605
summary: 여러 단계로 중첩된 메서드 호출에서 오류 정보를 out 매개변수로 일일이 전달하는 대신, 예외로 한 번에 전달하는 장점을 잔액 부족 예제로 설명합니다.
keywords: 사용자 정의 예외 장점, 예외 전파, 호출 스택, 콜 스택, 오류 전달, out 대신 예외, 잔액 부족, 도메인 예외, 비즈니스 예외
lesson: 사용자정의예외처리의장점
related: custom-exception, method-out, exception-try-catch
---
## 핵심
`출금 → 잔액확인1 → 잔액확인2 → 잔액확인3` 처럼 깊이 호출된 곳에서 문제가 생겼을 때:

**out으로 전달하면** 모든 중간 메서드가 오류 정보를 받아서 넘겨줘야 합니다.
```csharp
void 출금(out 잔액부족 부족) { 잔액확인1(out 부족); }
void 잔액확인1(out 잔액부족 부족) { 잔액확인2(out 부족); }
// ... 모든 단계에 out 이 필요
```

**예외로 던지면** 중간 메서드는 아무것도 몰라도 되고, 처리할 곳에서 한 번만 catch 합니다.
```csharp
class 잔액부족Exception(string message, decimal 잔액, decimal 출금액) : Exception(message)
{
    public decimal 잔액 { get; } = 잔액;
    public decimal 출금액 { get; } = 출금액;
}

void 출금() => 잔액확인1();
void 잔액확인1() => 잔액확인2();
void 잔액확인2() => 잔액확인3();
void 잔액확인3() => throw new 잔액부족Exception("잔액이 부족합니다.", 500, 1000);

try
{
    출금();
}
catch (잔액부족Exception e)
{
    Console.WriteLine($"{e.Message} 잔액: {e.잔액}, 출금액: {e.출금액}");
}
```

## 장점 정리
- 중간 코드가 오류 전달 코드로 더러워지지 않습니다.
- 오류 **종류별로** catch를 나눌 수 있습니다.
- 필요한 정보(잔액, 출금액)를 **타입 있는 속성**으로 전달합니다.

## 균형 잡기
예외는 "예외적인" 상황용입니다. 자주 일어나는 정상적인 실패(입력값 검증 등)는 `bool Try...()`나 결과 객체로 처리하는 것이 성능과 가독성 모두에 좋습니다.
