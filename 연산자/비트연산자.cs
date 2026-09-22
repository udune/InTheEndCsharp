namespace InTheEndCsharp.연산자;

public class 비트연산자
{
    public static void 실행()
    {
        int a = 192;
        int b = 168;

        Console.WriteLine($"a & b = {a & b}");
        Console.WriteLine($"a | b = {a | b}");
        Console.WriteLine($"a ^ b = {a ^ b}");
        Console.WriteLine($"~a = {~a}");
        Console.WriteLine($"a << 2 = {a << 2}");
        Console.WriteLine($"a >> 2 = {a >> 3}");
    }
}