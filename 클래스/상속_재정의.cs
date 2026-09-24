namespace InTheEndCsharp.클래스;

public class 상속_재정의
{
    public static void 실행()
    {
        Animal dog = new Dog();
        dog.Eat();
    }
    
    class Animal
    {
        public virtual void Eat()
        {
            Console.WriteLine("먹습니다.");
        }
    }
    
    class Dog : Animal
    {
        public override void Eat()
        {
            Console.WriteLine("개가 먹습니다.");
        }
    }
}