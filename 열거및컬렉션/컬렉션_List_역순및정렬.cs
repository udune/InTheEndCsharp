namespace InTheEndCsharp.열거및컬렉션;

public class 컬렉션_List_역순및정렬
{
    public static void 실행()
    {
        var intList = new List<int> { 3, 100, 5, -1, 20 };
        intList.Reverse();
        intList.Sort((a, b) => a.CompareTo(b));

        foreach (var value in intList)
        {
            Console.WriteLine(value);
        }
    }
}