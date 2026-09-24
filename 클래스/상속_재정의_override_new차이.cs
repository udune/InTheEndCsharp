namespace InTheEndCsharp.클래스;

public class 상속_재정의_override_new차이
{
    public static void 실행()
    {
        Animal dog = new Dog();
        Animal cat = new Cat();
        Cat cat2 = new Cat();
        dog.Eat();
        cat.Eat();
        cat2.Eat();
    }
    
    class Animal
    {
        public virtual void Eat()
        {
            Console.WriteLine("먹습니다.");
        }
    }

    class Cat : Animal
    {
        public new void Eat()
        {
            Console.WriteLine("고양이가 먹습니다.");
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