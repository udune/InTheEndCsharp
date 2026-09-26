---
id: error-cs0128
title: CS0128 / CS0136 - 같은 이름의 지역 변수가 이미 정의되어 있습니다
category: 컴파일 오류
order: 2808
summary: 한 범위 안에서 같은 이름의 변수를 두 번 선언하거나, 안쪽 블록에서 바깥 변수와 같은 이름을 쓸 때 나는 오류입니다.
keywords: CS0128, CS0136, 이미 이 범위 안에 정의되어 있습니다, 지역 변수 또는 함수가 이미 정의, A local variable named is already defined in this scope, 변수 중복 선언, 같은 이름 변수, 이름 충돌
related: error-cs0103, var-declaration
---
## 메시지
- `이름이 'd'인 지역 변수 또는 함수가 이미 이 범위 안에 정의되어 있습니다.` (CS0128)
- A local variable or function named 'd' is already defined in this scope

## 원인과 해결
```csharp
int d = 1;
int d = 2;        // CS0128: 다시 선언
d = 2;            // 해결: 값만 바꾸기

int count = 0;
for (int i = 0; i < 3; i++)
{
    int count = i;    // CS0136: 바깥 count 와 이름이 겹침
}

if (int.TryParse(a, out int n)) { }
if (int.TryParse(b, out int n)) { }   // CS0128: out 변수도 같은 범위에 선언됨 → n2 등 다른 이름
```
- switch의 case들은 **같은 범위**입니다. case마다 변수를 선언하려면 `case 1: { var x = ...; break; }`처럼 중괄호로 감싸세요.
