---
id: property-setter-access
title: setter 접근 제한 (private set, init)
category: 클래스
order: 615
summary: 밖에서는 읽기만 가능하게 하는 private set과, 객체 생성 시에만 값을 넣을 수 있는 init 접근자를 설명합니다.
keywords: private set, init, init 접근자, 불변, immutable, 읽기 전용, 생성 시에만, 객체 초기화, 접근 제한
lesson: 속성_setter접근제어자
related: properties, property-setter, record-type, const-readonly
---
## 핵심
```csharp
class Person
{
    public Person(string name) { Name = name; }

    private string name = "";
    public string Name
    {
        get { return name; }
        init { name = $"** {value} **"; }   // 생성자/객체 이니셜라이저에서만 설정 가능
    }
}

var p = new Person("홍길동");
Console.WriteLine(p.Name);  // ** 홍길동 **
// p.Name = "x";            // 컴파일 오류 CS8852: init 전용
```

## 세 가지 비교
```csharp
class Account
{
    public string Id { get; init; }           // 만들 때만 설정
    public decimal Balance { get; private set; } // 클래스 안에서만 변경
    public string Owner { get; set; }         // 어디서나 변경

    public void Deposit(decimal amount) => Balance += amount;
}

var acc = new Account { Id = "A-1", Owner = "홍" }; // init 은 여기서 OK
acc.Deposit(100);
// acc.Balance = 1;  // 오류: private set
// acc.Id = "A-2";   // 오류: init
```

## 언제 쓰나요?
- **private set**: 값이 바뀌긴 하지만 반드시 메서드(규칙)를 통해서만 바뀌어야 할 때
- **init**: 한 번 만들면 바뀌지 않아야 하는 데이터 (DTO, 설정값, record)
