---
id: properties
title: 속성(Property) 선언 - get/set과 자동 구현 속성
category: 클래스
order: 612
summary: 필드를 안전하게 노출하는 속성(get/set), 자동 구현 속성 { get; set; }, 초기값을 설명합니다.
keywords: 속성, 프로퍼티, property, get, set, getter, setter, 자동 구현 속성, auto property, value, 필드 vs 속성, required
lesson: 속성_선언
related: property-getter, property-setter, property-setter-access, access-modifiers
---
## 핵심
속성은 **밖에서 보면 필드처럼, 안에서는 메서드처럼** 동작합니다. 값을 읽을 때 `get`, 쓸 때 `set`이 실행됩니다.

```csharp
class Person
{
    // 1) 전체 구현 속성: 뒤에 private 필드(backing field)를 둠
    private string name = "";
    public string Name
    {
        get { return name; }
        set { name = value; }   // value = 대입된 값
    }

    // 2) 자동 구현 속성: 컴파일러가 필드를 자동으로 만들어 줌 (가장 많이 씀)
    public int Age { get; set; }

    // 3) 초기값
    public string City { get; set; } = "서울";
}

var p = new Person();
p.Name = "까불이";          // set 실행
Console.WriteLine(p.Name);  // get 실행
```

## 객체 이니셜라이저
속성은 생성과 동시에 채울 수 있습니다.
```csharp
var p = new Person { Name = "홍길동", Age = 30 };
```

## 필드 대신 속성을 쓰는 이유
- 나중에 검증 로직을 추가해도 사용하는 쪽 코드를 바꿀 필요가 없습니다.
- 읽기 전용/쓰기 제한 등 접근을 세밀하게 제어할 수 있습니다.
- WPF 데이터 바인딩, JSON 직렬화 등 대부분의 라이브러리는 **필드가 아니라 속성**을 대상으로 합니다.

## required (C# 11)
반드시 초기화해야 하는 속성을 표시합니다.
```csharp
class User { public required string Email { get; init; } }
var u = new User { Email = "a@b.com" }; // 빠뜨리면 컴파일 오류
```
