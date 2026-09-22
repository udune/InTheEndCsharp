namespace InTheEndCsharp.배열;

public class 다차원배열_Array클래스
{
    public static void 실행()
    {
        int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.WriteLine($"{i} * {j} = {matrix[i, j]}");
            }
        }

        Console.WriteLine(matrix.GetLength(0));
        Console.WriteLine(matrix.GetLength(1));

        int[][] jaggedArray =
        [
            [1, 2],
            [3, 4, 5],
            [6, 7, 8, 9]
        ];

        Console.WriteLine(jaggedArray[2][3]);

        foreach (int[] array in jaggedArray)
        {
            foreach (int item in array)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
        }

        int[] members = [5, 3, 8, 1, 2];
        Array.Sort(members);
        int index = Array.IndexOf(members, 8);

        foreach (var member in members)
        {
            Console.WriteLine(member);
        }

        Console.WriteLine(index);

        Console.WriteLine();

        Array.Resize(ref members, 7);

        foreach (var member in members)
        {
            Console.WriteLine(member);
        }
    }
}