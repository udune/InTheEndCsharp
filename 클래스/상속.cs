namespace InTheEndCsharp.클래스;

public class 상속
{
    public static void 실행()
    {
        Dog dog = new Dog();
        dog.Eat();
    }
    
    class Animal
    {
        public void Eat()
        {
            Console.WriteLine("먹습니다.");
        }
    }
    
    class Dog : Animal
    {
        
    }
}