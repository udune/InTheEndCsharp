---
id: recipe-regex
title: 정규식(Regex) - 패턴 검사, 추출, 치환
category: 실무 레시피
order: 2612
summary: 이메일/전화번호 형식 검사, 문자열에서 숫자만 추출, 패턴 치환 등 Regex의 기본 사용법과 자주 쓰는 패턴 모음입니다.
keywords: 정규식, 정규 표현식, regex, Regex, 패턴, 형식 검사, 이메일 검사, 전화번호 검사, 휴대폰 번호, 숫자만 추출, 문자 추출, 치환, Replace, Match, Matches, IsMatch, GeneratedRegex, 유효성 검사, 특수문자 제거
related: recipe-string-format, attribute-property-transform
---
## 기본
```csharp
using System.Text.RegularExpressions;

bool ok = Regex.IsMatch("010-1234-5678", @"^01[0-9]-\d{3,4}-\d{4}$");   // 형식 검사

Match m = Regex.Match("주문번호: A-1234", @"[A-Z]-(\d+)");
if (m.Success) Console.WriteLine(m.Groups[1].Value);                     // 1234

foreach (Match x in Regex.Matches("a1b22c333", @"\d+"))
    Console.WriteLine(x.Value);                                          // 1, 22, 333

string onlyDigits = Regex.Replace("010-1234-5678", @"\D", "");          // 01012345678
string clean = Regex.Replace("안녕!! @#하세요", @"[^\w\s가-힣]", "");     // 특수문자 제거
```

## 자주 쓰는 패턴
| 목적 | 패턴 |
|---|---|
| 숫자만 | `^\d+$` |
| 영문+숫자 | `^[a-zA-Z0-9]+$` |
| 한글만 | `^[가-힣]+$` |
| 이메일(간단) | `^[^@\s]+@[^@\s]+\.[^@\s]+$` |
| 휴대폰 | `^01[016789]-?\d{3,4}-?\d{4}$` |
| 공백 여러 개 | `\s+` (→ `" "` 로 치환) |
| 8자 이상, 영문+숫자 포함 | `^(?=.*[A-Za-z])(?=.*\d).{8,}$` |

## 기호 요약
`\d` 숫자, `\w` 글자/숫자/_, `\s` 공백, `.` 아무 문자, `^` 시작, `$` 끝, `*` 0개 이상, `+` 1개 이상, `?` 0~1개, `{n,m}` n~m개, `[abc]` 중 하나, `[^abc]` 제외, `( )` 그룹

## 성능: 반복 사용하면 GeneratedRegex (.NET 7+)
```csharp
public static partial class Patterns
{
    [GeneratedRegex(@"^\d{3}-\d{4}$")]
    public static partial Regex Zip();
}
bool ok = Patterns.Zip().IsMatch("123-4567");
```

## 주의할 점
- C# 문자열에서는 `@"..."`(축자 문자열)로 쓰면 `\`를 두 번 쓰지 않아도 됩니다.
- 사용자 입력을 패턴에 넣을 때는 `Regex.Escape(input)`으로 특수문자를 이스케이프하세요.
