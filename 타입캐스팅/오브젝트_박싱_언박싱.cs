namespace InTheEndCsharp.타입캐스팅;

public class 오브젝트_박싱_언박싱
{
    public static void 실행()
    {
        object stringObject = "C# Programming";
        object intObject = 123;
        object doubleObject = 3.14;
        object boolObject = true;
        object classObject = new TestClass();

        string text = (string) stringObject;
        Console.WriteLine($"object: {stringObject} string: {text}");
    }

    class TestClass
    {
        
    }
}