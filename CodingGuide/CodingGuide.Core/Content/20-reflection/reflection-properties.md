---
id: reflection-properties
title: 리플렉션 - 속성 목록 불러오기 (GetProperties, BindingFlags)
category: 리플렉션
order: 2002
summary: typeof와 GetType으로 Type을 얻고, GetProperties와 BindingFlags로 public/private 속성 목록을 가져오는 방법입니다.
keywords: typeof, GetType, Type, GetProperties, PropertyInfo, BindingFlags, NonPublic, Public, Instance, Static, private 속성, 속성 목록, 모든 속성 출력
lesson: 리플렉션_속성정보불러오기
related: reflection-methods, reflection-get-value, reflection-assembly-info
---
## Type 얻기
```csharp
Type t1 = typeof(Sample);            // 타입 이름으로
Type t2 = new Sample().GetType();    // 객체로 (실제 런타임 타입)
Type? t3 = Type.GetType("MyApp.Sample"); // 문자열로 (네임스페이스 포함 전체 이름)
```

## 속성 목록
```csharp
class Sample
{
    private int PrivateNumber1 { get; set; }
    public int Number1 { get; set; }
    public int Number2 { get; set; }
}

Type type = typeof(Sample);
foreach (PropertyInfo prop in type.GetProperties())    // 기본: public 인스턴스 + static
    Console.WriteLine(prop.Name);                       // Number1, Number2

var flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
foreach (var prop in type.GetProperties(flags))
    Console.WriteLine($"{prop.Name} : {prop.PropertyType.Name}");   // PrivateNumber1 포함
```

## BindingFlags 조합 규칙
- `Instance` 또는 `Static` 중 하나 이상 **반드시** 포함해야 합니다.
- `Public` 또는 `NonPublic` 중 하나 이상 **반드시** 포함해야 합니다.
- 하나라도 빠지면 결과가 빈 배열이 됩니다(흔한 실수).
- 부모 클래스의 private 멤버는 나오지 않습니다. `DeclaredOnly`를 주면 이 클래스에 선언된 것만 나옵니다.

## 활용 예: 객체 내용 자동 출력
```csharp
static string Dump(object obj) =>
    string.Join(", ", obj.GetType().GetProperties()
        .Select(p => $"{p.Name}={p.GetValue(obj)}"));
```
