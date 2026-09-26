namespace InTheEndCsharp.리플렉션;

public class 리플렉션_인스턴스생성
{
    public static void 실행()
    {
        Type type = typeof(Sample);
        
        object? instance = Activator.CreateInstance(type);
        Sample instance2 = Activator.CreateInstance<Sample>();
        
        Console.WriteLine($"Class: {instance}");
        Console.WriteLine($"Number1: {instance2.Number1}, Number2: {instance2.Number2}");
    }

    class Sample
    {
        private int PrivateNumber1 { get; set; }
        public int Number1 { get; set; } = 1;
        public int Number2 { get; set; } = 2;
        
        public void Print() => Console.WriteLine("Hello World!");
    }
}