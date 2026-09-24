namespace InTheEndCsharp.클래스;

public class 상속_상속체인
{
    public static void 실행()
    {
        Animal dog = new Dog();
        Animal cat = new Cat();
        dog.Eat();
        cat.Eat();

        Tiger tiger = new Tiger();
        tiger.Eat();
        tiger.Move();
        tiger.Yaong();
    }
    
    abstract class Animal
    {
        protected int hp;

        public abstract void Move();
        
        public virtual void Eat()
        {
            Console.WriteLine("먹습니다.");
        }
    }

    class Cat : Animal
    {
        public override void Move()
        {
            Console.WriteLine("고양이가 움직입니다.");
        }
        
        public new void Eat()
        {
            hp += 10;
            Console.WriteLine("고양이가 먹습니다.");
        }
        
        public void Yaong()
        {
            Console.WriteLine("야옹");
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
            hp += 15;
            Console.WriteLine("개가 먹습니다.");
        }
    }

    class Tiger : Cat
    {
        
    }
}