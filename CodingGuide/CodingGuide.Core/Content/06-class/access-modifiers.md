---
id: access-modifiers
title: 접근 제어자 (public, private, protected, internal)
category: 클래스
order: 603
summary: 클래스 멤버를 어디서 사용할 수 있는지 정하는 접근 제어자와 캡슐화 개념을 설명합니다.
keywords: 접근 제어자, 접근 한정자, public, private, protected, internal, protected internal, private protected, 캡슐화, 은닉, 기본 접근
lesson: 접근제어자
related: inheritance-protected, properties, property-setter-access, namespace
---
## 핵심
| 제어자 | 접근 가능한 곳 |
|---|---|
| `public` | 어디서나 |
| `private` | 같은 클래스 안에서만 (**멤버의 기본값**) |
| `protected` | 같은 클래스 + 상속받은 자식 클래스 |
| `internal` | 같은 어셈블리(프로젝트) 안 (**클래스의 기본값**) |
| `protected internal` | 같은 어셈블리 또는 자식 클래스 |
| `private protected` | 같은 어셈블리 안의 자식 클래스 |

```csharp
class Car
{
    string brand;          // 아무것도 안 쓰면 private
    public string Model = "소나타";

    public Car() { brand = "현대"; }
}

var car = new Car();
// Console.WriteLine(car.brand); // 컴파일 오류 CS0122: 보호 수준 때문에 접근할 수 없음
Console.WriteLine(car.Model);    // OK
```

## 왜 막아두나요? (캡슐화)
외부에서 필드를 마음대로 바꾸면 객체가 잘못된 상태가 될 수 있습니다. 데이터는 `private`으로 숨기고, 검증이 들어간 메서드나 속성으로만 바꾸게 합니다.
```csharp
class BankAccount
{
    private decimal _balance;
    public decimal Balance => _balance;          // 읽기만 공개

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("0보다 커야 합니다");
        _balance += amount;
    }
}
```

## 원칙
- **가능한 한 좁게** 시작하고, 필요할 때만 넓힙니다.
