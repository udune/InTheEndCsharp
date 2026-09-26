---
id: test-importance
title: 테스트 코드의 중요성 - 눈으로 확인에서 자동 검증으로
category: 테스트
order: 2401
summary: 결과를 콘솔에 찍어 눈으로 확인하는 방식의 한계와, 단위 테스트로 자동 검증해야 하는 이유를 Calculator 예제로 설명합니다.
keywords: 테스트, 단위 테스트, unit test, 테스트 코드, 자동 테스트, 회귀 테스트, 검증, 리팩토링 안전망, Calculator, 왜 테스트
lesson: 일반적인테스트_테스트코드의중요성
source: 테스트코드작성/Calculator.cs, Tests/Services/CalculatorTests.cs
related: xunit-basics, test-exception, di-interface
---
## 지금까지의 확인 방식
```csharp
var calculator = new Calculator();
int result = calculator.Add(3, 5);
Console.WriteLine(result == 8);   // True 가 찍히는지 눈으로 확인
```
문제점:
- 사람이 매번 실행하고 **눈으로** 확인해야 합니다.
- 기능이 100개가 되면 전부 다시 확인할 수 없습니다.
- 코드를 고친 뒤 **예전에 되던 기능이 망가져도(회귀)** 모릅니다.

## 단위 테스트
작은 단위(메서드)가 기대대로 동작하는지 **코드로 검증**하고, 명령 한 번으로 전부 실행합니다.
```csharp
public class CalculatorTests
{
    [Fact]
    public void Add_두수를_더한다()
    {
        var calculator = new Calculator();
        int result = calculator.Add(3, 5);
        Assert.Equal(8, result);
    }
}
```
```
dotnet test
```

## 테스트가 주는 것
- **안심하고 고칠 수 있음**: 리팩토링 후 테스트가 통과하면 기존 기능이 유지된 것
- **살아있는 문서**: 테스트 이름과 내용이 "이 메서드는 이렇게 동작한다"를 보여줌
- **좋은 설계 유도**: 테스트하기 쉬운 코드 = 의존성이 분리된 코드 (→ DI, 인터페이스)

> 이 저장소의 `Tests/Services/CalculatorTests.cs`는 아직 `Assert.Fail("This test needs an implementation")` 상태입니다. 다음 문서를 참고해 채워 보세요.
