namespace InTheEndCsharp.클래스;

public class 속성_선언
{
    public static void 실행()
    {
        Person person = new Person();
        person.Name = "까불이";

        Console.WriteLine(person.Name);
    }

    class Person
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Name2 { get; set; }
    }
}