namespace InTheEndCsharp.배열;

public class 반복문
{
    public static void 실행()
    {
        string[] fruits = ["사과", "바나나", "레몬"];
        
        for (int i = 0; i < 10; i++)
        {
            
        }

        for (int i = 0; i < fruits.Length; i++)
        {
            Console.WriteLine(fruits[i]);
        }

        foreach (string fruit in fruits)
        {
            Console.WriteLine(fruit);
        }

        int count = 0;
        int count2 = 0;
        int count3 = 0;

        while (count < 3)
        {
            Console.WriteLine($"카운트: {count}");
            count++;
        }

        do
        {
            Console.WriteLine($"카운트: {count2}");
            count2++;
        }
        while (count2 < 3);

        do
        {
            if (count3 == 1)
            {
                break;
            }

            Console.WriteLine($"카운트: {count3}");
            count3++;
        } while (count3 < 3);

        for (int i = 2; i <= 9; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                Console.WriteLine($"{i} x {j} = {i * j}");
            }

            Console.WriteLine();
        }
    }
}