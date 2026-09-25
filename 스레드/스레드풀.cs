namespace InTheEndCsharp.스레드;

public class 스레드풀
{
    public static void 실행()
    {
        Console.WriteLine("스레드 예제 시작!");

        for (int i = 1; i <= 5; i++)
        {
            // Thread thread = new Thread(DoWork);
            // thread.Start();
            ThreadPool.QueueUserWorkItem(state => DoWork());
        }
    }

    static void DoWork()
    {
        Console.WriteLine($"[ThreadPool] {Thread.CurrentThread.ManagedThreadId} 작업 시작");
        Thread.Sleep(2000);
        Console.WriteLine($"[ThreadPool] {Thread.CurrentThread.ManagedThreadId} 작업 완료");
    }
}