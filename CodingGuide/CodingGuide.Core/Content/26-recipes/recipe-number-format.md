---
id: recipe-number-format
title: 숫자 포맷 - 천 단위 콤마, 소수점 자리수, 퍼센트, 통화, 자리 채우기
category: 실무 레시피
order: 2602
summary: 숫자를 1,234,567 / 3.14 / 45.6% / ₩1,000 / 007 처럼 원하는 모양의 문자열로 바꾸는 표준·사용자 지정 서식 문자열입니다.
keywords: 숫자 포맷, 숫자 서식, 천단위 콤마, 콤마 찍기, 세 자리마다 콤마, 소수점 자리수, 소수점 둘째 자리, N0, N2, F2, P, C, D3, 앞에 0 채우기, 자리수 맞추기, 퍼센트, 통화, 원화, ToString 포맷, 16진수, X
related: recipe-rounding, recipe-string-format, recipe-parse-number, float-types
---
## 표준 서식
```csharp
int n = 1234567;
double d = 3.14159;
double rate = 0.456;

n.ToString("N0");      // "1,234,567"   천 단위 콤마
d.ToString("N2");      // "3.14"        콤마 + 소수 2자리
d.ToString("F3");      // "3.142"       소수 3자리 (콤마 없음)
rate.ToString("P1");   // "45.6%"       퍼센트
n.ToString("C0");      // "₩1,234,567"  통화 (현재 PC 문화권)
7.ToString("D3");      // "007"         정수 자리수 맞추기
255.ToString("X");     // "FF"          16진수
1234.5.ToString("E2"); // "1.23E+003"   지수
```

## 문자열 보간에서
```csharp
Console.WriteLine($"합계: {n:N0}원");        // 합계: 1,234,567원
Console.WriteLine($"비율: {rate:P0}");       // 비율: 46%
Console.WriteLine($"[{n,12:N0}]");           // 오른쪽 정렬 12칸
Console.WriteLine($"[{"이름",-10}]");        // 왼쪽 정렬 10칸
```

## 사용자 지정 서식
```csharp
d.ToString("0.00");      // "3.14"   (0 = 반드시 표시)
d.ToString("#.##");      // "3.14"   (# = 있으면 표시)
0.5.ToString("#.##");    // ".5"
0.5.ToString("0.##");    // "0.5"
1234.ToString("#,##0");  // "1,234"
5.ToString("+#;-#;0");   // "+5"     (양수;음수;0)
```

## 특정 문화권으로
```csharp
using System.Globalization;
n.ToString("C", new CultureInfo("en-US"));   // "$1,234,567.00"
d.ToString(CultureInfo.InvariantCulture);    // 항상 "." 소수점
```

## 주의할 점
- 서식 지정의 반올림은 **표시용**입니다. 계산에 쓸 값은 `Math.Round`로 따로 반올림하세요.
