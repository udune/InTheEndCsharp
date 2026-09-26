---
id: enumerator
title: 열거자 IEnumerator와 yield return
category: 열거자와 컬렉션
order: 1401
summary: 값을 하나씩 꺼내 주는 열거자(IEnumerator)의 MoveNext/Current 동작과, yield return으로 열거자를 쉽게 만드는 방법입니다.
keywords: 열거자, enumerator, IEnumerator, MoveNext, Current, yield, yield return, yield break, 반복자, iterator, 지연 실행
lesson: 열거자_Enumerator
related: enumerable, loops, linq-intro
---
## 핵심
`foreach`가 내부적으로 하는 일은 **열거자에게 "다음 값 있어?"(MoveNext)를 묻고, "지금 값"(Current)을 받는 것**의 반복입니다.

```csharp
IEnumerator<int> GetNumbers()
{
    yield return 1;       // 값을 하나 내보내고 여기서 "일시 정지"
    yield return 10;
    yield return 100;
    yield return 1000;
}

var e = GetNumbers();
while (e.MoveNext())      // 다음 값으로 이동 (없으면 false)
{
    Console.WriteLine(e.Current);
}
```

## yield return의 동작
- 메서드가 한 번에 끝까지 실행되지 않고, `MoveNext()`가 불릴 때마다 **다음 yield까지만** 실행됩니다.
- `yield break;`로 열거를 끝낼 수 있습니다.

```csharp
IEnumerable<int> Countdown(int from)
{
    for (int i = from; i > 0; i--)
    {
        if (i == 3) yield break;   // 3 에서 멈춤
        yield return i;
    }
}
```

## 평소에는
직접 `MoveNext`를 부를 일은 거의 없고, `foreach`와 `IEnumerable<T>`(다음 문서)를 씁니다.
