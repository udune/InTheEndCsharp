---
id: error-format
title: FormatException - 입력 문자열의 형식이 잘못되었습니다
category: 흔한 예외
order: 2707
summary: int.Parse, Convert.ToInt32, DateTime.Parse에 숫자/날짜 형식이 아닌 문자열(빈 문자열, 공백, 콤마, 소수점)을 넘길 때 나는 예외와 TryParse로의 해결, string.Format 중괄호 오류입니다.
keywords: FormatException, 입력 문자열의 형식이 잘못되었습니다, Input string was not in a correct format, The input string was not in a correct format, Parse 오류, 숫자 변환 실패, 빈 문자열 변환, string.Format 중괄호, 형식 오류
related: recipe-parse-number, convert-class, method-out
---
## 메시지
- 입력 문자열의 형식이 잘못되었습니다. / Input string was not in a correct format.

## 원인
```csharp
int.Parse("abc");       // ← 예외! 숫자가 아님
int.Parse("");          // ← 예외! 빈 문자열 (텍스트박스가 비어 있을 때 흔함)
int.Parse("3.5");       // ← 예외! 정수에 소수점
int.Parse("1,000");     // ← 예외! 콤마
double.Parse("3,14");   // 문화권에 따라 ← 예외!
DateTime.Parse("2026/13/45");   // ← 예외! 없는 날짜
```

## 해결
```csharp
if (!int.TryParse(textBox.Text, out int n))
{
    MessageBox.Show("숫자를 입력하세요");
    return;
}

int.TryParse("1,000", NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out int withComma);
double.TryParse("3.14", NumberStyles.Float, CultureInfo.InvariantCulture, out double d);
```

## string.Format / 보간의 중괄호
```csharp
string.Format("{0} {1}", a);      // ← 예외! 인자 개수 부족 (FormatException)
string.Format("{ 값 }", a);        // ← 예외! 중괄호를 글자로 쓰려면 {{ }} 로
$"{{JSON}} {value}";               // 보간에서도 {{ }} 로 이스케이프
```
