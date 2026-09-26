---
id: attribute-parameter-aop
title: 어트리뷰트 실전 - 매개변수 자동 변환 (인터셉터 + 어트리뷰트)
category: 어트리뷰트
order: 2109
summary: 메서드 매개변수에 [ToUpper], [Left(5)] 같은 어트리뷰트를 붙이고, AOP 인터셉터가 호출 전에 인자를 자동 변환하는 예제입니다.
keywords: 매개변수 어트리뷰트, parameter attribute, 인자 변환, 인터셉터, AttributeInterceptor, invocation.Arguments, 입력값 정규화, 전처리
lesson: CustomAttribute실전활용예제_Parameter
source: 어트리뷰트/Interceptors/AttributeInterceptor.cs, 어트리뷰트/Attributes/ToLowerAttribute.cs, 어트리뷰트/Attributes/LeftAttribute.cs
related: aop-proxy, attribute-property-transform
---
## 핵심
```csharp
public class MyService
{
    public virtual void DoSomething(
        [ToUpper] string upperStr,
        [ToLower] string lowerStr,
        [Left(5)] string leftStr)
    {
        Console.WriteLine($"upperStr: {upperStr}");
        Console.WriteLine($"lowerStr: {lowerStr}");
        Console.WriteLine($"leftStr: {leftStr}");
    }
}

var service = new MyService().CreateProxy(new AttributeInterceptor());
service.DoSomething("HeLLo", "WoRLd", "123456789");
// upperStr: HELLO
// lowerStr: world
// leftStr: 12345
```

## 인터셉터: 호출 직전에 인자를 바꿔치기
```csharp
public class AttributeInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var parameters = invocation.Method.GetParameters();
        for (int i = 0; i < parameters.Length; i++)
        {
            foreach (var attr in parameters[i].GetCustomAttributes().OfType<ITransformerAttribute<string>>())
            {
                if (invocation.Arguments[i] is string s)
                    invocation.Arguments[i] = attr.Transform(s);   // 인자 교체
            }
        }
        invocation.Proceed();   // 바뀐 인자로 실제 메서드 호출
    }
}
```

## 핵심 아이디어
- 어트리뷰트: **무엇을** 할지 선언 (규칙)
- 인터셉터: **언제/어떻게** 적용할지 (실행 엔진)
- 서비스 코드: 비즈니스 로직만 남음

ASP.NET Core의 `[FromBody]`, `[Required]` 모델 바인딩/검증도 같은 원리입니다.
