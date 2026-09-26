---
id: constructor-params
title: 매개변수가 있는 생성자와 생성자 체이닝
category: 클래스
order: 606
summary: 객체를 만들 때 값을 받아 초기화하는 생성자, 여러 생성자를 this(...)로 연결하는 방법, 주 생성자(primary constructor)를 설명합니다.
keywords: 생성자, 매개변수 생성자, 생성자 오버로딩, 생성자 체이닝, this(), primary constructor, 주 생성자, 초기화
lesson: 생성자_매개변수
related: fields-constructor, method-optional-params, inheritance, record-type
---
## 핵심
```csharp
public class Car
{
    private string brand;
    private string model;
    private string color;

    public Car(string brand, string model, string color)
    {
        this.brand = brand;
        this.model = model;
        this.color = color;
    }

    public void ShowInfo() =>
        Console.WriteLine($"{brand} {model} ({color})");
}

var car = new Car("현대", "소나타", "검정");
car.ShowInfo();
```

## 생성자 여러 개 + 체이닝
중복 코드를 줄이기 위해 `this(...)`로 다른 생성자를 호출합니다.
```csharp
public class Car
{
    public string Brand { get; }
    public string Color { get; }

    public Car(string brand) : this(brand, "흰색") { }
    public Car(string brand, string color)
    {
        Brand = brand;
        Color = color;
    }
}
```

## 주 생성자 (C# 12)
클래스 이름 옆에 매개변수를 바로 씁니다. DI(의존성 주입)에서 특히 편리합니다.
```csharp
public class Car(string brand, string model)
{
    public void ShowInfo() => Console.WriteLine($"{brand} {model}");
}

public class OrderService(ILogger logger, IRepository repo)
{
    public void Save() { logger.Log("저장"); }
}
```
