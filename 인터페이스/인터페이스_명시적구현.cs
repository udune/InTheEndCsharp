namespace InTheEndCsharp.인터페이스;

public interface 인터페이스_명시적구현
{
    public static void 실행()
    {
        IAnimal dog = new Dog();
        dog.MakeSound();
    }
}