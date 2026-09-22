namespace InTheEndCsharp;

public class bool_char타입
{
    public static void 실행()
    {
        bool isTrue = false;
        char character = 'A';
        char[] chars = ['i', 'o', 'u'];

        char upperA = '\u0041';
        int upperAInt = (int)upperA;
        int upperBInt = upperAInt + 1;
        char upperB = (char)upperBInt;
        
        Console.WriteLine(character); // A
        Console.WriteLine(upperA); // A
        Console.WriteLine(upperAInt); // 65
        Console.WriteLine(upperB); // B
    }
    
}