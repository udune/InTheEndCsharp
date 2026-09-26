---
id: error-cs0122
title: CS0122 - 보호 수준 때문에 액세스할 수 없습니다
category: 컴파일 오류
order: 2811
summary: private/protected/internal 멤버를 허용되지 않은 곳에서 사용할 때 나는 오류와, 접근 제어자를 여는 것보다 좋은 해결 방법입니다.
keywords: CS0122, 보호 수준 때문에 액세스할 수 없습니다, is inaccessible due to its protection level, private 접근, 접근 불가, 접근 제어자 오류, internal, protected
related: access-modifiers, properties, assembly-dll
---
## 메시지
- `보호 수준 때문에 'Hidden.secret'에 액세스할 수 없습니다.`
- 'Hidden.secret' is inaccessible due to its protection level

## 원인
```csharp
class Hidden { private int secret; }     // 아무것도 안 쓰면 멤버는 private
var x = new Hidden().secret;             // CS0122
```

## 해결 (좋은 순서대로)
1. 값을 읽기만 하면 된다면 **읽기 전용 속성**으로 공개
```csharp
class Hidden
{
    private int secret;
    public int Secret => secret;
}
```
2. 바꿔야 한다면 **검증이 들어간 메서드/속성**을 통해서
3. 정말 외부에서 자유롭게 써야 하는 값이라면 `public` 속성으로
4. 다른 프로젝트에서 안 보인다면 클래스에 `public`이 붙어 있는지 확인 (클래스의 기본은 `internal`)
5. 테스트 프로젝트에서만 필요하면 `InternalsVisibleTo`
