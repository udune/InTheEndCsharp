namespace InTheEndCsharp.린큐;

public class 린큐_쿼리_group
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

        var genderGroups = from student in students
            let average = student.Scores.Average()
            where average > 3
            orderby student.Age, student.Name, average descending
            group student by student.Gender into g
            select (Gender: g.Key, Group: g);

        foreach (var genderGroup in genderGroups)
        {
            // genderGroup.Gender
            Console.WriteLine($"Gender: {genderGroup.Gender}");
            
            // genderGroup.Group
            foreach (var student in genderGroup.Group)
            {
                Console.WriteLine($"{student.Name} {student.Age} Average : {student.Scores.Average()}");
            }

            Console.WriteLine("------------------------------");
        }
    }
}