---
id: attribute-read-metadata
title: 어트리뷰트 읽기 - 리플렉션으로 메타 정보 조회 (GetCustomAttributes)
category: 어트리뷰트
order: 2106
summary: 클래스에 붙인 [Info(작성자, 버전, 날짜)] 어트리뷰트를 리플렉션(GetCustomAttributes)으로 읽어 조건에 맞는 클래스 정보를 출력하는 예제입니다.
keywords: GetCustomAttributes, GetCustomAttribute, IsDefined, 어트리뷰트 읽기, 메타 정보 조회, 작성자, 버전 정보, 어셈블리의 모든 타입, GetTypes, 클래스 검색
lesson: CustomAttribute활용예제_메타정보읽어오기
source: 어트리뷰트/Attributes/InfoAttribute.cs, 어트리뷰트/Attributes/AttributeReader.cs, 어트리뷰트/Models/User.cs, 어트리뷰트/Models/Cart.cs
related: attribute-property-transform, reflection-assembly-info, attribute-usage
---
## 1) 어트리뷰트 정의
```csharp
[AttributeUsage(AttributeTargets.Class)]
public class InfoAttribute(string author, string version, string date) : Attribute
{
    public string Author { get; } = author;
    public string Version { get; } = version;
    public string Date { get; } = date;
    public string? Description { get; set; }
}
```

## 2) 클래스에 붙이기
```csharp
[Info("Kaburi", "1.0.0", "2024-12-26", Description = "사용자 모델 클래스 생성")]
public class User { ... }

[Info("Kaburi", "1.0.0", "2024-12-27", Description = "장바구니 모델 클래스")]
public class Cart { }
```

## 3) 리플렉션으로 읽기
```csharp
public static void ReadInfoAttributes(Type type, Func<InfoAttribute, bool> predicate)
{
    foreach (var attr in type.GetCustomAttributes<InfoAttribute>(inherit: false))
    {
        if (!predicate(attr)) continue;
        Console.WriteLine($"Class: {type.Name}, Author: {attr.Author}, Version: {attr.Version}");
    }
}

// 현재 어셈블리의 "모든 타입" 중 작성자가 Kaburi 인 클래스
Type[] types = Assembly.GetExecutingAssembly().GetTypes();
foreach (var t in types)
    ReadInfoAttributes(t, a => a.Author == "Kaburi");
```

## 자주 쓰는 API
```csharp
type.IsDefined(typeof(InfoAttribute));                 // 붙어 있는지
type.GetCustomAttribute<InfoAttribute>();              // 하나 (없으면 null, 여러 개면 예외)
type.GetCustomAttributes<InfoAttribute>();             // 전부
method.GetCustomAttribute<ObsoleteAttribute>();        // 메서드, 속성, 매개변수도 동일
```
