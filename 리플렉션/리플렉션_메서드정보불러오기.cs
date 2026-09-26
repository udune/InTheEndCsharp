using System.Reflection;

namespace InTheEndCsharp.리플렉션;

public class 리플렉션_메서드정보불러오기
{
    public static void 실행()
    {
        Type type = typeof(Sample);
        Console.WriteLine($"Class: {type.Name}");
        Console.WriteLine("Method:");

        foreach (var method in type.GetMethods())
        {
            Console.WriteLine($"Method: {method.Name}");
        }
    }

    class Sample
    {
        private int PrivateNumber1 { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        
        public void Print() => Console.WriteLine("Hello World!");
    }
}