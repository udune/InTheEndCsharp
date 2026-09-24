namespace InTheEndCsharp.클래스;

public class 소멸자
{
    public static void 실행()
    {
        void Test()
        {
            for (int i = 0; i < 1000000; i++)
            {
                new MyClass(i);
            }
        }

        //Test();
    }

    class MyClass
    {
        private readonly int index;

        public MyClass(int index)
        {
            this.index = index;
        }        
            
        ~MyClass()
        {
            Console.WriteLine(index);
        }
    }
}