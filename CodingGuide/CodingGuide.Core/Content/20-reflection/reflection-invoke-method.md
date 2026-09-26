---
id: reflection-invoke-method
title: 리플렉션 - 메서드 동적 호출 (Invoke)
category: 리플렉션
order: 2008
summary: 메서드 이름(문자열)으로 MethodInfo를 찾아 Invoke로 호출하고, 인자를 object 배열로 넘기는 방법입니다.
keywords: Invoke, MethodInfo.Invoke, 동적 호출, 이름으로 메서드 호출, 문자열로 메서드 실행, 정적 메서드 호출, TargetInvocationException, 명령 실행기
lesson: 리플렉션_동적메서드호출
related: reflection-methods, reflection-create-instance, delegate-basics
---
## 핵심
```csharp
class Sample
{
    public void Print(string text, int count) =>
        Console.WriteLine($"Hello World!: {text} - {count}");
}

Type type = typeof(Sample);
Sample instance = Activator.CreateInstance<Sample>();

MethodInfo? method = type.GetMethod("Print");
method?.Invoke(instance, ["까불이", 3]);   // (대상 객체, 인자 배열)
// Hello World!: 까불이 - 3
```

## 정적 메서드와 반환값
```csharp
MethodInfo run = typeof(정수형타입).GetMethod("실행")!;
run.Invoke(null, null);                        // static 이면 대상 객체 자리에 null

object? result = typeof(Math).GetMethod("Max", [typeof(int), typeof(int)])!
                             .Invoke(null, [3, 7]);   // 7 (object 로 박싱되어 반환)
```
이 가이드 프로그램의 [예제 실행] 버튼도 레슨 클래스에서 `실행` 메서드를 찾아 이렇게 호출합니다.

## 예외 처리
호출한 메서드 안에서 예외가 나면 `TargetInvocationException`으로 한 번 감싸져서 나옵니다.
```csharp
try { method.Invoke(instance, args); }
catch (TargetInvocationException ex)
{
    Console.WriteLine(ex.InnerException?.Message);   // 진짜 원인
}
```

## 성능
Invoke는 일반 호출보다 느립니다. 같은 메서드를 반복 호출한다면 델리게이트로 바꿔 두세요.
```csharp
var print = method.CreateDelegate<Action<string, int>>(instance);
print("빠름", 1);
```
