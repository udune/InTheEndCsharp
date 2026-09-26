using InTheEndCsharp.의존성주입.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InTheEndCsharp.의존성주입.Services;

public static class AppSettingsExtensions
{
    public static void AddAppSettings(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        services.Configure<AppSettings>(configuration);
    }
}