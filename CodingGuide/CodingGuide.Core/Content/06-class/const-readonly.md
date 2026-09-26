---
id: const-readonly
title: const와 readonly의 차이
category: 클래스
order: 617
summary: 컴파일 시점에 정해지는 상수 const와, 생성자에서 한 번 정할 수 있는 readonly 필드의 차이를 설명합니다.
keywords: const, readonly, 상수, 읽기 전용, 변경 불가, static readonly, 컴파일 타임 상수, 런타임 상수
lesson: const_readonly
related: static-members, property-setter-access, enum-type
---
## 핵심 차이
| | const | readonly |
|---|---|---|
| 값이 정해지는 때 | **컴파일할 때** | **실행 중 (선언 또는 생성자)** |
| 생성자에서 대입 | 불가 | 가능 |
| 사용 가능한 타입 | 숫자, bool, char, string, enum | 모든 타입 |
| static 여부 | 자동으로 static | 인스턴스별 (static readonly도 가능) |
| 지역 변수 | 가능 (`const string name = "John";`) | 불가 |

```csharp
class MyClass
{
    const double PI_CONST = 3.14159;
    readonly double pi_readonly = 3.14159;

    public MyClass()
    {
        // PI_CONST = 3.14;  // 오류: const 는 절대 변경 불가
        pi_readonly = 3.14;  // OK: 생성자 안에서는 가능
    }

    void MyMethod()
    {
        // pi_readonly = 1;  // 오류: 생성자 밖에서는 불가
        const string name = "John"; // 지역 상수
    }
}
```

## 언제 무엇을 쓰나요?
- 절대 바뀌지 않는 값(수학 상수, 고정 문자열): `const`
- 실행 시점에 정해지는 값(설정값, `DateTime.Now`, 객체): `readonly` 또는 `static readonly`
```csharp
static readonly HttpClient Http = new();
static readonly DateTime StartedAt = DateTime.Now;
```

## 주의할 점
- `readonly` 필드가 List 같은 참조 타입이면 **변수 재대입만 막을 뿐, 안의 내용(Add/Remove)은 바뀔 수 있습니다.**
- public const 값을 바꾸면 그 값을 사용하는 다른 어셈블리도 다시 컴파일해야 합니다(값이 복사되어 박히기 때문).
