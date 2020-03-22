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
    public partial class InputForm : Form
    {
        public int seed;

        public InputForm()
        {
            seed = 0;

            InitializeComponent();
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out seed);
        }
    }
}
