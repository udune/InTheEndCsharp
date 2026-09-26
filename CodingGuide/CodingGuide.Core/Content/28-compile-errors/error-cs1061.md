---
id: error-cs1061
title: CS1061 - 'X'에는 'Y'에 대한 정의가 포함되어 있지 않습니다
category: 컴파일 오류
order: 2805
summary: 해당 타입에 없는 메서드나 속성을 호출할 때(오타, 변수 타입이 부모/인터페이스, LINQ using 누락, Task를 await 안 함) 나는 오류입니다.
keywords: CS1061, 정의가 포함되어 있지 않고, 액세스 가능한 확장 메서드, does not contain a definition for, no accessible extension method, 메서드가 없음, 속성이 없음, 확장 메서드 없음, using System.Linq, await 안 함
related: error-cs0103, error-cs0246, extension-methods, interface-multiple
---
## 메시지
- `'string'에는 'NoSuchMethod'에 대한 정의가 포함되어 있지 않고, 'string' 형식의 첫 번째 인수를 허용하는 액세스 가능한 확장 메서드 'NoSuchMethod'이(가) 없습니다. using 지시문 또는 어셈블리 참조가 있는지 확인하세요.`
- 'string' does not contain a definition for 'NoSuchMethod' and no accessible extension method ...

## 흔한 원인
1. **오타/대소문자**: `list.count` → `list.Count`, `str.length` → `str.Length`
2. **배열과 List 혼동**: 배열은 `Length`, List는 `Count`. 배열에는 `Add`가 없습니다.
3. **변수 타입이 부모/인터페이스**
```csharp
IFlyable f = new Bird();
f.MakeSound();       // CS1061: IFlyable 에는 MakeSound 가 없음 → 필요한 타입으로 선언하거나 캐스팅
```
4. **확장 메서드의 using 누락**: `Where`, `Select`, `ToList` → `using System.Linq;`
5. **await을 빠뜨림**
```csharp
var users = GetUsersAsync();     // Task<List<User>>
users.Count;                     // CS1061: Task 에는 Count 가 없음
var users2 = await GetUsersAsync();
```
6. **LINQ 결과는 IEnumerable**: `query.Count` 대신 `query.Count()` 또는 `.ToList()` 후 사용
7. **nullable 값 타입**: `int? n; n.ToString("N0")` → `n.Value.ToString("N0")` 또는 `n?.ToString("N0")`
