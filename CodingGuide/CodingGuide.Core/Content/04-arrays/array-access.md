---
id: array-access
title: 배열 요소 접근 (인덱스, ^1, 범위)
category: 배열과 반복문
order: 402
summary: 인덱스로 배열 값을 읽고 쓰는 방법, 뒤에서부터 세는 ^ 연산자와 범위(..) 연산자를 설명합니다.
keywords: 인덱스, index, 요소, 접근, 마지막 요소, 끝에서, ^1, 범위, range, .., Length, 길이, 배열 길이, 배열 크기, 슬라이스, 자르기
lesson: 배열요소접근
related: array-declaration, loops, error-index-out-of-range
---
## 핵심
인덱스는 **0부터** 시작합니다. 마지막 요소의 인덱스는 `Length - 1` 입니다.

```csharp
int[] numbers = [10, 20, 30, 40];
string[] fruits = ["사과", "바나나", "레몬"];

numbers[2] = 100;                        // 쓰기
Console.WriteLine(numbers[2]);           // 100 (읽기)
Console.WriteLine(fruits.Length);        // 3
Console.WriteLine(fruits[fruits.Length - 1]); // 레몬
Console.WriteLine(fruits[^1]);           // 레몬 (^1 = 끝에서 첫 번째)
```

## 범위 연산자 (..)
```csharp
int[] arr = [0, 1, 2, 3, 4, 5];
int[] mid   = arr[1..4];  // [1, 2, 3]   (끝 인덱스는 포함 안 함)
int[] first = arr[..2];   // [0, 1]
int[] last2 = arr[^2..];  // [4, 5]
```
string에도 똑같이 쓸 수 있습니다: `"Hello"[1..3]` → `"el"`

## 주의할 점
- 범위를 벗어난 인덱스는 `IndexOutOfRangeException`을 던집니다.
- `arr[arr.Length]`는 항상 범위 밖입니다(흔한 실수).
