---
id: namespace
title: 네임스페이스와 using (using static, 별칭)
category: 네임스페이스와 어셈블리
order: 1901
summary: 클래스를 이름 공간으로 묶는 namespace, 다른 네임스페이스를 가져오는 using, 정적 멤버를 바로 쓰는 using static, 이름 충돌 해결법입니다.
keywords: 네임스페이스, namespace, using, using static, global using, 별칭, alias, 이름 충돌, 파일 범위 네임스페이스, CS0246, 형식 또는 네임스페이스 이름을 찾을 수 없습니다
lesson: 네임스페이스
source: 네임스페이스/MathHelper.cs
related: assembly-dll, error-cs0246, static-members
---
## 핵심
네임스페이스는 **클래스의 주소(폴더)** 입니다. 같은 이름의 클래스도 네임스페이스가 다르면 공존할 수 있습니다.

```csharp
// MathHelper.cs
namespace InTheEndCsharp.Utilities;     // 파일 범위 네임스페이스 (C# 10+, 권장)

public class MathHelper
{
    public static decimal Plus(decimal a, decimal b) => a + b;
}
```

```csharp
// 네임스페이스.cs
using static InTheEndCsharp.Utilities.MathHelper;   // 정적 멤버를 클래스 이름 없이 사용

namespace InTheEndCsharp.네임스페이스;

public class 네임스페이스
{
    public static void 실행()
    {
        decimal result = Plus(3.3M, 5.5M);   // MathHelper.Plus 를 바로 호출
        Console.WriteLine(result);            // 8.8
    }
}
```

## using의 여러 형태
```csharp
using System.Text;                          // 네임스페이스 가져오기
using static System.Math;                   // Max(1, 2) 처럼 바로 사용
using Json = System.Text.Json.JsonSerializer; // 별칭
global using System.Linq;                   // 프로젝트 전체에 적용 (C# 10+)
```

## 이름 충돌 해결
```csharp
using WinTimer = System.Windows.Forms.Timer;
using ThreadTimer = System.Threading.Timer;

var t1 = new System.Timers.Timer();   // 전체 이름으로 쓰기
```

## 자주 만나는 오류
`CS0246: 'XXX' 형식 또는 네임스페이스 이름을 찾을 수 없습니다` → 1) using이 빠졌거나, 2) 프로젝트 참조/NuGet 패키지가 없거나, 3) 오타입니다.

## 관례
- 네임스페이스는 `회사.프로젝트.폴더` 구조를 따라 짓습니다.
- `<ImplicitUsings>enable</ImplicitUsings>`이면 System, System.Linq, System.IO 등 자주 쓰는 using이 자동 포함됩니다.
