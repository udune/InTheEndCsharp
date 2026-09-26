---
id: error-cs0266
title: CS0029 / CS0266 / CS1503 - 암시적으로 형식을 변환할 수 없습니다
category: 컴파일 오류
order: 2803
summary: 타입이 맞지 않는 값을 변수에 넣거나 메서드 인자로 넘길 때 나는 변환 오류들(CS0029, CS0266, CS1503)의 원인과 캐스팅/파싱/괄호 해결법입니다.
keywords: CS0029, CS0266, CS1503, 암시적으로 형식을 변환할 수 없습니다, Cannot implicitly convert type, 명시적 변환이 있습니다, 캐스트가 있는지 확인하세요, An explicit conversion exists, are you missing a cast, 인수 변환할 수 없습니다, cannot convert from, 'string' 형식을 'int' 형식으로, 'double' 형식을 'int' 형식으로, 'void' 형식을 'System.Action' 형식으로, 타입 불일치
related: explicit-cast, convert-class, recipe-parse-number, delegate-action
---
## CS0266 - 명시적 변환이 있습니다
- `암시적으로 'double' 형식을 'int' 형식으로 변환할 수 없습니다. 명시적 변환이 있습니다. 캐스트가 있는지 확인하세요.`
```csharp
int b = 3.5;              // 오류
int b = (int)3.5;         // 3 (버림) — 캐스팅
int r = (int)Math.Round(3.5);   // 4 — 반올림이 필요하면
long big = 10; int small = (int)big;
```

## CS0029 - 변환 자체가 불가능
- `암시적으로 'string' 형식을 'int' 형식으로 변환할 수 없습니다.`
```csharp
int a = "text";                 // 오류: 문자열은 캐스팅으로 숫자가 되지 않음
int a = int.Parse("123");       // 파싱
int.TryParse(input, out int a2);
string s = 123.ToString();      // 반대 방향
```

### 괄호를 붙여서 나는 CS0029
- `암시적으로 'void' 형식을 'System.Action' 형식으로 변환할 수 없습니다.`
```csharp
Action[] arr = [Hello()];   // 오류: Hello() 는 "실행 결과(void)"
Action[] arr = [Hello];     // 메서드 자체를 넣으려면 괄호 없이
```
이 저장소 `Program.cs`의 앞부분 주석(`정수형타입.실행(),` 등)을 그대로 풀면 이 오류가 납니다. `정수형타입.실행,`으로 고쳐야 합니다.

## CS1503 - 인수 변환 불가
- `2 인수: 'string'에서 'int'(으)로 변환할 수 없습니다.`
```csharp
void Take(int a, int b) { }
Take(1, "x");     // 두 번째 인수 타입이 틀림
```
→ 메서드의 매개변수 타입과 넘기는 값의 타입을 맞추세요. 몇 번째 인수인지 메시지에 나와 있습니다.

## 그 밖의 흔한 경우
```csharp
List<int> list = new int[] { 1, 2 };        // 오류 → .ToList() 또는 [1, 2]
IEnumerable<int> q = ...; List<int> l = q;  // 오류 → q.ToList()
int? n = 5; int m = n;                      // 오류 → n ?? 0 또는 n.Value
Task<int> t = GetAsync(); int v = t;        // 오류 → await GetAsync()
```
