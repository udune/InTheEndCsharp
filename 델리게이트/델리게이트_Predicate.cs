namespace InTheEndCsharp.델리게이트;

public class 델리게이트_Predicate
{
    public static void 실행()
    {
        bool IsGreaterThanZero(int value)
        {
            return value > 0;
        }
        
        Predicate<int> predicate = IsGreaterThanZero;
        Console.WriteLine(predicate.Invoke(2));
    }
}