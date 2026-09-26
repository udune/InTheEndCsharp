using System.Reflection;

namespace InTheEndCsharp.리플렉션;

public class 리플렉션_동적속성읽고쓸때주의할점
{
    public static void 실행()
    {
        Type type = typeof(Sample);
        Sample instance = Activator.CreateInstance<Sample>();

        PropertyInfo? propertyInfo = type.GetProperty("Number1");
        if (propertyInfo!.CanRead)
        {
            var number1 = propertyInfo?.GetValue(instance);   
        }
        
        PropertyInfo? propertyInfo2 = type.GetProperty("Number2");
        if (propertyInfo2!.CanWrite)
        {
            propertyInfo2.SetValue(instance, 3);
        }
    }

    class Sample
    {
        private int number1;
        public int Number1 { set => number1 = value; }
        public int Number2 => 2;
    }
}