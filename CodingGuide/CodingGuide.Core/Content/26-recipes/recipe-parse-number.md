---
id: recipe-parse-number
title: 문자열을 숫자/날짜/enum으로 안전하게 변환하기
category: 실무 레시피
order: 2601
summary: 사용자 입력이나 파일에서 읽은 문자열을 int, double, decimal, DateTime, enum으로 예외 없이 변환하는 TryParse 패턴과 콤마·통화 기호·문화권 처리입니다.
keywords: 문자열을 숫자로, 문자열 숫자 변환, string to int, 숫자 변환, 파싱, parse, TryParse, 콤마 있는 숫자, 천단위 콤마, 통화, NumberStyles, CultureInfo, InvariantCulture, 소수점, 문자열을 날짜로, 문자열을 enum으로, 입력값 검증
related: convert-class, method-out, enum-type, recipe-number-format, recipe-datetime
---
## 기본: TryParse
```csharp
if (int.TryParse(input, out int n))
    Console.WriteLine(n * 2);
else
    Console.WriteLine("숫자가 아닙니다");

double.TryParse("3.14", out double d);
decimal.TryParse("1500.50", out decimal price);
long.TryParse("9999999999", out long big);
```

## 천 단위 콤마, 통화 기호가 있는 숫자
```csharp
using System.Globalization;

int.TryParse("1,234", NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out int a);   // 1234
decimal.TryParse("₩1,500", NumberStyles.Currency, new CultureInfo("ko-KR"), out decimal won);  // 1500
```

## 소수점 문자 주의 (문화권)
독일어 등은 소수점이 `,` 입니다. 파일/통신 데이터는 **InvariantCulture**로 파싱/출력하세요.
```csharp
double.Parse("3.14", CultureInfo.InvariantCulture);   // 어느 PC 에서나 3.14
value.ToString(CultureInfo.InvariantCulture);          // "3.14"
```

## 날짜
```csharp
DateTime.TryParse("2026-09-26", out DateTime dt);
DateTime.TryParseExact("20260926", "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt2);
```

## enum
```csharp
Enum.TryParse<Days>("Monday", ignoreCase: true, out var day);
```

## 입력 받아 반복 검증
```csharp
int ReadInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value)) return value;
        Console.WriteLine("숫자를 입력하세요.");
    }
}
```
