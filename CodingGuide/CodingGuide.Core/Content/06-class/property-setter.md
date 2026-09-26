---
id: property-setter
title: setter에서 값 가공·검증하기
category: 클래스
order: 614
summary: set 접근자에서 대입되는 값(value)을 가공하거나 검증하는 방법, 쓰기 전용 속성을 설명합니다.
keywords: setter, set, value, 검증, 유효성 검사, validation, 값 가공, 쓰기 전용, 범위 체크, INotifyPropertyChanged
lesson: 속성_setter
related: properties, property-setter-access, custom-exception
---
## 핵심
`set` 안의 `value`는 대입하려는 값입니다. 저장하기 전에 가공하거나 검증할 수 있습니다.

```csharp
class Person
{
    private string name = "";
    public string Name
    {
        set { name = $"** {value} **"; }   // 가공해서 저장 (get 이 없으니 쓰기 전용)
    }
    public string GetName() => name;
}

var p = new Person();
p.Name = "까불이";
Console.WriteLine(p.GetName()); // ** 까불이 **
```

## 검증하는 setter
```csharp
class Player
{
    private int _hp;
    public int Hp
    {
        get => _hp;
        set
        {
            if (value < 0) value = 0;           // 범위 보정
            if (value > 100) throw new ArgumentOutOfRangeException(nameof(value));
            _hp = value;
        }
    }
}
```

## C# 14: field 키워드
backing 필드를 직접 선언하지 않고 `field`로 접근할 수 있습니다.
```csharp
public string Name
{
    get;
    set => field = value.Trim();
}
```

## 주의할 점
- 쓰기 전용 속성(get 없는 속성)은 드뭅니다. 보통은 get도 함께 두세요.
- WPF에서 화면 갱신이 필요하면 setter에서 `PropertyChanged` 이벤트를 발생시킵니다.
