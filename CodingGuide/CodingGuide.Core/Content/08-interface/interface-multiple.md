---
id: interface-multiple
title: 인터페이스 다중 구현
category: 인터페이스
order: 802
summary: 한 클래스가 여러 인터페이스를 동시에 구현해 여러 능력을 갖게 하는 방법입니다.
keywords: 다중 구현, 다중 상속, 여러 인터페이스, IFlyable, 능력, 역할, 쉼표
lesson: 인터페이스_다중구현
source: 인터페이스/IAnimal.cs, 인터페이스/IFlyable.cs
related: interface-basics, interface-explicit, inheritance
---
## 핵심
클래스 상속은 하나만 되지만, **인터페이스는 여러 개**를 쉼표로 나열해 구현할 수 있습니다.

```csharp
public interface IFlyable { void Fly(); }

class Bird : IAnimal, IFlyable        // 두 역할을 모두 가짐
{
    public string Name { get; set; } = "짹짹이";
    public void MakeSound() => Console.WriteLine("짹짹");
    public void Fly() => Console.WriteLine("날아갑니다.");
}

IAnimal animal = new Bird();
IFlyable flyer = new Bird();
animal.MakeSound();
flyer.Fly();
// flyer.MakeSound();   // 오류: IFlyable 타입으로는 Fly 만 보임
```

## 부모 클래스 + 인터페이스
부모 클래스는 **맨 앞에** 씁니다.
```csharp
class Penguin : Animal, ISwimmable, IComparable<Penguin> { ... }
```

## 인터페이스로 필요한 능력만 요구하기
```csharp
void LetItFly(IFlyable f) => f.Fly();   // 날 수 있기만 하면 뭐든 OK
```
