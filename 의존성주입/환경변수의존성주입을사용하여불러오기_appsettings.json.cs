using Microsoft.Extensions.DependencyInjection;

namespace InTheEndCsharp.의존성주입;

public class 환경변수의존성주입을사용하여불러오기_appsettings_json
{
    public static void 실행()
    {
        var container = new Container();
        
        Main main = container.Services.GetRequiredService<Main>();
        main.Run();
    }
}