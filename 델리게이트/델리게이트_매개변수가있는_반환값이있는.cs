namespace InTheEndCsharp.델리게이트;

public class 델리게이트_매개변수가있는_반환값이있는
{
    delegate void Operation(int a, int b);
    delegate int OperationRet(int a, int b);
    
    public static void 실행()
    {
        void MyMethod(int a, int b)
        {
            Console.WriteLine("안녕하세요");
            Console.WriteLine(a+b);
        }

        int Plus(int a, int b)
        {
            return a + b;
        }

        Operation operation = MyMethod;
        OperationRet operationRet = Plus;
        operation(1,3);
        int result = operationRet(1,3);
        Console.WriteLine(result);
    }
}