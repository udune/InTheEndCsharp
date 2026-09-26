---
id: di-appsettings
title: appsettings.json 설정을 DI로 불러오기 (IOptions<T>)
category: 의존성 주입
order: 2210
summary: appsettings.json 파일의 설정(API 키, 포트)을 ConfigurationBuilder로 읽어 클래스에 바인딩하고, IOptions<AppSettings>로 주입받는 방법입니다.
keywords: appsettings.json, 설정 파일, 환경 설정, configuration, ConfigurationBuilder, AddJsonFile, IOptions, Options 패턴, services.Configure, 설정 바인딩, API 키, 환경 변수, 비밀 값, user secrets, CopyToOutputDirectory
lesson: 환경변수의존성주입을사용하여불러오기_appsettings_json
source: 의존성주입/Services/AppSettingsExtensions.cs, 의존성주입/Config/AppSettings.cs
related: di-refactoring, di-register-instance, recipe-json
---
## 1) 설정 파일
```json
{
  "ApiKey": {
    "OpenAI": "openai_123",
    "Claude": "claude_123"
  },
  "Port": 3306
}
```
빌드 출력 폴더로 복사되도록 csproj에 설정합니다.
```xml
<None Update="appsettings.json">
  <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
</None>
```

## 2) 설정을 담을 클래스 (JSON 구조와 이름을 맞춤)
```csharp
public class AppSettings
{
    public ApiKey ApiKey { get; set; } = new();
    public int Port { get; set; }
}
public class ApiKey
{
    public string OpenAI { get; set; } = "";
    public string Claude { get; set; } = "";
}
```

## 3) 읽어서 등록
```csharp
// NuGet: Microsoft.Extensions.Configuration.Json, Microsoft.Extensions.Options.ConfigurationExtensions
public static void AddAppSettings(this IServiceCollection services)
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()          // (선택) 환경 변수가 JSON 값을 덮어씀
        .Build();
    services.Configure<AppSettings>(configuration);
}
```

## 4) 주입받아 사용
```csharp
public class Main(IOptions<AppSettings> options)
{
    private readonly AppSettings appSettings = options.Value;
    public void Run() => Console.WriteLine($"Port: {appSettings.Port}");
}
```

## 설정 값 직접 읽기
```csharp
string? key = configuration["ApiKey:OpenAI"];        // 계층은 콜론(:)
int port = configuration.GetValue<int>("Port");
```

## 주의할 점
- **API 키 같은 비밀 값은 git에 올리지 마세요.** 개발 중에는 user-secrets, 운영에서는 환경 변수(`ApiKey__OpenAI`)를 씁니다.
- 파일을 못 찾으면 `FileNotFoundException`이 납니다. 출력 폴더에 복사됐는지 확인하세요.
