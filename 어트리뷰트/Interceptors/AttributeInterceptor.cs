using System.Reflection;
using Castle.DynamicProxy;
using InTheEndCsharp.어트리뷰트.Attributes;

namespace InTheEndCsharp.어트리뷰트.Interceptors;

public class AttributeInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        ProcessParameters(invocation);
        invocation.Proceed();
    }

    private void ProcessParameters(IInvocation invocation)
    {
        var parameters = invocation.Method.GetParameters();
        for (int i = 0; i < parameters.Length; i++)
        {
            var parameter = parameters[i];
            var attributes = parameter.GetCustomAttributes().OfType<ITransformerAttribute<string>>();
            foreach (var attribute in attributes)
            {
                if (invocation.Arguments[i] is string str)
                {
                    invocation.Arguments[i] = attribute.Transform(str);
                }
            }
        }
    }
}