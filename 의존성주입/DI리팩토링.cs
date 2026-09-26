namespace InTheEndCsharp.의존성주입;

public class DI리팩토링
{
    public static void 실행()
    {
        var container = new Container();
        Main main = container.StartUp();
        main.Add(1, 2);
    }
}