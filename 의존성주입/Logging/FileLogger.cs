namespace InTheEndCsharp.의존성주입.Logging;

public class FileLogger : ILogger
{
    private readonly string filePath;
    
    public FileLogger(string filePath)
    {
        this.filePath = filePath;
        Console.WriteLine("=== FileLogger 생성 ===");
    }

    public void Log(string message)
    {
        string logMessage = $"[FileLogger] {DateTime.Now}: {message}";
        File.AppendAllText(filePath, logMessage + Environment.NewLine);
        Console.WriteLine(logMessage);
    }
}