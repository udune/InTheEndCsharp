namespace InTheEndCsharp.의존성주입.Services;

public interface ISingletonClass { }

public class SingletonClass : ISingletonClass
{
    public SingletonClass()
    {
        Console.WriteLine("=== Singleton Class 생성 ===");
    }
}