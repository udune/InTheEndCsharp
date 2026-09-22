namespace InTheEndCsharp.조건문;

public class if_삼항연산자
{
    public static void 실행()
    {
        int a = 1;
        int b = 2;
        
        if (a == b)
        {
            Console.WriteLine("a와 b는 같다");
        }
        else
        {
            Console.WriteLine("a와 b는 같지 않다");
        }
        
        // string? input = Console.ReadLine();
        // int number = int.Parse(input ?? "0");
        // if (number % 2 == 0)
        // {
        //     Console.WriteLine("입력한 숫자는 짝수입니다.");
        // }
        // else
        // {
        //     Console.WriteLine("입력한 숫자는 홀수입니다.");
        // }
        // Console.WriteLine($"입력한 숫자는 {input} 입니다.");
    }
}