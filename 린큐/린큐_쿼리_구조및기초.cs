namespace InTheEndCsharp.린큐;

public class 린큐_쿼리_구조및기초
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

        // List<Student> newStudents = new List<Student>();
        // foreach (Student student in students)
        // {
        //     if (student.Age >= 21)
        //     {
        //         newStudents.Add(student);
        //     }
        // }
        
        var newStudents = from student in students 
            where student.Age >= 21 
            select student;

        foreach (Student student in newStudents)
        {
            Console.WriteLine(student);
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