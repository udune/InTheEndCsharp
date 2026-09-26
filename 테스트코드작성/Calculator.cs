namespace InTheEndCsharp.테스트코드작성;

public class Calculator
{
    public int Add(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;
    public int Multiply(int a, int b) => a * b;

    public double Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("0으로 나눌 수 없습니다.");
        }

        return (double)a / b;
    }
        
    public bool IsEven(int number) => number % 2 == 0;
}