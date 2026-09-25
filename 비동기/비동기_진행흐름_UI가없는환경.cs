namespace InTheEndCsharp.비동기;

public class 비동기_진행흐름_UI가없는환경
{
    public static void 실행()
    {
        async Task TaskAsync()
        {
            Console.WriteLine("Task Async Started");
            await Task.Delay(3000);
            Console.WriteLine("Task Async Finished");
        }

        async Task TaskAsync2()
        {
            Console.WriteLine("Task Async 2 Started");
            await Task.Delay(1500);
            Console.WriteLine("Task Async 2 Finished");
        }
        
        Task task1 = TaskAsync();
        Task task2 = TaskAsync2();

        Task.WhenAll(task1, task2).Wait();
    }
}