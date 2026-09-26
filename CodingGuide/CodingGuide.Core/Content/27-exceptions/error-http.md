---
id: error-http
title: HttpRequestException - 서버 연결 실패, SSL 오류, 타임아웃
category: 흔한 예외
order: 2711
summary: HttpClient 호출 시 서버가 꺼져 있거나(연결 거부), 개발용 HTTPS 인증서 문제(SSL), 응답이 늦어 타임아웃(TaskCanceledException)이 나는 경우의 원인과 처리입니다.
keywords: HttpRequestException, 대상 컴퓨터에서 연결을 거부했으므로 연결하지 못했습니다, No connection could be made because the target machine actively refused it, SSL, The SSL connection could not be established, 인증서 오류, dev-certs, TaskCanceledException, 타임아웃, The request was canceled due to the configured HttpClient.Timeout, 404, 500, 네트워크 오류, API 오류
related: api-httpclient-get, api-client, cancellation-token, task-whenall
---
## 1) 연결 거부
- 대상 컴퓨터에서 연결을 거부했으므로 연결하지 못했습니다. / No connection could be made because the target machine actively refused it.

→ 서버(예: APIBackend)가 **실행 중이 아니거나 포트가 다릅니다.** `launchSettings.json`의 포트와 클라이언트 BaseUrl을 비교하세요.

## 2) SSL 연결 실패
- The SSL connection could not be established.

→ 로컬 개발 인증서를 신뢰하지 않았습니다.
```
dotnet dev-certs https --trust
```
또는 개발 중에는 `http://` 주소를 사용합니다.

## 3) 타임아웃
- The request was canceled due to the configured HttpClient.Timeout of 100 seconds elapsing. (`TaskCanceledException`)
```csharp
var http = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
```

## 4) 404, 500 등 (예외가 아님!)
`GetAsync`는 404/500이어도 예외를 던지지 않습니다. 상태 코드를 확인하세요.
```csharp
var res = await http.GetAsync(url);
if (!res.IsSuccessStatusCode)
    Console.WriteLine($"{(int)res.StatusCode}: {await res.Content.ReadAsStringAsync()}");
```
`GetStringAsync`, `GetFromJsonAsync`, `EnsureSuccessStatusCode()`는 실패 코드면 `HttpRequestException`을 던집니다(`ex.StatusCode`로 확인).

## 통합 처리
```csharp
try
{
    var data = await http.GetFromJsonAsync<List<UserDto>>(url);
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"통신 실패: {ex.StatusCode} {ex.Message}");
}
catch (TaskCanceledException)
{
    Console.WriteLine("시간 초과");
}
```
