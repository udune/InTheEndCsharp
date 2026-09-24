namespace InTheEndCsharp.타입캐스팅;

public class 타입변환_is
{
    public static void 실행()
    {
        object obj = "C# Programming";

        if (obj is string str)
        {
            Console.WriteLine($"is 연산자 변환 성공 : {str}");
        }
    }
}