---
id: attribute-constructor-props
title: 어트리뷰트에 값 넘기기 - 생성자 매개변수와 명명된 속성
category: 어트리뷰트
order: 2104
summary: 어트리뷰트 생성자로 필수 값을, 공개 속성(Description = "...")으로 선택 값을 넘기는 방법과 넘길 수 있는 값의 제한입니다.
keywords: 어트리뷰트 매개변수, 어트리뷰트 생성자, 명명된 인자, named parameter, 어트리뷰트 속성, 위치 인자, 상수만 가능, CS0182, typeof
lesson: CustomAttribute생성자매개변수_속성추가
related: attribute-custom, attribute-usage, attribute-read-metadata
---
## 핵심
```csharp
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property)]
class MyCustomAttribute : Attribute
{
    public string Name { get; }                 // 생성자로 받는 필수 값
    public string? Description { get; set; }    // 이름으로 넘기는 선택 값 (public set 필요)

    public MyCustomAttribute() { Name = ""; }
    public MyCustomAttribute(string name) { Name = name; }
}

[MyCustom("custom", Description = "내용")]      // 위치 인자 + 명명된 속성
class MyClass
{
    [MyCustom(name: "custom", Description = "내용")]
    private string? TestProperty { get; set; }
}
```
> 예제의 생성자는 `name`을 받기만 하고 저장하지 않습니다. 나중에 읽으려면 위처럼 속성에 저장해야 합니다.

## 넘길 수 있는 값의 제한
어트리뷰트 인자는 **컴파일 시점 상수**여야 합니다.
- 가능: 숫자, bool, char, string, enum, `typeof(X)`, 이들의 1차원 배열
- 불가: `DateTime.Now`, `new List<int>()`, 변수 → 오류 CS0182

그래서 날짜 같은 값은 `"2024-12-27"`처럼 문자열로 넘깁니다.
