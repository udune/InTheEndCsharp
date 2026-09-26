using System.IO;
using System.Runtime.CompilerServices;
using LLama;
using LLama.Common;
using LLama.Native;
using LLama.Sampling;
using LLama.Transformers;

namespace CodingGuide.App.Ai;

/// <summary>
/// PC 안에서 직접 돌아가는 LLM(GGUF 모델 + llama.cpp).
/// 인터넷을 쓰지 않는다. 모델 파일은 실행 폴더의 models\*.gguf 에 둔다.
///
/// CPU 전용 백엔드(LLamaSharp.Backend.Cpu)를 쓰므로 GPU가 없는 PC에서도 동작한다.
/// 7B 모델(Q4_K_M, 약 4.7GB) 기준으로 RAM 8GB 이상이 필요하다.
/// </summary>
internal sealed class LocalLlm : IDisposable
{
    const int ContextSize = 8192;
    const int MaxAnswerTokens = 1024;

    private LLamaWeights? _weights;
    private ModelParams? _params;

    public string? ModelPath { get; private set; }
    public string ModelName => ModelPath is null ? "" : Path.GetFileNameWithoutExtension(ModelPath);
    public bool IsLoaded => _weights is not null;

    /// <summary>
    /// 모델 파일을 찾는다. 배포본은 실행 폴더의 models, 개발 중에는 상위 폴더들의 models 도 찾아본다.
    /// 여러 개면 가장 큰 파일(보통 가장 성능이 좋은 모델)을 쓴다.
    /// </summary>
    public static string? FindModel()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        for (int depth = 0; dir is not null && depth < 6; depth++, dir = dir.Parent)
        {
            var models = new DirectoryInfo(Path.Combine(dir.FullName, "models"));
            if (!models.Exists) continue;
            var best = models.EnumerateFiles("*.gguf").OrderByDescending(f => f.Length).FirstOrDefault();
            if (best is not null) return best.FullName;
        }
        return null;
    }

    public async Task LoadAsync(string modelPath, IProgress<float>? progress = null)
    {
        // llama.cpp 내부 로그가 Console 로 나가면 예제 실행 결과 창에 섞이므로 끈다. (첫 네이티브 호출 전에 설정해야 함)
        NativeLibraryConfig.All.WithLogCallback((_, _) => { });

        _params = new ModelParams(modelPath)
        {
            ContextSize = ContextSize,
            GpuLayerCount = 0,                                          // CPU 전용
            Threads = Math.Max(1, Environment.ProcessorCount / 2),       // 답변 생성: 물리 코어 수 정도가 가장 빠름
            BatchThreads = Environment.ProcessorCount,                   // 프롬프트 읽기: 논리 코어 전부 (측정상 약 35% 빨라짐)
            BatchSize = 512,
        };
        _weights = await LLamaWeights.LoadFromFileAsync(_params, CancellationToken.None, progress);
        ModelPath = modelPath;
    }

    /// <summary>답변을 조각(토큰) 단위로 흘려보낸다.</summary>
    public async IAsyncEnumerable<string> AskAsync(string systemPrompt, string userPrompt,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (_weights is null || _params is null)
            throw new InvalidOperationException("모델이 아직 로드되지 않았습니다.");

        // 모델 파일에 들어 있는 대화 템플릿(Qwen 은 <|im_start|> 형식)으로 프롬프트를 만든다.
        var template = new LLamaTemplate(_weights) { AddAssistant = true };
        template.Add("system", systemPrompt);
        template.Add("user", userPrompt);
        string prompt = PromptTemplateTransformer.ToModelPrompt(template);

        var executor = new StatelessExecutor(_weights, _params);
        var inference = new InferenceParams
        {
            MaxTokens = MaxAnswerTokens,
            AntiPrompts = ["<|im_end|>", "<|endoftext|>"],
            SamplingPipeline = new DefaultSamplingPipeline
            {
                Temperature = 0.3f,       // 낮을수록 사실 위주로 일관되게 답함
                TopP = 0.9f,
                RepeatPenalty = 1.1f,
            },
        };

        await foreach (var piece in executor.InferAsync(prompt, inference, cancellationToken))
        {
            // 멈춤 신호가 텍스트로 섞여 나오는 경우를 걸러낸다.
            if (piece.Contains("<|im_end|>") || piece.Contains("<|endoftext|>"))
            {
                string cut = piece.Split("<|im_end|>")[0].Split("<|endoftext|>")[0];
                if (cut.Length > 0) yield return cut;
                yield break;
            }
            yield return piece;
        }
    }

    public void Dispose() => _weights?.Dispose();
}
