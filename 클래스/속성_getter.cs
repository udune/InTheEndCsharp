namespace InTheEndCsharp.클래스;

public class 속성_getter
{
    public static void 실행()
    {
        Person person = new Person("까불이");
        Console.WriteLine(person.Name);
    }

    class Person
    {
        public Person(string name)
        {
            this.name = name;
        }

        private string name;
        public string Name => $"이름은 {name}입니다.";
    }
}