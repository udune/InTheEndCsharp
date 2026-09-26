---
id: extension-methods
title: 확장 메서드 (this 매개변수)
category: 클래스
order: 619
summary: 기존 타입(string, int, 남의 클래스)을 수정하지 않고 메서드를 추가한 것처럼 쓰는 확장 메서드입니다.
keywords: 확장 메서드, extension method, 확장 함수, this, 정적 클래스, 메서드 추가, string 확장, LINQ 원리, extension 블록
lesson: 정적클래스_확장함수
related: static-members, linq-method-where, di-refactoring
---
## 핵심
**정적 클래스**의 **정적 메서드**에서 첫 번째 매개변수 앞에 `this`를 붙이면, 그 타입의 메서드처럼 호출할 수 있습니다.

```csharp
static class StringExtensions
{
    public static void Print(this string text)
    {
        Console.WriteLine(text);
    }

    public static bool IsBlank(this string? text) => string.IsNullOrWhiteSpace(text);

    public static string Truncate(this string text, int max) =>
        text.Length <= max ? text : text[..max] + "…";
}

string name = "John";
name.Print();                     // 확장 메서드처럼 호출
StringExtensions.Print(name);     // 실제로는 이것과 같음
Console.WriteLine("긴 문장입니다".Truncate(3)); // 긴 문…
```

## 어디에 쓰이나요?
- **LINQ**의 `Where`, `Select` 등은 모두 `IEnumerable<T>`의 확장 메서드입니다.
- DI 등록을 묶는 `services.AddMyServices()` 같은 패턴도 확장 메서드입니다.

## 규칙
- 확장 메서드를 쓰려면 그 정적 클래스의 **네임스페이스를 using** 해야 합니다.
- 원래 타입에 같은 이름의 메서드가 있으면 원래 메서드가 우선합니다.
- private 멤버에는 접근할 수 없습니다(밖에서 붙인 것일 뿐).

## C# 14 extension 블록
```csharp
static class Ext
{
    extension(string s)
    {
        public bool IsBlank => string.IsNullOrWhiteSpace(s);   // 확장 "속성"도 가능
    }
}
```
