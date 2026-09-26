---
id: generic-constraint-classtype
title: 제네릭 제약 조건 - 특정 부모 클래스 (where T : Animal)
category: 제네릭
order: 1104
summary: where T : 부모클래스 제약으로 T가 특정 클래스의 자식임을 보장하고, 그 클래스의 멤버를 T에서 호출하는 방법입니다.
keywords: where T : 클래스, 기반 클래스 제약, base class constraint, 부모 클래스 제약, 추상 클래스 제약, 멤버 호출
lesson: 제네릭_제약조건_classtype
related: generic-constraint-interface, generic-constraint-new, abstract-class
---
## 핵심
T가 `Animal`(또는 자식)이라고 보장되므로, 제네릭 메서드 안에서 `Animal`의 멤버를 바로 쓸 수 있습니다.

```csharp
abstract class Animal
{
    public abstract string Name { get; }
    public abstract void MakeSound();
    public override string ToString() => $"제 이름은 {Name}입니다.";
}
class Dog : Animal
{
    public override string Name => "멍멍이";
    public override void MakeSound() => Console.WriteLine("멍멍");
}

T CreateAnimal<T>() where T : Animal, new()
{
    T instance = new T();
    instance.MakeSound();   // Animal 의 멤버 사용 가능
    return instance;
}

Dog dog = CreateAnimal<Dog>();   // 반환 타입이 Animal 이 아니라 Dog!
Console.WriteLine(dog);
// CreateAnimal<string>();       // 오류: string 은 Animal 이 아님
```

## 그냥 Animal을 받으면 안 되나요?
`Animal Create(Animal a)`처럼 쓰면 반환값이 항상 `Animal` 타입이라 다시 캐스팅해야 합니다. 제네릭은 **호출한 쪽의 구체 타입(Dog)을 그대로 유지**합니다.
