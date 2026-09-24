namespace InTheEndCsharp.클래스;

public class 속성_setter접근제어자
{
    public static void 실행()
    {
        Person person = new Person("홍길동");
        Console.WriteLine(person.Name);
    }

    class Person
    {
        public Person(string name)
        {
            Name = name;
        }
        
        private string name;
        public string Name
        {
            get { return name; }
            init
            {
                name = $"** {value} **";
            }
        }
    }
}