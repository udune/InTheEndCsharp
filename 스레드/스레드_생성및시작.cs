namespace InTheEndCsharp.스레드;

public class 스레드_생성및시작
{
    public static void 실행()
    {
        void DoWork()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"DoWork {i}");
            }
        }

        Thread thread = new Thread(DoWork);
        thread.Start();

        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Main Thread {i}");
        }
    }
}