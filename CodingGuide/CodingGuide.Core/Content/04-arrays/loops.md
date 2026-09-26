---
id: loops
title: 반복문 (for, foreach, while, do-while, break, continue)
category: 배열과 반복문
order: 403
summary: 같은 작업을 반복하는 4가지 반복문과 break/continue, 중첩 반복문(구구단)을 설명합니다.
keywords: 반복, 반복문, loop, for, foreach, while, do while, break, continue, 중첩 반복, 구구단, 순회, 무한 루프
lesson: 반복문
related: array-access, enumerator, list-create, linq-intro
---
## 4가지 반복문
```csharp
string[] fruits = ["사과", "바나나", "레몬"];

// 1) for - 횟수나 인덱스가 필요할 때
for (int i = 0; i < fruits.Length; i++)
    Console.WriteLine($"{i}: {fruits[i]}");

// 2) foreach - 모든 요소를 차례로 볼 때 (가장 많이 씀)
foreach (string fruit in fruits)
    Console.WriteLine(fruit);

// 3) while - 조건이 참인 동안 (0번 실행될 수도 있음)
int count = 0;
while (count < 3)
{
    Console.WriteLine(count);
    count++;
}

// 4) do-while - 최소 1번은 실행
int n = 0;
do
{
    Console.WriteLine(n);
    n++;
} while (n < 3);
```

## break와 continue
```csharp
for (int i = 0; i < 10; i++)
{
    if (i == 5) break;        // 반복문 전체를 빠져나감
    if (i % 2 == 0) continue; // 이번 회차만 건너뜀
    Console.WriteLine(i);     // 1, 3
}
```

## 중첩 반복문 - 구구단
```csharp
for (int i = 2; i <= 9; i++)
{
    for (int j = 1; j <= 9; j++)
        Console.WriteLine($"{i} x {j} = {i * j}");
    Console.WriteLine();
}
```

## 주의할 점
- `foreach` 안에서 순회 중인 List에 **추가/삭제하면** `InvalidOperationException`이 납니다. (→ 컬렉션 수정 예외)
- 무한 루프 `while (true)`는 반드시 `break` 조건을 둡니다.
- 인덱스로 뒤에서부터 지울 때는 `for (int i = list.Count - 1; i >= 0; i--)` 처럼 역순으로 돕니다.
