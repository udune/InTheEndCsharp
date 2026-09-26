---
id: reflection-methods
title: 리플렉션 - 메서드 목록 불러오기 (GetMethods, MethodInfo)
category: 리플렉션
order: 2003
summary: GetMethods로 타입의 메서드 목록을 가져오고, get_/set_ 같은 속성 접근자와 object에서 물려받은 메서드가 함께 나오는 이유를 설명합니다.
keywords: GetMethods, GetMethod, MethodInfo, 메서드 목록, 메서드 정보, 매개변수 정보, GetParameters, ReturnType, get_, set_, IsSpecialName
lesson: 리플렉션_메서드정보불러오기
related: reflection-invoke-method, reflection-properties
---
## 핵심
```csharp
class Sample
{
    public int Number1 { get; set; }
    public void Print() => Console.WriteLine("Hello World!");
}

Type type = typeof(Sample);
foreach (MethodInfo m in type.GetMethods())
    Console.WriteLine($"Method: {m.Name}");
```
출력에는 `Print`만 있는 게 아닙니다.
```
get_Number1, set_Number1   ← 속성은 내부적으로 get_/set_ 메서드
Print
GetType, ToString, Equals, GetHashCode  ← object 에서 물려받은 메서드
```

## 내가 선언한 메서드만 보기
```csharp
var mine = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
               .Where(m => !m.IsSpecialName);   // 속성 접근자(get_/set_) 제외
```

## 메서드 상세 정보
```csharp
MethodInfo? print = type.GetMethod("Print");
Console.WriteLine(print!.ReturnType);                     // System.Void
foreach (ParameterInfo p in print.GetParameters())
    Console.WriteLine($"{p.ParameterType.Name} {p.Name}");
```

## 주의할 점
- 이름이 같은 오버로드가 여러 개면 `GetMethod("Name")`은 `AmbiguousMatchException`을 던집니다. 매개변수 타입을 지정하세요: `GetMethod("Print", [typeof(string), typeof(int)])`
