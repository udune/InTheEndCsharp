---
id: attribute-custom
title: 커스텀 어트리뷰트 만들기 (class XxxAttribute : Attribute)
category: 어트리뷰트
order: 2103
summary: Attribute 클래스를 상속해 나만의 어트리뷰트를 만들고 클래스나 메서드에 붙이는 가장 기본적인 방법입니다.
keywords: 커스텀 어트리뷰트, 사용자 정의 어트리뷰트, custom attribute, Attribute 상속, 어트리뷰트 만들기, Attribute 접미사
lesson: CustomAttribute생성
related: attribute-constructor-props, attribute-usage, attribute-read-metadata
---
## 핵심
`Attribute`를 상속한 클래스를 만들면 됩니다. 이름은 `~Attribute`로 끝내고, 붙일 때는 `Attribute`를 생략할 수 있습니다.

```csharp
class MyCustomAttribute : Attribute
{
}

[MyCustom]                  // [MyCustomAttribute] 와 같음
class MyClass
{
    [MyCustom]
    public void MyMethod() { }
}
```

## 붙이기만 하면 아무 일도 안 일어납니다
어트리뷰트는 **표시(태그)** 일 뿐입니다. 의미를 갖게 하려면 누군가 리플렉션으로 읽어서 동작해야 합니다.
```csharp
bool marked = typeof(MyClass).IsDefined(typeof(MyCustomAttribute));   // True
var attr = typeof(MyClass).GetCustomAttribute<MyCustomAttribute>();
```

## 다음 단계
- 값 넘기기: 생성자 매개변수와 속성 (다음 문서)
- 붙일 수 있는 위치 제한: `[AttributeUsage]`
- 실전: 메타 정보 읽기, 속성 값 자동 변환, AOP
