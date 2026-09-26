using System.Diagnostics;

namespace InTheEndCsharp.어트리뷰트;

public class Attribute정의및적용하기_Obsolete_Conditional
{
    public static void 실행()
    {
        var myClass = new MyClass();
        myClass.Print();
        myClass.PrintValue("Hello World!");
    }

    class MyClass
    {
        [Obsolete("더 이상 사용되지 않는 메서드입니다. 앞으로는 PrintValue를 사용해주세요.")]
        // [Obsolete("더 이상 사용되지 않는 메서드입니다. 앞으로는 PrintValue를 사용해주세요.", true)]
        public void Print()
        {
            Console.WriteLine("Hello World!");
        }

        // [Conditional("DEBUG")]
        [Conditional("RELEASE")]
        public void PrintValue(string text)
        {
            Console.WriteLine(text);
        }
    }
}