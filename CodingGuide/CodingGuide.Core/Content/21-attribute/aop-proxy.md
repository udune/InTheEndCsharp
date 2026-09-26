---
id: aop-proxy
title: AOP 구현 - Castle DynamicProxy로 로깅/시간 측정 인터셉터
category: 어트리뷰트
order: 2108
summary: 원래 코드를 고치지 않고 메서드 호출 앞뒤에 로깅, 실행 시간 측정 같은 공통 기능을 끼워 넣는 AOP(관점 지향 프로그래밍)를 Castle.Core의 프록시와 인터셉터로 구현합니다.
keywords: AOP, 관점 지향 프로그래밍, aspect oriented programming, 인터셉터, interceptor, 프록시, proxy, DynamicProxy, Castle.Core, 횡단 관심사, 로깅, 실행 시간 측정, Stopwatch, virtual, 데코레이터
lesson: AOP_AspectOrientedProgramming구현
source: 어트리뷰트/Extensions/ProxyExtensions.cs, 어트리뷰트/Interceptors/LoggingInterceptor.cs, 어트리뷰트/Interceptors/TimingInterceptor.cs
related: attribute-parameter-aop, di-interface, recipe-stopwatch
---
## 문제
모든 서비스 메서드에 "시작/끝 로그"와 "걸린 시간"을 넣고 싶습니다. 메서드마다 직접 넣으면 코드가 중복되고 본래 로직이 흐려집니다. 이런 공통 기능을 **횡단 관심사**라고 합니다.

## 해결: 프록시가 대신 받아서 앞뒤로 처리
```
호출 → [프록시] → LoggingInterceptor → TimingInterceptor → 실제 MyService.Add
```

### 1) 인터셉터
```csharp
using Castle.DynamicProxy;

public class LoggingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine($"{invocation.TargetType.Name}.{invocation.Method.Name} Started");
        invocation.Proceed();          // 다음 인터셉터 또는 실제 메서드 실행
        Console.WriteLine($"{invocation.TargetType.Name}.{invocation.Method.Name} Ended");
    }
}

public class TimingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var sw = Stopwatch.StartNew();
        invocation.Proceed();
        Console.WriteLine($"{invocation.Method.Name}: {sw.ElapsedMilliseconds}ms");
    }
}
```

### 2) 프록시 생성 확장 메서드
```csharp
public static T CreateProxy<T>(this T target, params IInterceptor[] interceptors) where T : class
    => new ProxyGenerator().CreateClassProxyWithTarget(target, interceptors);
```

### 3) 사용
```csharp
public class MyService
{
    public virtual int Add(int a, int b) => a + b;   // virtual 이어야 가로챌 수 있음!
}

var service = new MyService().CreateProxy(new LoggingInterceptor(), new TimingInterceptor());
int result = service.Add(1, 2);   // 로그와 시간이 자동으로 찍힘
```

## 주의할 점
- 클래스 프록시는 **virtual 메서드만** 가로챕니다. 인터페이스 프록시(`CreateInterfaceProxyWithTarget`)를 쓰면 이 제약이 없습니다.
- 필요 패키지: `Castle.Core` (NuGet)
- `invocation.ReturnValue`로 반환값을 읽거나 바꿀 수 있습니다.
- async 메서드는 `Proceed()` 후 Task가 끝나기 전에 인터셉터가 끝나므로 시간 측정이 부정확합니다(별도 처리 필요).
