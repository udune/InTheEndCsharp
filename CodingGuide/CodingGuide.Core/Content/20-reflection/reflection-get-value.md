---
id: reflection-get-value
title: 리플렉션 - 속성 값 동적으로 읽기 (GetValue)
category: 리플렉션
order: 2005
summary: 속성 이름(문자열)으로 PropertyInfo를 찾아 GetValue로 값을 읽는 방법입니다. 반복문으로 Number1, Number2 같은 속성을 차례로 읽을 수 있습니다.
keywords: GetValue, GetProperty, PropertyInfo, 속성 값 읽기, 이름으로 속성 접근, 문자열로 속성, 동적 접근, 객체를 딕셔너리로
lesson: 리플렉션_동적속성값읽기
related: reflection-set-value, reflection-property-caution, reflection-properties
---
## 핵심
```csharp
class Sample
{
    public int Number1 { get; set; } = 1;
    public int Number2 { get; set; } = 2;
}

Type type = typeof(Sample);
Sample instance = new Sample();

foreach (int i in Enumerable.Range(1, 2))
{
    string name = $"Number{i}";                           // "Number1", "Number2"
    PropertyInfo? prop = type.GetProperty(name);
    object? value = prop?.GetValue(instance);             // 어떤 객체의 값인지 넘겨줌
    Console.WriteLine($"{name}: {value}");
}
```

## 활용: 객체 → Dictionary
```csharp
Dictionary<string, object?> ToDictionary(object obj) =>
    obj.GetType().GetProperties()
       .Where(p => p.CanRead)
       .ToDictionary(p => p.Name, p => p.GetValue(obj));
```

## 주의할 점
- 반환값은 `object?`입니다. 원래 타입으로 쓰려면 캐스팅: `(int)prop.GetValue(instance)!`
- 속성 이름이 틀리면 `GetProperty`가 `null`을 돌려줍니다. `?.`로 처리하거나 null 검사하세요.
- 정적(static) 속성은 `GetValue(null)`로 읽습니다.
