---
id: di-manual
title: 의존성 주입(DI)이란? - 컨테이너 없이 수동으로 주입하기
category: 의존성 주입
order: 2201
summary: 클래스가 필요한 객체(의존성)를 직접 new 하지 않고 생성자로 전달받는 의존성 주입의 개념을 컨테이너 없이 수동으로 구현해 봅니다.
keywords: 의존성 주입, DI, dependency injection, 의존성, 생성자 주입, 수동 주입, 결합도, 느슨한 결합, 제어의 역전, IoC, new 를 쓰지 않기, 테스트하기 쉬운 코드
lesson: DI사용하지않는수동주입
source: 의존성주입/Logging/Logger.cs
related: di-container-lifetime, di-interface, di-service-locator, interface-basics
---
## 의존성이란?
`ServiceA`가 일하려면 `MathService`가 필요하고, `MathService`는 `Logger`가 필요합니다. 이때 "ServiceA는 MathService에 **의존**한다"고 합니다.

## 나쁜 예: 안에서 직접 new
```csharp
class MathService
{
    private readonly Logger logger = new Logger();   // 어떤 로거를 쓸지 스스로 결정 → 바꾸기 어려움
}
```

## 의존성 주입: 밖에서 넣어준다
```csharp
class MathService
{
    private readonly Logger logger;
    public MathService(Logger logger)   // 필요한 것을 생성자로 "받는다"
    {
        this.logger = logger;
    }
    public int Add(int a, int b)
    {
        logger.Log("Add 함수 호출");
        return a + b;
    }
}

class ServiceA(MathService mathService)
{
    public int Add(int a, int b) => mathService.Add(a, b);
}

// 조립은 프로그램 시작점에서 한 번에
Logger logger = new Logger();
MathService mathService = new MathService(logger);
ServiceA serviceA = new ServiceA(mathService);
serviceA.Add(1, 2);
```

## 장점
- **교체 가능**: 콘솔 로거 → 파일 로거로 바꿔도 MathService는 수정할 필요 없음 (인터페이스와 함께 쓸 때)
- **테스트 쉬움**: 테스트에서는 가짜(Fake) 객체를 넣을 수 있음
- **의존 관계가 생성자에 드러남**: 무엇이 필요한지 한눈에 보임

## 문제점 → DI 컨테이너
클래스가 많아지면 `new A(new B(new C(...)))` 조립 코드가 길어집니다. 이 조립을 자동으로 해주는 것이 **DI 컨테이너**입니다.
