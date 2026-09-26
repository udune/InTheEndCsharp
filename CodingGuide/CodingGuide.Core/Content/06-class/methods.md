---
id: methods
title: 메서드 정의와 호출 (반환값, void)
category: 클래스
order: 604
summary: 클래스의 동작을 정의하는 메서드, 반환 타입과 return, void, 식 본문(=>) 메서드를 설명합니다.
keywords: 메서드, method, 함수, function, 호출, 반환, return, void, 반환값, 리턴, 식 본문, =>, 로컬 함수, local function
lesson: 메서드
related: method-params, method-optional-params, method-params-array, method-ref, method-out, lambda-expression
---
## 핵심
```csharp
class Car
{
    private string brand = "현대";

    // 반환값이 없는 메서드
    public void ShowInfo()
    {
        Console.WriteLine($"브랜드는 {brand}입니다.");
    }

    // 반환값이 있는 메서드
    public string GetBrand()
    {
        return brand;
    }

    // 한 줄이면 식 본문(=>)으로 짧게
    public string GetBrand2() => brand;
}

Car car = new Car();
car.ShowInfo();
string brand = car.GetBrand();
```

## 메서드 형태
`접근제어자 반환타입 이름(매개변수들) { 본문 }`
- `void`: 돌려주는 값 없음
- 반환 타입이 있으면 모든 경로에서 `return 값;`이 있어야 합니다. (없으면 CS0161 오류)

## 로컬 함수
메서드 **안에** 메서드를 만들 수 있습니다. 이 저장소의 예제들이 많이 쓰는 방식입니다.
```csharp
static void Run()
{
    int Add(int a, int b) => a + b;   // Run 안에서만 사용 가능
    Console.WriteLine(Add(1, 2));
}
```

## 메서드 오버로딩
이름이 같아도 **매개변수 타입/개수**가 다르면 여러 개 만들 수 있습니다.
```csharp
int Sum(int a, int b) => a + b;
double Sum(double a, double b) => a + b;
int Sum(int a, int b, int c) => a + b + c;
```
