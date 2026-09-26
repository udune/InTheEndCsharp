---
id: error-cs7036
title: CS7036 - 필수 매개 변수에 해당하는 인수가 없습니다
category: 컴파일 오류
order: 2809
summary: 매개변수가 있는 생성자/메서드를 인자 없이 호출하거나, 부모 클래스에 기본 생성자가 없는데 자식 생성자에서 base(...)를 호출하지 않을 때 나는 오류입니다.
keywords: CS7036, 필수 매개 변수에 해당하는 인수가 없습니다, There is no argument given that corresponds to the required parameter, 인수 부족, 생성자 인수, 기본 생성자 없음, base 생성자 호출
related: constructor-params, method-optional-params, inheritance
---
## 메시지
- `'Q.Q(int)'의 필수 매개 변수 'v'에 해당하는 인수가 없습니다.`
- There is no argument given that corresponds to the required parameter 'v' of 'Q.Q(int)'

## 원인 1: 인자를 빠뜨림
```csharp
class Q { public Q(int v) { } }
new Q();          // CS7036
new Q(10);        // 해결
```
매개변수 있는 생성자를 만들면 **기본 생성자는 자동으로 생기지 않습니다.** 둘 다 필요하면 직접 추가하거나 기본값을 줍니다.
```csharp
public Q() : this(0) { }
public Q(int v = 0) { }
```

## 원인 2: 상속에서 부모 생성자
```csharp
class Animal { public Animal(string name) { } }
class Dog : Animal
{
    public Dog() { }                 // CS7036: 부모의 Animal(string) 에 인자를 못 줌
    public Dog() : base("멍멍이") { }  // 해결
}
```

## 원인 3: DI/직렬화에서 매개변수 없는 생성자가 필요한 경우
`new()` 제약, `Activator.CreateInstance`, JSON 역직렬화 등은 매개변수 없는 생성자를 요구할 수 있습니다.
