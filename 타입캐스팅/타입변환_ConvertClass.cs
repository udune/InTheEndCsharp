namespace InTheEndCsharp.타입캐스팅;

public class 타입변환_ConvertClass
{
    public static void 실행()
    {
        string strNumber = "789";
        int convertedInt = Convert.ToInt32(strNumber);
        Console.WriteLine($"String: {strNumber}, ConvertedInt: {convertedInt}");
    }
}