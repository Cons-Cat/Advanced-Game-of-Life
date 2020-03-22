using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GOLSource
{
    public partial class Form1 : Form
    {
        // Tick speed of play button.
        private void buttonSpeed_Click(object sender, EventArgs e)
        {
            InputForm dlg = new InputForm();
            dlg.value = 100;
            dlg.label1.Text = "Input Tick Speed";
            dlg.label1.Location = new Point(dlg.Width / 2 - dlg.label1.Width / 2, 9);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                GameSpeed = dlg.value;
                TickLabel.Text = $"Tick Speed (ms) = {GameSpeed}";
                TickLabel.Invalidate();
            }
        }
    }
}
