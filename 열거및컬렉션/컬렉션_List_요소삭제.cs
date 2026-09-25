namespace InTheEndCsharp.열거및컬렉션;

public class 컬렉션_List_요소삭제
{
    public static void 실행()
    {
        var stringList = new List<string> { "a", "b", "c" };
        stringList.Remove("a");
        stringList.RemoveAt(0);
        stringList.RemoveAll((str) => str == "c" | str == "a");
        stringList.Clear();

        foreach (var item in stringList)
        {
            Console.WriteLine(item);
        }
    }    
}