---
id: inheritance-protected
title: 상속과 protected 접근 제어자
category: 상속
order: 704
summary: 외부에는 숨기고 자식 클래스에게만 공개하는 protected 멤버의 사용법입니다.
keywords: protected, 상속 접근, 자식에서 접근, 부모 필드, 캡슐화, hp
lesson: 상속_접근제어자
related: access-modifiers, inheritance-override, abstract-class
---
## 핵심
`protected`는 **자기 자신 + 자식 클래스**에서만 접근할 수 있습니다. 외부에는 읽기 전용 속성으로만 공개하는 패턴이 흔합니다.

```csharp
class Animal
{
    protected int hp = 100;      // 자식만 수정 가능
    public int Hp => hp;         // 외부에는 읽기만 공개

    public virtual void Eat() => Console.WriteLine("먹습니다.");
}

class Cat : Animal
{
    public override void Eat()
    {
        hp += 10;                // 자식이라 접근 가능
        Console.WriteLine("고양이가 먹습니다.");
    }
}

Animal cat = new Cat();
cat.Eat(); cat.Eat(); cat.Eat();
Console.WriteLine(cat.Hp);       // 130
// cat.hp = 999;                 // 오류: 외부에서는 접근 불가
```

## 팁
- protected 필드보다는 `protected set` 속성이 더 안전합니다.
```csharp
public int Hp { get; protected set; } = 100;
```
