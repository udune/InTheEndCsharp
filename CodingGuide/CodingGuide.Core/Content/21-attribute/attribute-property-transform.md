---
id: attribute-property-transform
title: 어트리뷰트 실전 - 속성 값 자동 변환 ([ToUpper], [ToLower], [Left(5)])
category: 어트리뷰트
order: 2107
summary: 속성에 [ToUpper] 같은 어트리뷰트를 붙이고, 확장 메서드가 리플렉션으로 읽어 값을 자동 변환하는 실전 예제입니다. 검증(Validation) 프레임워크의 원리와 같습니다.
keywords: 어트리뷰트 활용, 속성 변환, 자동 변환, 대문자 변환, 소문자 변환, 문자열 자르기, 데이터 정규화, 유효성 검사, validation, DataAnnotations, Required, MaxLength
lesson: CustomAttribute실전활용예제_Property
source: 어트리뷰트/Models/User.cs, 어트리뷰트/Attributes/ITransformerAttribute.cs, 어트리뷰트/Attributes/ToUpperAttribute.cs, 어트리뷰트/Attributes/LeftAttribute.cs, 어트리뷰트/Extensions/AttributeExtensions.cs
related: attribute-parameter-aop, attribute-read-metadata, extension-methods, reflection-set-value
---
## 구조
1. **변환 규칙 인터페이스**
```csharp
public interface ITransformerAttribute<T>
{
    T Transform(T value);
}
```
2. **규칙을 가진 어트리뷰트들**
```csharp
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public class ToUpperAttribute : Attribute, ITransformerAttribute<string>
{
    public string Transform(string value) => value.ToUpper();
}

public class LeftAttribute(int length) : Attribute, ITransformerAttribute<string>
{
    public string Transform(string value) => value.Length > length ? value[..length] : value;
}
```
3. **모델에 붙이기**
```csharp
public class User
{
    [ToUpper]  public string Email { get; set; } = "";
    [ToLower]  public string Name { get; set; } = "";
    [Left(5)]  public string Address { get; set; } = "";
}
```
4. **리플렉션으로 적용하는 확장 메서드**
```csharp
public static void ApplyAttributes(this object obj)
{
    foreach (var prop in obj.GetType().GetProperties())
    {
        foreach (var attr in prop.GetCustomAttributes().OfType<ITransformerAttribute<string>>())
        {
            if (prop.GetValue(obj) is string value)
                prop.SetValue(obj, attr.Transform(value));
        }
    }
}
```

## 실행
```csharp
var user = new User { Email = "Test@tEsT.Com", Name = "KaBurI", Address = "서울 종로구 세종대로" };
user.ApplyAttributes();
// Email: TEST@TEST.COM, Name: kaburi, Address: 서울 종로
```

## 실무에서의 같은 원리
.NET의 `System.ComponentModel.DataAnnotations`가 바로 이 방식입니다.
```csharp
public class SignUp
{
    [Required, EmailAddress] public string Email { get; set; } = "";
    [StringLength(20, MinimumLength = 2)] public string Name { get; set; } = "";
}
var results = new List<ValidationResult>();
bool ok = Validator.TryValidateObject(model, new ValidationContext(model), results, true);
```
