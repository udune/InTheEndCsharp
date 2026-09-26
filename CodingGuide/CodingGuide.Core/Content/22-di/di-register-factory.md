---
id: di-register-factory
title: 팩토리 등록에서 다른 서비스 꺼내 쓰기 (provider.GetRequiredService)
category: 의존성 주입
order: 2208
summary: 생성자에 일반 값(초기 카운트 0)과 다른 서비스(IMathService)가 함께 필요할 때, 등록 람다의 provider에서 서비스를 꺼내 직접 조립하는 방법입니다.
keywords: 팩토리 등록, IServiceProvider, provider, GetRequiredService, 혼합 매개변수, 값과 서비스 함께, ActivatorUtilities, CounterService, 초기값 주입
lesson: DI서비스구현체직접등록하기_Provider사용
source: 의존성주입/Services/CounterService.cs, 의존성주입/Container.cs
related: di-register-instance, di-service-locator, di-lifetime
---
## 문제
```csharp
public class CounterService(int defaultCount, IMathService mathService) : ICounterService
{
    private int count = defaultCount;
    public void Increase()
    {
        count = mathService.Increase(count);
        Console.WriteLine($"CounterService.Increase Result: {count}");
    }
}
```
컨테이너는 `int defaultCount`에 무엇을 넣어야 할지 모릅니다.

## 해결: 등록할 때 직접 조립
```csharp
services.AddTransient<ICounterService>(provider =>
{
    var mathService = provider.GetRequiredService<IMathService>();   // 서비스는 컨테이너에서
    return new CounterService(0, mathService);                        // 값은 직접
});
```

## 더 간단한 방법: ActivatorUtilities
서비스 매개변수는 자동으로 채우고, 나머지 값만 넘깁니다.
```csharp
services.AddTransient<ICounterService>(sp =>
    ActivatorUtilities.CreateInstance<CounterService>(sp, 0));
```

## 주의할 점
- 등록 람다 안의 `provider.GetRequiredService`는 괜찮은 사용입니다(컴포지션 루트 안이므로).
- Singleton 팩토리 안에서 Scoped 서비스를 꺼내면 캡티브 의존성 문제가 생깁니다.
