---
id: abstract-class
title: 추상 클래스 (abstract)
category: 상속
order: 705
summary: 직접 객체를 만들 수 없고, 자식에게 특정 메서드의 구현을 강제하는 추상 클래스와 추상 메서드를 설명합니다.
keywords: 추상 클래스, abstract, 추상 메서드, 구현 강제, 템플릿, 인스턴스 생성 불가, 추상 클래스 vs 인터페이스
lesson: 상속_추상클래스
related: interface-basics, inheritance-override, inheritance-chain, sealed-class
---
## 핵심
- `abstract class`: **new 할 수 없는** 미완성 부모 클래스
- `abstract` 메서드: 본문이 없고, 자식이 **반드시 override** 해야 함

```csharp
abstract class Animal
{
    protected int hp = 100;
    public abstract void Move();                      // 구현 없음 → 자식이 필수 구현
    public virtual void Eat() => Console.WriteLine("먹습니다."); // 공통 구현 제공 가능
}

class Dog : Animal
{
    public override void Move() => Console.WriteLine("개가 움직입니다.");
}

// var a = new Animal();   // 오류 CS0144: 추상 클래스는 인스턴스를 만들 수 없음
Animal dog = new Dog();
dog.Move();
```

## 추상 클래스 vs 인터페이스
| | 추상 클래스 | 인터페이스 |
|---|---|---|
| 상속 개수 | 하나만 | 여러 개 구현 가능 |
| 필드(상태) | 가질 수 있음 | 가질 수 없음 |
| 생성자 | 가능 | 불가 |
| 용도 | "~의 한 종류" + 공통 코드 공유 | "~할 수 있다" 능력 계약 |

공통 코드를 공유해야 하면 추상 클래스, 역할만 정하면 인터페이스를 고릅니다. 둘을 함께 쓰는 경우도 많습니다.
