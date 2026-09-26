---
id: null-coalescing
title: Nullable 타입과 null 관련 연산자 (?, ??, ??=, ?.)
category: 연산자
order: 206
summary: 값 타입에 null을 허용하는 int?와 null일 때 기본값을 주는 ??, null 조건 연산자 ?. 를 설명합니다.
keywords: null, 널, nullable, int?, ??, ??=, ?., HasValue, Value, 기본값, null 병합, null 체크, null 검사
lesson: Null병합연산자
related: error-null-reference, assignment-operators, logical-operators
---
## 핵심
```csharp
int? a = null;          // int 는 원래 null 불가. ? 를 붙이면 가능
int b = a ?? 3;         // a 가 null 이면 3

Console.WriteLine(a.HasValue);  // False
Console.WriteLine(b);           // 3
```

## null 관련 연산자 모음
- `??` : 왼쪽이 null이면 오른쪽 → `name ?? "익명"`
- `??=` : null일 때만 대입 → `list ??= new();`
- `?.` : 앞이 null이면 멈추고 null 반환 → `user?.Address?.City`
- `?[]` : 인덱서 버전 → `arr?[0]`
- `!` : "null 아님"을 컴파일러에 단언(경고만 없앰) → `name!.Length`

```csharp
string? city = user?.Address?.City ?? "알 수 없음";
int length = text?.Length ?? 0;
```

## 참조 타입의 nullable (C# 8+)
프로젝트에 `<Nullable>enable</Nullable>`이 켜져 있으면 `string`은 null 불가, `string?`은 null 가능으로 구분하고 경고를 줍니다. 경고(CS8600, CS8602 등)를 무시하지 말고 null 검사를 추가하세요.

## 주의할 점
- `int?`의 `.Value`는 null일 때 `InvalidOperationException`을 던집니다. `GetValueOrDefault()`나 `??`를 쓰세요.
