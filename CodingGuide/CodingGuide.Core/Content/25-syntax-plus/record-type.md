---
id: record-type
title: record - 값으로 비교되는 불변 데이터 타입
category: C# 문법 보강
order: 2502
summary: 한 줄로 선언하고, 내용이 같으면 같다고 판단하며(값 동등성), with로 일부만 바꾼 복사본을 만드는 record 타입입니다. DTO, 설정, 결과 객체에 적합합니다.
keywords: record, 레코드, record class, record struct, 불변, immutable, 값 동등성, value equality, with, with 식, 복사본, 분해, deconstruct, DTO, 데이터 클래스, 자동 ToString
related: struct-vs-class, property-setter-access, pattern-matching, comparison-operators
---
## 핵심
```csharp
public record Person(string Name, int Age);   // 한 줄 선언 (주 생성자 + init 속성 자동)

var p1 = new Person("홍길동", 30);
var p2 = new Person("홍길동", 30);

Console.WriteLine(p1 == p2);    // True  ← 내용이 같으면 같음 (class 였다면 False)
Console.WriteLine(p1);          // Person { Name = 홍길동, Age = 30 }  ← ToString 자동
// p1.Age = 31;                 // 오류: init 전용 (불변)

var older = p1 with { Age = 31 };   // 일부만 바꾼 "새" 객체
var (name, age) = p1;               // 분해
```

## class vs record
| | class | record |
|---|---|---|
| `==` | 같은 인스턴스인가 | **모든 속성 값이 같은가** |
| ToString | 타입 이름 | 속성 값까지 출력 |
| 변경 | 자유 | 기본은 불변 (`with`로 복사) |
| 용도 | 동작과 상태를 가진 객체 | 데이터를 담는 객체 |

## 여러 형태
```csharp
public record Point(int X, int Y);                  // record class (참조 타입)
public readonly record struct Money(decimal Amount, string Currency);   // 값 타입

public record User                                   // 일반 속성 스타일도 가능
{
    public required string Email { get; init; }
    public string Name { get; init; } = "";
    public List<string> Roles { get; init; } = [];   // 주의: 리스트 내용은 비교되지 않음 (참조 비교)
}
```

## 언제 쓰나요?
- API 요청/응답 DTO, 설정값, 메시지, 결과 값
- Dictionary의 키, `Distinct()`로 중복 제거할 객체 (값 비교가 필요할 때)
