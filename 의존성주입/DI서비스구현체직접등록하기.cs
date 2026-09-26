using InTheEndCsharp.의존성주입.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InTheEndCsharp.의존성주입;

public class DI서비스구현체직접등록하기
{
    public static void 실행()
    {
        Settings.LoggerType = LoggerType.File;
        
        var container = new Container();
        Main main = container.Services.GetRequiredService<Main>();
        main.CreateLog("hi");
    }
}