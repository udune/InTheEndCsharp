namespace InTheEndCsharp.열거및컬렉션;

public class 컬렉션_List_요소검색
{
    public static void 실행()
    {
        var fruitList = new List<string> { "apple", "orange", "pear" };
        bool hasData = fruitList.Contains("Apple", StringComparer.OrdinalIgnoreCase);
        int index = fruitList.IndexOf("apple");
        var selectedFruit = fruitList.Find((fruit) => fruit.StartsWith("o"));
        List<string> selectedFruits = fruitList.FindAll((fruit) => fruit.Contains('e'));
        Console.WriteLine(hasData);
        Console.WriteLine(index);
        Console.WriteLine(selectedFruit);
        foreach (var fruit in selectedFruits)
        {
            Console.WriteLine(fruit);
        }
    }    
}