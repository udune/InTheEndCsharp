namespace InTheEndCsharp.의존성주입.Services;

public interface IMathService
{
    int Add(int a, int b);
    int Increase(int a, int step = 1);
}