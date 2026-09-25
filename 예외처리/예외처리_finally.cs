namespace InTheEndCsharp.예외처리;

public class 예외처리_finally
{
    public static void 실행()
    {
        object obj = "abc";

        string Error()
        {
            try
            {
                double d = (double)obj;
            }
            catch (Exception e)
            {
                return "Exception 에러가 발생했습니다.";
            }
            finally
            {
                obj = null;
            }

            return "";
        }

        string errorMessage = Error();
        Console.WriteLine($"errorMessage: {errorMessage}, obj: {obj}");
    }
}