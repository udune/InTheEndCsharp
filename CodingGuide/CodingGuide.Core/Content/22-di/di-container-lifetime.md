---
id: di-container-lifetime
title: DI 컨테이너 만들기 (ServiceCollection, BuildServiceProvider)
category: 의존성 주입
order: 2202
summary: Microsoft.Extensions.DependencyInjection으로 서비스를 등록(AddTransient 등)하고 ServiceProvider를 만들어 객체 조립을 자동화하는 방법입니다.
keywords: DI 컨테이너, IoC 컨테이너, ServiceCollection, IServiceCollection, BuildServiceProvider, IServiceProvider, AddTransient, AddScoped, AddSingleton, 서비스 등록, Microsoft.Extensions.DependencyInjection
lesson: DI컨테이너생성및생명주기
related: di-service-locator, di-lifetime, di-manual, di-interface
---
## 핵심
```csharp
using Microsoft.Extensions.DependencyInjection;   // NuGet: Microsoft.Extensions.DependencyInjection

// 1) 등록부 만들기
IServiceCollection services = new ServiceCollection();

// 2) "이 타입이 필요하면 이렇게 만들어라"를 등록
services.AddTransient<Logger>();
services.AddTransient<MathService>();
services.AddTransient<ServiceA>();

// 3) 컨테이너(공급자) 생성
IServiceProvider provider = services.BuildServiceProvider();

// 4) 꺼내 쓰기 (다음 문서)
var serviceA = provider.GetRequiredService<ServiceA>();
```
컨테이너는 `ServiceA`의 생성자를 보고 `MathService`가 필요함을 알아내고, 다시 `MathService` 생성자를 보고 `Logger`를 만들어 **자동으로 조립**합니다.

## 등록 방법 3가지 (생명 주기)
| 메서드 | 객체를 만드는 시점 |
|---|---|
| `AddTransient` | 요청할 때마다 **새로** |
| `AddScoped` | **스코프(웹 요청 1건 등)마다 하나** |
| `AddSingleton` | 프로그램 전체에서 **딱 하나** |

## 주의할 점
- 등록하지 않은 타입을 요청하면 `InvalidOperationException: Unable to resolve service for type ...` 이 납니다. 생성자에 있는 모든 매개변수 타입이 등록되어 있어야 합니다.
- 생성자가 서로를 필요로 하면(A → B → A) 순환 의존 예외가 납니다.
