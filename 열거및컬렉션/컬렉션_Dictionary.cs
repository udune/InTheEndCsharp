namespace InTheEndCsharp.열거및컬렉션;

public class 컬렉션_Dictionary
{
    public static void 실행()
    {
        var fruitDict = new Dictionary<string, string>();
        fruitDict.Add("apple", "사과");
        fruitDict.Add("banana", "바나나");
        fruitDict.Add("cherry", "체리");

        var fruitDict2 = new Dictionary<string, string>()
        {
            { "apple", "사과" },
            { "banana", "바나나" },
            { "cherry", "체리" }
        };
        
        fruitDict["apple"] = "애플";
        fruitDict["lemon"] = "레몬";
        
        string fruit = fruitDict["apple"];
        Console.WriteLine(fruit);

        fruitDict.Remove("banana");
        
        bool hasKey = fruitDict2.TryGetValue("grape", out string? fruit2);
        if (hasKey)
        {
            Console.WriteLine(fruit2);
        }

        foreach (var kvp in fruitDict2.Keys)
        {
            Console.WriteLine($"키 : {kvp}");
        }
        
        foreach (var kvp in fruitDict2.Values)
        {
            Console.WriteLine($"밸류 : {kvp}");
        }
    }
}