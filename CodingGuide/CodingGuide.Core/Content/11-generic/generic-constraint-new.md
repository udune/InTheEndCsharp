---
id: generic-constraint-new
title: 제네릭 제약 조건 new() - 제네릭 안에서 객체 생성
category: 제네릭
order: 1103
summary: where T : new() 제약으로 제네릭 메서드 안에서 new T()를 호출해 객체를 만드는 방법과 타입 매개변수 여러 개 사용법입니다.
keywords: new(), new T(), 제네릭 객체 생성, 팩토리, 인스턴스 생성, where T : new(), 기본 생성자, CS0304
lesson: 제네릭_제약조건_new
related: generic-constraint-struct-class, generic-constraint-classtype, reflection-create-instance
---
## 핵심
T가 어떤 생성자를 가졌는지 모르면 `new T()`를 할 수 없습니다(오류 CS0304). `new()` 제약을 걸면 가능합니다.

```csharp
T Create<T>() where T : class, new()
{
    return new T();
}

void CreateTwo<T, T2>(out T a, out T2 b)
    where T : class, new()
    where T2 : class, new()      // 타입 매개변수마다 where 를 따로
{
    a = new T();
    b = new T2();
}

var dog = Create<Dog>();
CreateTwo(out Dog d, out Cat c);   // 타입 추론
Console.WriteLine(dog);            // 제 이름은 멍멍이입니다.
```

## 규칙
- `new()`는 제약 목록의 **맨 마지막**에 씁니다.
- 매개변수가 있는 생성자는 제약으로 요구할 수 없습니다. 그럴 때는 팩토리 델리게이트(`Func<T>`)를 받거나 `Activator.CreateInstance`를 씁니다.
```csharp
T Create<T>(Func<T> factory) => factory();
var car = Create(() => new Car("현대", "소나타"));
```
