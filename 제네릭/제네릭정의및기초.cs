namespace InTheEndCsharp.제네릭;

public class 제네릭정의및기초
{
    public static void 실행()
    {
        int a = 1;
        int b = 2;
        double aa = 1.12;
        double bb = 2.34;
        string aaa = "aaa";
        string bbb = "bbb";

        Console.WriteLine($"{a},{b}");
        Console.WriteLine($"{aa},{bb}");
        Console.WriteLine($"{aaa},{bbb}");

        void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        void Swap2<T, T2>(ref T a, ref T2 b)
        {
            
        }
        
        Swap(ref a, ref b);
        Swap(ref aa, ref bb);
        Swap(ref aaa, ref bbb);

        Console.WriteLine($"{a},{b}");
        Console.WriteLine($"{aa},{bb}");
        Console.WriteLine($"{aaa},{bbb}");
    }
}