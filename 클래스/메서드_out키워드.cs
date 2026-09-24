namespace InTheEndCsharp.클래스;

public class 메서드_out키워드
{
    public static void 실행()
    {
        void Test3(out string aaa)
        {
            if (false)
            {
                aaa = "b";
            }
            else
            {
                aaa = "c";
            }
            
            Console.WriteLine($"메서드 내 aaa : {aaa}");
        }

        string text = "1";

        bool success = int.TryParse(text, out int result);
        if (success)
        {
            Console.WriteLine(result);
        }
        else
        {
            Console.WriteLine($"변환에 실패했습니다. {result}");
        }
        
        Test3(out string aaa);

        Console.WriteLine(aaa);
    }
}