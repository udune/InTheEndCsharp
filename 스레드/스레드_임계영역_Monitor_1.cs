namespace InTheEndCsharp.스레드;

public class 스레드_임계영역_Monitor_1
{
    static int data = 0;
    static readonly Lock _lock = new Lock();
    
    public static void 실행()
    {
        Thread thread1 = new Thread(DoWork);
        Thread thread2 = new Thread(DoWork);
        
        thread1.Start();
        thread2.Start();
        
        thread1.Join();
        thread2.Join();

        Console.WriteLine("모든 쓰레드 완료");
    }

    private static void DoWork()
    {
        if (Monitor.TryEnter(_lock, TimeSpan.FromSeconds(500)))
        {
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}번 쓰레드 잠금 획득");
                Thread.Sleep(1000);
            }
            finally
            {
                Monitor.Exit(_lock);
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}번 쓰레드 잠금 해제");
            }
        }
        else
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}번 쓰레드 잠금을 획득하지 못했습니다.");
        }
    }
}