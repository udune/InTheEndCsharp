# 오프라인 PC 배포용 빌드 스크립트
# .NET 런타임까지 포함(self-contained)하므로 대상 PC에 .NET을 설치할 필요가 없다.
# 결과 폴더(또는 zip)를 USB로 옮겨 CodingGuide.App.exe를 실행하면 된다.
#
# 단일 exe(PublishSingleFile)로 만들지 않는 이유:
#   리플렉션 예제가 InTheEndCsharp.dll 파일을 직접 읽고(Assembly.LoadFrom),
#   DI 예제가 appsettings.json 파일을 읽기 때문에 파일들이 폴더에 그대로 있어야 한다.

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
$out = Join-Path $root 'publish\CodingGuide'

Write-Host '1/4 테스트 실행 (문서 무결성 + 검색 품질)'
dotnet test (Join-Path $root 'CodingGuide.Tests\CodingGuide.Tests.csproj')
if ($LASTEXITCODE -ne 0) { throw '테스트 실패 - 배포를 중단합니다.' }

Write-Host '2/4 게시 (win-x64, self-contained)'
if (Test-Path $out) { Remove-Item $out -Recurse -Force }
dotnet publish (Join-Path $root 'CodingGuide.App\CodingGuide.App.csproj') -c Release -r win-x64 --self-contained true -o $out
if ($LASTEXITCODE -ne 0) { throw '게시 실패' }

Write-Host '3/4 게시본 자체 점검 (모든 문서 렌더링)'
$p = Start-Process (Join-Path $out 'CodingGuide.App.exe') -ArgumentList '--selftest' -PassThru -Wait
Get-Content (Join-Path $out 'selftest.txt')
Remove-Item (Join-Path $out 'selftest.txt')
if ($p.ExitCode -ne 0) { throw '렌더링 점검 실패' }

Write-Host '4/4 압축 (AI 모델 제외)'
$zip = Join-Path $root 'publish\CodingGuide-win-x64.zip'
if (Test-Path $zip) { Remove-Item $zip }
Compress-Archive -Path "$out\*" -DestinationPath $zip
$size = [math]::Round((Get-Item $zip).Length / 1MB, 1)
Write-Host "완료: $zip ($size MB)"

# 로컬 AI 모델(수 GB)은 zip 에 넣지 않는다(Compress-Archive 는 2GB 넘는 파일을 처리하지 못함).
# 게시 폴더에는 복사해 두므로, 폴더째 USB 로 옮기면 AI 까지 바로 동작한다.
$models = Get-ChildItem (Join-Path $root 'models') -Filter *.gguf -ErrorAction SilentlyContinue
if ($models) {
    New-Item -ItemType Directory -Force (Join-Path $out 'models') | Out-Null
    $models | Copy-Item -Destination (Join-Path $out 'models')
    Write-Host "AI 모델 복사: $out\models ($($models.Name -join ', '))"
    Write-Host "  → AI 까지 쓰려면 zip 대신 '$out' 폴더 전체를 옮기거나, zip 을 푼 뒤 models 폴더를 따로 복사하세요."
} else {
    Write-Host 'AI 모델 없음: CodingGuide\models 에 .gguf 파일을 넣으면 AI 답변 기능이 켜집니다.'
}
