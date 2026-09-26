using System.Reflection;

namespace InTheEndCsharp.리플렉션;

public class 리플렉션_속성정보불러오기
{
    public static void 실행()
    {
        Type type = typeof(Sample);
        Console.WriteLine($"Class: {type.Name}");
        Console.WriteLine("Properties:");
        foreach (var prop in type.GetProperties())
        {
            Console.WriteLine(prop.Name);
        }

        foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            Console.WriteLine(prop.Name);
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