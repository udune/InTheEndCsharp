namespace InTheEndCsharp.스레드;

public class 스레드_임계영역_lock
{
    static int data = 0;
    static readonly Lock lockObject = new Lock();
    
    public static void 실행()
    {
        void DoWork()
        {
            for (int i = 0; i < 10; i++)
            {
                lock (lockObject)
                {
                    Thread.Sleep(1);
                    data++;
                }
            }
        }
        
        List<Thread> threads = new List<Thread>();
        for (int i = 0; i < 10; i++)
        {
            Thread thread = new Thread(DoWork);
            threads.Add(thread);
            thread.Start();
        }
        
        threads.ForEach(t => t.Join());
        Console.WriteLine(data);
    }
}