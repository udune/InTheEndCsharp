using Castle.DynamicProxy;

namespace InTheEndCsharp.어트리뷰트.Interceptors;

public class LoggingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        string className = invocation.TargetType.Name;
        string methodName = invocation.Method.Name;
            
        Console.WriteLine($"[{DateTime.Now}] {className}.{methodName} Started");
        invocation.Proceed();
        Console.WriteLine($"[{DateTime.Now}] {className}.{methodName} Ended");
    }
}