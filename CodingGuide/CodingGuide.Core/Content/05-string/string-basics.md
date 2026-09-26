---
id: string-basics
title: 문자열 다루기 (보간, Substring, Split, Join, StringBuilder)
category: 문자열
order: 501
summary: 문자열 생성, 보간($""), 자르기, 검색, 대소문자 변환, 분리/합치기, StringBuilder까지 문자열 기본기를 정리합니다.
keywords: 문자열, string, 보간, $"", 문자열 보간, string.Format, Substring, 자르기, Contains, 포함, ToUpper, ToLower, 대문자, 소문자, Split, 분리, 나누기, Join, 합치기, 이어붙이기, StringBuilder, Length, 길이, Replace, Trim, 공백 제거
lesson: 문자열
related: recipe-string-format, comparison-operators
---
## 만들기와 합치기
```csharp
string greeting = "Hello, World!";
string empty = "";           // 또는 string.Empty
string? nothing = null;

string first = "John", last = "Doe";
string full = $"{first} {last}";                      // 문자열 보간 (권장)
string formatted = string.Format("Name: {0}, {1}", first, last);
string concat = first + " " + last;
```

## 자주 쓰는 메서드
```csharp
string text = "Hello World!";
text.Length;                                          // 12
text.Substring(0, 5);                                 // "Hello" (시작, 길이)
text[..5];                                            // "Hello" (범위 연산자)
text.Contains("World");                               // True
text.Contains("hello", StringComparison.OrdinalIgnoreCase); // True (대소문자 무시)
text.StartsWith("He"); text.EndsWith("!");
text.IndexOf("o");                                    // 4 (없으면 -1)
text.ToUpper(); text.ToLower();
text.Replace("World", "C#");                          // "Hello C#!"
"  hi  ".Trim();                                      // "hi" (앞뒤 공백 제거)
string.IsNullOrWhiteSpace(text);                      // null/빈/공백 검사
```

## 분리하고 합치기
```csharp
string colors = "Red, Green, Blue";
string[] parts = colors.Split(',');   // ["Red", " Green", " Blue"]  ← 공백이 남음!
string[] clean = colors.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

string joined = string.Join("|", clean); // "Red|Green|Blue"
```

## StringBuilder - 반복해서 이어 붙일 때
string은 **불변(immutable)** 이라 `+=` 할 때마다 새 문자열이 만들어집니다. 반복문에서 많이 이어 붙일 때는 StringBuilder를 씁니다.
```csharp
using System.Text;

var sb = new StringBuilder();
for (int i = 0; i < 1000; i++)
    sb.Append(i).Append(',');
sb.AppendLine("끝");
string result = sb.ToString();
```

## 주의할 점
- 문자열 메서드는 원본을 바꾸지 않고 **새 문자열을 돌려줍니다**. `text.ToUpper();`만 쓰면 아무 일도 안 일어납니다. `text = text.ToUpper();`
- `Split(',')` 결과에는 앞뒤 공백이 남으니 `TrimEntries` 옵션을 쓰세요.
- 사용자에게 보여줄 때가 아니라 **비교할 때는** `StringComparison.Ordinal`/`OrdinalIgnoreCase`를 명시하는 것이 안전합니다.
