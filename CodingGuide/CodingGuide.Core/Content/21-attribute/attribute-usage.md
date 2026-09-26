---
id: attribute-usage
title: AttributeUsage - 적용 대상, 중복 허용, 상속 제어
category: 어트리뷰트
order: 2105
summary: AttributeUsage로 어트리뷰트를 붙일 수 있는 위치(AttributeTargets), 여러 번 붙이기(AllowMultiple), 자식 클래스 상속(Inherited)을 제어하는 방법입니다.
keywords: AttributeUsage, AttributeTargets, AllowMultiple, Inherited, 적용 대상, 중복 적용, 여러 번 붙이기, 어트리뷰트 상속, CS0579, CS0592
lesson: CustomAttribute동적제어
related: attribute-constructor-props, attribute-custom
---
## 핵심
```csharp
[AttributeUsage(
    AttributeTargets.Class,     // 클래스에만 붙일 수 있음
    AllowMultiple = true,       // 같은 곳에 여러 번 붙일 수 있음 (기본 false)
    Inherited = false)]         // 자식 클래스에는 물려주지 않음 (기본 true)
class MyCustomAttribute : Attribute
{
    public string? Description { get; set; }
    public MyCustomAttribute(string name) { }
}

[MyCustom("custom", Description = "내용")]
[MyCustom("custom", Description = "내용")]      // AllowMultiple = true 라서 OK
class MyClass { }

class MyCustom2 : MyClass { }   // Inherited = false → MyCustom2 에서는 어트리뷰트가 안 보임
```

## 확인해 보기
```csharp
typeof(MyClass).GetCustomAttributes<MyCustomAttribute>().Count();     // 2
typeof(MyCustom2).GetCustomAttributes<MyCustomAttribute>(inherit: true).Count(); // 0
```

## AttributeTargets 주요 값
`Class`, `Struct`, `Interface`, `Method`, `Property`, `Field`, `Parameter`, `ReturnValue`, `Assembly`, `All`
- `|`로 조합합니다: `AttributeTargets.Property | AttributeTargets.Parameter`

## 오류
- 허용되지 않은 위치에 붙이면 **CS0592**
- AllowMultiple이 false인데 두 번 붙이면 **CS0579** (중복 특성)
