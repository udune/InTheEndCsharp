namespace InTheEndCsharp.스레드;

public class 스레드_임계영역_Monitor_2
{
    private static object _lock =  new object();
    private static bool _isReady = false;

    public static void 실행()
    {
        Thread producer = new Thread(Producer);
        Thread consumer = new Thread(Consumer);
        
        consumer.Start();
        producer.Start();
        
        producer.Join();
        consumer.Join();

        Console.WriteLine("모든 스레드가 완료되었습니다.");
    }

    static void Producer()
    {
        lock (_lock)
        {
            Console.WriteLine("Producer: 생산자가 데이터를 준비중입니다...");
            Thread.Sleep(2000);
            _isReady = true;
            Console.WriteLine("Producer: 생산자가 데이터 준비 완료를 알립니다.");
            Monitor.Pulse(_lock);
        }
    }

    static void Consumer()
    {
        lock (_lock)
        {
            while (!_isReady)
            {
                Console.WriteLine("Consumer: 소비자가 데이터를 기다리고 있습니다...");
                Monitor.Wait(_lock);
            }

            Console.WriteLine("Consumer: 소비자가 데이터를 받았습니다.");
        }
    }
}