---
id: error-invalid-operation
title: InvalidOperationException - 시퀀스에 요소가 없습니다 / Nullable 개체에 값이 있어야 합니다
category: 흔한 예외
order: 2704
summary: 빈 컬렉션에 First/Single/Max/Average를 쓰거나, 값 없는 nullable의 .Value를 읽거나, 객체 상태가 맞지 않을 때 나는 InvalidOperationException의 대표 원인과 해결입니다.
keywords: InvalidOperationException, 시퀀스에 요소가 없습니다, Sequence contains no elements, 시퀀스에 일치하는 요소가 없습니다, Sequence contains no matching element, 시퀀스에 요소가 두 개 이상 있습니다, Sequence contains more than one element, Nullable 개체에 값이 있어야 합니다, Nullable object must have a value, Unable to resolve service for type, 잘못된 작업
related: linq-first-single, linq-aggregate, null-coalescing, error-collection-modified, di-container-lifetime
---
## 1) 시퀀스에 요소가 없습니다 (Sequence contains no elements)
빈 컬렉션에 `First()`, `Single()`, `Last()`, `Max()`, `Min()`, `Average()`를 호출했습니다.
```csharp
var list = new List<int>();
list.First();      // ← 예외!
list.Max();        // ← 예외!
users.First(u => u.Id == 99);   // ← 예외! 조건에 맞는 게 없음 (no matching element)

// 해결
list.FirstOrDefault();                           // 없으면 0/null
list.DefaultIfEmpty(0).Max();
double avg = list.Count > 0 ? list.Average() : 0;
```

## 2) 시퀀스에 요소가 두 개 이상 있습니다
`Single()`/`SingleOrDefault()`인데 조건에 맞는 게 여러 개입니다. 데이터에 중복이 있는지 확인하거나 `First`를 쓰세요.

## 3) Nullable 개체에 값이 있어야 합니다 (Nullable object must have a value)
```csharp
int? n = null;
int x = n.Value;          // ← 예외!
int y = n ?? 0;           // 해결
int z = n.GetValueOrDefault();
```

## 4) 컬렉션이 수정되었습니다
foreach 도중 Add/Remove → 별도 문서 참고

## 5) DI: Unable to resolve service for type 'X' while attempting to activate 'Y'
Y의 생성자가 요구하는 X가 컨테이너에 등록되지 않았습니다. `services.AddTransient<X>()` 등으로 등록하세요.

## 6) 그 밖에
- `Console.ReadKey`를 콘솔 없는 앱(WPF 등)에서 호출
- `Monitor.Exit`/`ReleaseMutex`를 잠금을 잡지 않은 스레드에서 호출
- 이미 시작한 Thread를 다시 `Start()`
