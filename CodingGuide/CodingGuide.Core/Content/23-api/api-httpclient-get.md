---
id: api-httpclient-get
title: HttpClient로 API 호출하기 (GET + JSON 역직렬화)
category: API 통신
order: 2302
summary: HttpClient로 서버에 GET 요청을 보내고, 응답 JSON 문자열을 System.Text.Json으로 C# 객체(DTO)로 바꾸는 방법과 상태 코드 확인입니다.
keywords: HttpClient, API 호출, GET 요청, REST 호출, 웹 요청, JSON 파싱, 역직렬화, deserialize, JsonSerializer, DTO, camelCase, IsSuccessStatusCode, GetFromJsonAsync, 서버에서 데이터 가져오기
source: APIApp/Form1.cs, APIApp/Dtos/UserDto.cs
related: api-client, api-post-headers, recipe-json, async-await, api-backend
---
## 기본 흐름
```csharp
readonly HttpClient httpClient = new HttpClient();     // 재사용! (매번 new 하지 않기)
const string BaseUrl = "https://localhost:7038";

async Task<List<UserDto>> GetUsersAsync()
{
    HttpResponseMessage response = await httpClient.GetAsync($"{BaseUrl}/Users");
    string json = await response.Content.ReadAsStringAsync();

    var users = JsonSerializer.Deserialize<List<UserDto>>(json, new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase      // JSON "name" ↔ C# Name
    });
    return users ?? [];
}
```

## 상태 코드 확인
```csharp
var response = await httpClient.GetAsync($"{BaseUrl}/Users/{id}");
if (response.IsSuccessStatusCode)              // 200~299
{
    var user = await response.Content.ReadFromJsonAsync<UserDto>();   // System.Net.Http.Json
}
else if (response.StatusCode == HttpStatusCode.NotFound)
{
    MessageBox.Show("사용자가 없습니다");
}

response.EnsureSuccessStatusCode();   // 실패면 HttpRequestException 을 던지게 하기
```

## 가장 짧은 방법 (System.Net.Http.Json)
```csharp
List<UserDto>? users = await httpClient.GetFromJsonAsync<List<UserDto>>($"{BaseUrl}/Users");
```
`GetFromJsonAsync`, `ReadFromJsonAsync`는 웹 기본값(camelCase 무시 등)을 자동 적용합니다.

## DTO 클래스
서버 JSON 구조와 같은 모양의 클래스를 만듭니다. 중첩 객체도 그대로 클래스로 둡니다.
```csharp
public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public AddressDto Address { get; set; } = new();
}
```

## 주의할 점
- `HttpClient`를 요청마다 `new`/`Dispose` 하면 소켓이 고갈될 수 있습니다. **하나를 만들어 재사용**하거나 `IHttpClientFactory`를 씁니다.
- 로컬 개발 서버의 HTTPS 인증서 오류가 나면 `dotnet dev-certs https --trust`를 실행합니다.
- 서버가 꺼져 있으면 `HttpRequestException`(연결 거부)이 납니다. try-catch로 처리하세요.
