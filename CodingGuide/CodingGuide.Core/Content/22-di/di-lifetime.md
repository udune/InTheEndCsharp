---
id: di-lifetime
title: 서비스 생명 주기 비교 - Singleton, Scoped, Transient
category: 의존성 주입
order: 2206
summary: AddSingleton, AddScoped, AddTransient로 등록한 서비스가 언제 새로 만들어지는지 스코프 예제로 비교하고, 각각 어떤 서비스에 써야 하는지 정리합니다.
keywords: 생명 주기, 수명, lifetime, Singleton, Scoped, Transient, AddSingleton, AddScoped, AddTransient, CreateScope, 스코프, 싱글톤, 캡티브 의존성, captive dependency, DbContext
lesson: DIAddScoped생명주기및AddTransient_AddSingleton과비교
source: 의존성주입/Services/SingletonService.cs, 의존성주입/Services/ScopedClass.cs, 의존성주입/Services/TransientClass.cs, 의존성주입/Container.cs
related: di-container-lifetime, di-refactoring, static-members
---
## 실험
각 클래스는 생성자에서 "=== ○○ Class 생성 ===" 을 출력합니다. 스코프 2개에서 각각 두 번씩 요청하면:

```csharp
services.AddSingleton<ISingletonClass, SingletonClass>();
services.AddScoped<IScopedClass, ScopedClass>();
services.AddTransient<ITransientClass, TransientClass>();

using (var scope = provider.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<ISingletonClass>();  // 생성
    scope.ServiceProvider.GetRequiredService<IScopedClass>();     // 생성
    scope.ServiceProvider.GetRequiredService<ITransientClass>();  // 생성
    scope.ServiceProvider.GetRequiredService<ISingletonClass>();  // (재사용)
    scope.ServiceProvider.GetRequiredService<IScopedClass>();     // (재사용)
    scope.ServiceProvider.GetRequiredService<ITransientClass>();  // 생성
}
using (var scope = provider.CreateScope()) { /* 같은 요청 반복 */ }
```

| | 1번 스코프 | 2번 스코프 | 합계 |
|---|---|---|---|
| Singleton | 1번 생성 | 재사용 | **1** |
| Scoped | 1번 생성 | 1번 생성 | **2** |
| Transient | 2번 생성 | 2번 생성 | **4** |

## 언제 무엇을?
| 생명 주기 | 적합한 서비스 |
|---|---|
| **Singleton** | 상태가 없거나 공유해야 하는 것: 설정, 캐시, HttpClient 팩토리, 로거 |
| **Scoped** | 요청(작업 단위) 동안 유지할 것: DB 컨텍스트(`DbContext`), 작업 단위 |
| **Transient** | 가볍고 상태가 없는 것: 계산기, 변환기 |

## 주의할 점
- **Singleton이 Scoped/Transient를 주입받으면** 그 객체도 사실상 싱글톤처럼 영원히 붙잡힙니다(캡티브 의존성). ASP.NET Core 개발 모드에서는 예외로 알려줍니다.
- Singleton은 여러 스레드에서 동시에 쓰이므로 **스레드 안전**해야 합니다.
- 스코프 밖(루트 provider)에서 Scoped 서비스를 꺼내면 사실상 싱글톤처럼 동작합니다. 반드시 `CreateScope()` 안에서 쓰세요.
