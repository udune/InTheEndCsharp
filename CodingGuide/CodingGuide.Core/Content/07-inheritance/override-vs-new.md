---
id: override-vs-new
title: override와 new(숨기기)의 차이
category: 상속
order: 703
summary: 부모 타입 변수로 호출했을 때 override는 자식 메서드가, new(메서드 숨기기)는 부모 메서드가 실행되는 차이를 설명합니다.
keywords: override, new, 메서드 숨기기, method hiding, 재정의 차이, 부모 메서드가 호출됨, CS0108, 경고
lesson: 상속_재정의_override_new차이
related: inheritance-override, inheritance-chain
---
## 결과로 보는 차이
```csharp
class Animal { public virtual void Eat() => Console.WriteLine("먹습니다."); }
class Dog : Animal { public override void Eat() => Console.WriteLine("개가 먹습니다."); }
class Cat : Animal { public new void Eat() => Console.WriteLine("고양이가 먹습니다."); }

Animal dog = new Dog();
Animal cat = new Cat();
Cat cat2 = new Cat();

dog.Eat();   // 개가 먹습니다.     ← override: 실제 객체 기준
cat.Eat();   // 먹습니다.          ← new: 변수 타입(Animal) 기준!
cat2.Eat();  // 고양이가 먹습니다.  ← 변수 타입이 Cat 이라서
```

## 정리
| | override | new |
|---|---|---|
| 의미 | 부모 메서드를 **대체** | 부모 메서드를 **가림** (별개의 메서드) |
| 호출 결정 기준 | 실제 객체 타입 (실행 시점) | 변수 타입 (컴파일 시점) |
| 다형성 | O | X |

## 주의할 점
- 부모에 같은 이름 메서드가 있는데 `override`도 `new`도 안 쓰면 경고 **CS0108/CS0114**가 뜨고 `new`처럼 동작합니다.
- 대부분의 경우 원하는 것은 **override**입니다. `new`는 부모 클래스를 고칠 수 없을 때 등 특수한 상황에서만 씁니다.
