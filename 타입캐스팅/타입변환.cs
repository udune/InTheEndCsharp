namespace InTheEndCsharp.타입캐스팅;

public class 타입변환
{
    public static void 실행()
    {
        object obj = "C# Programming";
        string? str = obj as string;
        int? num = obj as int?;
        
        if (str != null)
        {
            Console.WriteLine($"as 연산자 변환: {str}");
        }

        if (num != null)
        {
            Console.WriteLine($"as 연산자 변환: {num}");
        }
    }
}