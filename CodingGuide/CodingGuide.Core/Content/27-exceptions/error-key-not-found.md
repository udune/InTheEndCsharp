---
id: error-key-not-found
title: KeyNotFoundException - 지정한 키가 사전에 없습니다
category: 흔한 예외
order: 2706
summary: Dictionary에 없는 키를 인덱서 dict[key]로 조회할 때 나는 예외와, TryGetValue/ContainsKey/GetValueOrDefault로 안전하게 조회하는 방법입니다. 같은 키를 Add할 때의 ArgumentException도 다룹니다.
keywords: KeyNotFoundException, 지정한 키가 사전에 없습니다, The given key was not present in the dictionary, 키가 없음, 딕셔너리 오류, TryGetValue, ContainsKey, 동일한 키를 사용하는 항목이 이미 추가되었습니다, An item with the same key has already been added, ArgumentException 중복 키
related: dictionary, method-out
---
## 메시지
- 지정한 키가 사전에 없습니다. / The given key 'xxx' was not present in the dictionary.

```csharp
var dict = new Dictionary<string, int> { ["apple"] = 1 };
int n = dict["banana"];     // ← 예외!
```

## 해결
```csharp
if (dict.TryGetValue("banana", out int value))       // 권장
    Console.WriteLine(value);

int v2 = dict.GetValueOrDefault("banana");           // 없으면 0
int v3 = dict.GetValueOrDefault("banana", -1);       // 없으면 -1
if (dict.ContainsKey("banana")) { }
```

## 대소문자, 공백 때문에 못 찾는 경우
```csharp
var dict2 = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);   // "Apple" == "apple"
dict[key.Trim()];                                                            // 앞뒤 공백 제거
```

## 반대: 같은 키를 두 번 Add
- 동일한 키를 사용하는 항목이 이미 추가되었습니다. / An item with the same key has already been added.
```csharp
dict.Add("apple", 2);        // ← 예외! ArgumentException
dict["apple"] = 2;           // 덮어쓰기 (예외 없음)
dict.TryAdd("apple", 2);     // 있으면 false 반환
```
`ToDictionary`로 만들 때 원본에 중복 키가 있어도 같은 예외가 납니다 → `GroupBy`로 먼저 묶거나 `DistinctBy`로 중복을 제거하세요.
