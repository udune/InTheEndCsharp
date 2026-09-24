namespace InTheEndCsharp.클래스;

public class 메서드_ref키워드
{
    public static void 실행()
    {
        void Test3(ref string aaa)
        {
            aaa = "b";
            Console.WriteLine($"메서드 내 aaa : {aaa}");
        }
        
        string aaa = "a";
        
        Test3(ref aaa);

        Console.WriteLine(aaa);
    }
}