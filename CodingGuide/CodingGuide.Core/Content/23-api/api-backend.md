---
id: api-backend
title: ASP.NET Core Web API 만들기 (Controller, GET/POST, 라우팅)
category: API 통신
order: 2301
summary: ASP.NET Core로 사용자 목록 조회/단건 조회/생성 API를 만드는 방법. 컨트롤러, 라우트, 상태 코드(200/404/201), Swagger UI를 설명합니다.
keywords: Web API, REST API, ASP.NET Core, 컨트롤러, Controller, ControllerBase, ApiController, Route, HttpGet, HttpPost, FromBody, NotFound, CreatedAtAction, ActionResult, 라우팅, Swagger, OpenAPI, 백엔드, 서버 만들기
source: APIBackend/Controllers/UsersController.cs, APIBackend/Program.cs
related: api-httpclient-get, api-client, api-post-headers, recipe-json
---
## 서버 시작 코드 (Program.cs)
```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();     // 컨트롤러 사용
builder.Services.AddOpenApi();         // API 문서(OpenAPI) 생성

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "v1"));   // /swagger 에서 테스트 화면
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
```

## 컨트롤러
```csharp
[Route("[controller]")]      // 클래스 이름에서 Controller 를 뺀 "Users" → /users
[ApiController]
public class UsersController : ControllerBase
{
    public static List<UserDto> Users = [ /* 메모리 데이터 */ ];

    [HttpGet]                                   // GET /users
    public IEnumerable<UserDto> GetUsers() => Users;

    [HttpGet("{userId:int}")]                   // GET /users/3
    public ActionResult<UserDto> GetUser(int userId)
    {
        var user = Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return NotFound();    // 404
        return user;                            // 200 + JSON
    }

    [HttpPost]                                  // POST /users  (본문: JSON)
    public ActionResult<UserDto> CreateUser([FromBody] UserDto user)
    {
        string? apiKey = Request.Headers["X-API-KEY"];   // 요청 헤더 읽기
        user.Id = Users.Max(x => x.Id) + 1;
        Users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);   // 201 + Location 헤더
    }
}
```

## 자주 쓰는 응답
| 메서드 | 상태 코드 | 의미 |
|---|---|---|
| `Ok(data)` / 그냥 return | 200 | 성공 |
| `CreatedAtAction(...)` | 201 | 생성됨 |
| `NoContent()` | 204 | 성공, 본문 없음 (삭제 등) |
| `BadRequest("사유")` | 400 | 잘못된 요청 |
| `Unauthorized()` | 401 | 인증 필요 |
| `NotFound()` | 404 | 없음 |

## 실행과 테스트
- `dotnet run` 후 브라우저에서 `https://localhost:포트/swagger` → 화면에서 API를 직접 호출해 볼 수 있습니다.
- 포트는 `Properties/launchSettings.json`에 있습니다.
- `.http` 파일(APIBackend.http)로 IDE 안에서 요청을 보낼 수도 있습니다.

## 주의할 점
- 예제의 `static List`는 여러 요청이 동시에 수정하면 안전하지 않습니다. 실무에서는 DB를 쓰거나 lock/동시성 컬렉션을 사용합니다.
