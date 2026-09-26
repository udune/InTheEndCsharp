---
id: thread-intro
title: 스레드란? - 현재 스레드 확인
category: 스레드
order: 1701
summary: 프로그램 안에서 동시에 실행되는 작업 흐름인 스레드의 개념과 현재 스레드 ID를 확인하는 방법입니다.
keywords: 스레드, thread, 쓰레드, 멀티스레드, 동시 실행, 병렬, ManagedThreadId, CurrentThread, 메인 스레드, 프로세스
lesson: 스레드_정의_현재스레드확인
related: thread-create, threadpool, async-await, thread-race-condition
---
## 핵심
- **프로세스**: 실행 중인 프로그램 하나 (메모리를 따로 가짐)
- **스레드**: 프로세스 안에서 코드를 실행하는 **작업 흐름**. 한 프로세스에 여러 개가 있을 수 있고, **메모리(변수)를 공유**합니다.

```csharp
Console.WriteLine(Thread.CurrentThread.ManagedThreadId);   // 지금 이 코드를 실행 중인 스레드 번호
Console.WriteLine(Environment.CurrentManagedThreadId);     // 같은 값 (더 간단)
```

## 왜 스레드를 쓰나요?
- 오래 걸리는 작업(파일, 네트워크, 계산) 동안 **화면이 멈추지 않게**
- 여러 CPU 코어로 **계산을 나눠서** 빠르게

## 현대 C#에서는
직접 `Thread`를 만드는 일은 드물고, 대부분 아래를 씁니다.
- I/O 대기(파일, HTTP, DB): **async/await**
- CPU 계산을 백그라운드로: **`Task.Run`**
- 대량 데이터 병렬 처리: **`Parallel.ForEach`**, PLINQ

## 주의할 점
- 여러 스레드가 같은 변수를 동시에 바꾸면 **경쟁 상태**가 생깁니다. (→ lock)
- WinForms/WPF의 화면 컨트롤은 **UI 스레드에서만** 만질 수 있습니다.
