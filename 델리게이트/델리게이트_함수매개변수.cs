namespace InTheEndCsharp.델리게이트;

public class 델리게이트_함수매개변수
{
    delegate int Operation(int a, int b);

    public static void 실행()
    {
        void ApplyOperation(int a, int b, Operation operation)
        {
            int result = operation(a, b);
            Console.WriteLine(result);
        }
        
        int Plus(int a, int b) => a + b;
        int Minus(int a, int b) => a - b;
        
        ApplyOperation(5, 10, Plus);
        ApplyOperation(5, 10, Minus);
    }
}