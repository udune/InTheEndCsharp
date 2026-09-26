---
id: delegate-action
title: Action 델리게이트 - 반환값이 없는 메서드
category: 델리게이트와 이벤트
order: 1207
summary: 미리 정의된 제네릭 델리게이트 Action<T...>로 반환값이 없는(void) 메서드를 담는 방법입니다.
keywords: Action, Action<T>, void 델리게이트, 반환값 없는 델리게이트, 내장 델리게이트, 콜백, Action 배열
lesson: 델리게이트_Action
related: delegate-func, delegate-event, lambda-expression
---
## 핵심
`Action<매개변수 타입들>` — 반환값이 **없는** 메서드를 담습니다. 매개변수는 최대 16개까지 가능합니다.

```csharp
void Print(string str, int num)
{
    Console.WriteLine(str);
    Console.WriteLine(num);
}

Action<string, int> action = Print;
action.Invoke("델리게이트", 2);
action("델리게이트", 2);          // 같은 의미

Action hello = () => Console.WriteLine("hi");   // 매개변수 없는 Action
Action<string> log = Console.WriteLine;          // 기존 메서드 연결
```

## 활용 예
이 저장소의 `Program.cs`도 `Action[]` 배열에 예제 메서드들을 담아 차례로 실행합니다.
```csharp
Action[] practices = [정수형타입.실행, 소수형타입.실행];
foreach (var practice in practices)
    practice();
```
> 배열에 넣을 때는 `정수형타입.실행()`이 아니라 괄호 없는 `정수형타입.실행` 이어야 합니다. 괄호를 붙이면 메서드를 **실행한 결과(void)** 를 넣으려는 것이 되어 컴파일 오류가 납니다.

```csharp
// 작업 완료 후 콜백
void Download(string url, Action<string> onCompleted)
{
    string data = "...";
    onCompleted(data);
}
Download("http://...", data => Console.WriteLine($"받음: {data.Length}"));
```
