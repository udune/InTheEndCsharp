using InTheEndCsharp.의존성주입.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InTheEndCsharp.의존성주입;

public class DIAddScoped생명주기및AddTransient_AddSingleton과비교
{
    public static void 실행()
    {
        var container = new Container();
        var services = container.Services;

        Console.WriteLine("1번 스코프");
        using (var scope = services.CreateScope())
        { 
            scope.ServiceProvider.GetRequiredService<ISingletonClass>();
            scope.ServiceProvider.GetRequiredService<IScopedClass>();
            scope.ServiceProvider.GetRequiredService<ITransientClass>(); 
            
            scope.ServiceProvider.GetRequiredService<ISingletonClass>();
            scope.ServiceProvider.GetRequiredService<IScopedClass>();
            scope.ServiceProvider.GetRequiredService<ITransientClass>();
        }
        
        Console.WriteLine("2번 스코프");
        using (var scope = services.CreateScope())
        { 
            scope.ServiceProvider.GetRequiredService<ISingletonClass>();
            scope.ServiceProvider.GetRequiredService<IScopedClass>();
            scope.ServiceProvider.GetRequiredService<ITransientClass>(); 
            
            scope.ServiceProvider.GetRequiredService<ISingletonClass>();
            scope.ServiceProvider.GetRequiredService<IScopedClass>();
            scope.ServiceProvider.GetRequiredService<ITransientClass>();
        }
    }
}