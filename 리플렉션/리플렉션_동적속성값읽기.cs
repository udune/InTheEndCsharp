using System.Reflection;

namespace InTheEndCsharp.리플렉션;

public class 리플렉션_동적속성값읽기
{
    public static void 실행()
    {
        Type type = typeof(Sample);
        
        Sample instance = Activator.CreateInstance<Sample>();
        
        Console.WriteLine($"Number1: {instance.Number1}, Number2: {instance.Number2}");

        Console.WriteLine("------");

        foreach (int i in Enumerable.Range(1, 2))
        {
            string propertyName = $"Number{i}";
            PropertyInfo? propInfo = type.GetProperty(propertyName);
            object? number = propInfo?.GetValue(instance);
            Console.WriteLine($"{propertyName}: {number}");
        }
    }

    class Sample
    {
        private int PrivateNumber1 { get; set; }
        public int Number1 { get; set; } = 1;
        public int Number2 { get; set; } = 2;
        
        public void Print() => Console.WriteLine("Hello World!");
    }
}