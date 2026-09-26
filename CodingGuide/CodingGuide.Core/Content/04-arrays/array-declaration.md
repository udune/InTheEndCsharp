---
id: array-declaration
title: 배열 선언과 초기화
category: 배열과 반복문
order: 401
summary: 같은 타입의 값 여러 개를 고정 크기로 담는 배열을 만드는 여러 가지 방법입니다.
keywords: 배열, array, 선언, 초기화, new int[], 컬렉션 식, 컬렉션 표현식, collection expression, 기본값, 크기
lesson: 배열선언및초기화
related: array-access, loops, multidim-array, list-create
---
## 핵심
배열은 **크기가 고정**된, 같은 타입 값들의 묶음입니다.

```csharp
int[] numbers = new int[5];        // 크기 5, 모두 0 으로 채워짐
string[] names = new string[3];    // 모두 null

int[] a = new int[] { 10, 20, 30 }; // 전통적인 초기화
int[] b = { 10, 20, 30 };           // 축약
int[] c = [10, 20, 30, 40];         // C# 12 컬렉션 식 (권장)
string[] fruits = ["사과", "바나나", "레몬"];
int[] empty = [];                    // 빈 배열
```

## 타입별 기본값
- 숫자 타입: `0`
- bool: `false`
- char: `'\0'`
- 참조 타입(string, 클래스): `null`

## 배열 vs List
- 개수가 **변하지 않으면** 배열, **추가/삭제가 필요하면** `List<T>`를 씁니다.
- 배열 크기를 바꾸려면 `Array.Resize`로 새 배열을 만들어야 합니다.
