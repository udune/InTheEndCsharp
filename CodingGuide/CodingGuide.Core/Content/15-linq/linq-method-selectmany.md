---
id: linq-method-selectmany
title: SelectMany - 중첩 리스트 펼치기 (flatten)
category: LINQ
order: 1510
summary: 각 요소가 가진 리스트들을 하나의 평평한 시퀀스로 합치는 SelectMany(다른 언어의 flatMap)입니다.
keywords: SelectMany, 펼치기, 평탄화, flatten, flatMap, 중첩 리스트, 리스트의 리스트, 2차원을 1차원으로, 모든 점수
lesson: 린큐_메소드_SelectMany
source: 린큐/Student.cs
related: linq-method-select, multidim-array
---
## 핵심
```csharp
// 학생마다 Scores 리스트가 있음: Alice [5,3,9], Bob [8,3,2] ...
var allScores = students.SelectMany(s => s.Scores);   // 5,3,9,8,3,2,...

foreach (var score in allScores)
    Console.WriteLine(score);
```

## Select와 비교
```csharp
IEnumerable<List<int>> nested = students.Select(s => s.Scores);     // 리스트들의 목록
IEnumerable<int> flat = students.SelectMany(s => s.Scores);         // 하나로 펼친 목록
```

## 활용
```csharp
// 가변 배열 → 1차원
int[][] jagged = [[1, 2], [3], [4, 5]];
int[] flatArr = jagged.SelectMany(x => x).ToArray();   // [1,2,3,4,5]

// 원래 요소 정보도 같이
var pairs = students.SelectMany(s => s.Scores, (s, score) => $"{s.Name}: {score}");

// 여러 문장 → 단어 목록
var words = lines.SelectMany(l => l.Split(' '));
```
