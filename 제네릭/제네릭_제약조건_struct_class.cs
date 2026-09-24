namespace InTheEndCsharp.제네릭;

public class 제네릭_제약조건_struct_class
{
    public static void 실행()
    {
        int a = 1;
        int b = 2;
        double aa = 1.12;
        double bb = 2.34;
        string aaa = "aaa";
        string bbb = "bbb";
        bool aaaa = true;
        bool bbbb = false;
        Animal aaaaa = new Dog();
        Animal bbbbb = new Cat();

        Console.WriteLine($"{a},{b}");
        Console.WriteLine($"{aa},{bb}");
        Console.WriteLine($"{aaa},{bbb}");

        void Swap<T>(ref T a, ref T b) where T : class // struct, new(), interface
        {
            T temp = a;
            a = b;
            b = temp;
        }
        
        //Swap(ref a, ref b);
        //Swap(ref aa, ref bb);
        Swap(ref aaa, ref bbb);
        //Swap(ref aaaa, ref bbbb);
        Swap(ref aaaaa, ref bbbbb);

        // Console.WriteLine($"{a},{b}");
        // Console.WriteLine($"{aa},{bb}");
        // Console.WriteLine($"{aaa},{bbb}");
        // Console.WriteLine($"{aaaa},{bbbb}");
        Console.WriteLine($"{aaaaa},{bbbbb}");
    }

    abstract class Animal
    {
        public abstract string Name { get; }

        public override string ToString()
        {
            return $"제 이름은 {Name}입니다.";
        }
    }

    class Dog : Animal
    {
        public override string Name => "멍멍이";
    }

    class Cat : Animal
    {
        public override string Name => "냥냥이";
    }
}