---
id: interface-default
title: 인터페이스 기본 구현 (default interface method)
category: 인터페이스
order: 804
summary: 인터페이스 안에 본문이 있는 메서드를 두어 구현 클래스가 따로 만들지 않아도 쓰게 하는 기본 구현입니다.
keywords: 디폴트 구현, 기본 구현, default interface method, 인터페이스 메서드 본문, 인터페이스 확장, C# 8
lesson: 인터페이스_디폴트구현
source: 인터페이스/IAnimal.cs
related: interface-basics, abstract-class, extension-methods
---
## 핵심
C# 8부터 인터페이스 메서드에 **본문(기본 동작)** 을 넣을 수 있습니다. 구현 클래스는 이 메서드를 구현하지 않아도 됩니다.

```csharp
public interface IAnimal
{
    string Name { get; set; }
    void MakeSound();

    void PrintInformation()           // 기본 구현
    {
        Console.WriteLine($"안녕하세요 저는 {Name}입니다.");
    }
}

class Dog : IAnimal
{
    public string Name { get; set; } = "멍멍이";
    void IAnimal.MakeSound() => Console.WriteLine("멍멍");
    // PrintInformation 은 구현하지 않음
}

IAnimal dog = new Dog();
dog.PrintInformation();   // 안녕하세요 저는 멍멍이입니다.
```

## 주의할 점
- 기본 구현 메서드는 **인터페이스 타입 변수로만** 호출됩니다. `new Dog().PrintInformation()`은 오류입니다.
- 주 용도는 **이미 배포된 인터페이스에 새 메서드를 추가하면서 기존 구현 클래스들을 깨뜨리지 않는 것**입니다.
- 공통 로직을 공유하려는 목적이라면 추상 클래스나 확장 메서드가 더 적합한 경우가 많습니다.
