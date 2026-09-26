---
id: reflection-assembly-info
title: 리플렉션 - 어셈블리 메타 정보(버전) 가져오기
category: 리플렉션
order: 2001
summary: 리플렉션의 개념과 Assembly.LoadFrom으로 DLL을 불러와 버전, 파일 버전 등 메타 정보를 읽는 방법입니다.
keywords: 리플렉션, reflection, 메타데이터, 메타 정보, Assembly, Assembly.LoadFrom, GetExecutingAssembly, 버전, Version, FileVersionInfo, 프로그램 버전 표시, 런타임 타입 정보
lesson: 리플렉션_메타정보가져오기
related: reflection-properties, reflection-methods, assembly-dll, attribute-read-metadata
---
## 리플렉션이란?
실행 중에 **타입, 메서드, 속성 같은 코드 자체의 정보를 읽고 조작**하는 기능입니다. 이름(문자열)만으로 객체를 만들고 메서드를 호출할 수도 있습니다.

쓰이는 곳: 직렬화(JSON), DI 컨테이너, 테스트 프레임워크, ORM, 플러그인 로딩, 그리고 이 가이드 프로그램의 [예제 실행] 버튼도 리플렉션으로 `실행()` 메서드를 찾아 호출합니다.

## 어셈블리 정보 읽기
```csharp
using System.Diagnostics;
using System.Reflection;

string dllPath = Path.Combine(AppContext.BaseDirectory, "InTheEndCsharp.dll");
Assembly assembly = Assembly.LoadFrom(dllPath);

Console.WriteLine($"Version: {assembly.GetName().Version}");                          // 1.0.0.0
Console.WriteLine($"FileVersion: {FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion}");
```

## 현재 프로그램 정보
```csharp
Assembly me = Assembly.GetExecutingAssembly();   // 이 코드가 들어 있는 어셈블리
Assembly entry = Assembly.GetEntryAssembly()!;   // 실행한 exe
string? ver = entry.GetName().Version?.ToString();
```
버전은 csproj에서 지정합니다: `<Version>1.2.3</Version>`

## 주의할 점
- 예제는 `Environment.CurrentDirectory`(현재 작업 폴더) 기준으로 DLL을 찾습니다. 바로가기 등으로 실행하면 작업 폴더가 달라질 수 있으니 `AppContext.BaseDirectory`(실행 파일 폴더)가 더 안전합니다.
- 단일 파일(single-file)로 배포하면 `assembly.Location`이 빈 문자열이 됩니다.
