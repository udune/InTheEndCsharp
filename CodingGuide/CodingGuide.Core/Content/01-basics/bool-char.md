---
id: bool-char
title: bool과 char 타입
category: 값 타입과 변수
order: 103
summary: 참/거짓을 담는 bool과 문자 하나를 담는 char, 그리고 char와 정수(유니코드 코드) 사이의 변환을 설명합니다.
keywords: bool, 불리언, 참, 거짓, true, false, char, 문자, 유니코드, 아스키, ascii, 문자코드, 코드값
lesson: bool_char타입
related: string-basics, explicit-cast, logical-operators
---
## 핵심
- `bool`은 `true` 또는 `false`만 가집니다. 조건문, 논리 연산의 결과 타입입니다.
- `char`는 **문자 하나**를 작은따옴표로 표현합니다. 내부적으로는 2바이트 유니코드 숫자입니다.

```csharp
bool isTrue = false;
char character = 'A';
char upperA = 'A';        // 유니코드 표기, 'A'

int code = (int)upperA;        // 65
char upperB = (char)(code + 1); // 'B'

char[] vowels = ['i', 'o', 'u'];
```

## 자주 쓰는 char 메서드
```csharp
char.IsDigit('7');      // True  - 숫자인가
char.IsLetter('가');    // True  - 문자인가
char.IsWhiteSpace(' '); // True
char.ToUpper('a');      // 'A'
'7' - '0';              // 7   - 숫자 문자를 정수로
```

## 주의할 점
- `"A"`(큰따옴표)는 string, `'A'`(작은따옴표)는 char 입니다. 서로 다른 타입입니다.
- C#의 bool은 정수와 호환되지 않습니다. `if (1)` 은 컴파일 오류입니다.
