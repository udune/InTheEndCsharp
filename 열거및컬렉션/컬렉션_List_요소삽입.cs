namespace InTheEndCsharp.열거및컬렉션;

public class 컬렉션_List_요소삽입
{
    public static void 실행()
    {
        var stringList = new List<string> { "a", "b", "c" };
        List<string> anotherStringList = ["가", "나", "다"];
        stringList.Insert(1, "z");
        stringList.InsertRange(2, anotherStringList);

        foreach (var item in stringList)
        {
            Console.WriteLine(item);
        }
    }    
}