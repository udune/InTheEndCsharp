using System.Windows;
using System.Windows.Threading;

namespace CodingGuide.App;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        // 예제 중에는 현재 작업 폴더 기준으로 파일(InTheEndCsharp.dll, log.txt)을 찾는 것이 있다.
        // 바로가기 등으로 실행해도 동작하도록 실행 파일 폴더로 맞춘다.
        Environment.CurrentDirectory = AppContext.BaseDirectory;
        DispatcherUnhandledException += OnUnhandledException;
        base.OnStartup(e);
    }

    private static void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show(e.Exception.ToString(), "예상하지 못한 오류", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
    }
}
