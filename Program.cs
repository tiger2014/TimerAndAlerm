using Avalonia;
using TimerAndAlerm.Audio;

namespace TimerAndAlerm;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        // LibVLC native 库必须在创建任何 player 实例之前完成初始化
        AudioPlayerHost.EnsureInitialized();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
