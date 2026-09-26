using System.Reflection;

namespace InTheEndCsharp.리플렉션;

public class 리플렉션_동적메서드호출
{
    public static void 실행()
    {
        Type type = typeof(Sample);
        
        Sample instance = Activator.CreateInstance<Sample>();

        MethodInfo? methodInfo = type.GetMethod("Print");
        methodInfo?.Invoke(instance, ["까불이", 3]);
    }

    class Sample
    {
        private string privateStr = "abc";
        private int PrivateNumber1 { get; set; }
        public int Number1 { get; set; } = 1;
        public int Number2 { get; set; } = 2;
        
        public void Print(string text, int count) => Console.WriteLine($"Hello World!: {text} - {count}");
    }
}