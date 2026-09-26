using InTheEndCsharp.의존성주입.Logging;
using InTheEndCsharp.의존성주입.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InTheEndCsharp.의존성주입;

public class DI인터페이스적용
{
    public static void 실행()
    {
        // 컨테이너 생성
        IServiceCollection services = new ServiceCollection();
    
        // 서비스 등록
        services.AddTransient<ILogger, Logger>();
        services.AddTransient<IMathService, MathService>();
        services.AddTransient<Main>();
        
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        Main main = serviceProvider.GetRequiredService<Main>();
        main.Add(1, 2);
    }

    class Main
    {
        private readonly IMathService mathService;
        private readonly ILogger logger;
        
        public Main(IMathService mathService, ILogger logger)
        {
            this.mathService = mathService;
            this.logger = logger;
        }

        public int Add(int a, int b)
        {
            logger.Log("Add 함수 호출");
            int result = mathService.Add(a, b);
            logger.Log("Add 함수 결과: " + result);
            return result;
        }
    }
}