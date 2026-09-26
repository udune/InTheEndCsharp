---
id: di-refactoring
title: DI 구성 코드 정리하기 - Container 클래스와 등록 확장 메서드
category: 의존성 주입
order: 2205
summary: 서비스 등록 코드를 Container 클래스(ConfigureServices)나 IServiceCollection 확장 메서드로 모아 Program을 깔끔하게 만드는 리팩토링입니다.
keywords: DI 리팩토링, 컨테이너 클래스, ConfigureServices, 서비스 등록 정리, 확장 메서드 등록, AddMyServices, 컴포지션 루트, composition root, StartUp
lesson: DI리팩토링
source: 의존성주입/Container.cs, 의존성주입/Main.cs
related: di-service-locator, extension-methods, di-lifetime, di-register-factory
---
## 핵심
등록 코드를 한 곳(**컴포지션 루트**)에 모읍니다.
```csharp
public class Container
{
    public IServiceProvider Services { get; }

    public Container()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        Services = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ILogger, Logger>();
        services.AddTransient<IMathService, MathService>();
        services.AddTransient<Main>();
    }

    public Main StartUp() => Services.GetRequiredService<Main>();
}

// 사용하는 쪽은 단 두 줄
var main = new Container().StartUp();
main.Add(1, 2);
```

## 확장 메서드로 묶기 (ASP.NET Core 스타일)
```csharp
public static class ServiceRegistration
{
    public static IServiceCollection AddMathFeature(this IServiceCollection services)
    {
        services.AddTransient<IMathService, MathService>();
        services.AddTransient<ICounterService, CounterService>();
        return services;   // 체이닝 가능하도록 반환
    }
}

services.AddMathFeature()
        .AddSingleton<ILogger, Logger>();
```
이 저장소의 `services.AddAppSettings()`도 같은 방식입니다.

## 실무 참고: Generic Host
콘솔/WPF 앱에서도 `Microsoft.Extensions.Hosting`을 쓰면 DI, 설정(appsettings.json), 로깅이 한 번에 구성됩니다.
```csharp
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTransient<IMathService, MathService>();
using var host = builder.Build();
var main = host.Services.GetRequiredService<Main>();
```
