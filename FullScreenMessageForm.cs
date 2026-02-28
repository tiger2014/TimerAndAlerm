using System;
using System.Drawing;
using System.Windows.Forms;

namespace TimerAndAlerm
{
    public partial class FullScreenMessageForm : Form
    {
        public FullScreenMessageForm(string message)
        {
            InitializeComponent();
            lblMessage.Text = message;
        }

        private void FullScreenMessageForm_Load(object sender, EventArgs e)
        {
            // 全屏无边框
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;

            // 深色背景
            this.BackColor = ColorTranslator.FromHtml("#1A1A2E");

            // 消息标签样式
            Font msgFont = new Font("Segoe UI", 24, FontStyle.Bold);
            lblMessage.Font = msgFont;
            lblMessage.ForeColor = ThemeColors.ClockText;
            lblMessage.AutoSize = true;

            // 居中消息
            var screen = Screen.FromControl(this);
            lblMessage.MaximumSize = new Size(screen.Bounds.Width - 200, 0);
            lblMessage.Location = new Point(
                (screen.Bounds.Width - lblMessage.PreferredWidth) / 2,
                (screen.Bounds.Height - lblMessage.PreferredHeight) / 2
            );
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;

            // 关闭按钮
            Button closeButton = new Button();
            closeButton.Text = "关闭";
            closeButton.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            closeButton.Size = new Size(160, 50);
            closeButton.Location = new Point((screen.Bounds.Width - 160) / 2, screen.Bounds.Height - 120);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.BackColor = ThemeColors.Primary;
            closeButton.ForeColor = Color.White;
            closeButton.Cursor = Cursors.Hand;
            closeButton.Click += (s, ev) => this.Close();
            this.Controls.Add(closeButton);
        }
    }
}
