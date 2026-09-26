---
id: reflection-create-instance
title: 리플렉션 - 인스턴스 동적 생성 (Activator.CreateInstance)
category: 리플렉션
order: 2004
summary: 타입 정보(Type)나 문자열 타입 이름만으로 객체를 만드는 Activator.CreateInstance와 생성자 인자 전달 방법입니다.
keywords: Activator, CreateInstance, 동적 생성, 인스턴스 생성, 문자열로 객체 생성, 타입 이름으로 생성, 플러그인, 팩토리, 생성자 호출, MissingMethodException
lesson: 리플렉션_인스턴스생성
related: reflection-set-value, generic-constraint-new, di-container-lifetime
---
## 핵심
```csharp
Type type = typeof(Sample);

object? obj = Activator.CreateInstance(type);          // Type 으로 → object 반환
Sample s = Activator.CreateInstance<Sample>();         // 제네릭 → 캐스팅 불필요

Console.WriteLine(obj);                                // 네임스페이스.클래스명 출력
Console.WriteLine($"{s.Number1}, {s.Number2}");        // 1, 2
```

## 생성자에 인자 넘기기
```csharp
var car = (Car)Activator.CreateInstance(typeof(Car), "현대", "소나타")!;
```

## 문자열로 타입 찾아 만들기 (플러그인 방식)
```csharp
string typeName = "MyApp.Plugins.CsvExporter";          // 설정 파일 등에서 읽은 이름
Type? t = Type.GetType(typeName);
if (t != null && typeof(IExporter).IsAssignableFrom(t))
{
    var exporter = (IExporter)Activator.CreateInstance(t)!;
    exporter.Export();
}
```

## 주의할 점
- 매개변수 없는 생성자가 없는데 인자 없이 만들면 `MissingMethodException`이 납니다.
- `new T()`보다 느립니다. 반복 생성이 많다면 DI 컨테이너나 컴파일된 팩토리를 씁니다.
- `private` 생성자는 기본 호출로는 만들 수 없습니다.
