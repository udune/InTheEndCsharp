---
id: bit-operators
title: 비트 연산자 (&, |, ^, ~, <<, >>)
category: 연산자
order: 205
summary: 정수를 2진수 비트 단위로 계산하는 연산자와 플래그, 마스크 활용법입니다.
keywords: 비트, bit, 2진수, 이진수, and, or, xor, not, 시프트, shift, 마스크, 플래그, &, |, ^, ~, <<, >>
lesson: 비트연산자
related: logical-operators, enum-type
---
## 핵심
```csharp
int a = 192; // 1100_0000
int b = 168; // 1010_1000

Console.WriteLine(a & b);  // 128  (1000_0000) 둘 다 1 인 비트
Console.WriteLine(a | b);  // 232  (1110_1000) 하나라도 1 인 비트
Console.WriteLine(a ^ b);  // 104  (0110_1000) 서로 다른 비트
Console.WriteLine(~a);     // -193 모든 비트 반전
Console.WriteLine(a << 2); // 768  왼쪽으로 2칸 = ×4
Console.WriteLine(a >> 3); // 24   오른쪽으로 3칸 = ÷8
```
> 예제 코드의 마지막 줄은 출력 문구가 `a >> 2`이지만 실제로는 `a >> 3`을 계산합니다.

## 활용
```csharp
Convert.ToString(a, 2);               // "11000000" 2진 문자열
bool isOdd = (n & 1) == 1;            // 홀수 판별
int flags = READ | WRITE;             // 플래그 합치기
bool canWrite = (flags & WRITE) != 0; // 플래그 검사
```
- 실무에서는 비트 플래그를 `[Flags] enum`으로 표현하는 것이 읽기 쉽습니다.
- `&`, `|`를 bool에 쓰면 단락 평가 없이 양쪽을 모두 계산합니다.
