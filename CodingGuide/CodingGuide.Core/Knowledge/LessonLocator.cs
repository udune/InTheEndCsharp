using System.Reflection;

namespace CodingGuide.Core.Knowledge;

/// <summary>
/// 학습 예제 어셈블리에서 <c>public static void 실행()</c>을 가진 타입을 찾는다.
/// 문서의 "lesson:" 값은 이 타입 이름과 같아야 한다.
/// </summary>
public static class LessonLocator
{
    public const string EntryMethodName = "실행";

    public static IReadOnlyDictionary<string, MethodInfo> Find(Assembly assembly)
    {
        Type[] types;
        try
        {
            types = assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            types = ex.Types.Where(t => t != null).ToArray()!;
        }

        return types
            .Select(t => (Type: t, Method: t.GetMethod(EntryMethodName, BindingFlags.Public | BindingFlags.Static, Type.EmptyTypes)))
            .Where(x => x.Method is { ReturnType: var r } && r == typeof(void))
            .GroupBy(x => x.Type.Name)
            .ToDictionary(g => g.Key, g => g.First().Method!);
    }
}
