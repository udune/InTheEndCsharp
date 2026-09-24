namespace InTheEndCsharp.클래스;

public class 정적타입_static
{
    public static void 실행()
    {
        MyClass.Print();
    }

    class MyClass
    {
        public static string Name = "MyClass";
        
        public static void Print()
        {
            MyClass myClass = new MyClass();
            Console.WriteLine($"{myClass.MyText} Hello World!");
            
            Console.WriteLine($"{Name} Hello World!");
        }

        public string MyText => "My Text";

        public void MyMethod()
        {
            Print();
            Console.WriteLine(MyText);
        }
    }
}