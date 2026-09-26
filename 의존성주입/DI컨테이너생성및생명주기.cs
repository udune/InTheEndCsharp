using InTheEndCsharp.의존성주입.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace InTheEndCsharp.의존성주입;

public class DI컨테이너생성및생명주기
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
    }
    
    class MathService
    {
        private readonly Logger logger;

        public MathService(Logger logger)
        {
            this.logger = logger;
        }
        
        public int Add(int a, int b)
        {
            logger.Log("Add 함수 호출");
            int result = a + b;
            logger.Log("Add 함수 결과: " + result);
            return result;
        }
    }

    class ServiceA
    {
        private readonly MathService mathService;
        
        public ServiceA(MathService mathService)
        {
            this.mathService = mathService;
        }

        public int Add(int a, int b)
        {
            int result = mathService.Add(a, b);
            return result;
        }
    }
    
}