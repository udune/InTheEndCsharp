---
id: error-cs0246
title: CS0246 - 형식 또는 네임스페이스 이름을 찾을 수 없습니다
category: 컴파일 오류
order: 2802
summary: using 누락, NuGet 패키지 미설치, 프로젝트 참조 누락, 오타로 클래스나 네임스페이스를 찾지 못할 때 나는 오류와 해결 순서입니다.
keywords: CS0246, 형식 또는 네임스페이스 이름을 찾을 수 없습니다, using 지시문 또는 어셈블리 참조가 있는지 확인하세요, The type or namespace name could not be found, are you missing a using directive or an assembly reference, using 누락, NuGet 누락, 참조 누락, 패키지 설치
related: namespace, assembly-dll, error-cs0103
---
## 메시지
- `error CS0246: 'Foo' 형식 또는 네임스페이스 이름을 찾을 수 없습니다. using 지시문 또는 어셈블리 참조가 있는지 확인하세요.`
- The type or namespace name 'Foo' could not be found (are you missing a using directive or an assembly reference?)

## 확인 순서
1. **using이 있나요?** 타입이 있는 네임스페이스를 가져와야 합니다.
```csharp
using System.Text;            // StringBuilder
using System.Text.Json;       // JsonSerializer
using System.Diagnostics;     // Stopwatch
using System.Reflection;      // PropertyInfo, MethodInfo
using System.Collections.Concurrent;   // ConcurrentDictionary
using System.Text.RegularExpressions;  // Regex
using Microsoft.Extensions.DependencyInjection;   // ServiceCollection (NuGet 필요)
```
IDE에서 빨간 줄에 커서를 두고 `Ctrl + .`을 누르면 using을 자동으로 추가해 줍니다.

2. **NuGet 패키지를 설치했나요?** 예: `ServiceCollection` → `Microsoft.Extensions.DependencyInjection`, `ProxyGenerator` → `Castle.Core`
```
dotnet add package Microsoft.Extensions.DependencyInjection
```
3. **다른 프로젝트의 클래스인가요?** 프로젝트 참조(`ProjectReference`)를 추가하고, 그 클래스가 `public`인지 확인합니다.
4. **오타/대소문자**를 확인합니다.
5. **대상 프레임워크**: WPF/WinForms 타입은 `net10.0-windows`와 `<UseWPF>` / `<UseWindowsForms>`가 필요합니다.
