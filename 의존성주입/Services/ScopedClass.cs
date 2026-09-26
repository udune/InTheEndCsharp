namespace InTheEndCsharp.의존성주입.Services;

public interface IScopedClass { }

public class ScopedClass : IScopedClass
{
    public ScopedClass()
    {
        Console.WriteLine("=== Scoped Class 생성 ===");
    }
}