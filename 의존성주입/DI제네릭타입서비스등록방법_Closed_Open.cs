using Microsoft.Extensions.DependencyInjection;

namespace InTheEndCsharp.의존성주입;

public class DI제네릭타입서비스등록방법_Closed_Open
{
    public static void 실행()
    {
        var container = new Container();
        
        Main main = container.Services.GetRequiredService<Main>();
        main.Run();
    }
}