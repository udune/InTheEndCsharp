---
id: error-file-io
title: 파일 오류 - 파일을 찾을 수 없음, 경로 일부를 찾을 수 없음, 액세스 거부, 다른 프로세스에서 사용 중
category: 흔한 예외
order: 2709
summary: FileNotFoundException, DirectoryNotFoundException, UnauthorizedAccessException, IOException(파일 사용 중)의 원인과 해결(경로 확인, 작업 폴더, 권한, using으로 닫기)입니다.
keywords: FileNotFoundException, 파일을 찾을 수 없습니다, Could not find file, DirectoryNotFoundException, 경로의 일부를 찾을 수 없습니다, Could not find a part of the path, UnauthorizedAccessException, 경로에 대한 액세스가 거부되었습니다, Access to the path is denied, IOException, 다른 프로세스에서 사용 중이므로 프로세스가 파일에 액세스할 수 없습니다, The process cannot access the file because it is being used by another process, 파일 잠김, 권한 없음
related: recipe-file-io, recipe-using-dispose, exception-object
---
## FileNotFoundException - 파일을 찾을 수 없습니다
```csharp
File.ReadAllText("config.json");   // ← 예외! 어디를 기준으로 찾았을까?
```
- 상대 경로는 **현재 작업 폴더** 기준입니다. VS에서 실행하면 보통 `bin\Debug\net10.0\` 입니다.
- 파일을 프로젝트에 넣었다면 속성에서 **출력 디렉터리로 복사**를 설정했는지 확인하세요.
```csharp
string path = Path.Combine(AppContext.BaseDirectory, "config.json");
Console.WriteLine(path);                 // 실제로 찾는 경로를 찍어 보기
if (!File.Exists(path)) { /* 기본값 사용 */ }
```

## DirectoryNotFoundException - 경로의 일부를 찾을 수 없습니다
중간 폴더가 없습니다. 쓰기 전에 폴더를 만드세요.
```csharp
Directory.CreateDirectory(Path.GetDirectoryName(path)!);
File.WriteAllText(path, text);
```

## UnauthorizedAccessException - 경로에 대한 액세스가 거부되었습니다
- `C:\Program Files`, `C:\Windows` 같은 보호된 폴더에 쓰려고 함 → `AppData`나 문서 폴더에 저장하세요.
- **폴더 경로**를 파일처럼 열려고 함 (`File.ReadAllText(@"C:\temp")`)
- 읽기 전용 파일에 쓰기

## IOException - 다른 프로세스에서 사용 중
- 내 코드가 이전에 연 스트림을 **닫지 않았거나**(using 누락), 엑셀/메모장 등 다른 프로그램이 파일을 열고 있습니다.
```csharp
using (var fs = File.OpenWrite(path)) { ... }   // 반드시 using 으로 닫기
// 읽기만 할 때 다른 프로그램과 공유
using var fs2 = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
```

## 한 번에 처리
```csharp
try { File.WriteAllText(path, text); }
catch (UnauthorizedAccessException) { /* 권한 */ }
catch (IOException ex) { /* 사용 중, 경로 없음 등 (FileNotFound, DirectoryNotFound 의 부모) */ }
```
