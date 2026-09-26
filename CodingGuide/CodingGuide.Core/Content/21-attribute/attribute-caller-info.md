---
id: attribute-caller-info
title: 호출자 정보 어트리뷰트 (CallerMemberName, CallerFilePath, CallerLineNumber)
category: 어트리뷰트
order: 2102
summary: 매개변수에 붙이면 호출한 메서드 이름, 파일 경로, 줄 번호를 컴파일러가 자동으로 넣어 주는 어트리뷰트와 로그/INotifyPropertyChanged 활용입니다.
keywords: CallerMemberName, CallerFilePath, CallerLineNumber, CallerArgumentExpression, 호출자 정보, 로그에 메서드 이름, 줄 번호, 로깅, INotifyPropertyChanged, OnPropertyChanged
lesson: 매개변수에서사용되는Attributes
related: attribute-builtin, recipe-logging, property-setter
---
## 핵심
```csharp
using System.Runtime.CompilerServices;

class MyLogger
{
    public static void Log(
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        Console.WriteLine($"{memberName} ({Path.GetFileName(filePath)}:{lineNumber})");
    }
}

static void Test()
{
    MyLogger.Log();    // 인자 없이 호출해도 "Test (파일.cs:12)" 처럼 출력
}
```
- 매개변수에 **기본값이 반드시** 있어야 합니다.
- 값은 **컴파일할 때** 채워집니다(실행 비용 없음).

## 활용 1: 로그
```csharp
void Log(string message, [CallerMemberName] string caller = "") =>
    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {caller}: {message}");
```

## 활용 2: WPF 속성 변경 알림
```csharp
public event PropertyChangedEventHandler? PropertyChanged;
void OnPropertyChanged([CallerMemberName] string? name = null) =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

private string _title = "";
public string Title
{
    get => _title;
    set { _title = value; OnPropertyChanged(); }   // "Title" 이 자동으로 들어감
}
```

## CallerArgumentExpression (C# 10+)
인자로 넘긴 **식 자체의 문자열**을 받습니다.
```csharp
void Check(bool condition, [CallerArgumentExpression(nameof(condition))] string expr = "")
{
    if (!condition) throw new ArgumentException($"조건 실패: {expr}");
}
Check(age > 0);   // "조건 실패: age > 0"
```
