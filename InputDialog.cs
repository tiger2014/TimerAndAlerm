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
    public partial class InputDialog : Form
    {
        public string InputText { get; private set; }
        public InputDialog()
        {
            InitializeComponent();
        }

        public InputDialog(string inputdata)
        {
            InitializeComponent();
            textBox1.Text = inputdata;
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
