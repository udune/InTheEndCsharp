namespace InTheEndCsharp.린큐;

public class 린큐_쿼리_orderby
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

        var results = from student in students
            let average = student.Scores.Average()
            where average > 3
            orderby student.Age, student.Name, average descending
            select (MyName: student.Name, student.Age, average);

        foreach (var result in results)
        {
            Console.WriteLine($"{result.MyName} {result.Age} Average : {result.average}");
        }
    }
}