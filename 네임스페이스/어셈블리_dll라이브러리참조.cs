namespace InTheEndCsharp.네임스페이스;

public class 어셈블리_dll라이브러리참조
{
    public static int Add(int a, int b)
    {
        return a + b;
    }

    public static void 실행()
    {
        int result = 어셈블리_dll라이브러리참조.Add(1, 2);
        Console.WriteLine(result);
    }
}