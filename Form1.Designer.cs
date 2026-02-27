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
            label4 = new Label();
            label5 = new Label();
            button10 = new Button();
            label6 = new Label();
            nudHour = new NumericUpDown();
            nudMinute = new NumericUpDown();
            lblColon = new Label();
            button11 = new Button();
            ((System.ComponentModel.ISupportInitialize)nudHour).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMinute).BeginInit();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(97, 63);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(59, 35);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += btnStart_Click;
            // 
            // button1
            // 
            button1.Location = new Point(164, 63);
            button1.Name = "button1";
            button1.Size = new Size(55, 35);
            button1.TabIndex = 1;
            button1.Text = "Stop";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnStop_Click;
            // 
            // lblStopwatch
            // 
            lblStopwatch.AutoSize = true;
            lblStopwatch.Location = new Point(69, 115);
            lblStopwatch.Name = "lblStopwatch";
            lblStopwatch.Size = new Size(0, 15);
            lblStopwatch.TabIndex = 2;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(225, 63);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(55, 35);
            btnReset.TabIndex = 3;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 73);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 4;
            label1.Text = "秒表：";
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F);
            button2.Location = new Point(135, 149);
            button2.Name = "button2";
            button2.Size = new Size(21, 23);
            button2.TabIndex = 5;
            button2.Text = "-";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(199, 149);
            button3.Name = "button3";
            button3.Size = new Size(23, 23);
            button3.TabIndex = 6;
            button3.Text = "+";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // txbMins
            // 
            txbMins.Location = new Point(162, 149);
            txbMins.Name = "txbMins";
            txbMins.Size = new Size(32, 23);
            txbMins.TabIndex = 7;
            txbMins.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(85, 153);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 8;
            label2.Text = "分钟数： ";
            // 
            // button4
            // 
            button4.Location = new Point(12, 148);
            button4.Name = "button4";
            button4.Size = new Size(55, 23);
            button4.TabIndex = 9;
            button4.Text = "倒计时";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(85, 195);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(194, 15);
            progressBar1.TabIndex = 10;
            // 
            // button5
            // 
            button5.Location = new Point(22, 236);
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
            checkBox1.Location = new Point(25, 271);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(75, 19);
            checkBox1.TabIndex = 12;
            checkBox1.Text = "循环播放";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // txbAudios
            // 
            txbAudios.Location = new Point(25, 296);
            txbAudios.Multiline = true;
            txbAudios.Name = "txbAudios";
            txbAudios.ScrollBars = ScrollBars.Vertical;
            txbAudios.Size = new Size(258, 138);
            txbAudios.TabIndex = 13;
            // 
            // button6
            // 
            button6.Location = new Point(180, 236);
            button6.Name = "button6";
            button6.Size = new Size(50, 41);
            button6.TabIndex = 14;
            button6.Text = "播放";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(234, 149);
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
            label3.Location = new Point(14, 195);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 16;
            label3.Text = "倒计时进度： ";
            // 
            // button8
            // 
            button8.Location = new Point(237, 236);
            button8.Name = "button8";
            button8.Size = new Size(46, 41);
            button8.TabIndex = 17;
            button8.Text = "停止";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button9
            // 
            button9.Location = new Point(102, 236);
            button9.Name = "button9";
            button9.Size = new Size(64, 29);
            button9.TabIndex = 18;
            button9.Text = "清空列表";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 22);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 19;
            label4.Text = "北京时间：";
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(76, 7);
            label5.Name = "label5";
            label5.Size = new Size(147, 39);
            label5.TabIndex = 16;
            label5.Text = "00：00：00";
            // 
            // button10
            // 
            button10.Location = new Point(222, 14);
            button10.Name = "button10";
            button10.Size = new Size(57, 24);
            button10.TabIndex = 20;
            button10.Text = "敲钟";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 114);
            label6.Name = "label6";
            label6.Size = new Size(62, 15);
            label6.TabIndex = 21;
            label6.Text = "定时提醒：";
            // 
            // nudHour
            // 
            nudHour.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            nudHour.Location = new Point(97, 107);
            nudHour.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            nudHour.Name = "nudHour";
            nudHour.Size = new Size(59, 29);
            nudHour.TabIndex = 22;
            nudHour.TextAlign = HorizontalAlignment.Center;
            // 
            // nudMinute
            // 
            nudMinute.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            nudMinute.Location = new Point(164, 106);
            nudMinute.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            nudMinute.Name = "nudMinute";
            nudMinute.Size = new Size(55, 29);
            nudMinute.TabIndex = 23;
            nudMinute.TextAlign = HorizontalAlignment.Center;
            // 
            // lblColon
            // 
            lblColon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblColon.Location = new Point(153, 107);
            lblColon.Name = "lblColon";
            lblColon.Size = new Size(15, 25);
            lblColon.TabIndex = 25;
            lblColon.Text = ":";
            lblColon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button11
            // 
            button11.Location = new Point(225, 107);
            button11.Name = "button11";
            button11.Size = new Size(55, 29);
            button11.TabIndex = 24;
            button11.Text = "Start";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(295, 446);
            Controls.Add(button11);
            Controls.Add(nudMinute);
            Controls.Add(lblColon);
            Controls.Add(nudHour);
            Controls.Add(label6);
            Controls.Add(button10);
            Controls.Add(label5);
            Controls.Add(label4);
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
            ((System.ComponentModel.ISupportInitialize)nudHour).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMinute).EndInit();
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
        private Label label4;
        private Label label5;
        private Button button10;
        private Button btnLogOrders;
        private Button btnStopLog;
        private Label label6;
        private NumericUpDown nudHour;
        private NumericUpDown nudMinute;
        private Label lblColon;
        private Button button11;
    }
}
