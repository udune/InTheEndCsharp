namespace InTheEndCsharp.스레드;

public class 스레드_semaphore
{
    static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(2, 6);

    public static void 실행()
    {
        Console.WriteLine("SemaphoreSlim 동기 예제 시작!");
        // 입장권 추가
        semaphoreSlim.Release(2);
        
        // 여러 스레드 생성
        Thread[] threads = new Thread[10];
        for (int i = 1; i <= 10; i++)
        {
            int threadId = i;
            threads[i - 1] = new Thread(AccessSharedResource);
            threads[i - 1].Start(threadId);
        }
        
        // 모든 스레드가 완료될때까지 대기
        foreach (var thread in threads)
        {
            thread?.Join();
        }

        Console.WriteLine("모든 스레드 작업 완료!");
    }

    static void AccessSharedResource(object? id)
    {
        Console.WriteLine($"스레드 {id} - 공유 자원 접근 시도 중...");

        semaphoreSlim.Wait();
        try
        {
            Console.WriteLine($"스레드 {id} - 공유 자원에 접근!");
            Thread.Sleep(2000);
        }
        finally
        {
            Console.WriteLine($"스레드 {id} - 공유 자원 작업 완료");
            semaphoreSlim.Release();
        }
    }
}