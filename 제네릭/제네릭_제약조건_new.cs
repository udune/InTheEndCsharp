namespace InTheEndCsharp.제네릭;

public class 제네릭_제약조건_new
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
        
        CreateInstance2(out Dog dog, out Cat cat);
        
        var animal1 = CreateInstance3<Dog>();
        var animal2 = CreateInstance3<Cat>();

        Console.WriteLine(dog.ToString());
        Console.WriteLine(cat.ToString());
        Console.WriteLine(animal1.ToString());
        Console.WriteLine(animal2.ToString());
    }

    abstract class Animal
    {
        public abstract string Name { get; }

        public override string ToString()
        {
            return $"제 이름은 {Name}입니다.";
        }
    }

    class Dog : Animal
    {
        public override string Name => "멍멍이";
    }

    class Cat : Animal
    {
        public override string Name => "냥냥이";
    }
}