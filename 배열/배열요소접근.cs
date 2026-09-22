namespace InTheEndCsharp.배열;

public class 배열요소접근
{
    public static void 실행()
    {
        int[] numbers = [10, 20, 30, 40];
        string[] fruits = ["사과", "바나나", "레몬"];

        numbers[2] = 100;
        Console.WriteLine(numbers[2]);

        Console.WriteLine(fruits[fruits.Length - 1]);
        Console.WriteLine(fruits[^1]);
    }
}