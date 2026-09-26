---
id: recipe-file-io
title: 파일과 폴더 다루기 (읽기, 쓰기, 추가, 존재 확인, 경로 합치기)
category: 실무 레시피
order: 2606
summary: File.ReadAllText/WriteAllText, 줄 단위 읽기, 이어 쓰기, 폴더 만들기, 파일 목록, 경로 합치기(Path.Combine), 실행 파일 폴더 경로 등 파일 입출력 모음입니다.
keywords: 파일, 파일 읽기, 파일 쓰기, 파일 저장, 텍스트 파일, File.ReadAllText, File.WriteAllText, File.AppendAllText, ReadAllLines, ReadLines, StreamReader, StreamWriter, 폴더 만들기, Directory.CreateDirectory, 파일 목록, GetFiles, 파일 존재, File.Exists, 파일 삭제, 파일 복사, 파일 이동, Path.Combine, 확장자, 파일명, 실행 폴더, 바탕화면 경로, 인코딩, UTF-8, 한글 깨짐
related: recipe-json, recipe-csv, recipe-using-dispose, async-file-progress, error-file-io
---
## 통째로 읽고 쓰기
```csharp
string text = File.ReadAllText("memo.txt");                 // 기본 UTF-8
File.WriteAllText("memo.txt", "내용");                       // 덮어쓰기 (없으면 생성)
File.AppendAllText("log.txt", $"{DateTime.Now}: 시작\n");    // 뒤에 추가

string[] lines = File.ReadAllLines("data.txt");
File.WriteAllLines("out.txt", lines);

// 비동기 버전
string t = await File.ReadAllTextAsync("memo.txt");
```

## 큰 파일은 줄 단위로
```csharp
foreach (string line in File.ReadLines("big.log"))   // 한 줄씩 읽음 (메모리 절약)
{
    if (line.Contains("ERROR")) Console.WriteLine(line);
}

using var writer = new StreamWriter("out.txt", append: false);
writer.WriteLine("첫 줄");
```

## 존재 확인, 복사, 이동, 삭제
```csharp
if (File.Exists(path)) { }
File.Copy("a.txt", "b.txt", overwrite: true);
File.Move("b.txt", "c.txt");
File.Delete("c.txt");                         // 없어도 예외 없음

Directory.CreateDirectory(@"C:\temp\logs");   // 중간 폴더까지 생성, 이미 있어도 OK
bool dirExists = Directory.Exists(@"C:\temp");
string[] files = Directory.GetFiles(@"C:\temp", "*.txt", SearchOption.AllDirectories);
```

## 경로 다루기
```csharp
string full = Path.Combine(AppContext.BaseDirectory, "data", "user.json");   // 실행 파일 폴더 기준
Path.GetFileName(full);                    // user.json
Path.GetFileNameWithoutExtension(full);    // user
Path.GetExtension(full);                   // .json
Path.GetDirectoryName(full);               // ...\data
Path.ChangeExtension(full, ".bak");

string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string temp = Path.GetTempPath();
```

## 한글이 깨질 때
옛 프로그램이 만든 파일은 EUC-KR(CP949)일 수 있습니다.
```csharp
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var euckr = System.Text.Encoding.GetEncoding(949);
string text = File.ReadAllText("old.txt", euckr);
```

## 주의할 점
- 상대 경로("memo.txt")는 **현재 작업 폴더** 기준이라 실행 방법에 따라 달라집니다. `AppContext.BaseDirectory`를 기준으로 쓰세요.
- 경로는 문자열 `+`가 아니라 `Path.Combine`으로 합치세요.
