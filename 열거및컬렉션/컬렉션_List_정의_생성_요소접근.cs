namespace InTheEndCsharp.열거및컬렉션;

public class 컬렉션_List_정의_생성_요소접근
{
    public static void 실행()
    {
        List<string> stringList = new List<string>();
        stringList.Add("a");
        stringList.Add("b");
        stringList.Add("c");

        stringList[1] = "f";
        Console.WriteLine(stringList[1]);

        foreach (var item in stringList)
        {
            Console.WriteLine(item);
        }
    }
}