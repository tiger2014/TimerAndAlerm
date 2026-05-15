using LibVLCSharp.Shared;

namespace TimerAndAlerm.Audio;

/// <summary>
/// 基于 LibVLCSharp 的跨平台音频播放器实现。
/// 调用前须由 AudioPlayerHost.EnsureInitialized() 完成 native 库初始化。
/// </summary>
public sealed class LibVlcAudioPlayer : IAudioPlayer
{
    /// <summary>LibVLC 核心实例，持有 native 引擎生命周期。</summary>
    private readonly LibVLC _libVlc;

    /// <summary>底层播放器实例，负责实际音频解码与输出。</summary>
    private readonly MediaPlayer _mediaPlayer;

    /// <summary>保护 Play/Pause/Resume/Stop/Dispose 并发访问的锁对象。</summary>
    private readonly object _lock = new();

    /// <summary>当前加载的 media，null 表示未加载。</summary>
    private Media? _currentMedia;

    /// <summary>防止 EndReached 在异常情况下重复触发。</summary>
    private bool _endedRaised;

    /// <summary>标记是否已 Dispose，防止重复释放。</summary>
    private bool _disposed;

    /// <inheritdoc />
    public event EventHandler? PlaybackEnded;

    /// <inheritdoc />
    public AudioPlaybackState State
    {
        get
        {
            // LibVLC 的 VLCState 枚举直接映射
            return _mediaPlayer.State switch
            {
                VLCState.Playing => AudioPlaybackState.Playing,
                VLCState.Paused  => AudioPlaybackState.Paused,
                _                => AudioPlaybackState.Stopped,
            };
        }
    }

    /// <inheritdoc />
    public TimeSpan Duration
    {
        get
        {
            // Media.Duration 返回毫秒 long，-1 表示未知
            var ms = _currentMedia?.Duration ?? -1L;
            return ms > 0 ? TimeSpan.FromMilliseconds(ms) : TimeSpan.Zero;
        }
    }

    /// <inheritdoc />
    public float Volume
    {
        get => _mediaPlayer.Volume / 100f;
        set
        {
            // 外部 0.0-1.0 → 内部 0-100，超范围 clamp
            var clamped = Math.Clamp(value, 0f, 1f);
            _mediaPlayer.Volume = (int)(clamped * 100);
        }
    }

    public LibVlcAudioPlayer()
    {
        // AudioPlayerHost 已调用 Core.Initialize() 并配置好环境；这里直接创建引擎实例
        _libVlc = new LibVLC(enableDebugLogs: false);
        _mediaPlayer = new MediaPlayer(_libVlc);

        // 自然播完时触发；外部 Stop/Dispose 不订阅 Stopped，不触发 PlaybackEnded
        _mediaPlayer.EndReached += OnEndReached;
    }

    /// <inheritdoc />
    public void Play(string filePath)
    {
        lock (_lock)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);

            // 停掉当前播放（不触发 PlaybackEnded，因为是主动换曲）
            StopInternal();

            // 重置"已触发"标志，务必在 Play() 之前
            _endedRaised = false;
            _currentMedia = new Media(_libVlc, new Uri(filePath));
            _mediaPlayer.Play(_currentMedia);
        }
    }

    /// <inheritdoc />
    public void Pause()
    {
        lock (_lock)
        {
            if (_disposed) return;
            if (_mediaPlayer.State == VLCState.Playing)
                _mediaPlayer.Pause();
        }
    }

    /// <inheritdoc />
    public void Resume()
    {
        lock (_lock)
        {
            if (_disposed) return;
            if (_mediaPlayer.State == VLCState.Paused)
                _mediaPlayer.Play();
        }
    }

    /// <inheritdoc />
    public void Stop()
    {
        lock (_lock)
        {
            if (_disposed) return;
            StopInternal();
        }
    }

    /// <summary>
    /// 内部停止逻辑，由 Play/Stop/Dispose 共同调用。不触发 PlaybackEnded。
    /// 调用方须持有 _lock。
    /// </summary>
    private void StopInternal()
    {
        // LibVLC 3.x Stop() 是同步阻塞；在 lock 内调用（短暂阻塞可接受）
        _mediaPlayer.Stop();

        // 释放旧 media 对象
        _currentMedia?.Dispose();
        _currentMedia = null;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        lock (_lock)
        {
            if (_disposed) return;
            _disposed = true;

            _mediaPlayer.EndReached -= OnEndReached;

            _mediaPlayer.Stop();
            _currentMedia?.Dispose();
            _currentMedia = null;
        }

        // 在 lock 外按顺序释放 native 资源
        _mediaPlayer.Dispose();
        _libVlc.Dispose();
    }

    // ─── 事件处理 ──────────────────────────────────────────────────

    /// <summary>LibVLC 自然播完回调（在 LibVLC 内部线程触发）。</summary>
    private void OnEndReached(object? sender, EventArgs e)
    {
        bool shouldRaise;
        lock (_lock)
        {
            shouldRaise = !_endedRaised && !_disposed;
            if (shouldRaise) _endedRaised = true;
        }
        if (shouldRaise)
            PlaybackEnded?.Invoke(this, EventArgs.Empty);
    }

}
