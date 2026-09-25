namespace InTheEndCsharp.린큐;

public class 린큐_쿼리_join
{
    public static void 실행()
    {
        List<Student> students =
        [
            new Student { Id = 1, Age = 20, Gender = "F", Name = "Alice" },
            new Student { Id = 2, Age = 22, Gender = "M", Name = "Bob" },
            new Student { Id = 3, Age = 23, Gender = "M", Name = "Charlie" },
            new Student { Id = 4, Age = 21, Gender = "M", Name = "David" },
            new Student { Id = 5, Age = 20, Gender = "F", Name = "Eve" }
        ];

        List<Score> studentScores =
        [
            new Score() { StudentId = 1, ScoreValue = 2, Subject = "Math" },
            new Score() { StudentId = 1, ScoreValue = 3, Subject = "Science" },
            new Score() { StudentId = 1, ScoreValue = 4, Subject = "English" },
            new Score() { StudentId = 2, ScoreValue = 5, Subject = "Math" },
            new Score() { StudentId = 2, ScoreValue = 8, Subject = "Science" },
            new Score() { StudentId = 2, ScoreValue = 2, Subject = "English" },
            new Score() { StudentId = 3, ScoreValue = 4, Subject = "Math" },
            new Score() { StudentId = 3, ScoreValue = 4, Subject = "Science" },
            new Score() { StudentId = 3, ScoreValue = 4, Subject = "English" },
            new Score() { StudentId = 4, ScoreValue = 5, Subject = "Math" },
            new Score() { StudentId = 4, ScoreValue = 5, Subject = "Science" },
            new Score() { StudentId = 4, ScoreValue = 3, Subject = "English" },
            new Score() { StudentId = 5, ScoreValue = 1, Subject = "Math" },
            new Score() { StudentId = 5, ScoreValue = 7, Subject = "Science" },
            new Score() { StudentId = 5, ScoreValue = 9, Subject = "English" }
        ];

        var result = from student in students
            join score in studentScores on student.Id equals score.StudentId
            select (student, score);

        foreach (var (student, score) in result)
        {
            Console.WriteLine(
                $"{student.Name}: {score.ScoreValue} - {score.Subject}");
        }

    }
}