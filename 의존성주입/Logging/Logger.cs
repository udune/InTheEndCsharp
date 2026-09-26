namespace InTheEndCsharp.의존성주입.Logging;

public class Logger : ILogger
{
    public Logger()
    {
        Console.WriteLine("=== Logger 생성 ===");
    }
    
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
    }
}