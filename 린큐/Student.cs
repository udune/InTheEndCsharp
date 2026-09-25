namespace InTheEndCsharp.린큐;

public class Student
{
    public int Id {  get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Gender { get; set; } = "";
    public List<int> Scores { get; set; } = [];
    public override string ToString()
    {
        return $"Id : {Id}, Name : {Name}, Age : {Age}";
    }
}