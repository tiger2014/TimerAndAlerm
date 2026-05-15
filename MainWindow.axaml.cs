using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TimerAndAlerm.Audio;

namespace TimerAndAlerm;

public partial class MainWindow : Window
{
    // ─── 时钟相关 ───
    private readonly DispatcherTimer _clockTimer = new() { Interval = TimeSpan.FromSeconds(1) };
    /// <summary>已触发的报警键（小时*100+分钟），防止同一分钟内重复触发。</summary>
    private int _lastAlarmKey = -1;

    // ─── 秒表相关 ───
    private readonly Stopwatch _stopwatch = new();
    private readonly DispatcherTimer _stopwatchTimer = new() { Interval = TimeSpan.FromMilliseconds(10) };

    // ─── 倒计时相关 ───
    private DispatcherTimer? _countdownTimer;
    /// <summary>倒计时剩余秒数。</summary>
    private int _countdownRemainingSeconds;
    /// <summary>倒计时总秒数（用于进度百分比换算）。</summary>
    private int _countdownTotalSeconds;
    /// <summary>用户输入的"倒计时事由"，到点时显示在全屏弹窗上。</summary>
    private string _countdownReason = "";

    // ─── 定时提醒相关 ───
    private DispatcherTimer? _scheduledTimer;
    private DateTime _scheduledTarget;

    // ─── 音频相关 ───
    /// <summary>播放列表项：[0]=文件完整路径，[1]=显示用文件名。</summary>
    private readonly List<string[]> _audioList = new();
    /// <summary>当前播放的曲目下标。</summary>
    private int _currentTrackIndex;
    /// <summary>播放列表用的 player（长播放）。</summary>
    private IAudioPlayer? _playlistPlayer;
    /// <summary>系统提示音用的 player（短播放，daojishi.mp3 等）。</summary>
    private IAudioPlayer? _notifyPlayer;
    /// <summary>"敲钟"按钮用的 player。</summary>
    private IAudioPlayer? _bellPlayer;

    public MainWindow()
    {
        InitializeComponent();

        // 时钟：每秒更新北京时间显示，检查报警时间
        _clockTimer.Tick += OnClockTick;
        _clockTimer.Start();

        // 秒表：UI 刷新计时器
        _stopwatchTimer.Tick += (_, _) => StopwatchText.Text = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.fff");

        // 加载播放列表（如果 musicList.txt 存在）
        LoadMusicList();
    }

    // ─── 时钟 ───

    private void OnClockTick(object? sender, EventArgs e)
    {
        var beijing = GetBeijingTime();
        ClockText.Text = beijing.ToString("HH:mm:ss");
        CheckBeijingAlarms(beijing);
    }

