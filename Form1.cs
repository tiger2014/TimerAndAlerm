using NAudio.Wave;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Timer = System.Windows.Forms.Timer;

namespace TimerAndAlerm
{
    public partial class Form1 : Form
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;
        private Stopwatch stopwatch;
        private Timer alarmTimer;
        private Timer miaobiaotimer;
        private Timer daojishitimer;
        private List<string[]> audioList = new List<string[]>();
        private IWavePlayer wavePlayer;
        private AudioFileReader? audioFileReader;
        private int currentTrackIndex;
        private long pausedPosition;

        public Form1()
        {
            InitializeComponent();
            InitializeNotifyIcon();
            InitializeStopwatch();
            InitializeAlarmTimer();
            this.FormClosing += Form1_FormClosing; // 确保连接 FormClosing 事件
            this.Text = "Assistant";
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            contextMenu = new ContextMenuStrip();
            Bitmap iconBitmap = new Bitmap("Asset\\bell.png");

            // 转换为 Icon
            IntPtr hIcon = iconBitmap.GetHicon();

            notifyIcon.Icon = Icon.FromHandle(hIcon);  // 替换为你的应用图标

            ToolStripMenuItem showMenuItem = new ToolStripMenuItem("显示");
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("退出");

            showMenuItem.Click += ShowMenuItem_Click;
            exitMenuItem.Click += ExitMenuItem_Click;

            contextMenu.Items.Add(showMenuItem);
            contextMenu.Items.Add(exitMenuItem);

            notifyIcon.ContextMenuStrip = contextMenu;
            notifyIcon.Visible = true;
        }

        private void InitializeStopwatch()
        {
            stopwatch = new Stopwatch();
        }

