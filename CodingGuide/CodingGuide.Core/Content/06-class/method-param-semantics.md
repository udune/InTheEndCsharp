---
id: method-param-semantics
title: 매개변수 전달 방식 - 값 복사와 참조 (메서드 안에서 바꾸면 밖도 바뀌나?)
category: 클래스
order: 609
summary: 값 타입은 복사되어 전달되고, 참조 타입은 같은 객체를 가리키는 참조가 복사되어 전달되는 원리를 설명합니다.
keywords: 값 전달, 참조 전달, call by value, call by reference, 값 타입, 참조 타입, 복사, 매개변수 변경, 원본이 바뀌지 않음, 원본이 바뀜, string 불변
lesson: 메서드_매개변수의특징
related: method-ref, method-out, struct-vs-class, boxing-unboxing
---
## 결론부터
C#은 기본적으로 **값을 복사해서** 넘깁니다. 다만 참조 타입은 "객체의 주소"가 복사되므로 같은 객체를 함께 보게 됩니다.

```csharp
void Test(int a) { a++; }                 // 값 타입: 복사본만 바뀜
void Test2(int[] arr) { arr[0]++; }       // 참조 타입: 같은 배열을 수정
void Test3(string s) { s = "b"; }         // 변수(주소)에 새 문자열을 넣은 것뿐

int a = 10;
int[] arr = [10];
string s = "a";

Test(a);    Console.WriteLine(a);      // 10  (그대로)
Test2(arr); Console.WriteLine(arr[0]); // 11  (바뀜!)
Test3(s);   Console.WriteLine(s);      // "a" (그대로)
```

## 왜 string은 안 바뀌나요?
string은 참조 타입이지만 **불변(immutable)** 입니다. `s = "b"`는 기존 문자열을 고치는 게 아니라 **매개변수 변수에 새 문자열의 주소를 넣은 것**이라 밖의 변수에는 영향이 없습니다.

## 정리
| 매개변수에 한 일 | 값 타입(int, struct) | 참조 타입(배열, 클래스) |
|---|---|---|
| 내용 수정 (`arr[0]++`, `obj.Name = ..`) | 복사본만 바뀜 | **원본도 바뀜** |
| 새로 대입 (`x = new ...`) | 밖에 영향 없음 | 밖에 영향 없음 |

밖의 변수 자체를 바꾸고 싶으면 `ref`, 결과를 추가로 돌려주려면 `out`을 씁니다.
