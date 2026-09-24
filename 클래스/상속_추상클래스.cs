namespace InTheEndCsharp.클래스;

public class 상속_추상클래스
{
    public static void 실행()
    {
        Animal dog =  new Dog();
        Animal cat = new Cat();

        dog.Eat();
        dog.Move();

        cat.Eat();
        cat.Move();
    }

    abstract class Animal
    {
        protected int hp = 100;
        public int Hp => hp;

        public abstract void Move();
        
        public virtual void Eat()
        {
            Console.WriteLine("먹습니다.");
        }
    }

    class Dog : Animal
    {
        public override void Move()
        {
            Console.WriteLine("개가 움직입니다.");
        }

        public override void Eat()
        {
            Console.WriteLine("개가 먹습니다.");
        }
    }
    
    class Cat : Animal
    {
        public override void Move()
        {
            Console.WriteLine("고양이가 움직입니다.");
        }

        public override void Eat()
        {
            Console.WriteLine("고양이가 먹습니다.");
        }
    }
}