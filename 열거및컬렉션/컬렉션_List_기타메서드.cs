namespace InTheEndCsharp.열거및컬렉션;

public class 컬렉션_List_기타메서드
{
    public static void 실행()
    {
        var intList = new List<int> { 3, 100, 5, -1, 20 };
        
        int[] ints = intList.ToArray();

        Console.WriteLine(intList.Count);
        intList.Add(33);
        Console.WriteLine(intList.Count);
        intList.Clear();
        Console.WriteLine(intList.Count);
    }
}