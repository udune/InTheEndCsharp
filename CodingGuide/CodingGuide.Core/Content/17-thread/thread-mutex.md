---
id: thread-mutex
title: Mutex - 프로세스 간 잠금, 프로그램 중복 실행 방지
category: 스레드
order: 1708
summary: 이름 있는 Mutex로 서로 다른 프로그램(프로세스) 사이에서도 잠금을 공유하고, 프로그램이 두 번 실행되지 않게 막는 방법입니다.
keywords: Mutex, 뮤텍스, 프로세스 간 동기화, 중복 실행 방지, 단일 인스턴스, single instance, 프로그램 두 번 실행, WaitOne, ReleaseMutex, AbandonedMutexException, Global
lesson: 스레드_임계영역_Mutex
related: thread-lock, thread-semaphore
---
## 핵심
`lock`은 한 프로그램 안의 스레드끼리만 동작합니다. **이름 있는 Mutex**는 운영체제 수준의 잠금이라 **다른 프로그램과도** 공유됩니다.

```csharp
const string mutexName = "Global\\MyUniqueMutex";   // Global\ = 모든 사용자 세션에서 공유

using var mutex = new Mutex(false, mutexName, out bool createdNew);
if (createdNew)
    Console.WriteLine("뮤텍스를 새로 만들었습니다.");
else
    Console.WriteLine("다른 프로그램이 이미 사용 중입니다. 기다립니다...");

try
{
    mutex.WaitOne();                 // 잠금 획득 (다른 프로세스가 가지고 있으면 대기)
    Console.WriteLine("뮤텍스를 점유했습니다!");
    // 작업
    mutex.ReleaseMutex();            // 잡은 스레드가 직접 해제
}
catch (AbandonedMutexException)
{
    // 이전 소유자가 해제하지 않고 종료됨 → 잠금은 얻었지만 데이터가 불완전할 수 있음
}
```

## 활용: 프로그램 중복 실행 막기
```csharp
using var mutex = new Mutex(true, "MyApp_SingleInstance", out bool isFirst);
if (!isFirst)
{
    MessageBox.Show("이미 실행 중입니다.");
    return;
}
Application.Run(new MainForm());
```

## 주의할 점
- Mutex는 **잡은 스레드가 해제**해야 합니다. 다른 스레드에서 `ReleaseMutex`하면 예외가 납니다. 그래서 async/await 코드와는 잘 맞지 않습니다.
- lock보다 훨씬 느립니다. 프로세스 간 동기화가 필요할 때만 쓰세요.
