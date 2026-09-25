namespace InTheEndCsharp.예외처리;

public class 사용자정의예외처리
{
    public static void 실행()
    {
        try
        {
            throw new CustomException("커스텀 에러 발생", 2);
        }
        catch (CustomException e)
        {
            Console.WriteLine($"{e.ErrorCode}||{e.Message}");
        }
    }

    class CustomException : Exception
    {
        public int ErrorCode;
        
        public CustomException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}