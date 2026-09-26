---
id: error-cs0103
title: CS0103 - 이름이 현재 컨텍스트에 없습니다
category: 컴파일 오류
order: 2801
summary: 선언하지 않았거나, 범위(중괄호) 밖에서 쓰거나, 오타가 난 변수/메서드 이름을 사용할 때 나는 컴파일 오류입니다.
keywords: CS0103, 이름이 현재 컨텍스트에 없습니다, The name does not exist in the current context, 변수를 찾을 수 없음, 선언 안 함, 범위, 스코프, scope, 오타, 대소문자
related: error-cs0246, error-cs1061, methods, loops
---
## 메시지
- `error CS0103: 'undefinedVar' 이름이 현재 컨텍스트에 없습니다.`
- The name 'undefinedVar' does not exist in the current context

## 원인과 해결
1. **선언하지 않음 / 오타 / 대소문자 차이** — C#은 대소문자를 구분합니다. `count`와 `Count`는 다른 이름입니다.
2. **범위(스코프) 밖에서 사용** — 변수는 선언된 `{ }` 안에서만 보입니다.
```csharp
if (ok)
{
    int result = 10;
}
Console.WriteLine(result);   // CS0103: if 블록 밖

// 해결: 바깥에서 선언
int result = 0;
if (ok) result = 10;
Console.WriteLine(result);
```
```csharp
for (int i = 0; i < 3; i++) { }
Console.WriteLine(i);        // CS0103: i 는 for 안에서만 존재
```
3. **using/static 누락** — `Math` 없이 `Max(1, 2)`를 쓰려면 `using static System.Math;`가 필요합니다.
4. **다른 클래스의 멤버** — `클래스명.멤버` 또는 객체를 통해 접근해야 합니다.
5. **WinForms/WPF 컨트롤 이름** — 디자이너의 `Name`(x:Name)과 코드의 이름이 다른지 확인합니다.
