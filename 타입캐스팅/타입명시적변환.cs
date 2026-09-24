namespace InTheEndCsharp.타입캐스팅;

public class 타입명시적변환
{
    public static void 실행()
    {
        int intNum = 100;
        double doubleNum = intNum; // 암시적 변환
        Console.WriteLine($"int : {intNum}, double : {doubleNum}");

        double anotherDouble = 123.456;
        int anotherInt = (int) anotherDouble; // 명시적 변환
        Console.WriteLine($"double : {anotherDouble}, int : {anotherInt}");

        float floatNum = (float) anotherDouble;
        Console.WriteLine($"double: {anotherDouble}, float : {floatNum}");
    }
}