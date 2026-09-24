namespace InTheEndCsharp.인터페이스;

public class 인터페이스_다중구현
{
    public static void 실행()
    {
        IAnimal dog = new Dog();
        IFlyable bird = new Bird();
        
        dog.MakeSound();
        bird.Fly();
    } 
}