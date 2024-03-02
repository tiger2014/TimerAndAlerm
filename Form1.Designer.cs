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
            buttonStart = new Button();
            button1 = new Button();
            lblStopwatch = new Label();
            btnReset = new Button();
            label1 = new Label();
            button2 = new Button();
            button3 = new Button();
            txbMins = new TextBox();
            label2 = new Label();
            button4 = new Button();
            progressBar1 = new ProgressBar();
            button5 = new Button();
            checkBox1 = new CheckBox();
            txbAudios = new TextBox();
            button6 = new Button();
            button7 = new Button();
            label3 = new Label();
            button8 = new Button();
            button9 = new Button();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(12, 12);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(88, 35);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += btnStart_Click;
            // 
            // button1
            // 
            button1.Location = new Point(106, 12);
            button1.Name = "button1";
            button1.Size = new Size(82, 35);
            button1.TabIndex = 1;
            button1.Text = "Stop";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnStop_Click;
            // 
            // lblStopwatch
            // 
            lblStopwatch.AutoSize = true;
            lblStopwatch.Location = new Point(69, 64);
            lblStopwatch.Name = "lblStopwatch";
            lblStopwatch.Size = new Size(0, 15);
            lblStopwatch.TabIndex = 2;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(204, 12);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(75, 35);
            btnReset.TabIndex = 3;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 64);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 4;
            label1.Text = "秒表：";
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F);
            button2.Location = new Point(72, 99);
            button2.Name = "button2";
            button2.Size = new Size(21, 23);
            button2.TabIndex = 5;
            button2.Text = "-";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(136, 99);
            button3.Name = "button3";
            button3.Size = new Size(23, 23);
            button3.TabIndex = 6;
            button3.Text = "+";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // txbMins
            // 
            txbMins.Location = new Point(99, 99);
            txbMins.Name = "txbMins";
            txbMins.Size = new Size(32, 23);
            txbMins.TabIndex = 7;
            txbMins.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 103);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 8;
            label2.Text = "分钟数： ";
            // 
            // button4
            // 
            button4.Location = new Point(163, 99);
            button4.Name = "button4";
            button4.Size = new Size(55, 23);
            button4.TabIndex = 9;
            button4.Text = "倒计时";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(85, 144);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(194, 15);
            progressBar1.TabIndex = 10;
            // 
            // button5
            // 
            button5.Location = new Point(22, 185);
            button5.Name = "button5";
            button5.Size = new Size(65, 29);
            button5.TabIndex = 11;
            button5.Text = "添加音频";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(25, 220);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(75, 19);
            checkBox1.TabIndex = 12;
            checkBox1.Text = "循环播放";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // txbAudios
            // 
            txbAudios.Location = new Point(25, 245);
            txbAudios.Multiline = true;
            txbAudios.Name = "txbAudios";
            txbAudios.ScrollBars = ScrollBars.Vertical;
            txbAudios.Size = new Size(258, 138);
            txbAudios.TabIndex = 13;
            // 
            // button6
            // 
            button6.Location = new Point(175, 185);
            button6.Name = "button6";
            button6.Size = new Size(50, 41);
            button6.TabIndex = 14;
            button6.Text = "播放";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(234, 98);
            button7.Name = "button7";
            button7.Size = new Size(45, 23);
            button7.TabIndex = 15;
            button7.Text = "取消";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 144);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 16;
            label3.Text = "倒计时： ";
            // 
            // button8
            // 
            button8.Location = new Point(231, 185);
            button8.Name = "button8";
            button8.Size = new Size(46, 41);
            button8.TabIndex = 17;
            button8.Text = "停止";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button9
            // 
            button9.Location = new Point(93, 185);
            button9.Name = "button9";
            button9.Size = new Size(64, 29);
            button9.TabIndex = 18;
            button9.Text = "清空列表";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(295, 395);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(label3);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(txbAudios);
            Controls.Add(checkBox1);
            Controls.Add(button5);
            Controls.Add(progressBar1);
            Controls.Add(button4);
            Controls.Add(label2);
            Controls.Add(txbMins);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(btnReset);
            Controls.Add(lblStopwatch);
            Controls.Add(button1);
            Controls.Add(buttonStart);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

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
    }
}
