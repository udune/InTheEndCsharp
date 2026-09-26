---
id: error-cs0165
title: CS0165 - 할당되지 않은 지역 변수를 사용했습니다
category: 컴파일 오류
order: 2807
summary: 값을 넣지 않은(또는 일부 경로에서만 넣은) 지역 변수를 읽을 때 나는 오류와 해결(초기값 지정)입니다.
keywords: CS0165, 할당되지 않은 지역 변수를 사용했습니다, Use of unassigned local variable, 초기화 안 함, 변수 초기값, 지역 변수 초기화
related: var-declaration, error-cs0103, method-out
---
## 메시지
- `할당되지 않은 'c' 지역 변수를 사용했습니다.`
- Use of unassigned local variable 'c'

## 원인
C#은 지역 변수에 자동으로 0을 넣어 주지 않습니다(필드는 넣어 줌).
```csharp
int c;
Console.WriteLine(c);        // CS0165

string msg;
if (ok) msg = "성공";
Console.WriteLine(msg);      // CS0165: ok 가 false 면 값이 없음

int total;
foreach (var x in list) total += x;   // CS0165: 처음 값이 없음
```

## 해결
```csharp
int c = 0;
string msg = ok ? "성공" : "실패";
string msg2 = "";            // 기본값을 먼저
int total = 0;
```
`out` 매개변수로 받는 변수는 초기화하지 않아도 됩니다(`int.TryParse(s, out int n)`).
