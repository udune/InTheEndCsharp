---
id: thread-practice
title: 스레드 제어 실습 (IsBackground, IsAlive, Interrupt, Join)
category: 스레드
order: 1703
summary: 키 입력으로 스레드 상태 확인(IsAlive), 깨우기(Interrupt), 대기(Join)를 해보는 실습과 백그라운드 스레드의 의미입니다.
keywords: IsBackground, IsAlive, Interrupt, ThreadInterruptedException, Join, 스레드 상태, 스레드 중지, 스레드 종료, 백그라운드 스레드, ReadKey
lesson: 스레드_실습
runnable: false
related: thread-create, cancellation-token
---
> 이 예제는 **키보드 입력(Console.ReadKey)** 을 받기 때문에 이 프로그램 안에서는 실행할 수 없습니다. 원래 프로젝트(콘솔)에서 실행하세요.

## 예제 동작
```csharp
Thread thread = new Thread(DoWork);
thread.IsBackground = true;     // 메인이 끝나면 같이 종료
thread.Start();

while (true)
{
    char c = Console.ReadKey().KeyChar;
    if (c == 'q') break;                                         // 메인 종료 → 백그라운드 스레드도 종료
    if (c == 'a') Console.WriteLine("IsAlive: " + thread.IsAlive); // 살아 있는지
    if (c == 'i') thread.Interrupt();                            // Sleep 중이면 깨워서 예외 발생
    if (c == 'j') thread.Join();                                 // 끝날 때까지 대기
}
```

## Interrupt
Sleep/Join/Wait로 **대기 중인** 스레드에 `ThreadInterruptedException`을 발생시킵니다. 스레드 안에서 이 예외를 catch 해서 정리하고 끝내게 합니다.
```csharp
void DoWork()
{
    try
    {
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"DoWork {i}");
        }
    }
    catch (ThreadInterruptedException e)
    {
        Console.WriteLine("중단됨: " + e.Message);
    }
}
```

## 스레드를 멈추는 올바른 방법
- `Thread.Abort()`는 .NET Core 이후 **지원되지 않습니다**.
- 작업을 멈추고 싶으면 **CancellationToken**으로 "그만해 달라"고 요청하고, 작업 쪽에서 확인하고 스스로 끝내게 합니다.
