---
id: delegate-predicate
title: Predicate 델리게이트 - 조건 검사
category: 델리게이트와 이벤트
order: 1208
summary: 값을 하나 받아 bool을 돌려주는 Predicate<T>와, List.Find/FindAll/RemoveAll 등에서의 사용법입니다.
keywords: Predicate, Predicate<T>, 조건, 참 거짓, bool 반환, Find, FindAll, RemoveAll, Exists, 조건 검사
lesson: 델리게이트_Predicate
related: delegate-func, list-search, list-remove, linq-method-where
---
## 핵심
`Predicate<T>`는 `Func<T, bool>`과 모양이 같습니다. "이 값이 조건에 맞는가?"를 표현합니다.

```csharp
bool IsGreaterThanZero(int value) => value > 0;

Predicate<int> predicate = IsGreaterThanZero;
Console.WriteLine(predicate(2));    // True
Console.WriteLine(predicate(-1));   // False
```

## List 메서드에서 자주 사용
```csharp
List<int> nums = [3, -1, 8, 0, -5];

int first = nums.Find(n => n > 5);            // 8
List<int> positives = nums.FindAll(n => n > 0); // [3, 8]
bool hasNegative = nums.Exists(n => n < 0);   // True
int removed = nums.RemoveAll(n => n < 0);     // 2개 삭제
```

## Predicate vs Func<T, bool>
- `List<T>`, `Array`의 옛 메서드(`Find`, `FindAll`, `RemoveAll`)는 `Predicate<T>`를 받습니다.
- LINQ(`Where`, `Any`, `First`)는 `Func<T, bool>`을 받습니다.
- 둘은 모양이 같지만 **서로 다른 타입**이라 변수끼리 직접 대입은 안 됩니다. 람다로 넘기면 어느 쪽이든 자동으로 맞춰집니다.
