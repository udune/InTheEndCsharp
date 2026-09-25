namespace InTheEndCsharp.린큐;

public class 린큐_메소드_OrderBy_메소드체이닝
{
    public static void 실행()
    {
        List<Student> students =
        [
            new Student { Id = 1, Age = 20, Gender = "F", Scores = [5, 3, 9], Name = "Alice" },
            new Student { Id = 2, Age = 22, Gender = "M", Scores = [8, 3, 2], Name = "Bob" },
            new Student { Id = 3, Age = 23, Gender = "M", Scores = [4, 4, 1], Name = "Charlie" },
            new Student { Id = 4, Age = 21, Gender = "M", Scores = [5, 6, 2], Name = "David" },
            new Student { Id = 5, Age = 20, Gender = "F", Scores = [9, 8, 7], Name = "Eve" }
        ];

        var result = students.OrderBy(student => student.Age)
            .ThenByDescending(student => student.Age);
        var result2 = students.OrderByDescending(student => student.Age);

        foreach (var student in result)
        {
            Console.WriteLine(student);
        }

        Console.WriteLine("-------");
        
        foreach (var student in result2)
        {
            Console.WriteLine(student);
        }
    }
}