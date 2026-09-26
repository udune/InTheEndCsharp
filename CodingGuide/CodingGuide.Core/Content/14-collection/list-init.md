---
id: list-init
title: List 초기화 방법 (컬렉션 이니셜라이저, 컬렉션 식 [])
category: 열거자와 컬렉션
order: 1404
summary: List를 만들면서 값을 한 번에 채우는 컬렉션 이니셜라이저 { }와 C# 12 컬렉션 식 [ ], 스프레드(..)를 설명합니다.
keywords: 리스트 초기화, 컬렉션 초기화, 컬렉션 이니셜라이저, 컬렉션 식, collection expression, [], 스프레드, spread, .., 빈 리스트
lesson: 컬렉션_List_초기화
related: list-create, array-declaration, dictionary
---
## 핵심
```csharp
var list1 = new List<string> { "a", "b", "c" };   // 컬렉션 이니셜라이저
List<string> list2 = ["가", "나", "다"];           // C# 12 컬렉션 식 (권장)
List<int> empty = [];                              // 빈 리스트
```

## 컬렉션 식의 장점
- 배열, List, Span, `IEnumerable<T>` 등 **왼쪽 타입에 맞춰** 만들어 줍니다.
- `..`(스프레드)로 다른 컬렉션을 펼쳐 넣을 수 있습니다.
```csharp
int[] a = [1, 2];
List<int> b = [3, 4];
List<int> all = [..a, ..b, 5];      // [1, 2, 3, 4, 5]
IEnumerable<string> names = ["x", "y"];
```

## 용량 지정
개수를 미리 알면 용량을 지정해 재할당을 줄일 수 있습니다.
```csharp
var big = new List<int>(capacity: 10_000);
```
