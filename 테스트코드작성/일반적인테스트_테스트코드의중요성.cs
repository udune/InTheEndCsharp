namespace InTheEndCsharp.테스트코드작성;

public class 일반적인테스트_테스트코드의중요성
{
    public static void 실행()
    {
        var calculator = new Calculator();
        int result = calculator.Add(3, 5);
        Console.WriteLine(result == 8);
    }
}