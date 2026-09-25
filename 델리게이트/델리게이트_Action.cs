namespace InTheEndCsharp.델리게이트;

public class 델리게이트_Action
{
    public static void 실행()
    {
        void ActionMethod(string str, int num)
        {
            Console.WriteLine(str);
            Console.WriteLine(num);
        }
        
        Action<string, int> action = ActionMethod;
        action.Invoke("델리게이트", 2);
    }
}