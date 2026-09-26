using InTheEndCsharp.의존성주입.Config;
using InTheEndCsharp.의존성주입.Logging;
using InTheEndCsharp.의존성주입.Models;
using InTheEndCsharp.의존성주입.Repositories;
using InTheEndCsharp.의존성주입.Services;
using Microsoft.Extensions.Options;

namespace InTheEndCsharp.의존성주입;

public class Main
{
    private readonly IMathService mathService;
    private readonly ILogger logger;
    private readonly ICounterService counterService;
    
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Product> productRepository;
    
    AppSettings appSettings;
    
    int MyProperty { get; set; }
        
    // public Main(IMathService mathService, ILogger logger, ILogger logger2)
    public Main(IMathService mathService, ILogger logger, ICounterService counterService, IOptions<AppSettings> options, IRepository<User> userRepository, IRepository<Product> productRepository)
    {
        this.mathService = mathService;
        this.logger = logger;
        this.counterService = counterService;
        
        appSettings = options.Value;
        this.userRepository = userRepository;
        this.productRepository = productRepository;

        // Console.WriteLine($"logger == logger2 => {logger == logger2}");
    }

    public void CreateLog(string message)
    {
        logger.Log(message);
    }
    
    public int Add(int a, int b)
    {
        logger.Log("Add 함수 호출");
        int result = mathService.Add(a, b);
        logger.Log("Add 함수 결과: " + result);
        return result;
    }

    public void Increase()
    {
        counterService.Increase();
    }

    public void Run()
    {
        Console.WriteLine($"OpenAI API KEY: {appSettings.ApiKey.OpenAI}\n" +
                          $"Claude API KEY: {appSettings.ApiKey.Claude}\n" +
                          $"Port: {appSettings.Port}\n" +
                          $"---");
        
        userRepository.Add(new User { Name = "까불이", Age = 10 });
        userRepository.Add(new User { Name = "옥순이", Age = 20 });

        foreach (var item in userRepository.GetAll())
        {
            Console.WriteLine($"[User] name : {item.Name}, age : {item.Age}");
        }

        Console.WriteLine("-----");
        
        productRepository.Add(new Product { Name = "선풍기", Price = 30000 });
        productRepository.Add(new Product { Name = "컴퓨터", Price = 650000 });

        foreach (var item in productRepository.GetAll())
        {
            Console.WriteLine($"[Product] name : {item.Name}, price : {item.Price}");
        }
    }
}