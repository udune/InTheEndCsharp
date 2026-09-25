namespace InTheEndCsharp.스레드;

public class 스레드_임계영역_Mutex
{
    public static void 실행()
    {
        const string mutexName = "Global\\MyUniqueMutex";

        using (var mutex = new Mutex(false, mutexName, out var isCreatedNew))
        {
            if (isCreatedNew)
            {
                Console.WriteLine("뮤텍스를 새로 생성했습니다. 프로그램이 MutexCreator를 대신 점유합니다.");
            }
            else
            {
                Console.WriteLine("MutexCreator가 실행 중입니다. 뮤텍스를 기다립니다...");
            }

            try
            {
                mutex.WaitOne();
                Console.WriteLine("뮤텍스를 점유했습니다!");
            }
            catch (AbandonedMutexException)
            {
                Console.WriteLine("뮤텍스가 포기된 상태에서 점유되었습니다.");

                Console.WriteLine("뮤텍스를 유지하여 실행중입니다. 10초 후 종료합니다.");
                Thread.Sleep(10000);
                
                mutex.ReleaseMutex();
                Console.WriteLine("뮤텍스를 해제하였습니다.");
            }
        }
    }
}