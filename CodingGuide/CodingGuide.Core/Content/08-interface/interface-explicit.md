---
id: interface-explicit
title: 인터페이스 명시적 구현 (IAnimal.MakeSound)
category: 인터페이스
order: 803
summary: 인터페이스 이름을 붙여 구현해서, 인터페이스 타입으로만 호출되게 하는 명시적 구현과 이름 충돌 해결법입니다.
keywords: 명시적 구현, explicit implementation, 인터페이스 이름 충돌, 같은 이름 메서드, 숨기기, 인터페이스 타입으로만
lesson: 인터페이스_명시적구현
source: 인터페이스/IAnimal.cs
related: interface-basics, interface-multiple
---
## 핵심
`인터페이스명.메서드명`으로 구현하면 (접근 제어자 없이) **그 인터페이스 타입으로 볼 때만** 호출할 수 있습니다.

```csharp
class Dog : IAnimal
{
    public string Name { get; set; } = "멍멍이";

    void IAnimal.MakeSound()          // 명시적 구현 (public 을 쓰지 않음)
    {
        Console.WriteLine("멍멍");
    }
}

IAnimal dog = new Dog();
dog.MakeSound();          // OK

Dog d = new Dog();
// d.MakeSound();         // 오류: Dog 타입으로는 보이지 않음
((IAnimal)d).MakeSound(); // 캐스팅하면 OK
```

## 언제 쓰나요?
1. **두 인터페이스에 같은 이름의 메서드**가 있어서 따로 구현해야 할 때
```csharp
interface IPrinter { void Print(); }
interface IScanner { void Print(); }

class Machine : IPrinter, IScanner
{
    void IPrinter.Print() => Console.WriteLine("인쇄");
    void IScanner.Print() => Console.WriteLine("스캔 결과 출력");
}
```
2. 클래스의 공개 API를 깔끔하게 유지하고, 특정 인터페이스 용도로만 노출하고 싶을 때
