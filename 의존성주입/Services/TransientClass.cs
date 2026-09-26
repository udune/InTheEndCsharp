namespace InTheEndCsharp.의존성주입.Services;

public interface ITransientClass { }

public class TransientClass : ITransientClass
{
    public TransientClass()
    {
        Console.WriteLine("=== Transient Class 생성 ===");
    }
}