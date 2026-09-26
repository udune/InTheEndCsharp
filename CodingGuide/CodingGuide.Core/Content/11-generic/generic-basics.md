---
id: generic-basics
title: 제네릭 기초 - 제네릭 메서드 <T>
category: 제네릭
order: 1101
summary: 타입을 매개변수(T)로 받아 여러 타입에 같은 코드를 재사용하는 제네릭 메서드를 Swap 예제로 설명합니다.
keywords: 제네릭, generic, <T>, 타입 매개변수, 제네릭 메서드, Swap, 타입 추론, 코드 재사용, 템플릿
lesson: 제네릭정의및기초
related: generic-class, generic-constraint-struct-class, method-ref, boxing-unboxing
---
## 핵심
타입만 다르고 로직이 같은 메서드를 **하나로** 만듭니다. `T`는 호출할 때 실제 타입으로 바뀝니다.

```csharp
void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}

int a = 1, b = 2;
double aa = 1.12, bb = 2.34;
string s1 = "aaa", s2 = "bbb";

Swap(ref a, ref b);        // T = int (컴파일러가 추론)
Swap(ref aa, ref bb);      // T = double
Swap<string>(ref s1, ref s2); // 명시적으로 지정해도 됨
```

## 타입 매개변수 여러 개
```csharp
void Print<TKey, TValue>(TKey key, TValue value) =>
    Console.WriteLine($"{key} = {value}");

Print("나이", 30);
```

## 제네릭이 없다면?
`object`로 받으면 박싱이 생기고, 잘못된 타입을 넣어도 컴파일러가 막아주지 못합니다. 제네릭은 **타입 안전성 + 성능**을 모두 얻습니다.

## 익숙한 제네릭들
`List<T>`, `Dictionary<TKey, TValue>`, `Func<T, TResult>`, `Task<T>`, `IEnumerable<T>` 모두 제네릭입니다.
