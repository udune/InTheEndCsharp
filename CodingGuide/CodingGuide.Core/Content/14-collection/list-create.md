---
id: list-create
title: List<T> 만들기, 추가, 요소 접근
category: 열거자와 컬렉션
order: 1403
summary: 크기가 자동으로 늘어나는 동적 배열 List<T>를 만들고 Add로 추가하고 인덱스로 읽고 쓰는 기본 사용법입니다.
keywords: List, List<T>, 리스트, 동적 배열, Add, 추가, 인덱스, Count, 개수, 가변 크기, ArrayList
lesson: 컬렉션_List_정의_생성_요소접근
related: list-init, list-insert, list-remove, list-search, list-sort, array-declaration
---
## 핵심
```csharp
List<string> list = new List<string>();   // 빈 리스트
list.Add("a");
list.Add("b");
list.Add("c");

list[1] = "f";                   // 인덱스로 쓰기
Console.WriteLine(list[1]);      // f
Console.WriteLine(list.Count);   // 3 (배열의 Length 대신 Count)

foreach (var item in list)
    Console.WriteLine(item);
```

## 배열과의 차이
| | 배열 `T[]` | `List<T>` |
|---|---|---|
| 크기 | 고정 | 자동으로 늘어남 |
| 개수 | `Length` | `Count` |
| 추가/삭제 | 불가 | `Add`, `Insert`, `Remove` |

## 자주 쓰는 메서드 한눈에
- 추가: `Add`, `AddRange`, `Insert`, `InsertRange`
- 삭제: `Remove`, `RemoveAt`, `RemoveAll`, `Clear`
- 검색: `Contains`, `IndexOf`, `Find`, `FindAll`, `Exists`
- 정렬: `Sort`, `Reverse`
- 변환: `ToArray`, `AsReadOnly`

## 주의할 점
- `list[list.Count]`처럼 범위 밖을 접근하면 `ArgumentOutOfRangeException`이 납니다.
- `ArrayList`(제네릭이 아닌 옛 컬렉션)는 쓰지 마세요. 박싱이 생기고 타입 검사가 안 됩니다.
