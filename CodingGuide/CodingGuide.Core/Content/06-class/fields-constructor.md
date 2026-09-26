---
id: fields-constructor
title: 필드와 생성자
category: 클래스
order: 602
summary: 객체의 데이터를 저장하는 필드와, 객체가 만들어질 때 필드를 초기화하는 생성자를 설명합니다.
keywords: 필드, field, 멤버 변수, 생성자, constructor, 초기화, 기본 생성자, 객체 초기화
lesson: 필드_생성자
related: constructor-params, class-define, properties, access-modifiers
---
## 핵심
- **필드**: 클래스 안에 선언한 변수. 객체마다 따로 값을 가집니다.
- **생성자**: 클래스 이름과 같고 반환 타입이 없는 메서드. `new` 할 때 한 번 실행되어 초기값을 넣습니다.

```csharp
class Car
{
    public string brand;   // 필드
    public string model;
    public string color;

    public Car()           // 생성자 (매개변수 없음 = 기본 생성자)
    {
        brand = "현대";
        model = "소나타";
        color = "검정";
    }
}

Car car = new Car();       // 이 순간 생성자가 실행됨
Console.WriteLine(car.brand); // 현대
```

## 필드 초기값을 선언과 함께 쓰기
```csharp
class Car
{
    private string _brand = "현대";
    private readonly List<string> _options = new();
}
```

## 주의할 점
- 생성자를 하나도 안 만들면 컴파일러가 빈 기본 생성자를 자동으로 만들어 줍니다. 매개변수가 있는 생성자를 하나라도 만들면 기본 생성자는 **자동으로 생기지 않습니다**.
- 필드를 `public`으로 열어두기보다는 `private` 필드 + `public` 속성(Property)을 쓰는 것이 관례입니다.
