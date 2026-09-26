---
id: method-ref
title: ref 키워드 - 변수 자체를 넘기기
category: 클래스
order: 610
summary: 메서드 안에서 호출한 쪽의 변수 자체를 바꿀 수 있게 하는 ref 매개변수를 설명합니다.
keywords: ref, 참조 전달, call by reference, 원본 변경, swap, 교환, in, ref readonly
lesson: 메서드_ref키워드
related: method-out, method-param-semantics
---
## 핵심
`ref`를 붙이면 복사본이 아니라 **변수 그 자체(별명)** 가 전달됩니다. 선언과 호출 양쪽에 모두 `ref`를 씁니다.

```csharp
void Change(ref string text)
{
    text = "b";
}

string s = "a";
Change(ref s);
Console.WriteLine(s); // "b" ← 바뀜
```

## 대표 예: 두 값 바꾸기
```csharp
void Swap(ref int x, ref int y)
{
    int temp = x;
    x = y;
    y = temp;
}

int a = 1, b = 2;
Swap(ref a, ref b);   // a=2, b=1

// 튜플을 쓰면 ref 없이도 가능
(a, b) = (b, a);
```

## ref / out / in 비교
| 키워드 | 호출 전 초기화 | 메서드 안에서 대입 | 용도 |
|---|---|---|---|
| `ref` | 필수 | 선택 | 읽고 **수정** |
| `out` | 불필요 | **필수** | 결과를 추가로 돌려줌 |
| `in` | 필수 | 불가(읽기 전용) | 큰 struct를 복사 없이 전달 |

## 주의할 점
- 반환값을 여러 개 돌려주려고 ref를 남용하기보다는 **튜플이나 클래스**로 반환하는 것이 읽기 쉽습니다.
