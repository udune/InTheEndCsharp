---
id: error-invalid-cast
title: InvalidCastException - 지정된 캐스트가 잘못되었습니다
category: 흔한 예외
order: 2703
summary: 실제 타입과 다른 타입으로 강제 형변환하거나, 박싱된 값을 다른 타입으로 언박싱할 때 나는 예외와 해결(is/as, Convert)입니다.
keywords: InvalidCastException, 지정된 캐스트가 잘못되었습니다, Specified cast is not valid, Unable to cast object of type, 형식 개체를 형식으로 캐스팅할 수 없습니다, 캐스팅 오류, 형변환 오류, 언박싱 오류, object 에서 int
related: explicit-cast, as-operator, is-operator, boxing-unboxing
---
## 메시지
- 지정된 캐스트가 잘못되었습니다. / Specified cast is not valid.
- 'System.String' 형식 개체를 'System.Int32' 형식으로 캐스팅할 수 없습니다. / Unable to cast object of type 'System.String' to type 'System.Int32'.

## 원인
```csharp
object obj = "abc";
double d = (double)obj;         // ← 예외! 문자열을 double 로 "캐스팅" 할 수 없음

object boxed = 123;             // int 로 박싱
long l = (long)boxed;           // ← 예외! 언박싱은 정확히 같은 타입(int)으로만

Animal a = new Cat();
Dog d2 = (Dog)a;                // ← 예외! 실제 객체는 Cat

object cell = dataRow["Age"];   // DB 값이 long/decimal 일 수 있음
int age = (int)cell;            // ← 예외!
```

## 해결
```csharp
if (obj is double value) { }                  // 안전하게 검사 + 변환
Dog? dog = a as Dog;                          // 실패하면 null

long ok = (int)boxed;                         // 원래 타입으로 꺼낸 뒤 변환
int age = Convert.ToInt32(cell);              // 숫자 타입 사이 변환은 Convert
double.TryParse(obj.ToString(), out var d3);  // 문자열 → 숫자는 "캐스팅"이 아니라 "파싱"
```

## 기억할 것
- **캐스팅**은 "이미 그 타입인 것을 그 타입으로 보는 것"입니다.
- 문자열 "123"을 숫자 123으로 바꾸는 것은 캐스팅이 아니라 **Parse/Convert**입니다.
