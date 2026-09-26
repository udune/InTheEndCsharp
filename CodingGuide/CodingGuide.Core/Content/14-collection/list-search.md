---
id: list-search
title: List에서 요소 검색 (Contains, IndexOf, Find, FindAll)
category: 열거자와 컬렉션
order: 1407
summary: 값이 있는지, 몇 번째인지, 조건에 맞는 첫 요소/모든 요소를 찾는 List 검색 메서드들입니다.
keywords: 검색, 찾기, 포함 여부, 있는지 확인, Contains, IndexOf, Find, FindAll, FindIndex, Exists, 대소문자 무시, StringComparer
lesson: 컬렉션_List_요소검색
related: delegate-predicate, linq-method-where, list-create, hashset
---
## 핵심
```csharp
var fruits = new List<string> { "apple", "orange", "pear" };

bool has = fruits.Contains("apple");                                  // True
bool hasIgnoreCase = fruits.Contains("Apple", StringComparer.OrdinalIgnoreCase); // True (LINQ 버전)
int index = fruits.IndexOf("apple");                                   // 0 (없으면 -1)

string? first = fruits.Find(f => f.StartsWith("o"));                   // "orange" (없으면 null/default)
List<string> all = fruits.FindAll(f => f.Contains('e'));               // apple, orange, pear
int idx = fruits.FindIndex(f => f.Length > 5);                         // 1
bool any = fruits.Exists(f => f.EndsWith("r"));                        // True
```

## LINQ로 같은 일 하기
```csharp
fruits.Any(f => f.StartsWith("o"));         // Exists
fruits.FirstOrDefault(f => f.StartsWith("o")); // Find
fruits.Where(f => f.Contains('e')).ToList(); // FindAll
fruits.Count(f => f.Length > 4);            // 조건에 맞는 개수
```

## 성능 팁
- `List.Contains`는 처음부터 끝까지 비교합니다(O(n)). **포함 여부를 자주 확인**한다면 `HashSet<T>`를, **키로 찾는다면** `Dictionary<K,V>`를 쓰세요.
