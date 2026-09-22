namespace InTheEndCsharp.연산자;

public class 할당연산자
{
    public static void 실행()
    {
        decimal a = 10;
        a += 10;
        Console.WriteLine($"a: {a}");
        a -= 10;
        Console.WriteLine($"a: {a}");
        a *= 2;
        Console.WriteLine($"a: {a}");
        a /= 2;
        Console.WriteLine($"a: {a}");
        a %= 2;
        Console.WriteLine($"a: {a}");
    }
}