---
id: switch-statement
title: switch 문과 switch 식 (패턴, when)
category: 조건문
order: 302
summary: 값에 따라 분기하는 switch 문과, 값을 돌려주는 switch 식(=>), when 조건을 설명합니다.
keywords: switch, case, default, break, switch 식, switch expression, when, 패턴, 분기, 등급, 성적, 여러 경우
lesson: switch문
related: if-ternary, pattern-matching, enum-type
---
## switch 식 (C# 8+, 권장)
값을 **돌려주는** 형태입니다. 짧고, 나머지 경우를 `_`로 처리합니다.
```csharp
string grade = "B+";
string message = grade switch
{
    "A" => "우수한 성적입니다.",
    "B" or "B+" => "좋은 성적입니다.",
    var g when g.StartsWith("C") => "보통 성적입니다.",
    _ => "잘 모르겠습니다."   // default
};
```

## switch 문 (전통적인 형태)
```csharp
switch (grade)
{
    case "A":
        Console.WriteLine("우수");
        break;               // break 필수 (다음 case 로 넘어가지 않음)
    case "B":
    case "B+":               // 여러 case 를 묶기
        Console.WriteLine("좋음");
        break;
    case var g when g.StartsWith("C"):  // when 으로 추가 조건
        Console.WriteLine("보통");
        break;
    default:
        Console.WriteLine("?");
        break;
}
```

## 패턴과 함께 쓰기
```csharp
string ToGrade(int score) => score switch
{
    >= 90 => "A",
    >= 80 and < 90 => "B",
    < 0 => throw new ArgumentOutOfRangeException(nameof(score)),
    _ => "C 이하"
};

string Describe(object o) => o switch
{
    int i => $"정수 {i}",
    string s => $"문자열 {s}",
    null => "null",
    _ => "기타"
};
```

## 주의할 점
- switch 문에서 case 끝에 `break`(또는 return/throw)가 없으면 컴파일 오류(CS0163)입니다.
- 위에서부터 첫 번째로 맞는 경우가 선택되므로, 좁은 조건을 위에 둡니다.
