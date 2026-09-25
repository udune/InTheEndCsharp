namespace InTheEndCsharp.스레드;

public class 스레드_정의_현재스레드확인
{
    public static void 실행()
    {
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
    }
}