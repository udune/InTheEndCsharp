namespace InTheEndCsharp.제네릭;

public class 제네릭_제약조건_classtype
{
    public static void 실행()
    {
        Animal a = new Dog();
        Animal b = new Cat();

        Console.WriteLine($"{a},{b}");
        
        void CreateInstance<T>(out T a, out T b) where T : class, new() // struct, new(), interface
        {
            a = new T();
            b = new T();
        }
        
        void CreateInstance2<T, T2>(out T a, out T2 b) 
            where T : class, new() // struct, new(), interface
            where T2 : class, new()
        {
            a = new T();
            b = new T2();
        }

        T CreateInstance3<T>() where T : class, new()
        {
            return new T();
        }
        
        T CreateInstance4<T>() where T : Animal, new()
        {
            T instance = new T();
            instance.MakeSound();
            return instance;
        }
        
        var animal = CreateInstance4<Dog>();
        Console.WriteLine(animal.ToString());
    }

    abstract class Animal
    {
        public abstract string Name { get; }
        public abstract void MakeSound();

        public override string ToString()
        {
            return $"제 이름은 {Name}입니다.";
        }
    }

    class Dog : Animal
    {
        public override string Name => "멍멍이";
        public override void MakeSound()
        {
            Console.WriteLine("멍멍");
        }
    }

    class Cat : Animal
    {
        public override string Name => "냥냥이";
        public override void MakeSound()
        {
            Console.WriteLine("냥냥");
        }
    }
}