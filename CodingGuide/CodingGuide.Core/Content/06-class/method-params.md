---
id: method-params
title: 메서드 매개변수와 this 키워드
category: 클래스
order: 605
summary: 메서드에 값을 전달하는 매개변수와, 필드 이름과 매개변수 이름이 같을 때 쓰는 this를 설명합니다.
keywords: 매개변수, 파라미터, parameter, 인자, 인수, argument, this, 전달, 필드와 이름이 같을 때
lesson: 메서드_매개변수
related: methods, constructor-params, method-param-semantics, method-optional-params
---
## 핵심
```csharp
class Car
{
    private string brand;
    private string model;

    public void SetInfo(string brand, string model)  // 매개변수
    {
        this.brand = brand;   // this.brand = 필드, brand = 매개변수
        this.model = model;
    }
}

var car = new Car();
car.SetInfo("현대", "소나타");  // 인자(argument)
```

## this
- 지금 이 메서드를 실행 중인 **객체 자신**을 가리킵니다.
- 매개변수와 필드 이름이 같을 때 필드를 구분하기 위해 씁니다.
- private 필드를 `_brand`처럼 밑줄로 시작하면 `this` 없이도 구분됩니다.

## 이름 있는 인자 (named arguments)
순서와 상관없이 이름으로 전달할 수 있고, 읽기도 쉬워집니다.
```csharp
car.SetInfo(model: "소나타", brand: "현대");
```
