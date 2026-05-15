using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace TimerAndAlerm;

public partial class FullScreenMessageWindow : Window
{
    public FullScreenMessageWindow() => InitializeComponent();

    public FullScreenMessageWindow(string message) : this()
    {
        MessageText.Text = message;
    }

    private void OnCloseClick(object? sender, RoutedEventArgs e) => Close();

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape) Close();
    }
}
