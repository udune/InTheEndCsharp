namespace InTheEndCsharp.열거및컬렉션;

public class 컬렉션_Stack
{
    public static void 실행()
    {
        var fruits = new Stack<string>();
        fruits.Push("Apple");
        fruits.Push("Banana");
        fruits.Push("Orange");

        Console.WriteLine(fruits.Pop());
        Console.WriteLine(fruits.Pop());
        Console.WriteLine(fruits.Peek());
        
        fruits.TryPop(out string? fruit);
        Console.WriteLine(fruit);
    }
}