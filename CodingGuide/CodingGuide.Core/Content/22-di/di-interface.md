---
id: di-interface
title: DI에 인터페이스 적용하기 (AddTransient<ILogger, Logger>)
category: 의존성 주입
order: 2204
summary: 구체 클래스 대신 인터페이스에 의존하도록 만들고 컨테이너에 "인터페이스 → 구현" 쌍으로 등록해, 구현을 쉽게 교체할 수 있게 하는 방법입니다.
keywords: 인터페이스 주입, 인터페이스 등록, AddTransient<IService, Service>, 추상화에 의존, 의존성 역전, DIP, SOLID, 구현 교체, 목 객체, Mock, 테스트 대역
lesson: DI인터페이스적용
source: 의존성주입/Logging/ILogger.cs, 의존성주입/Services/IMathService.cs, 의존성주입/Services/MathService.cs
related: interface-basics, di-refactoring, di-register-factory, test-importance
---
## 핵심
```csharp
public interface ILogger { void Log(string message); }
public class Logger : ILogger { public void Log(string m) => Console.WriteLine($"[LOG] {m}"); }

public interface IMathService { int Add(int a, int b); }
public class MathService(ILogger logger) : IMathService
{
    public int Add(int a, int b) => a + b;
}

// "ILogger 가 필요하면 Logger 를 만들어 줘"
services.AddTransient<ILogger, Logger>();
services.AddTransient<IMathService, MathService>();
services.AddTransient<Main>();

class Main(IMathService mathService, ILogger logger)   // 구체 클래스를 모름
{
    public int Add(int a, int b)
    {
        logger.Log("Add 함수 호출");
        return mathService.Add(a, b);
    }
}
```

## 무엇이 좋아졌나?
구현을 바꿀 때 **등록 한 줄만** 바꾸면 됩니다.
```csharp
services.AddTransient<ILogger, FileLogger>();   // Main, MathService 코드는 그대로
```

## 테스트에서
```csharp
class FakeLogger : ILogger
{
    public List<string> Messages { get; } = new();
    public void Log(string m) => Messages.Add(m);
}

var main = new Main(new MathService(new FakeLogger()), new FakeLogger());
Assert.Equal(3, main.Add(1, 2));
```

## 같은 인터페이스를 여러 번 등록하면
마지막 등록이 이깁니다. 모두 받고 싶으면 `IEnumerable<ILogger>`를 생성자에 요청합니다.
