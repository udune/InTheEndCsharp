using System.Text;

namespace InTheEndCsharp.문자열;

public class 문자열
{
    public static void 실행()
    {
        string greeting = "Hello, World!";
        string emptyString = "";
        string? nullString = null;

        string firstName = "John";
        string lastName = "Doe";
        string fullName = $"{firstName} {lastName}";
        
        Console.WriteLine(greeting);
        Console.WriteLine(emptyString);
        Console.WriteLine(nullString);
        Console.WriteLine(fullName);
        
        string formatted = string.Format("Name: {0}, FullName: {1}", firstName, lastName);
        Console.WriteLine(formatted);
        Console.WriteLine(formatted.Length);
        
        string text = "Hello World!";
        Console.WriteLine(text.Substring(0, 5));
        Console.WriteLine(text.Contains("World"));
        Console.WriteLine(text.Contains("hello", StringComparison.OrdinalIgnoreCase));

        Console.WriteLine(text.ToUpper());
        Console.WriteLine(text.ToLower());
        
        string colors = "Red, Green, Blue";
        string[] colorArray = colors.Split(',');

        foreach (string color in colorArray)
        {
            Console.WriteLine(color);
        }
        
        string joinedColors = string.Join("|", colorArray);
        Console.WriteLine(joinedColors);

        StringBuilder sb = new StringBuilder();
        sb.Append("Hello");
        sb.Append("World");
        string result = sb.ToString();

        Console.WriteLine(result);
    }
}