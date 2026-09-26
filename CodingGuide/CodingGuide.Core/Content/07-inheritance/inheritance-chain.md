---
id: inheritance-chain
title: 상속 체인 (여러 단계 상속)
category: 상속
order: 706
summary: 부모-자식-손자로 이어지는 다단계 상속에서 멤버가 어떻게 전달되는지 설명합니다.
keywords: 상속 체인, 다단계 상속, 손자 클래스, 계층 구조, 상속 계층, multilevel inheritance
lesson: 상속_상속체인
related: inheritance, sealed-class, override-vs-new, abstract-class
---
## 핵심
`Animal → Cat → Tiger` 처럼 상속은 여러 단계로 이어질 수 있고, 손자는 조상의 모든 공개 멤버를 물려받습니다.

```csharp
abstract class Animal
{
    public abstract void Move();
    public virtual void Eat() => Console.WriteLine("먹습니다.");
}

class Cat : Animal
{
    public override void Move() => Console.WriteLine("고양이가 움직입니다.");
    public new void Eat() => Console.WriteLine("고양이가 먹습니다.");
    public void Yaong() => Console.WriteLine("야옹");
}

class Tiger : Cat { }   // Cat 의 모든 것을 물려받음

var tiger = new Tiger();
tiger.Eat();    // 고양이가 먹습니다. (변수 타입 Tiger → Cat 의 new Eat)
tiger.Move();   // 고양이가 움직입니다.
tiger.Yaong();  // 야옹

Animal cat = new Cat();
cat.Eat();      // 먹습니다. ← Cat.Eat 이 new 라서 부모 것이 호출됨
```

## 주의할 점
- 상속이 3단계 이상 깊어지면 어느 메서드가 호출될지 추적하기 어렵습니다. 얕게 유지하세요.
- 더 이상 재정의되면 안 되는 지점은 `sealed`로 막을 수 있습니다.
