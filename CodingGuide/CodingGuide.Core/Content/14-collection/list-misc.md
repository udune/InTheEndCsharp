---
id: list-misc
title: List 기타 메서드 (Count, ToArray, Clear, AsReadOnly)
category: 열거자와 컬렉션
order: 1409
summary: 개수 확인, 배열 변환, 전체 비우기 등 List의 나머지 유용한 멤버와 읽기 전용으로 공개하는 방법입니다.
keywords: Count, ToArray, Clear, AsReadOnly, 배열로 변환, 리스트를 배열로, 개수, Capacity, IReadOnlyList, 읽기 전용 리스트
lesson: 컬렉션_List_기타메서드
related: list-create, list-remove, property-setter-access
---
## 핵심
```csharp
var list = new List<int> { 3, 100, 5, -1, 20 };

int[] arr = list.ToArray();      // 배열로 복사
Console.WriteLine(list.Count);   // 5
list.Add(33);
Console.WriteLine(list.Count);   // 6
list.Clear();
Console.WriteLine(list.Count);   // 0

List<int> fromArray = arr.ToList();       // 배열 → List (LINQ)
List<int> fromArray2 = new List<int>(arr);
```

## 외부에 읽기 전용으로 공개하기
클래스 내부 List를 그대로 공개하면 밖에서 `Add`/`Clear` 할 수 있습니다.
```csharp
class Cart
{
    private readonly List<string> _items = new();
    public IReadOnlyList<string> Items => _items;   // 읽기만 가능
    public void Add(string item) => _items.Add(item);
}
```

## Count vs Count()
- `list.Count` : 속성, 바로 개수를 알려줌 (빠름)
- `list.Count(x => x > 0)` : LINQ 메서드, 조건에 맞는 개수를 **세어서** 반환
