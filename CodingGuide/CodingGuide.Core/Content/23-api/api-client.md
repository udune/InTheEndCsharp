---
id: api-client
title: 재사용 가능한 API 클라이언트 만들기 (ApiClient + ApiResponse<T>)
category: API 통신
order: 2303
summary: HttpClient 호출과 JSON 변환, 성공/실패 처리를 제네릭 ApiClient와 ApiResponse<T>로 감싸 화면 코드를 단순하게 만드는 방법입니다.
keywords: ApiClient, ApiResponse, API 래퍼, wrapper, 제네릭 API 호출, 공통 응답, 에러 처리, BaseAddress, HandleResponse, 리팩토링, DataGridView 바인딩
source: APIApp/Services/ApiClient.cs, APIApp/Models/ApiResponse.cs, APIApp/Form1.cs
related: api-httpclient-get, api-post-headers, generic-class, recipe-json
---
## 응답을 담는 제네릭 클래스
```csharp
public class ApiResponse<T>
{
    public int StatusCode { get; }
    public T Data { get; }
    public string? ErrorMessage { get; }
    public bool IsSuccess => StatusCode >= 200 && StatusCode <= 299;

    public ApiResponse(int statusCode, T data = default!, string? errorMessage = null)
    {
        StatusCode = statusCode;
        Data = data;
        ErrorMessage = errorMessage;
    }
}
```

## 클라이언트
```csharp
public class ApiClient
{
    private readonly HttpClient _httpClient = new();

    public ApiClient(string? baseUrl)
    {
        if (!string.IsNullOrEmpty(baseUrl))
            _httpClient.BaseAddress = new Uri(baseUrl);   // 이후엔 "/Users" 처럼 상대 경로만
    }

    public async Task<ApiResponse<TResponse>> GetAsync<TResponse>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        return await HandleResponseAsync<TResponse>(response);
    }

    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private async Task<ApiResponse<TResponse>> HandleResponseAsync<TResponse>(HttpResponseMessage response)
    {
        string body = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var data = JsonSerializer.Deserialize<TResponse>(body, JsonOptions);
            return new ApiResponse<TResponse>((int)response.StatusCode, data!);
        }
        return new ApiResponse<TResponse>((int)response.StatusCode, errorMessage: body);
    }
}
```

## 화면 코드가 단순해짐
```csharp
private readonly ApiClient _apiClient = new("https://localhost:7038");

private async void btnGetUsers_Click(object sender, EventArgs e)
{
    var response = await _apiClient.GetAsync<List<UserDto>>("/Users");
    if (response.IsSuccess)
        dgv.DataSource = response.Data;        // DataGridView 에 바로 표시
    else
        MessageBox.Show($"Error: {response.StatusCode} - {response.ErrorMessage}");
}
```

## 개선 포인트
- `JsonSerializerOptions`는 매번 new 하지 말고 static으로 재사용하세요(위 코드처럼). 매번 만들면 느립니다.
- 네트워크 예외(서버 꺼짐, 타임아웃)는 `HttpRequestException`/`TaskCanceledException`으로 오므로 HandleResponse 바깥에서 try-catch 해서 ApiResponse 실패로 바꿔 주면 더 안전합니다.
- DI를 쓴다면 `services.AddHttpClient<ApiClient>(c => c.BaseAddress = new Uri(...))`로 등록합니다.
