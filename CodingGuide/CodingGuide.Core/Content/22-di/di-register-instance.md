---
id: di-register-instance
title: 조건에 따라 다른 구현 등록하기 (설정에 따라 FileLogger/Logger)
category: 의존성 주입
order: 2207
summary: 설정값(LoggerType)에 따라 서로 다른 구현을 등록하고, 생성자에 문자열 같은 값이 필요한 구현(FileLogger)을 람다로 직접 만들어 등록하는 방법입니다.
keywords: 조건부 등록, 구현 선택, 설정에 따라, 팩토리 등록, 람다 등록, provider =>, 생성자에 값 전달, FileLogger, 파일 로그, 문자열 매개변수 주입
lesson: DI서비스구현체직접등록하기
source: 의존성주입/Container.cs, 의존성주입/Logging/FileLogger.cs, 의존성주입/Config/Settings.cs
related: di-register-factory, di-interface, di-refactoring
---
## 핵심
```csharp
if (Settings.LoggerType == LoggerType.File)
{
    // FileLogger 는 생성자에 파일 경로(string)가 필요 → 컨테이너가 알 수 없으므로 직접 만들어 준다
    services.AddTransient<ILogger>(provider => new FileLogger("log.txt"));
}
else
{
    services.AddTransient<ILogger, Logger>();
}
```
```csharp
Settings.LoggerType = LoggerType.File;
var main = new Container().Services.GetRequiredService<Main>();
main.CreateLog("hi");   // log.txt 에 "[FileLogger] 날짜: hi" 가 추가됨
```
> 이 가이드 프로그램에서 실행하면 `log.txt`는 프로그램 폴더에 만들어집니다. 또 `Settings.LoggerType`은 static이라 한 번 File로 바꾸면 프로그램을 다시 켤 때까지 유지됩니다.

## 등록 방법 모음
```csharp
services.AddTransient<ILogger, Logger>();                         // 타입으로
services.AddTransient<ILogger>(sp => new FileLogger("log.txt"));  // 팩토리 람다로
services.AddSingleton<ILogger>(new FileLogger("log.txt"));        // 이미 만든 인스턴스로 (싱글톤만)
```

## 더 나은 방법: 값은 설정(Options)으로
문자열 경로를 코드에 박는 대신 appsettings.json의 값을 `IOptions<T>`로 주입받으면 재컴파일 없이 바꿀 수 있습니다. (→ appsettings.json 문서)

## 참고: 키 있는 서비스 (.NET 8+)
같은 인터페이스의 여러 구현을 이름으로 구분해 등록할 수도 있습니다.
```csharp
services.AddKeyedTransient<ILogger, FileLogger>("file");
services.AddKeyedTransient<ILogger, Logger>("console");

class Main([FromKeyedServices("file")] ILogger logger) { }
```
