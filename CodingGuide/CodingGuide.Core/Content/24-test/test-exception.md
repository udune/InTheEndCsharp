---
id: test-exception
title: 예외와 가짜 객체 테스트 (Assert.Throws, Fake)
category: 테스트
order: 2403
summary: 예외가 발생해야 하는 상황을 Assert.Throws로 테스트하는 방법과, 의존성을 가짜(Fake) 객체로 바꿔 테스트하는 방법입니다.
keywords: Assert.Throws, 예외 테스트, 테스트 예외, 예외 검증, 예외 발생 테스트, 0으로 나누기 테스트, ThrowsAsync, 비동기 테스트, Fake, Mock, 목, 가짜 객체, 테스트 대역, 의존성 격리, Moq, NSubstitute
source: 테스트코드작성/Calculator.cs
related: xunit-basics, di-interface, custom-exception
---
## 예외가 나야 정상인 경우
```csharp
public double Divide(int a, int b)
{
    if (b == 0) throw new DivideByZeroException("0으로 나눌 수 없습니다.");
    return (double)a / b;
}

[Fact]
public void Divide_ByZero_Throws()
{
    var calc = new Calculator();
    var ex = Assert.Throws<DivideByZeroException>(() => calc.Divide(10, 0));
    Assert.Equal("0으로 나눌 수 없습니다.", ex.Message);
}
```

## 비동기 메서드 테스트
```csharp
[Fact]
public async Task LoadAsync_ReturnsData()
{
    var result = await service.LoadAsync();
    Assert.NotEmpty(result);
}

await Assert.ThrowsAsync<HttpRequestException>(() => client.GetAsync("bad"));
```

## 가짜 객체(Fake)로 의존성 끊기
DB, 파일, 네트워크를 쓰는 서비스는 테스트가 느리고 불안정합니다. 인터페이스로 의존하게 만들었다면 가짜를 넣을 수 있습니다.
```csharp
public interface ILogger { void Log(string message); }

class FakeLogger : ILogger
{
    public List<string> Messages { get; } = [];
    public void Log(string message) => Messages.Add(message);
}

[Fact]
public void Increase_LogsCall()
{
    var logger = new FakeLogger();
    var math = new MathService(logger);

    int result = math.Increase(1);

    Assert.Equal(2, result);
    Assert.Contains("MathService.Increase Called", logger.Messages);
}
```
Moq, NSubstitute 같은 라이브러리를 쓰면 가짜 클래스를 직접 만들지 않아도 됩니다.
