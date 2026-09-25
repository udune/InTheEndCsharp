namespace InTheEndCsharp.열거및컬렉션;

public class 컬렉션_Queue
{
    public static void 실행()
    {
        var fruits = new Queue<string>();
        fruits.Enqueue("apple");
        fruits.Enqueue("orange");
        fruits.Enqueue("grape");

        Console.WriteLine(fruits.Dequeue());
        Console.WriteLine(fruits.Dequeue());
        Console.WriteLine(fruits.Peek());
        
        fruits.TryDequeue(out string? fruit);
        Console.WriteLine(fruit);
    }
}