namespace InTheEndCsharp.스레드;

public class 스레드_경쟁상태
{
    public static void 실행()
    {
        int data = 0;
        
        void DoWork()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1);
                data++;
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