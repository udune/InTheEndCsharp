namespace InTheEndCsharp.어트리뷰트.Attributes;

public static class AttributeReader
{
    public static void ReadInfoAttributes(Type[] types, Func<InfoAttribute, bool> predicate)
    {
        foreach (var type in types)
        {
            ReadInfoAttributes(type, predicate);
        }
    }
    
    public static void ReadInfoAttributes(Type type, Func<InfoAttribute, bool> predicate)
    {
        var attributes = type.GetCustomAttributes(typeof(InfoAttribute), false);
        foreach (InfoAttribute attribute in attributes)
        {
            if (!predicate(attribute))
            {
                continue;
            }

            Console.WriteLine($"Class: {type.Name}");
            Console.WriteLine($"Author: {attribute.Author}");
            Console.WriteLine($"Version: {attribute.Version}");
            Console.WriteLine($"Date: {attribute.Date}");
            Console.WriteLine($"Description: {attribute.Description}");
            Console.WriteLine("-------------------------");
        }
    }
}