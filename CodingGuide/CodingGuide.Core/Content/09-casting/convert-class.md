---
id: convert-class
title: Convert 클래스와 Parse로 문자열 ↔ 숫자 변환
category: 타입 변환
order: 904
summary: Convert.ToInt32, int.Parse, int.TryParse, ToString으로 문자열과 숫자를 서로 변환하는 방법과 차이점입니다.
keywords: Convert, Convert.ToInt32, Parse, int.Parse, TryParse, 문자열을 숫자로, 숫자를 문자열로, int를 string으로, string을 int로, 정수를 문자열로, string to int, int to string, ToString, FormatException, 변환
lesson: 타입변환_ConvertClass
related: recipe-parse-number, method-out, explicit-cast, recipe-number-format
---
## 핵심
```csharp
string strNumber = "789";
int a = Convert.ToInt32(strNumber);   // 789
int b = int.Parse(strNumber);         // 789
bool ok = int.TryParse(strNumber, out int c); // 실패해도 예외 없음 (권장)

string s = 789.ToString();            // 숫자 → 문자열
string s2 = $"{789}";
```

## 차이점
| 입력 | `int.Parse` | `Convert.ToInt32` | `int.TryParse` |
|---|---|---|---|
| `"123"` | 123 | 123 | true, 123 |
| `"abc"` | FormatException | FormatException | false, 0 |
| `null` | ArgumentNullException | **0** | false, 0 |
| `"99999999999"` | OverflowException | OverflowException | false, 0 |

- 사용자 입력/파일 데이터처럼 **틀릴 수 있는 값은 TryParse**를 씁니다.
- Convert는 `Convert.ToInt32(3.7)` → 4 처럼 **반올림(은행가 반올림)** 을 합니다. `(int)3.7`은 3입니다.

## Convert의 다른 기능
```csharp
Convert.ToBoolean("true");     // True
Convert.ToString(255, 2);      // "11111111" (2진수)
Convert.ToInt32("ff", 16);     // 255 (16진수)
Convert.ToBase64String(bytes); // 바이트 → Base64
```
