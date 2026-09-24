namespace InTheEndCsharp.인터페이스;

public class 인터페이스_디폴트구현
{
    public static void 실행()
    {
        IAnimal dog = new Dog();
        IAnimal bird = new Bird();
        dog.PrintInformation();
        bird.PrintInformation();
    }   
}