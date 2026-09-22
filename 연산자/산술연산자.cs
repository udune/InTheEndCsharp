namespace InTheEndCsharp.연산자;

public class 산술연산자
{
    public static void 실행()
    {
        int a = 10;
        int b = 20;

        Console.WriteLine($"{a} + {b} = {a + b}");
        Console.WriteLine($"{a} - {b} = {a - b}");
        Console.WriteLine($"{a} * {b} = {a * b}");
        Console.WriteLine($"{a} / {b} = {a / b}");
        Console.WriteLine($"{a} % {b} = {a % b}");

        decimal c = 10;

        Console.WriteLine($"aa++ : {c++}");
        Console.WriteLine($"aa : {c}");
        Console.WriteLine($"++aa : {++c}");
    }
}