---
id: inheritance-override
title: 메서드 재정의 (virtual, override) - 다형성
category: 상속
order: 702
summary: 부모의 virtual 메서드를 자식이 override로 바꾸는 방법과, 부모 타입 변수로 자식 동작을 호출하는 다형성을 설명합니다.
keywords: 재정의, 오버라이드, override, virtual, 가상 메서드, 다형성, polymorphism, base.메서드, ToString 재정의
lesson: 상속_재정의
related: override-vs-new, abstract-class, inheritance, interface-basics
---
## 핵심
- 부모: `virtual` → "자식이 바꿔도 된다"
- 자식: `override` → "내 방식으로 바꾼다"

```csharp
class Animal
{
    public virtual void Eat() => Console.WriteLine("먹습니다.");
}

class Dog : Animal
{
    public override void Eat() => Console.WriteLine("개가 먹습니다.");
}

Animal dog = new Dog();   // 변수 타입은 Animal
dog.Eat();                // "개가 먹습니다." ← 실제 객체(Dog)의 메서드가 호출됨
```

## 다형성 (Polymorphism)
같은 코드로 여러 타입을 다룰 수 있습니다.
```csharp
List<Animal> animals = [new Dog(), new Cat(), new Animal()];
foreach (var a in animals)
    a.Eat();   // 각자 자기 방식으로 먹음
```

## 부모 동작도 살리기 - base
```csharp
public override void Eat()
{
    base.Eat();                     // 부모의 Eat 먼저 실행
    Console.WriteLine("꼬리를 흔듭니다");
}
```

## 자주 하는 재정의: ToString
```csharp
class Point
{
    public int X, Y;
    public override string ToString() => $"({X}, {Y})";
}
Console.WriteLine(new Point { X = 1, Y = 2 }); // (1, 2)
```
