---
id: as-operator
title: as 연산자 - 실패하면 null
category: 타입 변환
order: 903
summary: 변환에 실패해도 예외 대신 null을 돌려주는 as 연산자와 사용 시 주의점입니다.
keywords: as, as 연산자, 안전한 캐스팅, 변환 실패 null, 참조 타입 변환, nullable 변환
lesson: 타입변환
related: is-operator, explicit-cast, null-coalescing
---
## 핵심
```csharp
object obj = "C# Programming";

string? str = obj as string;   // 성공 → "C# Programming"
int? num = obj as int?;        // 실패 → null (예외 없음)

if (str != null) Console.WriteLine(str);
if (num != null) Console.WriteLine(num);
```

## (캐스팅) vs as
| | `(T)obj` | `obj as T` |
|---|---|---|
| 실패하면 | `InvalidCastException` | `null` |
| 쓸 수 있는 타입 | 모든 타입 | 참조 타입, nullable 값 타입(`int?`)만 |

## 요즘은 is 패턴을 더 많이 씁니다
`as` + null 검사를 한 줄로 줄일 수 있습니다.
```csharp
if (obj is string s)
    Console.WriteLine(s.Length);
```