        private void InitializeAlarmTimer()
        {
            alarmTimer = new Timer();
            alarmTimer.Interval = 1000; // 设置为每秒检查一次
            alarmTimer.Tick += AlarmTimer_Tick;
            alarmTimer.Start();
        }

        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            var beijintime = GetBeiJinTime();
            label5.Text= beijintime.ToString("HH:mm:ss");
            if (beijintime.Hour == 5 || beijintime.Hour == 11 || beijintime.Hour == 17 || beijintime.Hour == 23)
            {
                if (beijintime.Minute == 54 && beijintime.Second == 0)
                {
                    Task.Run(() =>
                    {
                        PlayNotification("Asset\\daojishi.mp3");
                    });
                    FullScreenMessageForm fullScreenMessage = new FullScreenMessageForm($"北京时间 {beijintime.ToString("HH:mm:ss")} 到了，准备发正念。当前本地时间 {DateTime.Now.ToString("HH:mm:ss")}");                    
                    fullScreenMessage.ShowDialog();
                }
                if (beijintime.Minute == 55 && beijintime.Second == 0)
                {
                    // 播放钟声
                    Task.Run(() =>
                    {
                        PlayNotification("Asset\\fzn15.mp3");
                    });
                }
            }
        }

        private DateTime GetBeiJinTime()
        {
            // 获取协调世界时（UTC）时间
            DateTime utcNow = DateTime.UtcNow;

            // 设置时区为中国标准时间（北京时间）
            TimeZoneInfo chinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");

            // 将UTC时间转换为本地时间
            DateTime beijingTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chinaTimeZone);

            return beijingTime;
        }

        private void ShowMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Dispose(); // 释放系统托盘图标资源

            // 在窗体关闭时释放资源
            if (wavePlayer != null)
            {
                wavePlayer.Stop();
                wavePlayer.Dispose();
            }

            if (alarmTimer != null)
            {
                alarmTimer.Stop();
                alarmTimer.Dispose();
                alarmTimer = null;
            }

            if (miaobiaotimer != null)
            {
                miaobiaotimer.Stop();
                miaobiaotimer.Dispose();
                miaobiaotimer = null;
            }

            if (daojishitimer != null)
            {
                daojishitimer.Stop();
                daojishitimer.Dispose();
                daojishitimer = null;
            }

            System.Windows.Forms.Application.Exit(); // 退出应用程序
            Environment.Exit(0);
            //Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            stopwatch.Start();
            if (miaobiaotimer == null)
            {
                miaobiaotimer = new Timer();
                miaobiaotimer.Interval = 1;
                miaobiaotimer.Tick += UpdateStopwatchDisplay;
                miaobiaotimer.Start();
            }
        }

        private void UpdateStopwatchDisplay(object sender, EventArgs e)
        {
            lblStopwatch.Text = stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.fff");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblStopwatch.Text = stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;        // 取消默认关闭操作
            Hide();                 // 隐藏窗体
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (miaobiaotimer != null)
            {
                miaobiaotimer.Stop();
                miaobiaotimer.Tick -= UpdateStopwatchDisplay;
                miaobiaotimer.Dispose();
                miaobiaotimer = null;
                lblStopwatch.Text = @"00:00:00.000";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var mins = 0;
            try
            {
                mins = int.Parse(string.IsNullOrWhiteSpace(txbMins.Text) ? "0" : txbMins.Text);
            }
            catch
            {
            }
            mins--;
            mins = mins < 0 ? 0 : mins;
            txbMins.Text = mins.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var mins = 0;
            try
            {
                mins = int.Parse(string.IsNullOrWhiteSpace(txbMins.Text) ? "0" : txbMins.Text);
            }
            catch
            {
            }
            mins++;
            mins = mins < 0 ? 0 : mins;
            txbMins.Text = mins.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var mins = 0;
            try
            {
                mins = int.Parse(string.IsNullOrWhiteSpace(txbMins.Text) ? "0" : txbMins.Text);
            }
            catch
            {
            }
            string inputData = string.Empty;
            using (InputDialog inputDialog = new InputDialog("请输入倒计时事由"))
            {
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    inputData = inputDialog.InputText;
                }
                else
                {
                    return;
                }
            }

            //if (mins <= 0) return;
            progressBar1.Maximum = mins * 60;
            progressBar1.Minimum = 0;
            progressBar1.Value = mins * 60;
            progressBar1.ForeColor = Color.OrangeRed;

            daojishitimer = new Timer();
            daojishitimer.Interval = 1000;
            daojishitimer.Tick += (sender, e) =>
            {
                progressBar1.Value = (progressBar1.Value - 1) < 0 ? 0 : (progressBar1.Value - 1);
                if (progressBar1.Value <= 0)
                {
                    if (daojishitimer != null)
                    { // 停止定时器
                        daojishitimer.Stop();
                        // 销毁定时器
                        daojishitimer.Dispose();
                    }
                    Task.Run(() =>
                    {
                        PlayNotification("Asset\\daojishi.mp3");
                    });
                    //MessageBox.Show($"{mins} 分钟到了！", "倒计时", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FullScreenMessageForm fullScreenMessage = new FullScreenMessageForm($"{mins} 分钟到了！请 ‘{inputData}’");
                    fullScreenMessage.ShowDialog();
                }
            };
            daojishitimer.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置对话框标题
            openFileDialog.Title = "选择音频文件";
            // 设置初始目录
            openFileDialog.InitialDirectory = "C:\\";
            // 启用多选
            openFileDialog.Multiselect = true;
            // 设置文件类型过滤
            openFileDialog.Filter = "所有文件 (*.*)|*.*";
            //openFileDialog.Filter = "MP3 文件 (*.mp3)|*.mp3|WMA 文件 (*.wma)|*.wma";

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string[] selectedPath = openFileDialog.FileNames;
                foreach (var item in selectedPath)
                {
                    if (Path.GetExtension(item).ToLower() == ".mp3" || Path.GetExtension(item).ToLower() == ".wma" || Path.GetExtension(item).ToLower() == ".wav")
                    {
                        if (!audioList.Any(s => s[0] == item))
                        {
                            string filename = Path.GetFileName(item);
                            string[] audio = { item, filename };
                            audioList.Add(audio);
                        }
                    }
                }

                for (int i = 0; i < audioList.Count; i++)
                {
                    txbAudios.Text += audioList[i][1] + Environment.NewLine;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (audioList.Count <= 0) return;
            currentTrackIndex = 0;
            PlayCurrentTrack();
        }

        public void PlayNotification(string filepath)
        {
            try
            {
                IWavePlayer player = new WaveOut(); // 你也可以选择其他 IWavePlayer 实现
                var audioFileReader1 = new AudioFileReader(filepath);
                player.Volume = 1.0f;           // 相对于原始音量的比例
                player.Init(audioFileReader1);
                player.Play();
            }
            catch (Exception ex)
            {
                //
            }
        }

        private void PlayCurrentTrack()
        {
            try
            {
                // 停止当前音轨
                if (wavePlayer != null)
                {
                    if (wavePlayer.PlaybackState == PlaybackState.Playing)
                    {
                        pausedPosition = audioFileReader.Position;
                        wavePlayer.Pause();
                        button6.Text = "恢复";
                        return;
                    }
                    else if (wavePlayer.PlaybackState == PlaybackState.Paused || button6.Text == "恢复")
                    {
                        audioFileReader.Position = pausedPosition;
                        wavePlayer.Play();
                        button6.Text = "暂停";
                        return;
                    }
                    wavePlayer.Stop();
                    wavePlayer.Dispose();
                }

                // 播放当前音轨
                wavePlayer = new WaveOut(); // 你也可以选择其他 IWavePlayer 实现
                audioFileReader = new AudioFileReader(audioList[currentTrackIndex][0]);
                wavePlayer.Volume = 0.5f;           // 相对于原始音量的比例

                wavePlayer.Init(audioFileReader);
                wavePlayer.PlaybackStopped += (sender, e) =>
                {
                    //if (wavePlayer.PlaybackState == PlaybackState.Paused) return;
                    if (button6.Text == "恢复") return;
                    if (checkBox1.Checked)
                    {
                        if (audioList.Count <= currentTrackIndex + 1) currentTrackIndex = -1;
                        currentTrackIndex++;
                        PlayCurrentTrack();
                    }
                    else
                    {
                        if (audioList.Count <= currentTrackIndex + 1) return;
                        currentTrackIndex++;
                        PlayCurrentTrack();
                    }
                };
                wavePlayer.Play();
                button6.Text = "暂停";

                txbAudios.Text = "当前音频： " + audioList[currentTrackIndex][1] + $"    {audioFileReader.TotalTime.ToString(@"mm\:ss")}" + Environment.NewLine + Environment.NewLine;
                for (int i = 0; i < audioList.Count; i++)
                {
                    txbAudios.Text += audioList[i][1] + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (daojishitimer != null)
            {
                daojishitimer.Stop();
                daojishitimer.Dispose();
                daojishitimer = null;
                progressBar1.Value = 0;
                txbMins.Text = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (wavePlayer != null)
            {
                wavePlayer.Stop();
                wavePlayer.Dispose();
            }

            txbAudios.Text = "";
            for (int i = 0; i < audioList.Count; i++)
            {
                txbAudios.Text += audioList[i][1] + Environment.NewLine;
            }

            button6.Text = "播放";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (wavePlayer != null)
            {
                wavePlayer.Stop();
                wavePlayer.Dispose();
            }
            audioList.Clear();
            txbAudios.Text = "";
            button6.Text = "播放";

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}