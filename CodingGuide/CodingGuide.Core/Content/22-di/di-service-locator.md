---
id: di-service-locator
title: 서비스 꺼내기 - GetRequiredService, 생성자 주입 vs 서비스 로케이터
category: 의존성 주입
order: 2203
summary: 컨테이너에서 GetRequiredService로 서비스를 꺼내는 방법과, 생성자 주입이 권장되고 서비스 로케이터 패턴은 최소한으로 써야 하는 이유입니다.
keywords: GetRequiredService, GetService, 서비스 로케이터, service locator, 생성자 주입, constructor injection, 서비스 가져오기, 진입점, 안티패턴, Unable to resolve service
lesson: DI적용_서비스로케이터패턴_생성자주입
related: di-container-lifetime, di-interface, di-refactoring
---
## 핵심
```csharp
IServiceCollection services = new ServiceCollection();
services.AddTransient<Logger>();
services.AddTransient<MathService>();
services.AddTransient<ServiceA>();
IServiceProvider provider = services.BuildServiceProvider();

ServiceA serviceA = provider.GetRequiredService<ServiceA>();   // 없으면 예외
serviceA.Add(1, 2);

class ServiceA(MathService mathService, Logger logger)   // 생성자 주입: 컨테이너가 채워 줌
{
    public int Add(int a, int b)
    {
        logger.Log("Add 함수 호출");
        return mathService.Add(a, b);
    }
}
```

## GetService vs GetRequiredService
- `GetService<T>()`: 등록 안 됐으면 **null**
- `GetRequiredService<T>()`: 등록 안 됐으면 **예외** (문제를 빨리 발견 → 권장)

## 생성자 주입 vs 서비스 로케이터
```csharp
// 생성자 주입 (권장): 필요한 것이 생성자에 드러남
class OrderService(IRepository repo, ILogger logger) { }

// 서비스 로케이터 (지양): 클래스 안에서 컨테이너에 직접 요청
class OrderService(IServiceProvider sp)
{
    void Save() => sp.GetRequiredService<IRepository>().Save();   // 무엇이 필요한지 숨겨짐
}
```
- `GetRequiredService`는 **프로그램 시작점(Main, 컨테이너 구성 코드)** 에서 최상위 객체 하나를 꺼낼 때만 쓰는 것이 원칙입니다.
- 나머지 객체들은 모두 생성자 주입으로 연결되게 합니다.
