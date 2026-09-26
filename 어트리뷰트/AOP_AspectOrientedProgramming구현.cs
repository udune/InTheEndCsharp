using System.Diagnostics;
using Castle.DynamicProxy;
using InTheEndCsharp.어트리뷰트.Extensions;
using InTheEndCsharp.어트리뷰트.Interceptors;

namespace InTheEndCsharp.어트리뷰트;

public class AOP_AspectOrientedProgramming구현
{
    public static void 실행()
    {
        var myServiceProxy = new MyService().CreateProxy(new LoggingInterceptor(), new TimingInterceptor());
        myServiceProxy.DoSomething();
        int result = myServiceProxy.Add(1, 2);
        Console.WriteLine(result);
    }

    public class MyService
    {
        public virtual void DoSomething()
        {
            Thread.Sleep(1000);
            Console.WriteLine("DoSomething");
        }
        
        public virtual int Add(int a, int b)
        {
            Thread.Sleep(2000);
            Console.WriteLine("Add Called");
            return a + b;
        }
    }
}