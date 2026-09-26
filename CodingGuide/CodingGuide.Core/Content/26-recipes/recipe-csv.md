---
id: recipe-csv
title: CSV 파일 읽고 쓰기
category: 실무 레시피
order: 2608
summary: 쉼표로 구분된 CSV 파일을 줄 단위로 읽어 객체 리스트로 만들고, 리스트를 CSV로 저장하는 간단한 방법과 엑셀 한글 깨짐 해결입니다.
keywords: CSV, csv, 엑셀, excel, 쉼표 구분, 표 데이터, 파일 읽기, 파일 저장, 내보내기, export, 가져오기, import, BOM, 엑셀 한글 깨짐, Split
related: recipe-file-io, linq-method-select, recipe-parse-number
---
## 읽기
```csharp
record Product(string Name, int Price, int Stock);

List<Product> products = File.ReadLines("products.csv")
    .Skip(1)                                             // 헤더 줄 건너뛰기
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Select(line => line.Split(','))
    .Select(c => new Product(c[0].Trim(), int.Parse(c[1]), int.Parse(c[2])))
    .ToList();
```

## 쓰기
```csharp
var lines = new List<string> { "Name,Price,Stock" };
lines.AddRange(products.Select(p => $"{p.Name},{p.Price},{p.Stock}"));

// 엑셀에서 한글이 깨지지 않게 UTF-8 BOM 포함
File.WriteAllLines("out.csv", lines, new System.Text.UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
```

## 값에 쉼표나 따옴표가 있을 때
`"서울, 종로구"`처럼 따옴표로 감싸진 값은 단순 `Split(',')`로는 잘못 나뉩니다.
```csharp
string Escape(string v) =>
    v.Contains(',') || v.Contains('"') || v.Contains('\n')
        ? $"\"{v.Replace("\"", "\"\"")}\""
        : v;
```
읽기도 제대로 하려면 `Microsoft.VisualBasic.FileIO.TextFieldParser`(기본 포함)나 CsvHelper 라이브러리를 씁니다.
```csharp
using Microsoft.VisualBasic.FileIO;
using var parser = new TextFieldParser("data.csv") { TextFieldType = FieldType.Delimited, HasFieldsEnclosedInQuotes = true };
parser.SetDelimiters(",");
while (!parser.EndOfData)
{
    string[] fields = parser.ReadFields()!;
}
```
