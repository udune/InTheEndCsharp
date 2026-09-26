---
id: enumerable
title: IEnumerable과 foreach가 동작하는 원리
category: 열거자와 컬렉션
order: 1402
summary: foreach로 순회할 수 있는 타입의 조건(GetEnumerator)과 IEnumerable<T>, yield로 지연 생성되는 시퀀스를 설명합니다.
keywords: IEnumerable, IEnumerable<T>, GetEnumerator, foreach 원리, 시퀀스, 지연 실행, lazy, yield, 커스텀 컬렉션, 무한 시퀀스
lesson: 열거형_Enumerable
related: enumerator, linq-intro, linq-deferred-execution, loops
---
## 핵심
`foreach`는 **`GetEnumerator()` 메서드를 가진 타입**이면 무엇이든 순회할 수 있습니다.

```csharp
class Collection
{
    public IEnumerator<int> GetEnumerator()   // 이것만 있으면 foreach 가능
    {
        yield return 1;
        yield return 10;
        yield return 100;
    }
}

foreach (int v in new Collection())
    Console.WriteLine(v);
```

## IEnumerable<T>를 반환하는 메서드
```csharp
IEnumerable<int> GetNumbers()
{
    yield return 1;
    yield return 10;
}

foreach (var v in GetNumbers()) Console.WriteLine(v);
```
`List<T>`, 배열, `Dictionary`, LINQ 결과 모두 `IEnumerable<T>`입니다. 그래서 매개변수를 `IEnumerable<T>`로 받으면 **어떤 컬렉션이든** 받을 수 있습니다.

## 지연 실행 (Lazy)
yield로 만든 시퀀스는 **꺼낼 때 계산**됩니다. 그래서 무한 시퀀스도 가능합니다.
```csharp
IEnumerable<int> Naturals()
{
    int n = 1;
    while (true) yield return n++;
}
var first5 = Naturals().Take(5).ToList();   // [1,2,3,4,5]
```

## 주의할 점
- IEnumerable을 두 번 foreach 하면 **두 번 계산**됩니다(파일 읽기, DB 조회라면 두 번 실행). 여러 번 쓸 거면 `.ToList()`로 한 번 확정하세요.
