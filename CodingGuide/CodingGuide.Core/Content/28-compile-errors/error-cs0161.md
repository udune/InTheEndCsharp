---
id: error-cs0161
title: CS0161 - 코드 경로 중 일부만 값을 반환합니다
category: 컴파일 오류
order: 2806
summary: 반환 타입이 있는 메서드에서 if/switch의 어떤 경우에는 return이 없을 때 나는 오류와 해결입니다.
keywords: CS0161, 코드 경로 중 일부만 값을 반환합니다, not all code paths return a value, return 누락, 반환값 없음, 모든 경로 반환
related: methods, if-ternary, switch-statement
---
## 메시지
- `'P.F(int)': 코드 경로 중 일부만 값을 반환합니다.`
- 'P.F(int)': not all code paths return a value

## 원인
```csharp
static int F(int n)
{
    if (n > 0)
        return 1;
    // n <= 0 이면? → 반환값이 없음
}
```

## 해결
```csharp
static int F(int n)
{
    if (n > 0) return 1;
    return 0;                       // 나머지 경우 처리
}

static int G(int n) => n > 0 ? 1 : 0;

static string Name(int code) => code switch
{
    1 => "하나",
    2 => "둘",
    _ => throw new ArgumentOutOfRangeException(nameof(code)),   // 예외도 "반환 경로"로 인정
};
```
- 반복문 안에서만 return 하는 경우, 반복문이 한 번도 안 돌 수도 있으니 **반복문 뒤에도** return이 필요합니다.
