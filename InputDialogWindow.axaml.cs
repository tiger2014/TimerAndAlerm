using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace TimerAndAlerm;

public partial class InputDialogWindow : Window
{
    public InputDialogWindow() => InitializeComponent();

    public InputDialogWindow(string prompt) : this()
    {
        PromptLabel.Text = prompt;
    }

    private void OnOk(object? sender, RoutedEventArgs e)
    {
        // ShowDialog<string?> 通过 Close(result) 返回结果
        Close(InputBox.Text ?? string.Empty);
    }

    private void OnCancel(object? sender, RoutedEventArgs e) => Close(null);

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        // 兜底快捷键；IsDefault/IsCancel 已经处理了大部分情况
        if (e.Key == Key.Enter) OnOk(sender, new RoutedEventArgs());
        else if (e.Key == Key.Escape) OnCancel(sender, new RoutedEventArgs());
    }
}
