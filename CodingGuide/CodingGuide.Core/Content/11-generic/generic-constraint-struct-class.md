---
id: generic-constraint-struct-class
title: 제네릭 제약 조건 where T - class / struct
category: 제네릭
order: 1102
summary: where T : class, where T : struct 제약으로 T에 참조 타입 또는 값 타입만 올 수 있게 제한하는 방법입니다.
keywords: 제약 조건, 제약조건, where, where T : class, where T : struct, notnull, unmanaged, 참조 타입만, 값 타입만, CS0452
lesson: 제네릭_제약조건_struct_class
related: generic-constraint-new, generic-constraint-classtype, generic-constraint-interface, generic-basics
---
## 핵심
`where` 절로 T에 올 수 있는 타입을 제한합니다.

```csharp
void Swap<T>(ref T a, ref T b) where T : class   // 참조 타입만 허용
{
    T temp = a; a = b; b = temp;
}

string s1 = "aaa", s2 = "bbb";
Animal x = new Dog(), y = new Cat();
Swap(ref s1, ref s2);   // OK: string 은 참조 타입
Swap(ref x, ref y);     // OK

int a = 1, b = 2;
// Swap(ref a, ref b);  // 오류 CS0452: int 는 참조 타입이 아님
```

## 제약 조건 종류
| 제약 | 의미 |
|---|---|
| `where T : class` | 참조 타입 |
| `where T : struct` | null 불가능한 값 타입 |
| `where T : new()` | 매개변수 없는 public 생성자가 있어야 함 |
| `where T : 부모클래스` | 그 클래스이거나 자식 |
| `where T : 인터페이스` | 그 인터페이스를 구현 |
| `where T : notnull` | null 불가 타입 |
| `where T : unmanaged` | 참조를 포함하지 않는 값 타입 (int, 단순 struct) |

여러 개는 쉼표로 이어 쓰며, `new()`는 **맨 마지막**에 둡니다: `where T : class, IDisposable, new()`

## 왜 제한하나요?
제약을 걸면 그 능력을 **T에 대해 사용할 수 있게** 됩니다. 예: `where T : IComparable<T>` 이면 `a.CompareTo(b)` 호출 가능.
