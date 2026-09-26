---
id: boxing-unboxing
title: object 타입과 박싱/언박싱
category: 타입 변환
order: 902
summary: 모든 타입의 조상인 object에 값을 담는 박싱(boxing)과 다시 꺼내는 언박싱(unboxing), 성능상의 주의점을 설명합니다.
keywords: object, 박싱, 언박싱, boxing, unboxing, 값 타입, 참조 타입, 힙, 스택, 성능, ArrayList
lesson: 오브젝트_박싱_언박싱
related: explicit-cast, generic-basics, struct-vs-class
---
## 핵심
C#의 모든 타입은 `object`를 상속하므로 무엇이든 object 변수에 담을 수 있습니다.

```csharp
object s = "C# Programming";   // 참조 타입은 그대로 참조
object i = 123;                // 박싱: int 값이 힙에 상자로 복사됨
object d = 3.14;
object b = true;
object c = new TestClass();

string text = (string)s;       // 캐스팅으로 꺼냄
int n = (int)i;                // 언박싱: 상자에서 값을 꺼냄
```

## 박싱이 문제가 되는 이유
- 박싱할 때마다 **힙에 새 객체가 생기고** GC 부담이 늘어납니다.
- 언박싱은 **정확히 같은 타입**으로만 됩니다.
```csharp
object o = 123;
long l = (long)o;      // InvalidCastException! int 로 박싱했으므로
long ok = (int)o;      // int 로 꺼낸 뒤 long 으로 변환
```

## 어떻게 피하나요?
- `ArrayList`, `Hashtable` 같은 옛 컬렉션 대신 **제네릭 컬렉션**(`List<int>`, `Dictionary<K,V>`)을 씁니다.
- 매개변수를 `object`로 받는 대신 **제네릭 메서드** `void M<T>(T value)`를 씁니다.
