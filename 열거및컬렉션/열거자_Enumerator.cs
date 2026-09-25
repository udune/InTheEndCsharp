namespace InTheEndCsharp.열거및컬렉션;

public class 열거자_Enumerator
{
    public static void 실행()
    {
        IEnumerator<int> GetEnumerator()
        {
            yield return 1;
            yield return 10;
            yield return 100;
            yield return 1000;
        }
        
        var enumerator = GetEnumerator();

        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current);    
        }
        
    }
}