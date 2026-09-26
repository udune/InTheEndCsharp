# InTheEndCsharp

C# 기초 문법부터 비동기, 리플렉션, 의존성 주입, API 통신, 테스트까지 주제별로 정리한 C# 학습 저장소입니다.
학습 내용을 오프라인에서 검색하고 예제를 바로 실행해 볼 수 있는 **WPF 코딩 가이드**(로컬 LLM 질문 기능 포함)도 함께 들어 있습니다.

- 대상 프레임워크: **.NET 10** (`net10.0`, WinForms/WPF 프로젝트는 `net10.0-windows`)
- 테스트: xUnit v3 + Microsoft.Testing.Platform (`global.json`)

## 프로젝트 구성

| 프로젝트 | 종류 | 설명 |
|---|---|---|
| `InTheEndCsharp` | 콘솔 | 주제별 학습 예제 모음 (루트 폴더의 한글 폴더들) |
| `AsyncUI` | WinForms | UI 스레드와 `async`/`await` 동작 비교 예제 |
| `APIBackend` | ASP.NET Core Web API | `/users` 사용자 API (Swagger UI 제공) |
| `APIApp` | WinForms | `HttpClient`로 `APIBackend`를 호출하는 클라이언트 |
| `Tests` | xUnit | 학습용 단위 테스트 (`Calculator`) |
| `CodingGuide` | WPF + 라이브러리 + 테스트 | 오프라인 C# 코딩 가이드 ([자세히](CodingGuide/README.md)) |

## 학습 주제

루트의 각 폴더가 하나의 주제이며, 파일 하나가 `public static void 실행()` 메서드를 가진 예제 하나입니다.

| 순서 | 폴더 | 내용 |
|---|---|---|
| 1 | `값타입및선언` | 정수/실수/bool/char/enum, 명시적·암시적 선언 |
| 2 | `연산자` | 산술, 비교, 할당, 논리, 비트, Null 병합 연산자 |
| 3 | `조건문` | if, 삼항 연산자, switch |
| 4 | `배열` | 선언/초기화, 요소 접근, 반복문, 다차원 배열·Array 클래스 |
| 5 | `문자열` | 문자열 기초 |
| 6 | `클래스` | 메서드, 매개변수(ref/out/params), 생성자, 속성, static, 확장 메서드, 상속, 추상/sealed |
| 7 | `인터페이스` | 다중 구현, 명시적 구현, 디폴트 구현 |
| 8 | `타입캐스팅` | 명시적 변환, 박싱/언박싱, Convert, is/as |
| 9 | `구조체` | struct와 class 비교 |
| 10 | `제네릭` | 제네릭 기초, 제약 조건, 제네릭 클래스 |
| 11 | `델리게이트` | 멀티캐스트, 이벤트, Func/Action/Predicate/Comparison |
| 12 | `람다` | 람다 표현식 |
| 13 | `열거및컬렉션` | IEnumerator/IEnumerable, List, Dictionary, Queue, Stack |
| 14 | `린큐` | 쿼리 구문과 메서드 구문 (select, where, orderby, group, join 등) |
| 15 | `예외처리` | try/catch/finally, 예외 객체, 사용자 정의 예외 |
| 16 | `스레드` | 스레드 생성, 경쟁 상태, lock/Monitor/Mutex/Semaphore, 스레드 풀 |
| 17 | `비동기` | async/await, UI가 없는 환경의 진행 흐름 (+ `AsyncUI`) |
| 18 | `네임스페이스` | 네임스페이스와 어셈블리(DLL) 참조 |
| 19 | `리플렉션` | 메타 정보 조회, 동적 인스턴스 생성·속성·필드·메서드 호출 |
| 20 | `어트리뷰트` | Obsolete/Conditional, 커스텀 어트리뷰트, Castle.Core 프록시로 구현한 AOP |
| 21 | `의존성주입` | 수동 주입, DI 컨테이너와 생명주기, 제네릭 서비스 등록, `appsettings.json` 바인딩 |
| 22 | `APIBackend` / `APIApp` | Web API 서버와 WinForms 클라이언트 통신 |
| 23 | `테스트코드작성` / `Tests` | 테스트 코드의 중요성, xUnit 단위 테스트 |

## 실행 방법

### 콘솔 학습 예제

`Program.cs`의 `practiceArray`에서 실행할 예제의 주석을 풀고 실행합니다.

```csharp
Action[] practiceArray =
[
    // 정수형타입.실행(),
    람다표현식.실행,
];
```

```
dotnet run --project InTheEndCsharp.csproj
```

> 루트 콘솔 프로젝트는 하위 프로젝트 폴더(`AsyncUI`, `APIApp`, `APIBackend`, `Tests`, `CodingGuide`)를 빌드 대상에서 제외합니다.
> 새 하위 프로젝트를 추가하면 `InTheEndCsharp.csproj`의 `SubProjectDirs`에 폴더를 추가하세요.

### API 서버와 클라이언트

```
dotnet run --project APIBackend    # http://localhost:5168/swagger
dotnet run --project APIApp        # 서버가 켜진 상태에서 실행
```

| 메서드 | 경로 | 설명 |
|---|---|---|
| GET | `/users` | 사용자 목록 |
| GET | `/users/{userId}` | 사용자 단건 조회 |
| POST | `/users` | 사용자 추가 |

### 테스트

```
dotnet test Tests
dotnet test CodingGuide/CodingGuide.Tests
```

## 오프라인 코딩 가이드 (CodingGuide)

인터넷 없이 C# 개념, 실무 레시피, 오류 해결법을 검색하고 이 저장소의 학습 예제를 바로 실행해 보는 WPF 프로그램입니다.

- 마크다운 지식 문서 검색 (BM25 + 한글 바이그램 + 동의어 + 오타 보정)
- 문서와 연결된 학습 예제 코드 보기 및 실행
- GGUF 모델을 넣으면 로컬 LLM(LLamaSharp, CPU 전용)으로 문서 기반 질문 답변(RAG)

```
dotnet run --project CodingGuide/CodingGuide.App
```

모델 설치, 오프라인 PC 배포, 문서 추가 방법은 [CodingGuide/README.md](CodingGuide/README.md)를 참고하세요.

## 요구 사항

- .NET 10 SDK
- Windows (WinForms/WPF 프로젝트 실행 시)
