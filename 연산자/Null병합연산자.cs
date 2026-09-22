namespace InTheEndCsharp.연산자;

public class Null병합연산자
{
    public static void 실행()
    {
        int? a = null;
        int b = a ?? 3;

        int? c = 2;
        int? d = 5;
        
        Console.WriteLine($"a = {a}");
        Console.WriteLine($"a값이 있는가? {a.HasValue}");
        Console.WriteLine($"b = {b}");
        Console.WriteLine($"c = {c}");
        Console.WriteLine($"d = {d}");
    }
}