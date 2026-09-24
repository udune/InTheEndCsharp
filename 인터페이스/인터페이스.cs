namespace InTheEndCsharp.인터페이스;

public class 인터페이스
{
    public static void 실행()
    {
        IAnimal dog = new Dog();
        dog.MakeSound();
        
        IAnimal bird = new Bird();
        bird.MakeSound();
    }
}