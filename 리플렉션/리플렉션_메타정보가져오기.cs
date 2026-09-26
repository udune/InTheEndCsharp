using System.Diagnostics;
using System.Reflection;

namespace InTheEndCsharp.리플렉션;

public class 리플렉션_메타정보가져오기
{
    public static void 실행()
    {
        string dllFilePath = Path.Combine(Environment.CurrentDirectory, "InTheEndCsharp.dll");
        Assembly assembly = Assembly.LoadFrom(dllFilePath);
        Console.WriteLine($"Version: {assembly.GetName().Version}");
        Console.WriteLine($"FileVersion: {GetFileVersion()}");

        string GetFileVersion()
        {
            FileVersionInfo? fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo?.FileVersion!;
        }
    }
}