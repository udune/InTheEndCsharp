using System.Diagnostics;
using Castle.DynamicProxy;

namespace InTheEndCsharp.어트리뷰트.Interceptors;

public class TimingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        string className = invocation.TargetType.Name;
        string methodName = invocation.Method.Name;
            
        var stopwatch = Stopwatch.StartNew();
            
        invocation.Proceed();
        stopwatch.Stop();

        Console.WriteLine($"Class: {className}, Method: {methodName}, Elapsed: {stopwatch.ElapsedMilliseconds}");
    }
}