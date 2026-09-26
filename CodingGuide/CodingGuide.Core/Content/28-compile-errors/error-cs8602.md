---
id: error-cs8602
title: CS8602 / CS8600 / CS8618 - nullable 경고 (null 가능 참조에 대한 역참조)
category: 컴파일 오류
order: 2812
summary: Nullable이 켜진 프로젝트에서 null일 수 있는 값을 검사 없이 사용하거나, null 불가 속성을 초기화하지 않을 때 나는 경고와 올바른 해결 방법입니다.
keywords: CS8602, CS8600, CS8618, CS8604, CS8603, null 가능 참조에 대한 역참조입니다, Dereference of a possibly null reference, null 리터럴 또는 가능한 null 값을 null을 허용하지 않는 형식으로 변환하는 중입니다, 생성자를 종료할 때 null이 아닌 값을 포함해야 합니다, Non-nullable property must contain a non-null value when exiting constructor, required, nullable 경고, 노란 줄, null 경고
related: null-coalescing, error-null-reference, properties
---
## CS8602 - null 가능 참조에 대한 역참조입니다
```csharp
string? s = GetName();       // null 일 수 있음
Console.WriteLine(s.Length); // 경고: 실행 시 NullReferenceException 가능

// 해결
if (s != null) Console.WriteLine(s.Length);
Console.WriteLine(s?.Length ?? 0);
if (s is null) return;
```

## CS8600 - null 값을 null을 허용하지 않는 형식으로 변환하는 중입니다
```csharp
string t = null;                  // 경고
string? t2 = null;                // 해결: null 이 올 수 있으면 ? 를 붙임
string t3 = dict.GetValueOrDefault("k") ?? "";
```

## CS8618 - 생성자를 종료할 때 null이 아닌 값을 포함해야 합니다
- `null을 허용하지 않는 속성 'NotNull'은(는) 생성자를 종료할 때 null이 아닌 값을 포함해야 합니다. 'required' 한정자를 추가하거나 속성을(를) nullable로 선언하는 것이 좋습니다.`
```csharp
public string Name { get; set; }                  // 경고
public string Name { get; set; } = "";            // 1) 기본값
public required string Name { get; set; }         // 2) 만들 때 반드시 설정
public string? Name { get; set; }                 // 3) null 허용
```

## ! 연산자는 최후의 수단
`s!.Length`는 "null 아님을 내가 보장한다"는 표시로 **경고만 없앱니다**. 실제로 null이면 여전히 예외가 납니다. 확실할 때만 쓰세요.

## 왜 경고를 고쳐야 하나요?
이 경고들은 실행 중 `NullReferenceException`이 날 수 있는 곳을 **미리** 알려줍니다. 경고를 0개로 유지하는 습관이 가장 좋은 예방책입니다.
