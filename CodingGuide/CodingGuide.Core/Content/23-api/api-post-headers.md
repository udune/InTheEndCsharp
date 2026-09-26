---
id: api-post-headers
title: POST 요청 보내기 - JSON 본문과 요청 헤더(API 키, 토큰)
category: API 통신
order: 2304
summary: 객체를 JSON으로 직렬화해 POST 요청 본문으로 보내고, X-API-KEY나 Authorization 같은 헤더를 붙이는 방법입니다.
keywords: POST, POST 요청, 데이터 보내기, JSON 전송, StringContent, 직렬화, serialize, 요청 헤더, header, X-API-KEY, Authorization, Bearer 토큰, HttpRequestMessage, SendAsync, PostAsJsonAsync, PUT, DELETE
source: APIApp/Services/ApiClient.cs, APIApp/Form1.cs
related: api-client, api-httpclient-get, recipe-json, api-backend
---
## JSON 본문 + 헤더로 POST
```csharp
public async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(
    string url, TRequest body, Dictionary<string, string>? headers = null)
{
    string json = JsonSerializer.Serialize(body);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
    if (headers != null)
        foreach (var (key, value) in headers)
            request.Headers.Add(key, value);           // 요청별 헤더

    var response = await _httpClient.SendAsync(request);
    return await HandleResponseAsync<TResponse>(response);
}

// 사용
var newUser = new UserDto { Name = "New User", Email = "test@test.com", Username = "newuser" };
var response = await _apiClient.PostAsync<UserDto, UserDto>(
    "/Users", newUser,
    headers: new() { ["X-API-KEY"] = "api-value" });
```

## 짧은 방법
```csharp
var res = await httpClient.PostAsJsonAsync("/Users", newUser);   // System.Net.Http.Json
var created = await res.Content.ReadFromJsonAsync<UserDto>();
```

## 인증 토큰 (Bearer)
```csharp
httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", accessToken);        // 모든 요청에 적용
```

## PUT / DELETE
```csharp
await httpClient.PutAsJsonAsync($"/Users/{id}", user);
await httpClient.DeleteAsync($"/Users/{id}");
```

## 주의할 점
- `DefaultRequestHeaders`는 HttpClient를 공유하는 **모든 요청**에 붙습니다. 요청마다 다른 헤더는 `HttpRequestMessage.Headers`에 넣으세요.
- `Content-Type` 같은 본문 헤더는 `request.Headers`가 아니라 `content.Headers`에 속합니다.
