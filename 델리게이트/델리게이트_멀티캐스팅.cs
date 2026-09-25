namespace InTheEndCsharp.델리게이트;

public class 델리게이트_멀티캐스팅
{
    delegate int Operation(int a, int b);
    
    public static void 실행()
    {
        int Plus(int a, int b)
        {
            Console.WriteLine($"{a} + {b}는 {a+b}");
            return a + b;
        }

        int Minus(int a, int b)
        {
            Console.WriteLine($"{a} - {b}는 {a-b}");
            return a - b;
        }

        Operation operation = Plus;
        operation += Minus;
        int result =  operation(1,3);
        Console.WriteLine(result);
    }
}