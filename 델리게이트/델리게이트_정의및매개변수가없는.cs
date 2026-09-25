namespace InTheEndCsharp.델리게이트;

public class 델리게이트_정의및매개변수가없는
{
    delegate void MyDelegate();
    
    public static void 실행()
    {
        void MyMethod()
        {
            Console.WriteLine("안녕하세요");
        }
        
        MyDelegate myDelegate = MyMethod;
        myDelegate();
    }
}