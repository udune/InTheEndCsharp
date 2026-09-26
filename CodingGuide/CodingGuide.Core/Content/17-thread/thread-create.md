---
id: thread-create
title: 스레드 생성과 시작 (new Thread, Start, Join)
category: 스레드
order: 1702
summary: new Thread(메서드)로 스레드를 만들고 Start로 시작해 메인 스레드와 동시에 실행되는 모습, Join으로 끝날 때까지 기다리는 방법입니다.
keywords: Thread, new Thread, Start, Join, 스레드 생성, 스레드 시작, 동시 실행, 기다리기, 매개변수 전달, ParameterizedThreadStart, Thread.Sleep
lesson: 스레드_생성및시작
related: thread-intro, thread-practice, threadpool, task-run
---
## 핵심
```csharp
void DoWork()
{
    for (int i = 0; i < 10; i++)
    {
        Thread.Sleep(1000);                 // 1초 쉬기 (이 스레드만 멈춤)
        Console.WriteLine($"DoWork {i}");
    }
}

Thread thread = new Thread(DoWork);         // 만들기 (아직 실행 안 됨)
thread.Start();                             // 시작 → 이제 두 흐름이 동시에 진행

for (int i = 0; i < 10; i++)
{
    Thread.Sleep(1000);
    Console.WriteLine($"Main Thread {i}");
}
// 출력이 DoWork / Main Thread 섞여서 나옴 (순서는 매번 다를 수 있음)
```

## 끝날 때까지 기다리기 - Join
```csharp
thread.Start();
thread.Join();          // thread 가 끝날 때까지 현재 스레드가 대기
Console.WriteLine("작업 끝");
```

## 값 넘기기
```csharp
var t1 = new Thread(id => Console.WriteLine($"스레드 {id}"));
t1.Start(7);                              // object 로 전달

int n = 5;
var t2 = new Thread(() => Work(n));       // 람다로 캡처 (타입 안전, 권장)
t2.Start();
```

## 포그라운드 vs 백그라운드
- 기본(포그라운드) 스레드가 하나라도 살아 있으면 **프로그램이 종료되지 않습니다**.
- `thread.IsBackground = true`이면 메인이 끝날 때 함께 종료됩니다.
