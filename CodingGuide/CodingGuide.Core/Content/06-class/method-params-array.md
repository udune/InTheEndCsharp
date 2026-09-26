---
id: method-params-array
title: 가변 매개변수 params
category: 클래스
order: 608
summary: 개수가 정해지지 않은 인자를 받는 params 키워드를 설명합니다.
keywords: params, 가변 매개변수, 가변 인자, 개수가 정해지지 않은, 여러 개 인자, variadic
lesson: 메서드_가변매개변수
related: method-optional-params, method-params, array-declaration
---
## 핵심
`params`를 붙이면 인자를 **쉼표로 나열**해서 넘길 수 있고, 메서드 안에서는 배열로 받습니다.

```csharp
public void ShowInfo(params string[] options)
{
    foreach (string option in options)
        Console.WriteLine(option);
}

car.ShowInfo("brand", "model", "color"); // 3개
car.ShowInfo("brand");                    // 1개
car.ShowInfo();                           // 0개 (빈 배열)
car.ShowInfo(new[] { "a", "b" });         // 배열을 직접 넘겨도 됨

int Sum(params int[] numbers) => numbers.Sum();
Console.WriteLine(Sum(1, 2, 3, 4));       // 10
```

## 규칙
- `params`는 메서드에 **하나만**, **마지막 매개변수**로만 쓸 수 있습니다.
- C# 13부터는 배열뿐 아니라 `params List<T>`, `params ReadOnlySpan<T>` 등도 가능합니다.
- `Console.WriteLine("{0} {1}", a, b)`, `string.Format`, `string.Join` 등이 params를 씁니다.
