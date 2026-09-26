---
id: xunit-basics
title: xUnit 테스트 작성법 (Fact, Theory, Assert, AAA 패턴)
category: 테스트
order: 2402
summary: xUnit으로 테스트 프로젝트를 만들고 [Fact], [Theory]+[InlineData], Assert 메서드로 테스트를 작성해 dotnet test로 실행하는 방법입니다.
keywords: xUnit, xunit, Fact, Theory, InlineData, Assert, Assert.Equal, Assert.True, AAA, Arrange Act Assert, 테스트 프로젝트, dotnet test, 테스트 실행, 테스트 이름 짓기, 매개변수 테스트
source: Tests/Services/CalculatorTests.cs, 테스트코드작성/Calculator.cs
related: test-importance, test-exception
---
## 테스트 프로젝트 준비
```
dotnet new xunit3 -n MyApp.Tests         (템플릿이 없으면 xunit.v3 패키지를 직접 추가)
dotnet add MyApp.Tests reference MyApp
dotnet test
```
이 저장소의 `Tests/InTheEndCsharp.Tests.csproj`는 `xunit.v3` 패키지와 본 프로젝트 참조를 가지고 있습니다.

## [Fact] - 입력 없는 테스트 하나
```csharp
public class CalculatorTests
{
    [Fact]
    public void Add_ReturnsSum()
    {
        // Arrange (준비)
        var calculator = new Calculator();

        // Act (실행)
        int result = calculator.Add(3, 5);

        // Assert (검증)
        Assert.Equal(8, result);        // (기대값, 실제값) 순서
    }

    [Fact]
    public void Subtract_ReturnsDifference()
    {
        var calculator = new Calculator();
        Assert.Equal(-2, calculator.Subtract(3, 5));
    }
}
```

## [Theory] - 여러 입력으로 같은 테스트
```csharp
[Theory]
[InlineData(2, true)]
[InlineData(3, false)]
[InlineData(0, true)]
[InlineData(-4, true)]
public void IsEven_Works(int number, bool expected)
{
    Assert.Equal(expected, new Calculator().IsEven(number));
}
```

## 자주 쓰는 Assert
```csharp
Assert.Equal(expected, actual);
Assert.NotEqual(a, b);
Assert.True(condition); Assert.False(condition);
Assert.Null(obj); Assert.NotNull(obj);
Assert.Contains("abc", text); Assert.Empty(list);
Assert.Equal(0.333, result, precision: 3);        // 실수 비교
Assert.Throws<DivideByZeroException>(() => calc.Divide(1, 0));
```

## 좋은 테스트 습관
- 테스트 하나는 **한 가지**만 검증합니다.
- 이름에 "무엇을, 어떤 상황에서, 어떤 결과"를 담습니다: `Divide_ByZero_Throws`
- 테스트끼리 **순서나 공유 상태에 의존하지 않게** 합니다.
