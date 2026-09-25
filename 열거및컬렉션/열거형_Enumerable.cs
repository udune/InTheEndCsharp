namespace InTheEndCsharp.열거및컬렉션;

public class 열거형_Enumerable
{
    public static void 실행()
    {
        Collection collection = new Collection();
        foreach (int value in collection)
        {
            Console.WriteLine(value);
        }

        Console.WriteLine("=========");

        IEnumerable<int> GetEnumerable()
        {
            yield return 1;
            yield return 10;
            yield return 100;
            yield return 1000;
        }

        foreach (var value in GetEnumerable())
        {
            Console.WriteLine(value);
        }
        
        var enumerator = GetEnumerable().GetEnumerator();
        enumerator.MoveNext();
        int value2 = enumerator.Current;
        Console.WriteLine(value2);
    }

    class Collection
    {
        public IEnumerator<int> GetEnumerator()
        {
            yield return 1;
            yield return 10;
            yield return 100;
            yield return 1000;
        }
    }
}