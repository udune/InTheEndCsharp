---
id: reflection-property-caution
title: 리플렉션으로 속성 읽고 쓸 때 주의할 점 (CanRead, CanWrite)
category: 리플렉션
order: 2009
summary: get만 있거나 set만 있는 속성을 리플렉션으로 다룰 때 예외를 피하기 위해 CanRead/CanWrite를 확인하는 방법입니다.
keywords: CanRead, CanWrite, 읽기 전용 속성, 쓰기 전용 속성, Property set method not found, Property get method not found, ArgumentException, 리플렉션 예외, 인덱서
lesson: 리플렉션_동적속성읽고쓸때주의할점
related: reflection-get-value, reflection-set-value, property-getter
---
## 문제
```csharp
class Sample
{
    private int number1;
    public int Number1 { set => number1 = value; }   // set 만 있음
    public int Number2 => 2;                          // get 만 있음
}

typeof(Sample).GetProperty("Number1")!.GetValue(obj);     // ArgumentException: get 메서드 없음
typeof(Sample).GetProperty("Number2")!.SetValue(obj, 3);  // ArgumentException: set 메서드 없음
```

## 해결: 먼저 확인
```csharp
PropertyInfo p1 = type.GetProperty("Number1")!;
if (p1.CanRead)
    Console.WriteLine(p1.GetValue(instance));   // 실행 안 됨

PropertyInfo p2 = type.GetProperty("Number2")!;
if (p2.CanWrite)
    p2.SetValue(instance, 3);                   // 실행 안 됨
```

## 그 밖의 함정
- **인덱서**(`this[int i]`)도 속성으로 나옵니다. `p.GetIndexParameters().Length > 0`이면 건너뛰세요.
- `CanWrite`가 true여도 setter가 private일 수 있습니다: `p.SetMethod?.IsPublic`
- `init` 속성도 리플렉션으로는 값을 쓸 수 있습니다(컴파일러 규칙이라서).
- 속성 이름이 틀리면 `GetProperty`가 null → `!`로 무시하면 `NullReferenceException`이 납니다.
