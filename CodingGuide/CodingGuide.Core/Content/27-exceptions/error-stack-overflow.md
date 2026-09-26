---
id: error-stack-overflow
title: StackOverflowException - 프로그램이 갑자기 종료됨 (무한 재귀)
category: 흔한 예외
order: 2710
summary: 메서드가 끝없이 자기 자신을 호출하거나, 속성이 자기 자신을 반환해 스택이 넘치면서 프로그램이 catch도 못 하고 종료되는 문제의 원인과 해결입니다.
keywords: StackOverflowException, 스택 오버플로, stack overflow, 무한 재귀, 재귀 호출, 프로그램 강제 종료, 갑자기 꺼짐, 속성 자기 참조, getter 무한 호출, 재귀 종료 조건
related: properties, property-setter, stack
---
## 증상
프로그램이 **예외 창 없이 갑자기 종료**되거나, 출력 창에 `Stack overflow.`가 찍히며 멈춥니다. try-catch로 잡을 수 없습니다.

## 원인 1: 속성이 자기 자신을 부름 (가장 흔함)
```csharp
private string name;
public string Name
{
    get => Name;            // ← 예외! name 이 아니라 Name → 자기 자신을 무한 호출
    set => Name = value;    // ← 예외!
}
```
해결: 백킹 필드(`name`, `_name`)를 쓰거나 자동 속성 `{ get; set; }` 을 쓰세요.

## 원인 2: 종료 조건 없는 재귀
```csharp
int Factorial(int n) => n * Factorial(n - 1);            // ← 예외! 멈추지 않음
int Factorial2(int n) => n <= 1 ? 1 : n * Factorial2(n - 1);   // 종료 조건
```

## 원인 3: 서로를 부르는 메서드 / 이벤트
A가 B를 부르고 B가 A를 부르는 구조, 또는 값 변경 이벤트 안에서 다시 값을 바꿔 이벤트가 연쇄 발생하는 경우.
```csharp
textBox.TextChanged += (s, e) => textBox.Text = textBox.Text.ToUpper();   // 같은 값이면 멈추는지 확인 필요
```

## 깊은 재귀가 필요한 경우
트리 탐색 등이 수만 단계로 깊어질 수 있으면 재귀 대신 `Stack<T>`를 이용한 반복문으로 바꾸세요.
