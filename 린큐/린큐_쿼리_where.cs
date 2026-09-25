namespace InTheEndCsharp.린큐;

public class 린큐_쿼리_where
{
    public static void 실행()
    {
        List<Student> students =
        [
            new Student { ID = 1, Name = "Alice", Age = 20 },
            new Student { ID = 2, Name = "Bob", Age = 21 },
            new Student { ID = 3, Name = "Charlie", Age = 18 },
            new Student { ID = 4, Name = "David", Age = 19 },
            new Student { ID = 5, Name = "Eve", Age = 20 }
        ];

        var results = from student in students
            where student.Age >= 21 || student.Name == "Alice"
            select new { MyName = student.Name, student.Age };

        foreach (var result in results)
        {
            Console.WriteLine($"{result.MyName} {result.Age}");
        }
    }

    class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"ID : {ID}, NAME : {Name}, AGE : {Age}";
        }
    }
}