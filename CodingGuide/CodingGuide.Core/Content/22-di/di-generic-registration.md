---
id: di-generic-registration
title: 제네릭 서비스 등록 - Closed 타입과 Open 타입 (IRepository<>)
category: 의존성 주입
order: 2209
summary: IRepository<User>처럼 타입을 하나하나 등록하는 closed generic 방식과, typeof(IRepository<>)로 모든 T를 한 번에 등록하는 open generic 방식입니다.
keywords: 제네릭 등록, open generic, closed generic, typeof(IRepository<>), Repository<T>, 제네릭 저장소, 제네릭 서비스, 한 번에 등록
lesson: DI제네릭타입서비스등록방법_Closed_Open
source: 의존성주입/Repositories/Repository.cs, 의존성주입/Container.cs
related: generic-class, di-lifetime, di-refactoring
---
## 제네릭 저장소
```csharp
public interface IRepository<T>
{
    void Add(T item);
    List<T> GetAll();
}

public class Repository<T> : IRepository<T>
{
    private readonly List<T> items = [];
    public void Add(T item) => items.Add(item);
    public List<T> GetAll() => items;
}
```

## Closed 타입 등록 - 타입마다 한 줄씩
```csharp
services.AddTransient<IRepository<User>, Repository<User>>();
services.AddTransient<IRepository<Product>, Repository<Product>>();
```

## Open 타입 등록 - 한 줄로 모든 T
```csharp
services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
// IRepository<User>, IRepository<Product>, IRepository<Order> ... 모두 해결됨
```

## 사용
```csharp
class Main(IRepository<User> userRepository, IRepository<Product> productRepository)
{
    public void Run()
    {
        userRepository.Add(new User { Name = "까불이", Age = 10 });
        productRepository.Add(new Product { Name = "선풍기", Price = 30000 });
        foreach (var u in userRepository.GetAll())
            Console.WriteLine($"[User] {u.Name}, {u.Age}");
    }
}
```

## 참고
- 둘 다 등록되어 있으면 **closed 등록이 우선**합니다.
- `typeof(IRepository<>)`의 `<>`는 "타입 인자가 비어 있는 제네릭 정의"를 뜻합니다. 두 개면 `typeof(IMap<,>)`.
- 로깅의 `ILogger<T>`, 옵션의 `IOptions<T>`가 open generic으로 등록된 대표적인 예입니다.
