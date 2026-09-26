---
id: recipe-datetime
title: 날짜와 시간 다루기 (DateTime, TimeSpan, 포맷, 차이 계산)
category: 실무 레시피
order: 2605
summary: 현재 시각, 날짜 더하기/빼기, 두 날짜 차이, yyyy-MM-dd 포맷, 요일, 월말, DateOnly/TimeOnly, UTC와 DateTimeOffset까지 날짜/시간 작업 모음입니다.
keywords: 날짜, 시간, DateTime, 현재 시간, 오늘 날짜, Now, Today, UtcNow, 날짜 포맷, yyyy-MM-dd, 날짜 형식, 날짜 더하기, AddDays, 날짜 차이, 며칠 차이, TimeSpan, 요일, DayOfWeek, 월말, 말일, DaysInMonth, DateOnly, TimeOnly, DateTimeOffset, 타임존, 나이 계산
related: recipe-parse-number, recipe-stopwatch, recipe-number-format
---
## 현재
```csharp
DateTime now = DateTime.Now;        // 현재 로컬 시각
DateTime today = DateTime.Today;    // 오늘 00:00
DateTime utc = DateTime.UtcNow;     // 협정 세계시 (서버/DB 저장용)
DateOnly date = DateOnly.FromDateTime(now);   // 날짜만 (.NET 6+)
```

## 포맷
```csharp
now.ToString("yyyy-MM-dd");            // 2026-09-26
now.ToString("yyyy-MM-dd HH:mm:ss");   // 2026-09-26 14:05:09 (HH = 24시간)
now.ToString("yyyy년 M월 d일 dddd");    // 2026년 9월 26일 토요일
now.ToString("tt h:mm");               // 오후 2:05
now.ToString("yyyyMMdd_HHmmss");       // 파일 이름용
$"{now:MM/dd}";
```
> `MM`은 월, `mm`은 분입니다. 헷갈리기 쉬운 부분입니다.

## 계산
```csharp
DateTime later = now.AddDays(7).AddHours(-3);
DateTime nextMonth = now.AddMonths(1);

TimeSpan diff = new DateTime(2026, 12, 25) - today;
Console.WriteLine(diff.Days);          // 남은 일수
Console.WriteLine(diff.TotalHours);    // 전체 시간(소수)

bool isPast = someDate < DateTime.Now;
DayOfWeek dow = now.DayOfWeek;         // Saturday
bool weekend = dow is DayOfWeek.Saturday or DayOfWeek.Sunday;

int lastDay = DateTime.DaysInMonth(2026, 2);          // 28
var firstOfMonth = new DateTime(now.Year, now.Month, 1);
var endOfMonth = firstOfMonth.AddMonths(1).AddDays(-1);
```

## 만 나이
```csharp
int Age(DateOnly birth, DateOnly today)
{
    int age = today.Year - birth.Year;
    if (birth > today.AddYears(-age)) age--;
    return age;
}
```

## 주의할 점
- 서버 저장/비교는 UTC(`DateTime.UtcNow` 또는 `DateTimeOffset`)로 하고, 화면에 보여줄 때 로컬로 바꾸는 것이 안전합니다.
- 테스트하기 쉽게 하려면 `DateTime.Now`를 직접 쓰지 말고 `TimeProvider`(.NET 8+)를 주입받습니다.
