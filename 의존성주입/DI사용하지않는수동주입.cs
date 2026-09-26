using InTheEndCsharp.의존성주입.Logging;

namespace InTheEndCsharp.의존성주입;

public class DI사용하지않는수동주입
{
    public static void 실행()
    {
        Logger logger = new Logger();
        MathService mathService = new MathService(logger);
        ServiceA serviceA = new ServiceA(mathService);
        serviceA.Add(1, 2);
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