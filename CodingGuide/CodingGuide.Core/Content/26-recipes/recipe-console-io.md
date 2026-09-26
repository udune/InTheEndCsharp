---
id: recipe-console-io
title: 콘솔 입출력 - 입력 받기, 색상, 한글 깨짐, 메뉴 만들기
category: 실무 레시피
order: 2614
summary: Console.ReadLine으로 입력 받기, 키 입력(ReadKey), 글자 색 바꾸기, 한글 깨짐 해결, 간단한 콘솔 메뉴 반복 구조입니다.
keywords: 콘솔, console, 입력, 입력 받기, ReadLine, ReadKey, 키 입력, 출력, WriteLine, Write, 글자 색, ForegroundColor, 콘솔 색상, 한글 깨짐, OutputEncoding, 메뉴, 화면 지우기, Clear, 프로그램 종료 방지, 아무 키나
related: recipe-parse-number, if-ternary, loops, thread-practice
---
## 입력 받기
```csharp
Console.Write("이름: ");
string? name = Console.ReadLine();          // Enter 까지 한 줄 (null 일 수 있음)

Console.Write("나이: ");
if (!int.TryParse(Console.ReadLine(), out int age))
    Console.WriteLine("숫자를 입력하세요");

ConsoleKeyInfo key = Console.ReadKey(intercept: true);   // 키 하나 (화면에 표시 안 함)
if (key.Key == ConsoleKey.Escape) return;
```

## 출력 꾸미기
```csharp
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("오류!");
Console.ResetColor();

Console.Clear();                              // 화면 지우기
Console.Title = "내 프로그램";
```

## 한글이 깨질 때
```csharp
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;
```

## 메뉴 반복 구조
```csharp
while (true)
{
    Console.WriteLine("1. 추가  2. 목록  0. 종료");
    Console.Write("> ");
    switch (Console.ReadLine())
    {
        case "1": Add(); break;
        case "2": List(); break;
        case "0": return;
        default: Console.WriteLine("잘못된 입력"); break;
    }
}
```

## 끝나고 창이 바로 닫힐 때
```csharp
Console.WriteLine("아무 키나 누르면 종료합니다.");
Console.ReadKey();
```

## 주의할 점
- `Console.ReadKey`는 실제 콘솔 창이 있어야 동작합니다. WPF/WinForms 앱이나 출력이 리디렉션된 환경에서는 `InvalidOperationException`이 납니다.
