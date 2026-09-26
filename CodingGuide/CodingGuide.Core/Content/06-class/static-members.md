---
id: static-members
title: static - 정적 필드, 정적 메서드, 정적 클래스
category: 클래스
order: 618
summary: 객체 없이 클래스 이름으로 바로 쓰는 static 멤버와, 인스턴스 멤버와의 차이, 정적 클래스를 설명합니다.
keywords: static, 정적, 정적 메서드, 정적 필드, 정적 클래스, 인스턴스, 클래스 멤버, 공유, 유틸리티, CS0120
lesson: 정적타입_static
related: extension-methods, const-readonly, di-lifetime, error-cs0120
---
## 핵심
- **인스턴스 멤버**: 객체마다 따로 존재. `new` 한 객체로 접근 (`car.Drive()`)
- **static 멤버**: 클래스에 **딱 하나** 존재하고 모든 곳에서 공유. 클래스 이름으로 접근 (`Math.Max()`)

```csharp
class MyClass
{
    public static string Name = "MyClass";   // 정적 필드 (공유)
    public string MyText => "My Text";        // 인스턴스 속성

    public static void Print()
    {
        // Console.WriteLine(MyText);  // 오류 CS0120: static 에서는 인스턴스 멤버에 바로 접근 불가
        var obj = new MyClass();
        Console.WriteLine($"{obj.MyText} / {Name}");  // 객체를 만들면 OK
    }

    public void MyMethod()
    {
        Print();                    // 인스턴스 → static 은 자유롭게 호출 가능
        Console.WriteLine(MyText);
    }
}

MyClass.Print();   // 객체 없이 호출
```

## 정적 클래스
모든 멤버가 static이고, `new` 할 수 없는 클래스입니다. 도구 모음(유틸리티)에 씁니다. 예: `Math`, `Console`, `File`
```csharp
static class MathUtil
{
    public static int Square(int x) => x * x;
}
```

## 주의할 점
- static 필드는 프로그램 전체에서 공유되므로, **여러 스레드가 동시에 바꾸면 경쟁 상태**가 생깁니다.
- 테스트하기 어렵고 결합도가 높아지므로, 상태를 가진 서비스는 static 대신 DI의 Singleton을 고려하세요.
