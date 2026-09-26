using Castle.DynamicProxy;

namespace InTheEndCsharp.어트리뷰트.Extensions;

public static class ProxyExtensions
{
    public static T CreateProxy<T>(this T target, params IInterceptor[] interceptors) where T : class
    {
        var proxyGenerator = new ProxyGenerator();
        return proxyGenerator.CreateClassProxyWithTarget(target, interceptors);
    }
}