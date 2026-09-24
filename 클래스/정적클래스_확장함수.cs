namespace InTheEndCsharp.클래스;

public class 정적클래스_확장함수
{
    public static void 실행()
    {
        string name = "John";
        name.Print();
        MyClass.Print(name);
    }
}

static class MyClass
{
    public static void Print(this string text)
    {
        Console.WriteLine(text);
    }
}