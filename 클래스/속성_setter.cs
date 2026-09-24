namespace InTheEndCsharp.클래스;

public class 속성_setter
{
    
    public static void 실행()
    {
        Person person = new Person();
        person.Name = "까불이";
        Console.WriteLine(person.GetName());
    }

    class Person
    {
        private string name;

        public string Name
        {
            set
            {
                name = $"** {value} **";
            }
        }

        public string GetName()
        {
            return name;
        }
    }
}