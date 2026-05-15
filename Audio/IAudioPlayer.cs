namespace TimerAndAlerm.Audio;

/// <summary>音频播放器的状态枚举。</summary>
public enum AudioPlaybackState
{
    /// <summary>已停止（未播放）。</summary>
    Stopped,

    /// <summary>正在播放中。</summary>
    Playing,

    /// <summary>已暂停。</summary>
    Paused,
}

/// <summary>
/// 跨平台音频播放器抽象。一个实例同一时刻只播一个文件。
/// 实现要线程安全到"从 UI 线程随便调"的程度。
/// </summary>
public interface IAudioPlayer : IDisposable
{
    /// <summary>自然播完时触发。外部 Stop()/Dispose() 不会触发本事件。在后台线程触发，订阅方自行 Marshal 到 UI 线程。</summary>
    event EventHandler? PlaybackEnded;

    /// <summary>当前播放状态。</summary>
    AudioPlaybackState State { get; }

    /// <summary>当前播放音频总时长；未加载时返回 TimeSpan.Zero。</summary>
    TimeSpan Duration { get; }

    /// <summary>音量，范围 0.0 - 1.0；超出范围由实现 clamp。</summary>
    float Volume { get; set; }

    /// <summary>从头播放指定文件。若已有播放则先停。filePath 是绝对路径。</summary>
    void Play(string filePath);

    /// <summary>暂停当前播放。当前不在 Playing 状态时是 no-op。</summary>
    void Pause();

    /// <summary>从暂停位置继续。当前不在 Paused 状态时是 no-op。</summary>
    void Resume();

    /// <summary>停止播放并释放当前 media。不触发 PlaybackEnded。</summary>
    void Stop();
}
