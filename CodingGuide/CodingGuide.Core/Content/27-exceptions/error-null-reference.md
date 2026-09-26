---
id: error-null-reference
title: NullReferenceException - 개체 참조가 개체의 인스턴스로 설정되지 않았습니다
category: 흔한 예외
order: 2701
summary: null인 변수의 속성이나 메서드를 사용할 때 발생하는 가장 흔한 예외의 원인 찾기와 해결 방법(null 검사, ?., ??, nullable 경고 활용)입니다.
keywords: NullReferenceException, null reference, 널 참조, 개체 참조가 개체의 인스턴스로 설정되지 않았습니다, Object reference not set to an instance of an object, null 오류, 널 에러, NRE, null 인데 사용
related: null-coalescing, error-cs8602, linq-first-single, dictionary
---
## 메시지
- 개체 참조가 개체의 인스턴스로 설정되지 않았습니다.
- Object reference not set to an instance of an object.

## 원인
**null인 변수에 점(.)을 찍었다**는 뜻입니다.
```csharp
string? name = null;
int len = name.Length;          // ← 예외!

List<int>? list = null;
list.Add(1);                    // ← 예외! new 를 안 함

var user = users.FirstOrDefault(u => u.Id == 99);
Console.WriteLine(user.Name);   // ← 예외! 못 찾아서 null

class Order { public List<Item> Items { get; set; } }   // 초기화 안 함
order.Items.Add(item);          // ← 예외!
```

## 찾는 방법
1. 예외 창의 **줄 번호**로 가서, 그 줄에서 **점(.) 앞에 있는 것들** 중 무엇이 null인지 확인합니다.
2. 디버거에서 중단점을 걸고 변수에 마우스를 올려 봅니다.
3. 한 줄에 `a.B.C.D`처럼 길게 이어져 있으면 줄을 나눠 어느 단계인지 좁힙니다.

## 해결
```csharp
if (user != null) Console.WriteLine(user.Name);        // null 검사
if (user is null) return;                               // 조기 반환
Console.WriteLine(user?.Name ?? "알 수 없음");          // ?. 과 ??
int len = name?.Length ?? 0;

public List<Item> Items { get; set; } = [];             // 선언과 동시에 초기화
ArgumentNullException.ThrowIfNull(user);                 // 들어오자마자 명확한 예외로
```

## 예방
- 프로젝트에 `<Nullable>enable</Nullable>`을 켜고 **CS8602, CS8618 경고를 무시하지 마세요.** 컴파일러가 null 위험을 미리 알려 줍니다.
- 컬렉션 속성은 항상 빈 컬렉션으로 초기화합니다.
