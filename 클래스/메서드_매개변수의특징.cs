using System.IO.Compression;

namespace InTheEndCsharp.클래스;

public class 메서드_매개변수의특징
{
    public static void 실행()
    {
        void Test(int a)
        {
            a++;
            Console.WriteLine($"메서드 내 a : {a}");
        }

        void Test2(int[] aa)
        {
            aa[0]++;
            Console.WriteLine($"메서드 내 aa : {aa[0]}");
        }
        
        void Test3(string aaa)
        {
            aaa = "b";
            Console.WriteLine($"메서드 내 aaa : {aaa}");
        }
        
        int a = 10;
        int[] aa = [10];
        string aaa = "a";
        
        Test(a);
        Test2(aa);
        Test3(aaa);

        Console.WriteLine(a);
        Console.WriteLine(aa[0]);
        Console.WriteLine(aaa);
    }
}