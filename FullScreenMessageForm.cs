using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // 设置窗体为全屏
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;


            // 获取当前字体
            Font currentFont = lblMessage.Font;
            // 创建新字体，修改字体大小为14
            Font newFont = new Font(currentFont.FontFamily, 20, currentFont.Style);

            // 添加关闭按钮
            Button closeButton = new Button();
            closeButton.Font = newFont;
            closeButton.Height = 60;
            closeButton.Text = "关闭";
            closeButton.Click += (s, ev) => this.Close();
            closeButton.Dock = DockStyle.Top;
            // 设置按钮的上下边距
            closeButton.Margin = new Padding(0, 40, 0, 0);
            this.Controls.Add(closeButton);

            // 手动调整消息文本的位置和大小
            lblMessage.Location = new Point(1982 / 2 - 120, 1084 / 2 - 50);
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;

            // 设置 TextBox 的新字体
            lblMessage.Font = newFont;

            this.TopMost = true;
        }
    }
}
