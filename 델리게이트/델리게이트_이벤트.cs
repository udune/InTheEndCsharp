namespace InTheEndCsharp.델리게이트;

public class 델리게이트_이벤트
{
    delegate void ValueChangedHandler(int result, string message);

    public static void 실행()
    {
        Calculate calculate = new Calculate();
        calculate.OnValueChanged += CalculateOnOnValueChanged;
        
        calculate.Plus(5);
        calculate.Plus(3);
        
        calculate.OnValueChanged -= CalculateOnOnValueChanged;
        
        calculate.Minus(2);
        calculate.Minus(10);
    }

    private static void CalculateOnOnValueChanged(int result, string message)
    {
        Console.WriteLine($"{message} - 현재 값 : {result}");
    }

    class Calculate
    {
        private int value;
        public event ValueChangedHandler? OnValueChanged;

        public void Plus(int value)
        {
            this.value += value;
            
            OnValueChanged?.Invoke(this.value, $"{value}를 더했습니다.");
        }

        public void Minus(int value)
        {
            this.value -= value;
            OnValueChanged?.Invoke(this.value, $"{value}를 뺐습니다.");
        }
    }
}