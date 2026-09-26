---
id: reflection-set-value
title: 리플렉션 - 속성 값 동적으로 쓰기 (SetValue)
category: 리플렉션
order: 2006
summary: 속성 이름으로 PropertyInfo를 찾아 SetValue로 값을 바꾸는 방법과, 문자열 값을 속성 타입으로 변환해 넣는 활용 예입니다.
keywords: SetValue, 속성 값 쓰기, 동적 대입, 이름으로 값 설정, 매핑, 자동 매핑, CSV 매핑, Convert.ChangeType, ArgumentException
lesson: 리플렉션_동적속성값쓰기
related: reflection-get-value, reflection-property-caution, reflection-field
---
## 핵심
```csharp
Type type = typeof(Sample);
Sample instance = new Sample();
Console.WriteLine($"{instance.Number1}, {instance.Number2}");   // 1, 2

foreach (int i in Enumerable.Range(1, 2))
{
    PropertyInfo? prop = type.GetProperty($"Number{i}");
    prop?.SetValue(instance, i * 10);                            // (대상 객체, 새 값)
}

Console.WriteLine($"{instance.Number1}, {instance.Number2}");   // 10, 20
```

## 활용: 문자열 데이터 → 객체 자동 채우기
```csharp
T Map<T>(Dictionary<string, string> row) where T : new()
{
    var obj = new T();
    foreach (var prop in typeof(T).GetProperties().Where(p => p.CanWrite))
    {
        if (row.TryGetValue(prop.Name, out var text))
            prop.SetValue(obj, Convert.ChangeType(text, prop.PropertyType));
    }
    return obj;
}

var user = Map<User>(new() { ["Name"] = "홍길동", ["Age"] = "30" });
```

## 주의할 점
- 값의 타입이 속성 타입과 맞지 않으면 `ArgumentException`이 납니다. (`int` 속성에 `"10"` 문자열 X)
- 쓰기 전용/읽기 전용 여부는 `CanWrite`로 먼저 확인하세요. (다음 문서 참고)
