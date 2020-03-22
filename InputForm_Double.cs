using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOLSource
{
    public partial class InputForm_Double : Form
    {
        public int value1;
        public int value2;

        public InputForm_Double()
        {
            value1 = 0;
            value2 = 0;

            InitializeComponent();
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            if (
                !string.IsNullOrEmpty(textBox1.Text)
                && !string.IsNullOrEmpty(textBox2.Text)
                )
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out value1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox2.Text, out value2);
        }
    }
}
