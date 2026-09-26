---
id: property-getter
title: 읽기 전용 속성과 계산 속성 (get만, =>)
category: 클래스
order: 613
summary: get만 있는 읽기 전용 속성과, 값을 저장하지 않고 매번 계산해서 돌려주는 식 본문 속성(=>)입니다.
keywords: getter, get, 읽기 전용, readonly property, 계산 속성, computed property, =>, 식 본문 속성, 파생 값
lesson: 속성_getter
related: properties, property-setter, const-readonly
---
## 핵심
```csharp
class Person
{
    private string name;
    public Person(string name) { this.name = name; }

    // 식 본문 속성: 읽을 때마다 계산
    public string Name => $"이름은 {name}입니다.";
}

var p = new Person("까불이");
Console.WriteLine(p.Name);   // 이름은 까불이입니다.
// p.Name = "x";             // 컴파일 오류: set 이 없음
```

## 세 가지 읽기 전용 형태의 차이
```csharp
class Circle
{
    public double Radius { get; }              // 생성자에서만 설정 가능
    public Circle(double r) { Radius = r; }

    public double Area => Math.PI * Radius * Radius;   // 매번 계산 (저장 안 함)

    public DateTime Created { get; } = DateTime.Now;   // 생성 시점 한 번만 계산해서 저장
}
```
- `=> 식` : 읽을 때마다 **다시 계산**합니다.
- `{ get; } = 식` : 객체를 만들 때 **한 번만** 계산해 저장합니다.

`public DateTime Now => DateTime.Now;` 와 `public DateTime Now { get; } = DateTime.Now;`는 결과가 완전히 다르니 주의하세요.
