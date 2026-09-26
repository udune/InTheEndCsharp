---
id: interface-basics
title: 인터페이스 기초 (interface, 구현)
category: 인터페이스
order: 801
summary: 클래스가 반드시 가져야 할 메서드와 속성을 약속(계약)으로 정의하는 인터페이스와 그 구현 방법입니다.
keywords: 인터페이스, interface, 구현, 계약, 약속, I 접두사, 다형성, 느슨한 결합, 추상화
lesson: 인터페이스
source: 인터페이스/IAnimal.cs, 인터페이스/IFlyable.cs
related: interface-multiple, interface-explicit, interface-default, abstract-class, di-interface
---
## 핵심
인터페이스는 **"이런 기능이 있어야 한다"는 약속**만 정의합니다. 구현은 클래스가 합니다. 이름은 관례적으로 `I`로 시작합니다.

```csharp
public interface IAnimal
{
    string Name { get; set; }   // 속성 약속
    void MakeSound();           // 메서드 약속 (본문 없음, 자동으로 public)
}

class Bird : IAnimal
{
    public string Name { get; set; } = "짹짹이";
    public void MakeSound() => Console.WriteLine("짹짹");   // 반드시 구현
}

IAnimal bird = new Bird();   // 인터페이스 타입 변수로 사용
bird.MakeSound();
```

## 왜 쓰나요?
사용하는 쪽이 **구체 클래스가 아니라 약속(인터페이스)에만 의존**하게 되어, 구현을 쉽게 바꿀 수 있습니다.
```csharp
interface ILogger { void Log(string msg); }
class ConsoleLogger : ILogger { public void Log(string m) => Console.WriteLine(m); }
class FileLogger : ILogger { public void Log(string m) => File.AppendAllText("log.txt", m + "\n"); }

class OrderService(ILogger logger)      // 어떤 로거든 받을 수 있음
{
    public void Order() => logger.Log("주문 완료");
}
```
이것이 **의존성 주입(DI)** 과 **단위 테스트(가짜 객체로 교체)** 의 기반입니다.

## 규칙
- 인터페이스의 멤버를 하나라도 구현하지 않으면 컴파일 오류 **CS0535**가 납니다.
- 인터페이스는 `new` 할 수 없습니다.
