namespace InTheEndCsharp.델리게이트;

public class 델리게이트_Comparison
{
    public static void 실행()
    {
        int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }
        
        Comparison<int> comparison = Compare;
        
        Console.WriteLine(comparison.Invoke(5, 3));
        Console.WriteLine(comparison.Invoke(3, 3));
        Console.WriteLine(comparison.Invoke(3, 5));
    }
}