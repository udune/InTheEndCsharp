namespace InTheEndCsharp.예외처리;

public class 예외처리_정의및trycatch
{
    public static void 실행()
    {
        int[] ints = [1, 2, 3];

        try
        {
            int i = ints[5];
        }
        catch
        {
            Console.WriteLine("예외가 발생되었습니다.");   
        }

        Console.WriteLine("Hello World!");
    }
}