# C# 오프라인 코딩 가이드

인터넷 없이 C# 개념, 실무 레시피, 오류 해결법을 검색하고 학습 예제를 바로 실행해 보는 WPF 프로그램입니다.

## 구성
| 프로젝트 | 역할 |
|---|---|
| `CodingGuide.Core` | 지식 문서(`Content/**/*.md`), 동의어 사전, 검색 엔진(BM25 + 한글 바이그램 + 동의어 + 오타 보정) |
| `CodingGuide.App` | WPF 화면, 마크다운 렌더링, 학습 예제(`InTheEndCsharp`) 실행 |
| `CodingGuide.Tests` | 문서 무결성(링크, 예제 누락) + 검색 품질 회귀 테스트 |

## 배포 (오프라인 PC)
```
powershell -File CodingGuide\publish.ps1
```
`publish\CodingGuide-win-x64.zip`을 옮겨 압축을 풀고 `CodingGuide.App.exe`를 실행합니다. .NET 설치가 필요 없습니다.

## 문서 추가하기
`CodingGuide.Core/Content/<분류폴더>/<id>.md` 파일을 만듭니다.
```
---
id: recipe-something            # 고유 id (영문 kebab-case)
title: 제목
category: 실무 레시피            # 목차의 분류 이름
order: 2699                      # 목차 정렬 순서
summary: 한두 문장 요약 (검색 결과에 표시)
keywords: 사람들이 검색할 표현들, 영어 키워드, 오류 메시지 원문
lesson: 예제클래스이름            # (선택) 실행 버튼과 예제 코드 탭에 연결
source: 폴더/파일.cs              # (선택) 추가로 보여줄 소스
related: other-id, another-id    # (선택) 관련 문서
runnable: false                  # (선택) 키보드 입력이 필요한 예제
---
## 본문 (마크다운)
```
- 새 학습 예제(`public static void 실행()`)를 추가하면 문서가 없을 때 테스트가 실패해서 알려줍니다.
- 검색이 잘 안 되는 표현은 `keywords`나 `Content/synonyms.txt`에 추가하고, `SearchQualityTests`에 질문을 한 줄 추가해 두세요.
- 확인: `dotnet test CodingGuide/CodingGuide.Tests`
