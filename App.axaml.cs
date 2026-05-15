using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace TimerAndAlerm;

public partial class App : Application
{
    /// <summary>缓存的 desktop lifetime 引用，托盘事件回调要用。</summary>
    private IClassicDesktopStyleApplicationLifetime? _desktop;

    /// <summary>标记"真正退出"中，让 MainWindow.Closing 不再阻止关闭。</summary>
    private bool _forceExit;

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _desktop = desktop;
            var main = new MainWindow();
            // 关闭按钮 → 隐藏到托盘，保留 WinForms 版本的行为
            main.Closing += OnMainWindowClosing;
            desktop.MainWindow = main;
        }
        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>关闭按钮拦截：除非用户从托盘菜单选"退出"，否则只是隐藏窗体。隐藏前持久化播放列表。</summary>
    private void OnMainWindowClosing(object? sender, WindowClosingEventArgs e)
    {
        if (sender is MainWindow mw) mw.SaveMusicList();
        if (_forceExit) return;
        e.Cancel = true;
        if (sender is Window w) w.Hide();
    }

    /// <summary>托盘左键点击：恢复主窗体。</summary>
    private void OnTrayClicked(object? sender, EventArgs e) => ShowMainWindow();

    /// <summary>托盘菜单"显示"。</summary>
    private void OnShowClick(object? sender, EventArgs e) => ShowMainWindow();

    /// <summary>托盘菜单"退出"。退出前保存播放列表。</summary>
    private void OnExitClick(object? sender, EventArgs e)
    {
        if (_desktop?.MainWindow is MainWindow mw) mw.SaveMusicList();
        _forceExit = true;
        _desktop?.Shutdown();
    }

    /// <summary>显示并激活主窗体。</summary>
    private void ShowMainWindow()
    {
        if (_desktop?.MainWindow is not { } w) return;
        w.Show();
        w.WindowState = WindowState.Normal;
        w.Activate();
    }
}
