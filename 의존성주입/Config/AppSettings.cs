namespace InTheEndCsharp.의존성주입.Config;

public class AppSettings
{
    public ApiKey ApiKey { get; set; } = new ApiKey();
    public int Port { get; set;  }
    
}

public class ApiKey
{
    public string OpenAI { get; set; }
    public string Claude { get; set; }
}