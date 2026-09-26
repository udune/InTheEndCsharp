using System.Reflection;
using System.Runtime.ExceptionServices;
using CodingGuide.Core.Knowledge;

namespace CodingGuide.App.Running;

/// <summary>
/// 학습 예제 프로젝트(InTheEndCsharp)의 <c>실행()</c> 메서드를 리플렉션으로 찾아 호출한다.
/// 예제는 Thread.Sleep, .Wait() 등으로 오래 막힐 수 있으므로 전용 스레드에서 실행한다.
/// </summary>
internal sealed class LessonRunner
{
    private readonly IReadOnlyDictionary<string, MethodInfo> _lessons =
        LessonLocator.Find(typeof(InTheEndCsharp.테스트코드작성.Calculator).Assembly);

    public int Count => _lessons.Count;

    public bool Exists(string lesson) => _lessons.ContainsKey(lesson);

    public Task RunAsync(string lesson)
    {
        var method = _lessons[lesson];
        return Task.Factory.StartNew(() =>
        {
            try
            {
                method.Invoke(null, null);
            }
            catch (TargetInvocationException ex) when (ex.InnerException is not null)
            {
                // 리플렉션 호출이 감싼 예외를 벗겨서 원래 예외를 그대로 전달한다.
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
            }
        }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }
}
