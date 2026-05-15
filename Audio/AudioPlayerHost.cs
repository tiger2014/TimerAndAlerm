using System.Runtime.InteropServices;

namespace TimerAndAlerm.Audio;

/// <summary>
/// LibVLC native 库的全局初始化入口。
/// 在 Program.Main 早期调用一次 EnsureInitialized()。
/// </summary>
public static class AudioPlayerHost
{
    /// <summary>是否已完成初始化，双重检查锁的外层快速路径标志。</summary>
    private static bool _initialized;

    /// <summary>保护初始化临界区的锁对象。</summary>
    private static readonly object _gate = new();

    /// <summary>
    /// VLC.app 内 native 库的标准路径（macOS）。
    /// </summary>
    private const string VlcAppLibDir = "/Applications/VLC.app/Contents/MacOS/lib";

    /// <summary>
    /// 确保 LibVLC native 库已初始化。幂等，多次调用安全。
    /// macOS 上优先用 NuGet 包附带的 libvlc.dylib（输出目录）；
    /// 若架构不匹配（如 Apple Silicon 环境使用了 x86_64 NuGet 包），
    /// 则回退到 /Applications/VLC.app/Contents/MacOS/lib。
    /// </summary>
    public static void EnsureInitialized()
    {
        if (_initialized) return;
        lock (_gate)
        {
            if (_initialized) return;
            var libDir = ResolveLibVlcDirectory();
            PreloadDependencies(libDir);
            LibVLCSharp.Shared.Core.Initialize(libDir);
            _initialized = true;
        }
    }

    /// <summary>
    /// 在 macOS 上检测 NuGet 附带的 libvlc.dylib 是否与当前进程架构兼容；
    /// 不兼容时返回 VLC.app 内的库路径作为回退。
    /// 其它平台返回 null（让 LibVLCSharp 按默认搜索路径查找）。
    /// </summary>
    private static string? ResolveLibVlcDirectory()
    {
        if (!OperatingSystem.IsMacOS()) return null;

        // NuGet 包附带的 libvlc.dylib 会被复制到输出目录根部
        var nugetDylib = Path.Combine(AppContext.BaseDirectory, "libvlc.dylib");
        if (File.Exists(nugetDylib) && IsMachOCompatible(nugetDylib))
            return null; // 让 LibVLCSharp 用默认路径找到它

        // Apple Silicon 环境：回退到已安装的 VLC.app
        if (Directory.Exists(VlcAppLibDir))
            return VlcAppLibDir;

        // 找不到，让 LibVLCSharp 报自己的错误
        return null;
    }

    /// <summary>
    /// 在 macOS 上，libvlc.dylib 通过 @rpath 依赖 libvlccore.dylib，
    /// 且 libvlc_new 依赖 VLC_PLUGIN_PATH 环境变量找到解码插件。
    /// 若使用 VLC.app 内的库：
    ///   1. 用 NativeLibrary.Load 预加载 libvlccore（解决 rpath）
    ///   2. 设置 VLC_PLUGIN_PATH 指向 VLC.app 插件目录（解决 libvlc_new 返回 NULL）
    /// </summary>
    private static void PreloadDependencies(string? libDir)
    {
        if (!OperatingSystem.IsMacOS() || libDir is null) return;

        // 步骤 1：libvlccore 必须比 libvlc 先加载，否则 rpath 解析失败
        var coreLib = Path.Combine(libDir, "libvlccore.dylib");
        if (File.Exists(coreLib))
            NativeLibrary.Load(coreLib);

        // 步骤 2：设置插件路径，libvlc_new 必须能找到解码插件，否则返回 NULL
        // 注意：.NET 的 Environment.SetEnvironmentVariable 在 macOS 上不会调用 libc 的 setenv，
        // 所以 native 库通过 getenv 读不到。必须 P/Invoke 直接调 libc.setenv。
        var pluginsDir = Path.Combine(Path.GetDirectoryName(libDir)!, "plugins");
        if (Directory.Exists(pluginsDir))
            SetNativeEnv("VLC_PLUGIN_PATH", pluginsDir);
    }

    /// <summary>
    /// macOS / Linux 下，把环境变量直接写到 libc 的 environ，让 native 库 getenv 能读到。
    /// .NET 自己的 Environment.SetEnvironmentVariable 在这些平台上只更新托管层缓存。
    /// </summary>
    private static void SetNativeEnv(string name, string value)
    {
        if (OperatingSystem.IsMacOS() || OperatingSystem.IsLinux())
            _ = setenv(name, value, 1);
    }

    [System.Runtime.InteropServices.DllImport("libc", EntryPoint = "setenv")]
    private static extern int setenv(string name, string value, int overwrite);

    /// <summary>
    /// 粗检 Mach-O dylib 文件头中的 CPU 类型是否与当前进程架构匹配。
    /// 只读取前 8 字节，不依赖任何外部工具。
    /// </summary>
    private static bool IsMachOCompatible(string dylibPath)
    {
        try
        {
            Span<byte> header = stackalloc byte[8];
            using var fs = File.OpenRead(dylibPath);
            if (fs.Read(header) < 8) return false;

            // Mach-O magic: 0xFEEDFACF (64-bit little-endian) 或 0xCFFAEDFE (big-endian)
            // Fat binary magic: 0xBEBAFECA (universal binary)
            uint magic = BitConverter.ToUInt32(header[..4]);
            const uint MH_MAGIC_64 = 0xFEEDFACF;
            const uint MH_CIGAM_64 = 0xCFFAEDFE;
            const uint FAT_MAGIC   = 0xBEBAFECA;

            if (magic == FAT_MAGIC) return true; // universal binary，肯定兼容

            if (magic != MH_MAGIC_64 && magic != MH_CIGAM_64) return false;

            // cputype 在 offset 4：arm64 = 0x0100000C，x86_64 = 0x01000007
            uint cpuType = magic == MH_CIGAM_64
                ? (uint)System.Net.IPAddress.NetworkToHostOrder((int)BitConverter.ToUInt32(header[4..8]))
                : BitConverter.ToUInt32(header[4..8]);

            bool isArm64  = cpuType == 0x0100000Cu;
            bool isX86_64 = cpuType == 0x01000007u;

            return RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.Arm64 => isArm64,
                Architecture.X64   => isX86_64,
                _                  => false,
            };
        }
        catch
        {
            return false;
        }
    }

    /// <summary>创建一个新的 player 实例。调用前确保已 EnsureInitialized。</summary>
    public static IAudioPlayer CreatePlayer() => new LibVlcAudioPlayer();
}
