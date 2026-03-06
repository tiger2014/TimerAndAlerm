namespace TimerAndAlerm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelClock = new Panel();
            label4 = new Label();
            label5 = new Label();
            button10 = new Button();
            panelStopwatch = new Panel();
            label1 = new Label();
            buttonStart = new Button();
            button1 = new Button();
            btnReset = new Button();
            lblStopwatch = new Label();
            panelTimer = new Panel();
            label6 = new Label();
            nudHour = new NumericUpDown();
            lblColon = new Label();
            nudMinute = new NumericUpDown();
            button11 = new Button();
            label2 = new Label();
            button4 = new Button();
            button2 = new Button();
            txbMins = new TextBox();
            button3 = new Button();
            button7 = new Button();
            label3 = new Label();
            progressBar1 = new ProgressBar();
            panelAudio = new Panel();
            button5 = new Button();
            button9 = new Button();
            button6 = new Button();
            button8 = new Button();
            checkBox1 = new CheckBox();
            txbAudios = new TextBox();
            panelClock.SuspendLayout();
            panelStopwatch.SuspendLayout();
            panelTimer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudHour).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMinute).BeginInit();
            panelAudio.SuspendLayout();
            SuspendLayout();
            // 
            // panelClock
            // 
            panelClock.Controls.Add(label4);
            panelClock.Controls.Add(label5);
            panelClock.Controls.Add(button10);
            panelClock.Location = new Point(10, 10);
            panelClock.Name = "panelClock";
            panelClock.Size = new Size(328, 65);
            panelClock.TabIndex = 100;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(8, 20);
            label4.Name = "label4";
            label4.Size = new Size(74, 20);
            label4.TabIndex = 19;
            label4.Text = "北京时间:";
            // 
            // label5
            // 
            label5.Font = new Font("Consolas", 22F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(89, 11);
            label5.Name = "label5";
            label5.Size = new Size(144, 38);
            label5.TabIndex = 16;
            label5.Text = "00:00:00";
            // 
            // button10
            // 
            button10.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button10.Location = new Point(238, 10);
            button10.Name = "button10";
            button10.Size = new Size(87, 44);
            button10.TabIndex = 20;
            button10.Text = "敲钟";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // panelStopwatch
            // 
            panelStopwatch.Controls.Add(label1);
            panelStopwatch.Controls.Add(buttonStart);
            panelStopwatch.Controls.Add(button1);
            panelStopwatch.Controls.Add(btnReset);
            panelStopwatch.Controls.Add(lblStopwatch);
            panelStopwatch.Location = new Point(10, 76);
            panelStopwatch.Name = "panelStopwatch";
            panelStopwatch.Size = new Size(328, 71);
            panelStopwatch.TabIndex = 101;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(3, 18);
            label1.Name = "label1";
            label1.Size = new Size(47, 20);
            label1.TabIndex = 4;
            label1.Text = "秒表：";
            // 
            // buttonStart
            // 
            buttonStart.Font = new Font("Segoe UI", 14F);
            buttonStart.Location = new Point(53, 6);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(85, 45);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += btnStart_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 14F);
            button1.Location = new Point(144, 6);
            button1.Name = "button1";
            button1.Size = new Size(92, 45);
            button1.TabIndex = 1;
            button1.Text = "Stop";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnStop_Click;
            // 
            // btnReset
            // 
            btnReset.Font = new Font("Segoe UI", 14F);
            btnReset.Location = new Point(242, 6);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(83, 45);
            btnReset.TabIndex = 3;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // lblStopwatch
            // 
            lblStopwatch.AutoSize = true;
            lblStopwatch.Font = new Font("Consolas", 10F, FontStyle.Bold);
            lblStopwatch.Location = new Point(68, 44);
            lblStopwatch.Name = "lblStopwatch";
            lblStopwatch.Size = new Size(0, 17);
            lblStopwatch.TabIndex = 2;
            // 
            // panelTimer
            // 
            panelTimer.Controls.Add(label6);
            panelTimer.Controls.Add(nudHour);
            panelTimer.Controls.Add(lblColon);
            panelTimer.Controls.Add(nudMinute);
            panelTimer.Controls.Add(button11);
            panelTimer.Controls.Add(label2);
            panelTimer.Controls.Add(button4);
            panelTimer.Controls.Add(button2);
            panelTimer.Controls.Add(txbMins);
            panelTimer.Controls.Add(button3);
            panelTimer.Controls.Add(button7);
            panelTimer.Controls.Add(label3);
            panelTimer.Controls.Add(progressBar1);
            panelTimer.Location = new Point(10, 153);
            panelTimer.Name = "panelTimer";
            panelTimer.Size = new Size(328, 139);
            panelTimer.TabIndex = 102;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(10, 12);
            label6.Name = "label6";
            label6.Size = new Size(83, 21);
            label6.TabIndex = 21;
            label6.Text = "定时提醒：";
            // 
            // nudHour
            // 
            nudHour.Font = new Font("Microsoft YaHei UI", 12F);
            nudHour.Location = new Point(107, 9);
            nudHour.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            nudHour.Name = "nudHour";
            nudHour.Size = new Size(50, 28);
            nudHour.TabIndex = 22;
            nudHour.TextAlign = HorizontalAlignment.Center;
            // 
            // lblColon
            // 
            lblColon.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            lblColon.Location = new Point(163, 12);
            lblColon.Name = "lblColon";
            lblColon.Size = new Size(12, 18);
            lblColon.TabIndex = 25;
            lblColon.Text = ":";
            lblColon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nudMinute
            // 
            nudMinute.Font = new Font("Microsoft YaHei UI", 12F);
            nudMinute.Location = new Point(180, 9);
            nudMinute.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            nudMinute.Name = "nudMinute";
            nudMinute.Size = new Size(50, 28);
            nudMinute.TabIndex = 23;
            nudMinute.TextAlign = HorizontalAlignment.Center;
            // 
            // button11
            // 
            button11.Font = new Font("Segoe UI", 14F);
            button11.Location = new Point(242, 9);
            button11.Name = "button11";
            button11.Size = new Size(83, 31);
            button11.TabIndex = 24;
            button11.Text = "Start";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(74, 50);
            label2.Name = "label2";
            label2.Size = new Size(62, 19);
            label2.TabIndex = 8;
            label2.Text = "分钟数： ";
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 12F);
            button4.Location = new Point(10, 46);
            button4.Name = "button4";
            button4.Size = new Size(59, 28);
            button4.TabIndex = 9;
            button4.Text = "倒计时";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F);
            button2.Location = new Point(142, 46);
            button2.Name = "button2";
            button2.Size = new Size(28, 28);
            button2.TabIndex = 5;
            button2.Text = "-";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // txbMins
            // 
            txbMins.Font = new Font("Segoe UI", 12F);
            txbMins.Location = new Point(172, 45);
            txbMins.Name = "txbMins";
            txbMins.Size = new Size(38, 29);
            txbMins.TabIndex = 7;
            txbMins.TextAlign = HorizontalAlignment.Center;
            // 
            // button3
            // 
            button3.Location = new Point(213, 46);
            button3.Name = "button3";
            button3.Size = new Size(28, 28);
            button3.TabIndex = 6;
            button3.Text = "+";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button7
            // 
            button7.Font = new Font("Segoe UI", 12F);
            button7.Location = new Point(242, 47);
            button7.Name = "button7";
            button7.Size = new Size(83, 28);
            button7.TabIndex = 15;
            button7.Text = "取消";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(10, 91);
            label3.Name = "label3";
            label3.Size = new Size(105, 21);
            label3.TabIndex = 16;
            label3.Text = "倒计时进度： ";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(121, 91);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(204, 20);
            progressBar1.TabIndex = 10;
            // 
            // panelAudio
            // 
            panelAudio.Controls.Add(button5);
            panelAudio.Controls.Add(button9);
            panelAudio.Controls.Add(button6);
            panelAudio.Controls.Add(button8);
            panelAudio.Controls.Add(checkBox1);
            panelAudio.Controls.Add(txbAudios);
            panelAudio.Location = new Point(10, 298);
            panelAudio.Name = "panelAudio";
            panelAudio.Size = new Size(328, 240);
            panelAudio.TabIndex = 103;
            // 
            // button5
            // 
            button5.Location = new Point(10, 10);
            button5.Name = "button5";
            button5.Size = new Size(80, 30);
            button5.TabIndex = 11;
            button5.Text = "添加音频";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button9
            // 
            button9.Location = new Point(104, 10);
            button9.Name = "button9";
            button9.Size = new Size(80, 30);
            button9.TabIndex = 18;
            button9.Text = "清空列表";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button6
            // 
            button6.Location = new Point(197, 10);
            button6.Name = "button6";
            button6.Size = new Size(54, 30);
            button6.TabIndex = 14;
            button6.Text = "播放";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button8
            // 
            button8.Location = new Point(267, 10);
            button8.Name = "button8";
            button8.Size = new Size(54, 30);
            button8.TabIndex = 17;
            button8.Text = "停止";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(12, 50);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(75, 19);
            checkBox1.TabIndex = 12;
            checkBox1.Text = "循环播放";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // txbAudios
            // 
            txbAudios.Location = new Point(10, 76);
            txbAudios.Multiline = true;
            txbAudios.Name = "txbAudios";
            txbAudios.ScrollBars = ScrollBars.Vertical;
            txbAudios.Size = new Size(280, 154);
            txbAudios.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 550);
            Controls.Add(panelClock);
            Controls.Add(panelStopwatch);
            Controls.Add(panelTimer);
            Controls.Add(panelAudio);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Assistant";
            panelClock.ResumeLayout(false);
            panelClock.PerformLayout();
            panelStopwatch.ResumeLayout(false);
            panelStopwatch.PerformLayout();
            panelTimer.ResumeLayout(false);
            panelTimer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudHour).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMinute).EndInit();
            panelAudio.ResumeLayout(false);
            panelAudio.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelClock;
        private Panel panelStopwatch;
        private Panel panelTimer;
        private Panel panelAudio;
        private Button buttonStart;
        private Button button1;
        private Label lblStopwatch;
        private Button btnReset;
        private Label label1;
        private Button button2;
        private Button button3;
        private TextBox txbMins;
        private Label label2;
        private Button button4;
        private ProgressBar progressBar1;
        private Button button5;
        private CheckBox checkBox1;
        private TextBox txbAudios;
        private Button button6;
        private Button button7;
        private Label label3;
        private Button button8;
        private Button button9;
        private Label label4;
        private Label label5;
        private Button button10;
        private Label label6;
        private NumericUpDown nudHour;
        private NumericUpDown nudMinute;
        private Label lblColon;
        private Button button11;
    }
}
