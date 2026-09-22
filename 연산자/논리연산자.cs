namespace InTheEndCsharp.연산자;

public class 논리연산자
{
    public static void 실행()
    {
        bool a = true;
        bool b = false;

        Console.WriteLine($"a && b = {a && b}");
        Console.WriteLine($"a || b = {a || b}");
        Console.WriteLine($"!(a || b) = {!(a || b)}");
        Console.WriteLine($"a && b = {a && b}");
        
    }
    
    
}