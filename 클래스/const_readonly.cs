namespace InTheEndCsharp.클래스;

public class const_readonly
{
    public static void 실행()
    {
        
    }
    
    class MyClass
    {
        const double PI_CONST = 3.14159;
        readonly double pi_readonly = 3.14159;

        public MyClass()
        {
            // PI_CONST = 3.14; // 불가능
            pi_readonly = 3.14; // 가능
        }

        void MyMethod()
        {
            // PI_CONST = 3.14; // 불가능
            // PI_CONST = 3.14; // 불가능

            const string name = "John";
        }
    }
}