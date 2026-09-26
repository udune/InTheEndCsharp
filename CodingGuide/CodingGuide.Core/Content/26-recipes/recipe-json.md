---
id: recipe-json
title: JSON 직렬화/역직렬화 (System.Text.Json) - 객체 ↔ JSON, 파일 저장
category: 실무 레시피
order: 2607
summary: System.Text.Json으로 객체를 JSON 문자열로 바꾸고(직렬화) 다시 객체로 읽는(역직렬화) 방법, 들여쓰기·camelCase·한글 옵션, 파일로 설정 저장/불러오기입니다.
keywords: JSON, json, 직렬화, 역직렬화, serialize, deserialize, JsonSerializer, System.Text.Json, JSON 파싱, JSON 읽기, JSON 저장, 설정 저장, 객체를 문자열로, camelCase, 들여쓰기, WriteIndented, 한글 깨짐, \uXXXX, JsonPropertyName, JsonIgnore, JsonNode, 동적 JSON, Newtonsoft
related: api-httpclient-get, recipe-file-io, di-appsettings, record-type
---
## 기본
```csharp
using System.Text.Json;

var user = new User { Name = "홍길동", Age = 30 };

string json = JsonSerializer.Serialize(user);           // {"Name":"홍길동","Age":30}
User? back = JsonSerializer.Deserialize<User>(json);    // 다시 객체로

List<User>? users = JsonSerializer.Deserialize<List<User>>(jsonArray);
```

## 자주 쓰는 옵션 (static으로 한 번만 만들어 재사용)
```csharp
using System.Text.Encodings.Web;
using System.Text.Unicode;

static readonly JsonSerializerOptions Options = new()
{
    WriteIndented = true,                                  // 보기 좋게 들여쓰기
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,     // Name → "name"
    PropertyNameCaseInsensitive = true,                    // 읽을 때 대소문자 무시
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), // 한글을 홍 대신 그대로 출력
};
```
> 한글이 `홍길동`처럼 나오는 것은 깨진 게 아니라 이스케이프된 것입니다. 위 Encoder 옵션으로 그대로 출력할 수 있습니다.

## 파일로 저장/불러오기 (설정 파일)
```csharp
string path = Path.Combine(AppContext.BaseDirectory, "settings.json");

// 저장
await File.WriteAllTextAsync(path, JsonSerializer.Serialize(settings, Options));

// 불러오기 (없으면 기본값)
Settings loaded = File.Exists(path)
    ? JsonSerializer.Deserialize<Settings>(await File.ReadAllTextAsync(path), Options) ?? new()
    : new Settings();
```

## 속성별 제어
```csharp
public class User
{
    [JsonPropertyName("user_name")] public string Name { get; set; } = "";
    [JsonIgnore] public string Password { get; set; } = "";
}
```

## 클래스 없이 읽기 (JsonNode)
```csharp
using System.Text.Json.Nodes;
JsonNode? node = JsonNode.Parse(json);
string? city = node?["address"]?["city"]?.GetValue<string>();
```

## 주의할 점
- 역직렬화 대상 클래스에는 **public 속성**과 (보통) **매개변수 없는 생성자**가 필요합니다. 필드는 기본적으로 무시됩니다.
- 속성 이름이 JSON과 대소문자가 다르면 값이 비어 있습니다 → `PropertyNameCaseInsensitive` 또는 `CamelCase` 옵션.
