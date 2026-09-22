namespace InTheEndCsharp.연산자;

public class 비교연산자
{
    public static void 실행()
    {
        Console.WriteLine($"1 == 1 : {1 == 1}" );
        Console.WriteLine($"1 == 2 : {1 == 2}" );
        
        Console.WriteLine($"1 != 1 : {1 != 1}" );
        Console.WriteLine($"1 != 2 : {1 != 2}" );

        string a = "안녕";
        string b = "안녕";
        Console.WriteLine(a == b);
    }
}