namespace InTheEndCsharp.열거및컬렉션;

public class 컬렉션_List_초기화
{
    public static void 실행()
    {
        var stringList = new List<string> { "a", "b", "c" };
        List<string> anotherStringList = ["가", "나", "다"];

        foreach (var item in stringList)
        {
            Console.WriteLine(item);
        }
        
        foreach (var item in anotherStringList)
        {
            Console.WriteLine(item);
        }
    }
}