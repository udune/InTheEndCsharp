---
id: list-insert
title: List에 요소 삽입 (Insert, InsertRange, AddRange)
category: 열거자와 컬렉션
order: 1405
summary: 원하는 위치에 요소를 끼워 넣는 Insert/InsertRange와 뒤에 여러 개를 붙이는 AddRange를 설명합니다.
keywords: 삽입, 끼워넣기, Insert, InsertRange, AddRange, 중간에 추가, 맨 앞에 추가, 리스트 합치기, 두 리스트 합치기
lesson: 컬렉션_List_요소삽입
related: list-create, list-remove, linq-intro
---
## 핵심
```csharp
var list = new List<string> { "a", "b", "c" };
List<string> other = ["가", "나", "다"];

list.Insert(1, "z");          // 인덱스 1 에 삽입 → a, z, b, c
list.InsertRange(2, other);   // 인덱스 2 에 여러 개 → a, z, 가, 나, 다, b, c
list.Insert(0, "처음");        // 맨 앞
list.AddRange(other);          // 맨 뒤에 여러 개
```

## 두 리스트 합치기
```csharp
var merged = new List<string>(list1);   // 복사본을 만들고
merged.AddRange(list2);                 // 뒤에 붙이기

var merged2 = list1.Concat(list2).ToList();   // LINQ
List<string> merged3 = [..list1, ..list2];    // 컬렉션 식
```

## 주의할 점
- `Insert`는 뒤의 요소를 모두 한 칸씩 밀기 때문에 **큰 리스트의 앞쪽 삽입은 느립니다**. 앞뒤로 자주 넣고 빼면 `LinkedList<T>`나 `Queue`/`Stack`을 고려하세요.
- 인덱스가 `Count`보다 크면 `ArgumentOutOfRangeException`입니다. (`Count`와 같으면 맨 뒤에 추가)
