---
id: error-cs0120
title: CS0120 - static이 아닌 필드, 메서드 또는 속성에 개체 참조가 필요합니다
category: 컴파일 오류
order: 2804
summary: static 메서드(Main 등) 안에서 인스턴스 멤버를 객체 없이 바로 사용할 때 나는 오류와 해결(객체 생성 또는 static으로 변경)입니다.
keywords: CS0120, 개체 참조가 필요합니다, static이 아닌 필드, 비정적 필드 메서드 또는 속성, An object reference is required for the non-static field method or property, static Main 에서 호출, 인스턴스 멤버, 정적 메서드
related: static-members, class-define, methods
---
## 메시지
- `static이 아닌 필드, 메서드 또는 속성 'P.inst'에 개체 참조가 필요합니다.`
- An object reference is required for the non-static field, method, or property 'P.inst'

## 원인
static 메서드는 **객체 없이** 실행되므로 "어느 객체의" 필드인지 알 수 없습니다.
```csharp
class Program
{
    int count = 1;               // 인스턴스 필드
    void Print() { }             // 인스턴스 메서드

    static void Main()
    {
        Console.WriteLine(count);   // CS0120
        Print();                    // CS0120
    }
}
```

## 해결
```csharp
// 1) 객체를 만들어서 사용
var p = new Program();
Console.WriteLine(p.count);
p.Print();

// 2) 공유가 목적이면 static 으로
static int count = 1;
static void Print() { }
```
이 저장소의 예제들이 `public static void 실행()` 안에서 **로컬 함수**나 `new Car()`를 쓰는 이유도 이것입니다.

## WinForms/WPF에서
`Form1.label1.Text = ...`처럼 **클래스 이름**으로 다른 폼의 컨트롤에 접근하면 같은 오류가 납니다. 열려 있는 폼 **객체**의 참조를 넘겨받아 사용하세요.
