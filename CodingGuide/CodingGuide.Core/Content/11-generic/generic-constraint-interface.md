---
id: generic-constraint-interface
title: 제네릭 제약 조건 - 인터페이스 (where T : IAnimal)
category: 제네릭
order: 1105
summary: where T : 인터페이스 제약으로 특정 기능을 구현한 타입만 받는 방법과 IComparable 같은 실전 예입니다.
keywords: where T : 인터페이스, 인터페이스 제약, interface constraint, IComparable, 비교 가능, 최댓값, 제네릭 Max
lesson: 제네릭_제약조건_interface
related: generic-constraint-classtype, interface-basics, list-sort
---
## 핵심
```csharp
interface IAnimal
{
    string Name { get; }
    void MakeSound();
}
class Bird : IAnimal
{
    public string Name => "짹짹이";
    public void MakeSound() => Console.WriteLine("짹짹");
}

T Create<T>() where T : IAnimal, new()
{
    T instance = new T();
    instance.MakeSound();   // 인터페이스 멤버 호출 가능
    return instance;
}

var bird = Create<Bird>();
Console.WriteLine(bird.Name);
```

## 실전 예: 비교 가능한 타입의 최댓값
```csharp
T Max<T>(T a, T b) where T : IComparable<T> =>
    a.CompareTo(b) >= 0 ? a : b;

Max(3, 7);           // 7
Max("apple", "kiwi"); // "kiwi"
```

## 제네릭 수학 (C# 11+)
숫자 타입 전체에 대해 +, * 를 쓰는 제네릭 메서드를 만들 수 있습니다.
```csharp
using System.Numerics;
T Sum<T>(IEnumerable<T> values) where T : INumber<T>
{
    T total = T.Zero;
    foreach (var v in values) total += v;
    return total;
}
```
