---
id: reflection-field
title: 리플렉션 - private 필드 읽고 쓰기 (GetField, NonPublic)
category: 리플렉션
order: 2007
summary: GetField와 BindingFlags.NonPublic으로 외부에서 접근할 수 없는 private 필드의 값을 읽고 바꾸는 방법과 그 위험성입니다.
keywords: GetField, FieldInfo, private 필드 접근, private 필드 읽기, private 필드 값, private 값 바꾸기, 외부에서 private, 비공개 필드, NonPublic, 캡슐화 우회, 테스트용 접근, UnsafeAccessor
lesson: 리플렉션_동적필드읽고쓰기
related: reflection-properties, reflection-set-value, access-modifiers
---
## 핵심
```csharp
class Sample
{
    private string privateStr = "abc";
}

Type type = typeof(Sample);
var instance = new Sample();

FieldInfo? field = type.GetField("privateStr", BindingFlags.NonPublic | BindingFlags.Instance);
Console.WriteLine(field?.GetValue(instance));   // abc
field!.SetValue(instance, "가나다");
Console.WriteLine(field.GetValue(instance));    // 가나다
```
`private`는 **컴파일러의 규칙**일 뿐이라 리플렉션으로는 접근할 수 있습니다.

## 자동 구현 속성의 숨은 필드
`public int Age { get; set; }`의 실제 필드 이름은 `<Age>k__BackingField` 입니다.

## 주의할 점
- 캡슐화를 깨뜨립니다. 클래스 내부 구현이 바뀌면(필드 이름 변경) 예고 없이 깨집니다.
- 테스트나 디버깅 도구 같은 **특수한 경우에만** 쓰세요.
- `readonly` 필드도 SetValue로 바뀌지만 권장하지 않습니다.

## 더 빠른 방법 (.NET 8+)
```csharp
[UnsafeAccessor(UnsafeAccessorKind.Field, Name = "privateStr")]
static extern ref string GetPrivateStr(Sample s);

GetPrivateStr(instance) = "변경";   // 리플렉션보다 훨씬 빠름
```
