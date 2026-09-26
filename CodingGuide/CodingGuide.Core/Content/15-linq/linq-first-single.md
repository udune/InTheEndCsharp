---
id: linq-first-single
title: 하나만 찾기 - First, FirstOrDefault, Single, Last, ElementAt
category: LINQ
order: 1551
summary: 조건에 맞는 요소 하나를 꺼내는 First/FirstOrDefault/Single/SingleOrDefault/Last의 차이와 예외 상황입니다.
keywords: First, FirstOrDefault, Single, SingleOrDefault, Last, LastOrDefault, ElementAt, 하나 찾기, 첫 번째, 마지막, 없으면 null, Sequence contains no elements, 시퀀스에 요소가 없습니다, 한 개만
related: linq-method-where, list-search, error-invalid-operation
---
## 한눈에 비교
| 메서드 | 0개일 때 | 2개 이상일 때 |
|---|---|---|
| `First()` | **예외** | 첫 번째 |
| `FirstOrDefault()` | `default` (null/0) | 첫 번째 |
| `Single()` | **예외** | **예외** |
| `SingleOrDefault()` | `default` | **예외** |
| `Last()` / `LastOrDefault()` | 예외 / default | 마지막 |

```csharp
var bob = users.FirstOrDefault(u => u.Name == "Bob");   // 없으면 null
if (bob is null) Console.WriteLine("없음");

var admin = users.Single(u => u.Role == "Admin");       // 정확히 1명이어야 함
var last = logs.Last();
var third = list.ElementAtOrDefault(2);                  // 인덱스 2 (없으면 default)

// 기본값 지정 (.NET 6+)
var user = users.FirstOrDefault(u => u.Id == id, new User { Name = "게스트" });
```

## 어떤 걸 써야 하나요?
- 대부분 **`FirstOrDefault` + null 검사**가 안전합니다.
- "반드시 있어야 한다, 없으면 버그다"라면 `First` (예외가 문제를 빨리 드러냄)
- "반드시 하나뿐이어야 한다"(ID로 조회 등)면 `Single`/`SingleOrDefault`

## 주의할 점
- 값 타입 리스트(`List<int>`)의 `FirstOrDefault`는 없을 때 **0**을 돌려줍니다. 실제 0과 구분이 필요하면 `Any`로 먼저 확인하세요.
- "Sequence contains no elements"(시퀀스에 요소가 없습니다) 예외는 대부분 `First`/`Single`/`Max`/`Average`를 빈 컬렉션에 쓴 경우입니다.
