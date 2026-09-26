namespace InTheEndCsharp.의존성주입.Repositories;

public interface IRepository<T>
{
    void Add(T item);
    List<T> GetAll();
}

public class Repository<T> : IRepository<T>
{
    private readonly List<T> items = [];
    
    public void Add(T item)
    {
        items.Add(item);
    }

    public List<T> GetAll()
    {
        return items;
    }
}