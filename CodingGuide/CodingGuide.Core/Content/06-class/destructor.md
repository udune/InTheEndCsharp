---
id: destructor
title: 소멸자(종료자)와 가비지 컬렉터, IDisposable
category: 클래스
order: 616
summary: 객체가 메모리에서 정리될 때 실행되는 소멸자(~클래스), 가비지 컬렉터(GC)의 동작, 실무에서 쓰는 using/IDisposable을 설명합니다.
keywords: 소멸자, 종료자, finalizer, destructor, ~, 가비지 컬렉터, GC, 메모리 해제, 메모리 관리, IDisposable, Dispose, using, 리소스 해제
lesson: 소멸자
related: recipe-using-dispose, class-define, exception-finally
---
## 핵심
C#은 **가비지 컬렉터(GC)** 가 더 이상 쓰이지 않는 객체를 자동으로 치웁니다. 소멸자는 GC가 객체를 치우기 직전에 호출됩니다.

```csharp
class MyClass
{
    private readonly int index;
    public MyClass(int index) { this.index = index; }

    ~MyClass()                    // 소멸자: ~클래스이름, 매개변수/접근제어자 없음
    {
        Console.WriteLine(index);
    }
}

for (int i = 0; i < 1_000_000; i++)
    new MyClass(i);   // 메모리가 차면 GC 가 돌면서 소멸자가 "언젠가" 불림
```
> 예제의 `Test()` 호출은 주석 처리되어 있습니다. 풀어서 실행하면 GC가 도는 시점에 순서 없이 번호가 찍히는 것을 볼 수 있습니다.

## 소멸자를 직접 쓸 일은 거의 없습니다
- **언제 호출될지 알 수 없습니다.** (GC가 결정)
- 소멸자가 있는 객체는 GC가 한 번 더 처리해야 해서 **느려집니다**.

## 실무: 파일/DB/네트워크 같은 자원은 IDisposable + using
```csharp
using (var writer = new StreamWriter("log.txt"))
{
    writer.WriteLine("hello");
}   // 블록을 벗어나는 즉시 Dispose() 호출 → 파일 닫힘

// C# 8+ using 선언: 현재 범위(메서드)가 끝날 때 Dispose
using var conn = new SqlConnection(cs);
```
