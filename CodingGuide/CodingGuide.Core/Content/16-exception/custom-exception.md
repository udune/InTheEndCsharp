---
id: custom-exception
title: 사용자 정의 예외 만들기 (class MyException : Exception)
category: 예외 처리
order: 1604
summary: Exception을 상속해 에러 코드 같은 추가 정보를 담은 나만의 예외 클래스를 만들고 throw 하는 방법입니다.
keywords: 사용자 정의 예외, 커스텀 예외, custom exception, throw, 예외 던지기, 예외 만들기, Exception 상속, 에러 코드, ErrorCode
lesson: 사용자정의예외처리
related: custom-exception-benefit, exception-object, inheritance
---
## 핵심
```csharp
class CustomException : Exception
{
    public int ErrorCode { get; }

    public CustomException(string message, int errorCode) : base(message)   // 메시지는 부모에게
    {
        ErrorCode = errorCode;
    }
}

try
{
    throw new CustomException("커스텀 에러 발생", 2);   // 예외 던지기
}
catch (CustomException e)
{
    Console.WriteLine($"{e.ErrorCode} || {e.Message}");
}
```

## 관례
- 이름은 `~Exception`으로 끝냅니다.
- 원인이 된 예외를 함께 전달하려면 `innerException`을 받는 생성자를 둡니다.
```csharp
public class DataLoadException : Exception
{
    public DataLoadException(string message, Exception inner) : base(message, inner) { }
}

try { File.ReadAllText(path); }
catch (IOException ex)
{
    throw new DataLoadException($"{path} 를 읽을 수 없습니다", ex);
}
```

## 먼저 기존 예외를 고려하세요
인자 검사 같은 흔한 상황은 이미 있는 예외가 더 적절합니다.
```csharp
ArgumentNullException.ThrowIfNull(user);
ArgumentOutOfRangeException.ThrowIfNegative(amount);
if (!IsReady) throw new InvalidOperationException("준비되지 않았습니다");
```
