---
id: multidim-array
title: 다차원 배열, 가변 배열과 Array 클래스 (Sort, IndexOf, Resize)
category: 배열과 반복문
order: 404
summary: 행렬 형태의 2차원 배열, 행마다 길이가 다른 가변 배열, Array 클래스의 정렬/검색/크기 변경 메서드입니다.
keywords: 2차원 배열, 다차원, 행렬, matrix, 가변 배열, jagged, GetLength, Array.Sort, Array.IndexOf, Array.Resize, Array.Reverse, 정렬, 검색, 크기 변경
lesson: 다차원배열_Array클래스
related: array-declaration, loops, list-sort, list-search
---
## 2차원 배열 `int[,]`
직사각형 모양(모든 행의 길이가 같음)입니다.
```csharp
int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };

for (int i = 0; i < matrix.GetLength(0); i++)      // 행 수 = 2
    for (int j = 0; j < matrix.GetLength(1); j++)  // 열 수 = 3
        Console.WriteLine($"[{i},{j}] = {matrix[i, j]}");
```

## 가변 배열 `int[][]` (배열의 배열)
행마다 길이가 다를 수 있습니다.
```csharp
int[][] jagged =
[
    [1, 2],
    [3, 4, 5],
    [6, 7, 8, 9]
];
Console.WriteLine(jagged[2][3]); // 9

foreach (int[] row in jagged)
    Console.WriteLine(string.Join(", ", row));
```

## Array 클래스의 유용한 메서드
```csharp
int[] members = [5, 3, 8, 1, 2];
Array.Sort(members);                 // [1, 2, 3, 5, 8] 제자리 정렬
int index = Array.IndexOf(members, 8); // 4 (없으면 -1)
Array.Reverse(members);              // 역순
Array.Resize(ref members, 7);        // 크기 7, 늘어난 칸은 0
bool has = Array.Exists(members, m => m > 5);
int[] copy = (int[])members.Clone();
```

## 주의할 점
- `Array.Resize`는 기존 배열을 바꾸는 게 아니라 **새 배열을 만들어 변수에 다시 넣습니다**. 그래서 `ref`가 필요합니다.
- 크기가 자주 바뀌면 배열 대신 `List<T>`를 쓰세요.
