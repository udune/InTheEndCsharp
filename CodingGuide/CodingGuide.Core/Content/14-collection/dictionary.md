---
id: dictionary
title: Dictionary<TKey, TValue> - 키로 값 찾기
category: 열거자와 컬렉션
order: 1412
summary: 키-값 쌍을 저장하고 키로 빠르게 찾는 Dictionary의 추가, 수정, 조회(TryGetValue), 삭제, 순회 방법입니다.
keywords: 딕셔너리, Dictionary, 사전, 해시맵, map, 키 값, key value, TryGetValue, ContainsKey, Add, Remove, Keys, Values, KeyValuePair, KeyNotFoundException, 개수 세기, 빈도
lesson: 컬렉션_Dictionary
related: method-out, error-key-not-found, hashset, linq-method-groupby
---
## 핵심
```csharp
var fruit = new Dictionary<string, string>();
fruit.Add("apple", "사과");          // 추가 (키가 이미 있으면 예외!)
fruit.Add("banana", "바나나");

var fruit2 = new Dictionary<string, string>   // 초기화
{
    ["apple"] = "사과",
    ["cherry"] = "체리",
};

fruit["apple"] = "애플";            // 수정 (없으면 추가)
fruit["lemon"] = "레몬";            // 추가
string name = fruit["apple"];       // 조회 (없으면 KeyNotFoundException!)
fruit.Remove("banana");             // 삭제
```

## 안전하게 조회하기
```csharp
if (fruit.TryGetValue("grape", out string? value))
    Console.WriteLine(value);
else
    Console.WriteLine("없음");

bool exists = fruit.ContainsKey("apple");
string v = fruit.GetValueOrDefault("grape", "기본값");
fruit.TryAdd("apple", "x");        // 이미 있으면 false, 예외 없음
```

## 순회
```csharp
foreach (var (key, val) in fruit)           // 분해
    Console.WriteLine($"{key} = {val}");

foreach (KeyValuePair<string, string> kv in fruit)
    Console.WriteLine($"{kv.Key} = {kv.Value}");

foreach (var key in fruit.Keys) { }
foreach (var val in fruit.Values) { }
```

## 활용: 개수 세기
```csharp
var counts = new Dictionary<string, int>();
foreach (var word in words)
    counts[word] = counts.GetValueOrDefault(word) + 1;
```

## 주의할 점
- 키는 **중복 불가**입니다. `Add`로 같은 키를 넣으면 `ArgumentException`.
- 문자열 키를 대소문자 구분 없이 쓰려면: `new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)`
- 순서가 보장되지 않는다고 생각하세요. 정렬이 필요하면 `SortedDictionary`나 `OrderBy`를 씁니다.
