---
id: error-index-out-of-range
title: IndexOutOfRangeException / ArgumentOutOfRangeException - 인덱스가 범위를 벗어났습니다
category: 흔한 예외
order: 2702
summary: 배열이나 List에서 존재하지 않는 위치(음수, Length/Count 이상)에 접근할 때 나는 예외의 원인과 해결(경계 검사, <= 실수, 빈 컬렉션)입니다.
keywords: IndexOutOfRangeException, ArgumentOutOfRangeException, 인덱스가 배열 범위를 벗어났습니다, Index was outside the bounds of the array, 인덱스가 범위를 벗어났습니다, Index was out of range, Must be non-negative and less than the size of the collection, 범위 초과, 배열 범위, off by one, Substring 오류
related: array-access, loops, list-create, exception-object
---
## 메시지
- 배열: 인덱스가 배열 범위를 벗어났습니다. / Index was outside the bounds of the array. → `IndexOutOfRangeException`
- List/문자열: 인덱스가 범위를 벗어났습니다. 음수가 아니어야 하며 컬렉션의 크기보다 작아야 합니다. / Index was out of range. → `ArgumentOutOfRangeException`

## 흔한 원인
```csharp
int[] arr = [10, 20, 30];            // 유효한 인덱스: 0, 1, 2
arr[3];                              // ← 예외! Length 와 같은 인덱스

for (int i = 0; i <= arr.Length; i++) // ← 예외! <= 가 아니라 < 여야 함
    Console.WriteLine(arr[i]);

var list = new List<int>();
list[0] = 5;                          // ← 예외! 비어 있는 List 에 인덱스로 대입 (Add 를 써야 함)

"abc".Substring(1, 5);                // ← 예외! 시작 + 길이가 문자열 길이를 넘음
args[0];                              // ← 예외! 명령줄 인자가 없을 때
```

## 해결
```csharp
for (int i = 0; i < arr.Length; i++) { }   // < 사용
foreach (var x in arr) { }                  // 인덱스가 필요 없으면 foreach

if (index >= 0 && index < list.Count)       // 경계 검사
    Console.WriteLine(list[index]);

var first = list.FirstOrDefault();          // 비어 있을 수 있으면
var third = list.ElementAtOrDefault(2);
string part = text.Length >= 5 ? text[..5] : text;
```

## 디버깅 팁
예외가 난 줄에서 **인덱스 값**과 **Length/Count**를 비교해 보세요. 대부분 1 차이(off-by-one)입니다.
