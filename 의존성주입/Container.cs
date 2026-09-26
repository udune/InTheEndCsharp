using InTheEndCsharp.의존성주입.Config;
using InTheEndCsharp.의존성주입.Logging;
using InTheEndCsharp.의존성주입.Models;
using InTheEndCsharp.의존성주입.Repositories;
using InTheEndCsharp.의존성주입.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InTheEndCsharp.의존성주입;

public class Container
{
    readonly IServiceProvider serviceProvider;
    public IServiceProvider Services;
    
    private void ConfigureServices(IServiceCollection services)
    {
        services.AddAppSettings();
        
        services.AddSingleton<ILogger, Logger>();
        services.AddTransient<IMathService, MathService>();
        services.AddTransient<ICounterService>(provider =>
        {
            var mathService = provider.GetRequiredService<IMathService>();
            return new CounterService(0, mathService);
        });
        services.AddTransient<Main>();
        
        services.AddSingleton<ISingletonClass, SingletonClass>();
        services.AddScoped<IScopedClass, ScopedClass>();
        services.AddTransient<ITransientClass, TransientClass>();

        // close generic type
        services.AddTransient<IRepository<User>, Repository<User>>();
        services.AddTransient<IRepository<Product>, Repository<Product>>();
        
        // open generic type
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

        if (Settings.LoggerType == LoggerType.File)
        {
            services.AddTransient<ILogger>(provider => new FileLogger("log.txt"));
        }
        else
        {
            services.AddTransient<ILogger, Logger>();
        }
    }
    
    public Container()
    {
        // 컨테이너 생성
        IServiceCollection services = new ServiceCollection();
        
        // 서비스 등록
        ConfigureServices(services);
        
        serviceProvider = services.BuildServiceProvider();
        Services = serviceProvider;
    }

    public Main StartUp()
    {
        return serviceProvider.GetRequiredService<Main>();
    }
}