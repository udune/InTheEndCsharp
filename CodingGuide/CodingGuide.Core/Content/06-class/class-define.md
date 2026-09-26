---
id: class-define
title: 클래스 정의와 객체 생성 (new)
category: 클래스
order: 601
summary: 클래스(설계도)를 정의하고 new로 객체(인스턴스)를 만드는 가장 기본적인 방법입니다.
keywords: 클래스, class, 객체, 인스턴스, instance, new, 생성, 설계도, 객체지향, OOP
lesson: 정의_객체생성
related: fields-constructor, methods, access-modifiers, struct-vs-class, record-type
---
## 핵심
- **클래스**는 데이터(필드/속성)와 동작(메서드)을 묶은 **설계도**입니다.
- **객체(인스턴스)** 는 설계도로 `new` 해서 만든 실제 물건입니다. 하나의 클래스로 여러 객체를 만들 수 있습니다.

```csharp
class Car
{
    public string Brand = "현대";          // 필드 (데이터)
    public void Drive() => Console.WriteLine($"{Brand} 출발!"); // 메서드 (동작)
}

Car car1 = new Car();   // 객체 생성
var car2 = new Car();   // var 사용
Car car3 = new();       // C# 9 target-typed new

car1.Drive();
```

## 클래스는 참조 타입
변수에는 객체 자체가 아니라 **객체의 주소(참조)** 가 들어 있습니다.
```csharp
Car a = new Car();
Car b = a;          // 같은 객체를 가리킴
b.Brand = "기아";
Console.WriteLine(a.Brand); // "기아"
```
값을 통째로 복사하는 타입이 필요하면 `struct`를 봅니다.

## 이름 규칙 (관례)
- 클래스, 메서드, 속성: `PascalCase` (예: `CarFactory`, `GetBrand`)
- 지역 변수, 매개변수: `camelCase` (예: `carCount`)
- private 필드: `_camelCase` (예: `_brand`)
