---
id: delegate-comparison
title: Comparison 델리게이트 - 정렬 기준 정하기
category: 델리게이트와 이벤트
order: 1209
summary: 두 값을 비교해 음수/0/양수를 돌려주는 Comparison<T>와 CompareTo, List.Sort에 정렬 기준을 넘기는 방법입니다.
keywords: Comparison, Comparison<T>, CompareTo, 비교, 정렬 기준, Sort, 내림차순, 오름차순, 음수 0 양수, IComparer
lesson: 델리게이트_Comparison
related: list-sort, linq-method-orderby, generic-constraint-interface
---
## 핵심
`Comparison<T>`는 `(T x, T y) => int` 모양입니다.
- **음수**: x가 앞 (x < y)
- **0**: 같음
- **양수**: x가 뒤 (x > y)

```csharp
int Compare(int x, int y) => x.CompareTo(y);

Comparison<int> comparison = Compare;
Console.WriteLine(comparison(5, 3));   // 1
Console.WriteLine(comparison(3, 3));   // 0
Console.WriteLine(comparison(3, 5));   // -1
```

## List.Sort에 정렬 기준 넘기기
```csharp
List<Person> people = [...];

people.Sort((a, b) => a.Age.CompareTo(b.Age));      // 나이 오름차순
people.Sort((a, b) => b.Age.CompareTo(a.Age));      // 나이 내림차순 (a, b 순서를 바꿈)
people.Sort((a, b) =>
{
    int byAge = a.Age.CompareTo(b.Age);
    return byAge != 0 ? byAge : string.Compare(a.Name, b.Name, StringComparison.Ordinal);
});                                                  // 나이, 같으면 이름
```

## 팁
- 새 리스트를 만들어도 된다면 LINQ의 `OrderBy(p => p.Age).ThenBy(p => p.Name)`가 더 읽기 쉽습니다.
- `a - b`로 비교값을 만들면 큰 수에서 오버플로가 날 수 있으니 `CompareTo`를 쓰세요.
