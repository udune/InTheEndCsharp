using System.Runtime.CompilerServices;

namespace InTheEndCsharp.어트리뷰트;

public class 매개변수에서사용되는Attributes
{
    public static void 실행()
    {
        Test();
    }

    static void Test()
    {
        MyLogger.Log();
    }

    class MyLogger
    {
        public static void Log(
            [CallerMemberName] string memberName = "", 
            [CallerFilePath] string filePath = "", 
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            Console.WriteLine(memberName);
            Console.WriteLine(filePath);
            Console.WriteLine(sourceLineNumber);
        }
    }
}