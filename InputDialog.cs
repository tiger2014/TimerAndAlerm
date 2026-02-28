using System;
using System.Drawing;
using System.Windows.Forms;

namespace TimerAndAlerm
{
    public partial class InputDialog : Form
    {
        public string InputText { get; private set; }
        public InputDialog()
        {
            InitializeComponent();
            ApplyTheme();
        }

        public InputDialog(string inputdata)
        {
            InitializeComponent();
            ApplyTheme();
            textBox1.Text = inputdata;
        }

        private void ApplyTheme()
        {
            this.BackColor = ThemeColors.FormBackground;
            this.ForeColor = ThemeColors.TextPrimary;

            label1.ForeColor = ThemeColors.TextPrimary;

            textBox1.BackColor = ThemeColors.TextBoxBackground;
            textBox1.ForeColor = ThemeColors.TextPrimary;
            textBox1.BorderStyle = BorderStyle.FixedSingle;

            ThemeHelper.StyleButton(buttonOK, ButtonRole.Success);
            ThemeHelper.StyleButton(buttonCancel, ButtonRole.Neutral);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            InputText = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
