using System.IO;
using System.Text;

namespace CodingGuide.App.Running;

/// <summary>
/// Console.Out/Console.Error를 대신 받아 버퍼에 모아 두는 TextWriter.
/// 예제는 여러 스레드에서 Console.WriteLine을 호출하므로 잠금으로 보호하고,
/// 화면은 타이머로 주기적으로 Drain()해서 가져간다(출력이 많아도 UI가 멈추지 않도록).
/// 예제 실행이 끝난 뒤 늦게 도착하는 출력(스레드 풀, 소멸자 등)도 그대로 표시된다.
/// </summary>
internal sealed class ConsoleRouter : TextWriter
{
    private readonly StringBuilder _pending = new();
    private readonly Lock _lock = new();

    public override Encoding Encoding => Encoding.UTF8;

    public override void Write(char value)
    {
        lock (_lock) _pending.Append(value);
    }

    public override void Write(string? value)
    {
        lock (_lock) _pending.Append(value);
    }

    public override void WriteLine(string? value)
    {
        lock (_lock) _pending.Append(value).Append('\n');
    }

    public override void WriteLine()
    {
        lock (_lock) _pending.Append('\n');
    }

    public string Drain()
    {
        lock (_lock)
        {
            if (_pending.Length == 0) return "";
            string text = _pending.ToString();
            _pending.Clear();
            return text;
        }
    }
}
