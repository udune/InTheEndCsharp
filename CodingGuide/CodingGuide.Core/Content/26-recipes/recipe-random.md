---
id: recipe-random
title: 랜덤(난수) - 무작위 숫자, 섞기, 뽑기, GUID
category: 실무 레시피
order: 2611
summary: Random.Shared로 범위 내 정수/실수 난수 만들기, 리스트 섞기(Shuffle), 무작위로 하나 뽑기, 로또 번호, 보안용 난수와 GUID입니다.
keywords: 랜덤, random, 난수, 무작위, 임의의 수, Random.Shared, Next, 주사위, 섞기, 셔플, shuffle, 무작위 뽑기, 로또, 중복 없는 난수, GetItems, GUID, 고유 ID, 비밀번호 생성, RandomNumberGenerator
related: linq-take-skip, hashset
---
## 기본
```csharp
int dice = Random.Shared.Next(1, 7);          // 1 ~ 6 (끝 값은 포함 안 됨!)
int any = Random.Shared.Next(100);            // 0 ~ 99
double d = Random.Shared.NextDouble();        // 0.0 이상 1.0 미만
bool coin = Random.Shared.Next(2) == 0;
```
> `new Random()`을 반복문 안에서 계속 만들지 말고 `Random.Shared`(.NET 6+)를 쓰세요.

## 섞기, 뽑기
```csharp
int[] cards = Enumerable.Range(1, 10).ToArray();
Random.Shared.Shuffle(cards);                          // 제자리 섞기 (.NET 8+)

string pick = names[Random.Shared.Next(names.Count)];  // 하나 뽑기
string[] three = Random.Shared.GetItems(names.ToArray(), 3);   // 3개 (중복 가능, .NET 8+)

// 중복 없이 N개 뽑기 (로또)
int[] lotto = Enumerable.Range(1, 45).OrderBy(_ => Random.Shared.Next()).Take(6).Order().ToArray();
```

## 재현 가능한 난수 (테스트용)
```csharp
var rng = new Random(42);   // 시드가 같으면 항상 같은 순서
```

## 보안이 필요한 경우 (비밀번호, 토큰)
```csharp
using System.Security.Cryptography;
int secure = RandomNumberGenerator.GetInt32(0, 100);
string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(16));
```

## 고유 ID
```csharp
Guid id = Guid.NewGuid();              // 3f2504e0-4f89-11d3-9a0c-0305e82c3301
Guid sortable = Guid.CreateVersion7(); // 시간 순 정렬 가능 (.NET 9+)
```
