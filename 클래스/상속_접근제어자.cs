namespace InTheEndCsharp.클래스;

public class 상속_접근제어자
{
    public static void 실행()
    {
        Animal cat = new Cat();
        cat.Eat();
        cat.Eat();
        cat.Eat();

        Console.WriteLine($"Cat Hp : {cat.Hp}");
        
        Animal dog = new Dog();
        dog.Eat();
        dog.Eat();
        dog.Eat();

        Console.WriteLine($"Cat Hp : {dog.Hp}");
    }

    class Animal
    {
        protected int hp = 100;
        public int Hp => hp;

        public virtual void Eat()
        {
            Console.WriteLine("먹습니다.");
        }
    }

    class Cat : Animal
    {
        public override void Eat()
        {
            hp += 10;
            Console.WriteLine("고양이가 먹습니다.");
        }
    }

    class Dog : Animal
    {
        public override void Eat()
        {
            hp += 15;
            Console.WriteLine("개가 먹습니다.");
        }
    }
}