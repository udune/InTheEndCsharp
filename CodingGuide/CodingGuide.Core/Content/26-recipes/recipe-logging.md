---
id: recipe-logging
title: 로그 남기기 - 간단한 파일 로거부터 Microsoft.Extensions.Logging까지
category: 실무 레시피
order: 2613
summary: 날짜별 로그 파일에 기록하는 간단한 로거 구현과, 실무 표준인 ILogger<T>(Microsoft.Extensions.Logging)의 사용법, 로그 레벨입니다.
keywords: 로그, 로깅, logging, 로그 파일, 로그 남기기, 파일 로그, ILogger, ILogger<T>, LogInformation, LogError, 로그 레벨, Debug, Warning, Error, Serilog, NLog, 예외 로그, 오류 기록
related: di-register-instance, attribute-caller-info, recipe-file-io, exception-try-catch
---
## 간단한 파일 로거 (의존성 주입 챕터의 FileLogger 확장)
```csharp
public interface ILogger { void Log(string message); }

public class DailyFileLogger : ILogger
{
    private readonly string _dir = Path.Combine(AppContext.BaseDirectory, "logs");
    private static readonly Lock _lock = new();

    public void Log(string message)
    {
        Directory.CreateDirectory(_dir);
        string path = Path.Combine(_dir, $"{DateTime.Now:yyyy-MM-dd}.log");   // 날짜별 파일
        lock (_lock)   // 여러 스레드가 동시에 쓰지 않게
            File.AppendAllText(path, $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
    }
}
```

## 예외 로그
```csharp
catch (Exception ex)
{
    logger.Log(ex.ToString());   // Message 만이 아니라 ToString() → 스택 트레이스까지
    throw;
}
```

## 실무 표준: Microsoft.Extensions.Logging
```csharp
// NuGet: Microsoft.Extensions.Logging.Console
services.AddLogging(b => b.AddConsole().SetMinimumLevel(LogLevel.Information));

public class OrderService(ILogger<OrderService> logger)
{
    public void Order(int id)
    {
        logger.LogInformation("주문 처리 시작 {OrderId}", id);   // 구조화 로그: 문자열 보간 대신 {이름}
        try { /* ... */ }
        catch (Exception ex)
        {
            logger.LogError(ex, "주문 실패 {OrderId}", id);
        }
    }
}
```

## 로그 레벨
`Trace` < `Debug` < `Information` < `Warning` < `Error` < `Critical`
- 운영 환경은 보통 Information 이상만 기록합니다.
- 파일 저장, 날짜별 분할, 보관 기간 등은 Serilog/NLog 같은 라이브러리를 붙이면 쉽습니다.

## 주의할 점
- 비밀번호, 토큰, 개인정보는 로그에 남기지 마세요.
