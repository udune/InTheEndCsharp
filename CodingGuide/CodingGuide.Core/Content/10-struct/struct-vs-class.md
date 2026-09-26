---
id: struct-vs-class
title: 구조체(struct)와 클래스의 차이 - 값 타입 vs 참조 타입
category: 구조체
order: 1001
summary: struct는 값 타입이라 대입/전달 시 복사되고, class는 참조 타입이라 같은 객체를 공유하는 차이와 선택 기준을 설명합니다.
keywords: 구조체, struct, 값 타입, 참조 타입, 복사, class 차이, 스택, 힙, record struct, readonly struct
lesson: 구조체_class와비교
related: method-param-semantics, record-type, class-define, boxing-unboxing
---
## 핵심
```csharp
struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

Point p = new Point { X = 10, Y = 20 };

void ChangePoint(Point point)   // 복사본이 전달됨
{
    point.X = 100;
    point.Y = 200;
}

ChangePoint(p);
Console.WriteLine($"{p.X},{p.Y}");   // 10,20 ← 원본 그대로
```
`Point`가 **class**였다면 출력은 `100,200`이 됩니다.

## 비교
| | struct | class |
|---|---|---|
| 종류 | 값 타입 | 참조 타입 |
| 대입/전달 | **값 전체 복사** | 참조(주소) 복사 → 같은 객체 공유 |
| null | 불가 (`Point?`는 가능) | 가능 |
| 상속 | 불가 (인터페이스 구현은 가능) | 가능 |
| `==` 기본 동작 | 정의 안 됨 (직접 구현) | 같은 객체인지 비교 |

## 언제 struct를 쓰나요?
- **작고(16바이트 이하 권장), 변하지 않는** 값 묶음: 좌표, 색상, 금액+통화, 날짜(`DateTime`도 struct)
- 대부분의 경우는 class를 쓰면 됩니다.

## 권장 형태
```csharp
readonly record struct Point(int X, int Y);   // 불변 + 값 비교 + ToString 자동
var a = new Point(1, 2);
var b = a with { X = 5 };
```

## 주의할 점
- `List<Point>`의 요소를 `list[0].X = 5;`로 바꿀 수 없습니다(복사본이라 오류 CS1612). 새 값을 통째로 넣어야 합니다.
