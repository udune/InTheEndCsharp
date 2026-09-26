---
id: list-remove
title: List에서 요소 삭제 (Remove, RemoveAt, RemoveAll, Clear)
category: 열거자와 컬렉션
order: 1406
summary: 값으로 지우는 Remove, 위치로 지우는 RemoveAt, 조건으로 지우는 RemoveAll, 전부 지우는 Clear와 순회 중 삭제 주의점입니다.
keywords: 삭제, 제거, 지우기, Remove, RemoveAt, RemoveAll, RemoveRange, Clear, 조건으로 삭제, 순회 중 삭제, foreach 삭제 오류
lesson: 컬렉션_List_요소삭제
related: list-create, delegate-predicate, error-collection-modified
---
## 핵심
```csharp
var list = new List<string> { "a", "b", "c", "a" };

list.Remove("a");                 // 처음 나오는 "a" 하나만 삭제 → b, c, a  (성공 여부 bool 반환)
list.RemoveAt(0);                 // 인덱스 0 삭제 → c, a
list.RemoveAll(s => s == "c" || s == "a"); // 조건에 맞는 것 모두 삭제, 삭제 개수 반환
list.RemoveRange(0, 2);           // 인덱스 0 부터 2개
list.Clear();                     // 전부 삭제
```
> 예제 코드의 `str == "c" | str == "a"`는 `|`(비트 OR)를 썼습니다. bool에도 동작하지만 단락 평가를 하는 `||`가 일반적입니다.

## 순회하면서 삭제하면 오류!
```csharp
foreach (var item in list)
    if (item == "a") list.Remove(item);   // InvalidOperationException: 컬렉션이 수정되었습니다
```
해결 방법:
```csharp
list.RemoveAll(item => item == "a");          // 1) RemoveAll (가장 좋음)

for (int i = list.Count - 1; i >= 0; i--)     // 2) 뒤에서부터 인덱스로
    if (list[i] == "a") list.RemoveAt(i);

foreach (var item in list.ToList())           // 3) 복사본을 순회
    if (item == "a") list.Remove(item);
```

## 주의할 점
- `Remove`는 **첫 번째 하나만** 지웁니다. 모두 지우려면 `RemoveAll`.
- 클래스 객체를 `Remove(obj)`로 지울 때는 `Equals`로 비교합니다(기본은 같은 인스턴스인지).
