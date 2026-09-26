namespace InTheEndCsharp.의존성주입.Services;

public interface ICounterService
{
    void Increase();
}

public class CounterService : ICounterService
{
    private int count;
    private readonly IMathService mathService;

    public CounterService(int defaultCount, IMathService mathService)
    {
        count = defaultCount;
        this.mathService = mathService;
    }
    
    public void Increase()
    {
        count = mathService.Increase(count);
        Console.WriteLine($"CounterService.Increase Result: {count}");
    }
}