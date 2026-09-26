using InTheEndCsharp.의존성주입.Logging;

namespace InTheEndCsharp.의존성주입.Services;

public class MathService : IMathService
{
    ILogger logger;
    
    public MathService(ILogger logger)
    {
        this.logger = logger;
    }
    
    public int Add(int a, int b)
    {
        int result = a + b;
        return result;
    }

    public int Increase(int a, int step = 1)
    {
        logger.Log("MathService.Increase Called");
        return a + step;
    }
}