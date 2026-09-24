namespace InTheEndCsharp.제네릭;

public class 제네릭_제약조건_interface
{
    public static void 실행()
    {
        T CreateInstance<T>() where T : IAnimal, new()
        {
            T instance = new T();
            instance.MakeSound();
            return instance;
        }
        
        var animal = CreateInstance<Bird>();
        Console.WriteLine(animal.Name);
    }

    interface IAnimal
    {
        string Name { get; }
        
        void MakeSound();
    }

    class Dog : IAnimal
    {
        public string Name => "멍멍이";
        public void MakeSound()
        {
            Console.WriteLine("멍멍");
        }
    }

    class Cat : IAnimal
    {
        public string Name => "냥냥이";
        public void MakeSound()
        {
            Console.WriteLine("냥냥");
        }
    }

    class Bird : IAnimal
    {
        public string Name => "짹짹이";

        public void MakeSound()
        {
            Console.WriteLine("짹짹");
        }
    }
}