namespace InTheEndCsharp.예외처리;

public class 예외처리_예외객체
{
    public static void 실행()
    {
        int[] ints = [1, 2, 3];
        object obj = "abc";

        try
        {
            double d = (double)obj;
            int i = ints[5];
        }
        catch (InvalidCastException e)
        {
            Console.WriteLine("InvalidCastException 예외가 발생되었습니다.");
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine("IndexOutOfRangeException 예외가 발생되었습니다.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine("Hello World!");
    }
}