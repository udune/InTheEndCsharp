---
id: delegate-event
title: 이벤트 (event) - 값이 바뀌면 알려주기
category: 델리게이트와 이벤트
order: 1204
summary: event 키워드로 외부에서 구독(+=)/해제(-=)만 가능한 알림을 만들고, 상태가 바뀔 때 구독자에게 알리는 방법입니다.
keywords: 이벤트, event, 구독, 발행, 옵저버, observer, 알림, EventHandler, EventArgs, Invoke, ?.Invoke, 콜백, 버튼 클릭, 이벤트 핸들러
lesson: 델리게이트_이벤트
related: delegate-multicast, delegate-basics, delegate-action, async-ui
---
## 핵심
이벤트는 델리게이트에 **안전장치**를 씌운 것입니다. 외부에서는 `+=`/`-=`만 할 수 있고, 호출(발생)은 **이벤트를 가진 클래스만** 할 수 있습니다.

```csharp
delegate void ValueChangedHandler(int result, string message);

class Calculate
{
    private int value;
    public event ValueChangedHandler? OnValueChanged;   // 이벤트 선언

    public void Plus(int n)
    {
        value += n;
        OnValueChanged?.Invoke(value, $"{n}를 더했습니다.");  // 구독자가 있으면 알림
    }
}

void Handler(int result, string msg) => Console.WriteLine($"{msg} - 현재 값 : {result}");

var calc = new Calculate();
calc.OnValueChanged += Handler;    // 구독
calc.Plus(5);                      // "5를 더했습니다. - 현재 값 : 5"
calc.OnValueChanged -= Handler;    // 구독 해제
calc.Plus(3);                      // 아무 출력 없음
// calc.OnValueChanged(1, "x");    // 오류: 외부에서 이벤트 발생 불가
```

## .NET 표준 이벤트 패턴 (EventHandler)
실무와 WinForms/WPF는 `(object sender, EventArgs e)` 모양을 씁니다.
```csharp
class ValueChangedEventArgs(int value) : EventArgs
{
    public int Value { get; } = value;
}

class Counter
{
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;
    private int _count;
    public void Increase()
    {
        _count++;
        ValueChanged?.Invoke(this, new ValueChangedEventArgs(_count));
    }
}

counter.ValueChanged += (sender, e) => Console.WriteLine(e.Value);
```
`button.Click += btnAsync_Click;` 같은 UI 이벤트도 같은 원리입니다.

## 주의할 점
- 구독한 쪽이 먼저 사라져야 할 때는 반드시 `-=`로 해제하세요. 안 하면 **메모리 누수**가 생길 수 있습니다.
- 이벤트 발생은 `?.Invoke`로 null(구독자 없음)을 확인합니다.
