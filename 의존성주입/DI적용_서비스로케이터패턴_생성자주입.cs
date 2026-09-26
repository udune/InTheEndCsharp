using InTheEndCsharp.의존성주입.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace InTheEndCsharp.의존성주입;

public class DI적용_서비스로케이터패턴_생성자주입
{
    public static void 실행()
    {
        // 컨테이너 생성
        IServiceCollection services = new ServiceCollection();
    
        // 서비스 등록
        services.AddTransient<Logger>();
        services.AddTransient<MathService>();
        services.AddTransient<ServiceA>();
        
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ServiceA serviceA = serviceProvider.GetRequiredService<ServiceA>();
        serviceA.Add(1, 2);
    }
    
    class MathService
    {
        public int Add(int a, int b)
        {
            int result = a + b;
            return result;
        }
    }

    class ServiceA
    {
        private readonly MathService mathService;
        private readonly Logger logger;
        
        public ServiceA(MathService mathService, Logger logger)
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