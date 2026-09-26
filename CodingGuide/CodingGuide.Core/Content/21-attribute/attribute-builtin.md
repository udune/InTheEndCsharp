---
id: attribute-builtin
title: 어트리뷰트란? - Obsolete, Conditional
category: 어트리뷰트
order: 2101
summary: 코드에 [대괄호]로 붙이는 부가 정보인 어트리뷰트의 개념과, 내장 어트리뷰트 Obsolete(사용 중지 경고)와 Conditional(조건부 호출)을 설명합니다.
keywords: 어트리뷰트, attribute, 특성, 애트리뷰트, 대괄호, 메타데이터, Obsolete, 사용 중지, deprecated, 경고, Conditional, DEBUG, 디버그 전용, 조건부 컴파일, CS0618
lesson: Attribute정의및적용하기_Obsolete_Conditional
related: attribute-caller-info, attribute-custom, attribute-read-metadata
---
## 어트리뷰트란?
클래스, 메서드, 속성, 매개변수 등에 **추가 정보(메타데이터)** 를 붙이는 문법입니다. 그 자체로는 동작하지 않고, **컴파일러나 프레임워크, 리플렉션 코드가 읽어서** 동작을 바꿉니다.

```csharp
[Obsolete]            // 컴파일러가 읽음
[Fact]                // xUnit 이 읽음 (테스트 메서드)
[HttpGet]             // ASP.NET Core 가 읽음
[JsonPropertyName("user_name")]  // System.Text.Json 이 읽음
```

## Obsolete - 이제 쓰지 마세요
```csharp
class MyClass
{
    [Obsolete("더 이상 사용되지 않습니다. PrintValue를 사용해주세요.")]
    public void Print() => Console.WriteLine("Hello World!");

    [Obsolete("사용 금지", true)]      // true 면 경고가 아니라 컴파일 오류
    public void OldMethod() { }
}

new MyClass().Print();   // 경고 CS0618 + 메시지 표시 (실행은 됨)
```

## Conditional - 조건이 정의됐을 때만 호출
```csharp
[Conditional("DEBUG")]           // Debug 빌드에서만 호출 코드가 남음
public void PrintValue(string text) => Console.WriteLine(text);
```
- 조건 기호가 정의되지 않으면 **호출하는 코드 자체가 컴파일에서 빠집니다.**
- 예제는 `[Conditional("RELEASE")]`입니다. `RELEASE` 기호는 기본으로 정의되지 않으므로 `PrintValue`는 **아무것도 출력하지 않습니다.** (Release 빌드의 기본 기호는 `RELEASE`가 아니라 `DEBUG`가 없는 상태입니다.)
- 반환 타입이 `void`인 메서드에만 붙일 수 있습니다.

비슷한 기능: `#if DEBUG ... #endif` (전처리기 지시문)
