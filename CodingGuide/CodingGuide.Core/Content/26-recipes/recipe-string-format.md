---
id: recipe-string-format
title: 문자열 실전 - 여러 줄 문자열, 경로 문자열, 패딩, 뒤집기, 개수 세기
category: 실무 레시피
order: 2604
summary: 원시 문자열(""" """), 축자 문자열(@""), 자리 맞추기(PadLeft), 문자열 뒤집기, 특정 문자 개수 세기, 대소문자 무시 비교 등 자주 찾는 문자열 작업 모음입니다.
keywords: 여러 줄 문자열, 멀티라인, raw string, 원시 문자열, """, 축자 문자열, verbatim, @, 역슬래시, 이스케이프, \n, 줄바꿈, PadLeft, PadRight, 자리 맞추기, 문자열 뒤집기, 역순, 개수 세기, 글자 수, 대소문자 무시 비교, 공백 제거, 줄 단위 분리, 문자열 반복
related: string-basics, recipe-number-format, recipe-regex
---
## 특수 문자와 여러 줄
```csharp
string path1 = "C:\\Users\\me\\file.txt";   // \\ 로 역슬래시
string path2 = @"C:\Users\me\file.txt";     // @ 축자 문자열: 이스케이프 없음
string tab = "이름\t나이\n홍길동\t30";       // \t 탭, \n 줄바꿈
string nl = Environment.NewLine;             // OS 줄바꿈 (Windows: \r\n)

// 원시 문자열 (C# 11+): 따옴표, 역슬래시, 줄바꿈을 그대로
string json = """
    {
      "name": "홍길동",
      "path": "C:\temp"
    }
    """;
string interpolated = $$"""{"id": {{id}}}""";   // $ 두 개면 {{ }} 로 보간
```

## 자리 맞추기
```csharp
"7".PadLeft(3, '0');     // "007"
"abc".PadRight(6, '.');  // "abc..."
$"{name,-10}|{age,5}";   // 왼쪽 10칸 | 오른쪽 5칸
```

## 자주 찾는 작업
```csharp
string s = "Hello World";

new string(s.Reverse().ToArray());               // 뒤집기 "dlroW olleH"
s.Count(c => c == 'o');                          // 특정 문자 개수: 2
s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;   // 단어 수
string.Equals(a, b, StringComparison.OrdinalIgnoreCase);      // 대소문자 무시 비교
s.Replace(" ", "");                              // 모든 공백 제거
string.Concat(s.Where(c => !char.IsWhiteSpace(c)));           // 공백류 전부 제거
text.Split(["\r\n", "\n"], StringSplitOptions.None);          // 줄 단위 분리
new string('-', 20);                             // "--------------------" 반복
string.Join(", ", list);                         // 목록 → "a, b, c"
char.ToUpper(s[0]) + s[1..];                     // 첫 글자 대문자
s.IndexOf("World", StringComparison.OrdinalIgnoreCase);
```

## 한글 관련
```csharp
"안녕".Length;                                       // 2 (글자 수)
System.Text.Encoding.UTF8.GetByteCount("안녕");      // 6 (UTF-8 바이트 수)
```
