namespace InTheEndCsharp.제네릭;

public class 제네릭_class
{
    public static void 실행()
    {
        var box = new GenericBox<string>();
        box.Add("myGeneric");
        
        var intBox = new GenericBox<int>();
        intBox.Add(1);

        var item = box.GetItem();
        var intItem = intBox.GetItem();
        Console.WriteLine(item);
        Console.WriteLine(intItem);
    }

    class GenericBox<T>
    {
        private T item;

        public void Add(T item)
        {
            this.item = item;
        }

        public T GetItem()
        {
            return item;
        }
    }
}