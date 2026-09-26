using InTheEndCsharp.어트리뷰트.Attributes;
using InTheEndCsharp.어트리뷰트.Extensions;
using InTheEndCsharp.어트리뷰트.Interceptors;

namespace InTheEndCsharp.어트리뷰트;

public class CustomAttribute실전활용예제_Parameter
{
    public static void 실행()
    {
        var myServiceProxy = new MyService().CreateProxy(new AttributeInterceptor());
        myServiceProxy.DoSomething("HeLLo", "WoRLd", "123456789");
    }

    public class MyService
    {
        public virtual void DoSomething(
            [ToUpper]
            string upperStr,
            [ToLower]
            string lowerStr,
            [Left(5)]
            string leftStr
            )
        {
            Console.WriteLine($"upperStr: {upperStr}");
            Console.WriteLine($"lowerStr: {lowerStr}");
            Console.WriteLine($"leftStr: {leftStr}");
        }
    }
}