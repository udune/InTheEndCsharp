---
id: generic-class
title: 제네릭 클래스 (class Box<T>)
category: 제네릭
order: 1106
summary: 클래스 선언에 타입 매개변수를 두어 여러 타입을 담을 수 있는 재사용 가능한 클래스를 만드는 방법입니다.
keywords: 제네릭 클래스, generic class, Box<T>, 컨테이너, 저장소, Repository<T>, ApiResponse<T>, 제네릭 타입
lesson: 제네릭_class
related: generic-basics, di-generic-registration, api-client, list-create
---
## 핵심
```csharp
class GenericBox<T>
{
    private T? item;
    public void Add(T item) => this.item = item;
    public T? GetItem() => item;
}

var box = new GenericBox<string>();
box.Add("myGeneric");
// box.Add(1);              // 오류: string 상자에는 string 만

var intBox = new GenericBox<int>();
intBox.Add(1);

Console.WriteLine(box.GetItem());    // myGeneric
Console.WriteLine(intBox.GetItem()); // 1
```

## 실무에서 자주 보는 제네릭 클래스
```csharp
// API 응답을 감싸는 공통 형태 (APIApp 의 ApiResponse<T>)
public class ApiResponse<T>(int statusCode, T data = default!, string? errorMessage = null)
{
    public int StatusCode { get; } = statusCode;
    public T Data { get; } = data;
    public string? ErrorMessage { get; } = errorMessage;
    public bool IsSuccess => StatusCode >= 200 && StatusCode <= 299;
}

// 엔티티별 저장소 (의존성주입 챕터의 Repository<T>)
public class Repository<T> where T : class
{
    private readonly List<T> _items = new();
    public void Add(T item) => _items.Add(item);
    public IReadOnlyList<T> GetAll() => _items;
}
```

## 참고
- `T?`: T가 참조 타입이면 null 허용, 값 타입이면 `default`로 처리됩니다.
- `default(T)` 또는 `default`: T의 기본값(0, null, false 등).