    /// <summary>跨平台获取北京时间。优先用 IANA "Asia/Shanghai"，找不到回退到 UTC+8。</summary>
    private static DateTime GetBeijingTime()
    {
        try
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById("Asia/Shanghai");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
        }
        catch
        {
            return DateTime.UtcNow.AddHours(8);
        }
    }

    /// <summary>检查北京时间是否到 5/11/17/23 点的 :54 弹窗 / :55 敲钟时刻。</summary>
    private void CheckBeijingAlarms(DateTime beijing)
    {
        if (beijing.Hour != 5 && beijing.Hour != 11 && beijing.Hour != 17 && beijing.Hour != 23)
            return;

        int currentKey = beijing.Hour * 100 + beijing.Minute;

        if (beijing.Minute == 54 && _lastAlarmKey != currentKey)
        {
            _lastAlarmKey = currentKey;
            PlayNotificationSound("daojishi.mp3");
            // 非模态全屏弹窗，避免阻塞 timer
            var win = new FullScreenMessageWindow(
                $"北京时间 {beijing:HH:mm:ss} 到了，准备发正念。当前本地时间 {DateTime.Now:HH:mm:ss}");
            win.Show();
        }
        else if (beijing.Minute == 55 && _lastAlarmKey != currentKey)
        {
            _lastAlarmKey = currentKey;
            RingBell();
        }
    }

    // ─── 敲钟按钮 ───

    private void OnBellClick(object? sender, RoutedEventArgs e) => RingBell();

    private void RingBell()
    {
        // 已经在敲了就忽略
        if (BellButton.Content?.ToString() == "聆听") return;
        BellButton.Content = "聆听";
        BellButton.IsEnabled = false;

        _bellPlayer?.Dispose();
        _bellPlayer = AudioPlayerHost.CreatePlayer();
        _bellPlayer.Volume = 0.99f;
        _bellPlayer.PlaybackEnded += (_, _) => Dispatcher.UIThread.Post(() =>
        {
            BellButton.Content = "敲钟";
            BellButton.IsEnabled = true;
        });
        _bellPlayer.Play(AssetPath("fzn15.mp3"));
    }

    // ─── 通知音 ───

    private void PlayNotificationSound(string fileName)
    {
        _notifyPlayer?.Dispose();
        _notifyPlayer = AudioPlayerHost.CreatePlayer();
        _notifyPlayer.Volume = 1.0f;
        _notifyPlayer.Play(AssetPath(fileName));
    }

    // ─── 秒表 ───

    private void OnStopwatchStart(object? sender, RoutedEventArgs e)
    {
        _stopwatch.Start();
        _stopwatchTimer.Start();
    }

    private void OnStopwatchStop(object? sender, RoutedEventArgs e)
    {
        _stopwatch.Stop();
        _stopwatchTimer.Stop();
    }

    private void OnStopwatchReset(object? sender, RoutedEventArgs e)
    {
        _stopwatchTimer.Stop();
        _stopwatch.Reset();
        StopwatchText.Text = "00:00:00.000";
    }

    // ─── 倒计时分钟数 +/- ───

    private void OnMinusClick(object? sender, RoutedEventArgs e)
    {
        int m = ParseMinutes();
        m = Math.Max(0, m - 1);
        MinutesBox.Text = m.ToString();
    }

    private void OnPlusClick(object? sender, RoutedEventArgs e)
    {
        int m = ParseMinutes();
        MinutesBox.Text = (m + 1).ToString();
    }

    private int ParseMinutes()
    {
        return int.TryParse(MinutesBox.Text, out var m) ? Math.Max(0, m) : 0;
    }

    // ─── 倒计时 Start/Cancel ───

    private async void OnCountdownStart(object? sender, RoutedEventArgs e)
    {
        int minutes = ParseMinutes();

        // 弹输入对话框拿"事由"
        var dialog = new InputDialogWindow("请输入倒计时事由");
        var reason = await dialog.ShowDialog<string?>(this);
        if (reason is null) return;  // 用户取消
        _countdownReason = reason;

        // 先把已有倒计时停掉
        _countdownTimer?.Stop();
        _countdownTimer = null;

        _countdownTotalSeconds = minutes * 60;
        _countdownRemainingSeconds = _countdownTotalSeconds;
        UpdateCountdownProgress();

        _countdownTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _countdownTimer.Tick += OnCountdownTick;
        _countdownTimer.Start();
    }

    private void OnCountdownTick(object? sender, EventArgs e)
    {
        _countdownRemainingSeconds = Math.Max(0, _countdownRemainingSeconds - 1);
        UpdateCountdownProgress();

        if (_countdownRemainingSeconds <= 0)
        {
            _countdownTimer?.Stop();
            _countdownTimer = null;
            PlayNotificationSound("daojishi.mp3");
            var win = new FullScreenMessageWindow($"{_countdownTotalSeconds / 60} 分钟到了！请 '{_countdownReason}'");
            win.Show();
        }
    }

    private void UpdateCountdownProgress()
    {
        if (_countdownTotalSeconds <= 0)
        {
            CountdownProgress.Value = 0;
            return;
        }
        CountdownProgress.Value = (double)(_countdownTotalSeconds - _countdownRemainingSeconds) / _countdownTotalSeconds * 100.0;
    }

    private void OnCountdownCancel(object? sender, RoutedEventArgs e)
    {
        _countdownTimer?.Stop();
        _countdownTimer = null;
        _countdownRemainingSeconds = 0;
        _countdownTotalSeconds = 0;
        CountdownProgress.Value = 0;
        MinutesBox.Text = "";
    }

    // ─── 定时提醒 Start/Cancel ───

    private void OnScheduleClick(object? sender, RoutedEventArgs e)
    {
        // 已经在跑 → 取消
        if (ScheduleButton.Content?.ToString() == "Cancel")
        {
            _scheduledTimer?.Stop();
            _scheduledTimer = null;
            ScheduleButton.Content = "Start";
            HourInput.IsEnabled = true;
            MinuteInput.IsEnabled = true;
            return;
        }

        int hour = (int)(HourInput.Value ?? 0);
        int minute = (int)(MinuteInput.Value ?? 0);
        var now = DateTime.Now;
        _scheduledTarget = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
        if (_scheduledTarget <= now) _scheduledTarget = _scheduledTarget.AddDays(1);

        ScheduleButton.Content = "Cancel";
        HourInput.IsEnabled = false;
        MinuteInput.IsEnabled = false;

        _scheduledTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _scheduledTimer.Tick += OnScheduledTick;
        _scheduledTimer.Start();
    }

    private void OnScheduledTick(object? sender, EventArgs e)
    {
        if (DateTime.Now < _scheduledTarget) return;

        _scheduledTimer?.Stop();
        _scheduledTimer = null;
        ScheduleButton.Content = "Start";
        HourInput.IsEnabled = true;
        MinuteInput.IsEnabled = true;

        PlayNotificationSound("daojishi.mp3");
        var win = new FullScreenMessageWindow($"定时提醒：{DateTime.Now:HH:mm} 到了！");
        win.Show();
    }

    // ─── 音频播放列表 ───

    /// <summary>
    /// 用户播放列表的持久化路径。
    /// Win：%AppData%\TimerAndAlerm\musicList.txt
    /// Mac：~/Library/Application Support/TimerAndAlerm/musicList.txt
    /// </summary>
    private static readonly string MusicListPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "TimerAndAlerm",
        "musicList.txt");

    /// <summary>启动时从 AppData 加载播放列表（文件不存在则忽略）。</summary>
    private void LoadMusicList()
    {
        if (!File.Exists(MusicListPath)) return;

        try
        {
            var lines = File.ReadAllLines(MusicListPath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var trimmed = line.Trim();
                // 文件可能已被用户删除，加载时不剔除，下次保存时由用户决定是否清理
                _audioList.Add(new[] { trimmed, Path.GetFileName(trimmed) });
            }
            RefreshAudioListUi();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"加载播放列表失败：{ex.Message}");
        }
    }

    /// <summary>把当前播放列表持久化到 AppData。退出和隐藏到托盘前调用。</summary>
    public void SaveMusicList()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(MusicListPath)!);
            File.WriteAllLines(MusicListPath, _audioList.ConvertAll(a => a[0]));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"保存播放列表失败：{ex.Message}");
        }
    }

    /// <summary>把当前 _audioList 写到 textbox 显示。</summary>
    private void RefreshAudioListUi()
    {
        AudioListBox.Text = string.Join(Environment.NewLine, _audioList.ConvertAll(a => a[1]));
    }

    /// <summary>添加音频按钮：弹文件选择器，过滤 .mp3/.wma/.wav，去重后追加到列表。</summary>
    private async void OnAddAudio(object? sender, RoutedEventArgs e)
    {
        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "选择音频文件",
            AllowMultiple = true,
            FileTypeFilter = new[]
            {
                new FilePickerFileType("音频文件")
                {
                    Patterns = new[] { "*.mp3", "*.wma", "*.wav" }
                }
            }
        });

        foreach (var file in files)
        {
            var fullPath = file.TryGetLocalPath();
            if (string.IsNullOrEmpty(fullPath)) continue;
            var ext = Path.GetExtension(fullPath).ToLowerInvariant();
            if (ext != ".mp3" && ext != ".wma" && ext != ".wav") continue;
            if (_audioList.Exists(a => a[0] == fullPath)) continue;
            _audioList.Add(new[] { fullPath, Path.GetFileName(fullPath) });
        }
        RefreshAudioListUi();
    }

    private void OnClearAudio(object? sender, RoutedEventArgs e)
    {
        StopPlaylist();
        _audioList.Clear();
        AudioListBox.Text = "";
        PlayAudioButton.Content = "播放";
    }

    /// <summary>播放/暂停/恢复 三态按钮。</summary>
    private void OnPlayAudio(object? sender, RoutedEventArgs e)
    {
        if (_audioList.Count == 0) return;

        // 已有 player：处于暂停 → 恢复；处于播放 → 暂停
        if (_playlistPlayer is not null)
        {
            if (_playlistPlayer.State == AudioPlaybackState.Playing)
            {
                _playlistPlayer.Pause();
                PlayAudioButton.Content = "恢复";
                return;
            }
            if (_playlistPlayer.State == AudioPlaybackState.Paused)
            {
                _playlistPlayer.Resume();
                PlayAudioButton.Content = "暂停";
                return;
            }
        }

        // 从头开始播
        _currentTrackIndex = 0;
        StartTrack(_currentTrackIndex);
    }

    /// <summary>停止按钮：停止播放，恢复"播放"标签，不清空列表。</summary>
    private void OnStopAudio(object? sender, RoutedEventArgs e)
    {
        StopPlaylist();
        PlayAudioButton.Content = "播放";
    }

    private void StopPlaylist()
    {
        if (_playlistPlayer is null) return;
        _playlistPlayer.Stop();
        _playlistPlayer.Dispose();
        _playlistPlayer = null;
    }

    /// <summary>启动指定下标的曲目。</summary>
    private void StartTrack(int index)
    {
        if (index < 0 || index >= _audioList.Count) return;

        // 先停掉旧的
        _playlistPlayer?.Stop();
        _playlistPlayer?.Dispose();

        _playlistPlayer = AudioPlayerHost.CreatePlayer();
        _playlistPlayer.Volume = 0.5f;
        _playlistPlayer.PlaybackEnded += OnPlaylistTrackEnded;
        _playlistPlayer.Play(_audioList[index][0]);
        PlayAudioButton.Content = "暂停";

        // 更新顶部"当前音频"行
        AudioListBox.Text =
            $"当前音频: {_audioList[index][1]}" + Environment.NewLine + Environment.NewLine
            + string.Join(Environment.NewLine, _audioList.ConvertAll(a => a[1]));
    }

    /// <summary>当前曲目自然播完：循环则播下一首（到底回到 0），否则停止。</summary>
    private void OnPlaylistTrackEnded(object? sender, EventArgs e)
    {
        // 切回 UI 线程操作控件
        Dispatcher.UIThread.Post(() =>
        {
            int next = _currentTrackIndex + 1;
            bool loop = LoopCheckBox.IsChecked == true;

            if (next >= _audioList.Count)
            {
                if (!loop)
                {
                    StopPlaylist();
                    PlayAudioButton.Content = "播放";
                    return;
                }
                next = 0;
            }

            _currentTrackIndex = next;
            StartTrack(next);
        });
    }

    // ─── 资源路径 ───

    /// <summary>构造 Asset 目录下文件的绝对路径。</summary>
    private static string AssetPath(string fileName)
        => Path.Combine(AppContext.BaseDirectory, "Asset", fileName);
}
