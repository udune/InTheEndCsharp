namespace InTheEndCsharp.람다;

public class 람다표현식
{
    public static void 실행()
    {
        Func<int, int, int> operation = (a, b) => a + b;
        Action<int, int> operation2 = (a, b) => { Console.WriteLine(a + b); };

        Console.WriteLine(operation.Invoke(1, 2));
        operation2.Invoke(1, 2);
    }
}