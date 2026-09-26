---
id: method-optional-params
title: 선택적 매개변수(기본값)와 이름 있는 인자
category: 클래스
order: 607
summary: 매개변수에 기본값을 주어 생략할 수 있게 하는 선택적 매개변수와, 이름으로 인자를 넘기는 named argument입니다.
keywords: 선택적 매개변수, optional, 기본값, 디폴트, default parameter, 이름 있는 인자, named argument, 생략
lesson: 생성자_선택적매개변수
related: method-params, method-params-array, constructor-params
---
## 핵심
```csharp
public class Car
{
    private string brand, model, color;

    // color 를 생략하면 "파랑"
    public Car(string brand, string model, string color = "파랑")
    {
        this.brand = brand; this.model = model; this.color = color;
    }

    public void ShowInfo(bool displayBrand = true, bool displayModel = true, bool displayColor = true)
    {
        if (displayBrand) Console.WriteLine($"브랜드는 {brand}입니다.");
        if (displayModel) Console.WriteLine($"모델은 {model}입니다.");
        if (displayColor) Console.WriteLine($"컬러는 {color}입니다.");
    }
}

var car = new Car("현대", "소나타");     // color = "파랑"
car.ShowInfo(displayModel: false);      // 이름으로 원하는 것만 지정
```

## 규칙
- 선택적 매개변수는 **항상 필수 매개변수 뒤**에 옵니다.
- 기본값은 **컴파일 시점 상수**여야 합니다(`"문자열"`, `0`, `null`, `default` 등). `DateTime.Now`, `new List<int>()`는 안 됩니다.
```csharp
void Log(string msg, DateTime? time = null)
{
    time ??= DateTime.Now;   // 이렇게 우회
}
```
- bool 인자가 여러 개면 `ShowInfo(true, false, true)`는 읽기 어렵습니다. **이름 있는 인자**를 쓰세요.
