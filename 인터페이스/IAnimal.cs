namespace InTheEndCsharp.인터페이스;

public interface IAnimal
{
    void MakeSound();
    string Name { get; set; }

    void PrintInformation()
    {
        Console.WriteLine($"안녕하세요 저는 {Name}입니다.");
    }
}

class Dog : IAnimal
{
    public string Name { get; set; } = "멍멍이";
    
    void IAnimal.MakeSound()
    {
        Console.WriteLine("멍멍");
    }
}

class Bird : IAnimal, IFlyable
{
    public string Name { get; set; } = "짹짹이";
    
    public void MakeSound()
    {
        Console.WriteLine("짹짹");
    }

    public void Fly()
    {
        Console.WriteLine("날아갑니다.");
    }
}