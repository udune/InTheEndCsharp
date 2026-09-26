---
id: sealed-class
title: sealed - 상속과 재정의 막기
category: 상속
order: 707
summary: 클래스가 더 이상 상속되지 않게 하거나, 특정 override 메서드가 더 이상 재정의되지 않게 막는 sealed 키워드입니다.
keywords: sealed, 봉인, 상속 금지, 재정의 금지, sealed override, 최종 클래스, final
lesson: 상속_sealed
related: inheritance-chain, abstract-class, inheritance-override
---
## 두 가지 용법
```csharp
// 1) sealed 클래스: 상속 자체를 금지
sealed class Config { }
// class MyConfig : Config { }   // 오류 CS0509

// 2) sealed override: 이 메서드는 여기서 재정의 끝
class Cat : Animal
{
    public sealed override void Move() => Console.WriteLine("고양이가 움직입니다.");
}

class Tiger : Cat
{
    // public override void Move() { }  // 오류 CS0239: sealed 멤버는 재정의 불가
}
```

## 언제 쓰나요?
- 상속을 고려하지 않고 설계한 클래스는 `sealed`로 두면 의도가 분명해지고, 런타임이 약간 더 빠르게 호출할 수 있습니다.
- `string`, 대부분의 `record` 등 .NET 기본 타입 상당수가 sealed 입니다.
