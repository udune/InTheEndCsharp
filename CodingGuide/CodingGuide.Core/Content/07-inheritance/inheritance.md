---
id: inheritance
title: 상속 기초 (: 부모클래스, base)
category: 상속
order: 701
summary: 부모 클래스의 필드와 메서드를 자식 클래스가 물려받는 상속과, base 키워드로 부모 생성자/메서드를 호출하는 방법입니다.
keywords: 상속, inheritance, 부모 클래스, 자식 클래스, 기반 클래스, 파생 클래스, base, 콜론, 재사용, is-a
lesson: 상속
related: inheritance-override, inheritance-protected, abstract-class, interface-basics
---
## 핵심
`class 자식 : 부모` 형태로 선언하면 부모의 public/protected 멤버를 그대로 물려받습니다.

```csharp
class Animal
{
    public void Eat() => Console.WriteLine("먹습니다.");
}

class Dog : Animal     // Dog 는 Animal 이다 (is-a 관계)
{
    public void Bark() => Console.WriteLine("멍멍");
}

Dog dog = new Dog();
dog.Eat();    // 부모에게 물려받은 메서드
dog.Bark();

Animal a = dog;   // 자식은 부모 타입 변수에 담을 수 있음 (업캐스팅)
```

## base - 부모 생성자 호출
```csharp
class Animal
{
    public string Name { get; }
    public Animal(string name) { Name = name; }
}

class Dog : Animal
{
    public Dog(string name) : base(name) { }   // 부모 생성자에 전달
}
```

## 규칙
- C#은 **클래스 단일 상속**만 됩니다(부모는 하나). 여러 능력을 붙이려면 인터페이스를 여러 개 구현합니다.
- 모든 클래스는 결국 `object`를 상속합니다(`ToString`, `Equals`, `GetHashCode`).
- 상속보다 **조합(composition)** 이 더 유연한 경우가 많습니다. "A는 B이다"가 자연스러울 때만 상속하세요.
