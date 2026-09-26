---
id: assembly-dll
title: 어셈블리와 DLL 라이브러리 참조 (프로젝트 참조, NuGet)
category: 네임스페이스와 어셈블리
order: 1902
summary: 빌드 결과물인 어셈블리(.dll/.exe), 클래스 라이브러리를 만들어 다른 프로젝트에서 참조하는 방법, internal의 의미, NuGet 패키지입니다.
keywords: 어셈블리, assembly, dll, 라이브러리, 클래스 라이브러리, class library, 프로젝트 참조, ProjectReference, 참조 추가, NuGet, 패키지, PackageReference, internal, 오프라인 패키지
lesson: 어셈블리_dll라이브러리참조
related: namespace, access-modifiers, reflection-assembly-info
---
## 핵심
- **어셈블리**: 프로젝트를 빌드한 결과물(.dll 또는 .exe). 코드와 메타데이터(타입 정보)가 들어 있습니다.
- 공통 코드를 **클래스 라이브러리(.dll)** 로 만들어 여러 프로젝트에서 재사용할 수 있습니다.

```csharp
public class 어셈블리_dll라이브러리참조
{
    public static int Add(int a, int b) => a + b;   // public 이어야 다른 어셈블리에서 사용 가능
}
```

## 라이브러리 만들고 참조하기 (명령줄)
```
dotnet new classlib -n MyLib
dotnet add MyApp/MyApp.csproj reference MyLib/MyLib.csproj
```
csproj에는 이렇게 기록됩니다.
```xml
<ItemGroup>
  <ProjectReference Include="..\MyLib\MyLib.csproj" />
</ItemGroup>
```
이 저장소의 `Tests` 프로젝트도 `InTheEndCsharp.csproj`를 이렇게 참조합니다.

## internal
`internal` 멤버는 **같은 어셈블리 안에서만** 보입니다. 테스트 프로젝트에만 열어주려면:
```xml
<ItemGroup>
  <InternalsVisibleTo Include="MyLib.Tests" />
</ItemGroup>
```

## NuGet 패키지
외부 라이브러리는 NuGet으로 받습니다.
```
dotnet add package Microsoft.Extensions.DependencyInjection
```
```xml
<PackageReference Include="Castle.Core" Version="5.2.1" />
```

## 인터넷이 없는 환경에서 패키지 쓰기
- 인터넷 되는 PC에서 한 번 `dotnet restore` 하면 `%USERPROFILE%\.nuget\packages`에 캐시됩니다. 이 폴더를 복사하면 오프라인에서도 복원됩니다.
- 또는 `.nupkg` 파일들을 한 폴더에 모아 로컬 소스로 등록합니다.
```
dotnet nuget add source D:\offline-packages -n offline
```
