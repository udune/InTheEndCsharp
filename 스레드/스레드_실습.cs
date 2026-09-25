namespace InTheEndCsharp.스레드;

public class 스레드_실습
{
    public static void 실행()
    {
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
                Console.WriteLine(e.Message);
            }
        }

        Thread thread = new Thread(DoWork);
        thread.IsBackground = true;
        thread.Start();

        while (true)
        {
            char c = Console.ReadKey().KeyChar;
            Console.WriteLine("↓↓↓↓↓↓↓↓");

            if (c == 'q')
            {
                break;
            }

            if (c == 'a')
            {
                Console.WriteLine("IsAlive: " + thread.IsAlive);
            }

            if (c == 'i')
            {
                thread.Interrupt();
            }

            if (c == 'j')
            {
                thread.Join();
            }
        }

        Console.WriteLine("메인 스레드 완료!");
    }
}