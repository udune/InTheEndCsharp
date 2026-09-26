---
id: error-cs0535
title: CS0535 / CS0534 - 인터페이스 멤버(추상 멤버)를 구현하지 않습니다
category: 컴파일 오류
order: 2810
summary: 인터페이스를 구현한다고 선언했지만 멤버 일부를 만들지 않았거나, 추상 클래스의 abstract 멤버를 override하지 않았을 때 나는 오류입니다.
keywords: CS0535, CS0534, 인터페이스 멤버를 구현하지 않습니다, does not implement interface member, 추상 멤버를 구현하지 않습니다, does not implement inherited abstract member, 인터페이스 구현 누락, override 누락, 빠른 작업
related: interface-basics, abstract-class, interface-explicit
---
## 메시지
- `'Q'은(는) 'I.M()' 인터페이스 멤버를 구현하지 않습니다.` (CS0535)
- `'Dog'은(는) 상속된 추상 멤버 'Animal.Move()'을(를) 구현하지 않습니다.` (CS0534)

## 해결
```csharp
interface I { void M(); }

class Q : I
{
    public void M() { }        // public 이어야 하고, 이름/매개변수/반환 타입이 정확히 같아야 함
}

abstract class Animal { public abstract void Move(); }
class Dog : Animal
{
    public override void Move() { }   // override 필수
}
```
IDE에서 클래스 이름의 빨간 줄에 커서를 두고 `Ctrl + .` → **"인터페이스 구현"** / **"추상 클래스 구현"** 을 선택하면 뼈대가 자동으로 만들어집니다.

## 구현했는데도 오류가 나면
- `public`을 빠뜨림 (인터페이스 멤버 구현은 public 이어야 함, 명시적 구현 제외)
- 매개변수 타입이나 반환 타입이 다름 (`int` vs `long`, `Task` vs `void`)
- 속성의 `get`/`set` 구성이 다름
