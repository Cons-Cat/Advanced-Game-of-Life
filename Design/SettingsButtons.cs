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
                toolStripStatusLabelTickRate.Text = $"Tick Speed (ms) = {GameSpeed}";
            }
        }

        // Change size of grid.
        private void buttonWorldSize_Click(object sender, EventArgs e)
        {
            InputForm_Double dlg = new InputForm_Double();
            dlg.value1 = 25;
            dlg.value2 = 25;
            dlg.label1.Text = "Input Universe Dimensions";
            dlg.label1.Location = new Point(dlg.Width / 2 - dlg.label1.Width / 2, 9);

            dlg.label2.Text = "Width";
            dlg.label3.Text = "Height";

            dlg.textBox1.Text = Program.universe.GetLength(0).ToString();
            dlg.textBox2.Text = Program.universe.GetLength(1).ToString();

            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (dlg.value1 != Program.universe.GetLength(0) || dlg.value1 != Program.universe.GetLength(1)) // If the value has changed.
                {
                    Program.ReSizeUniverse(dlg.value1, dlg.value2);

                    UpdatePanels();
                    graphicsPanel1.Invalidate();
                }
            }
        }

        private void buttonFiniteToggle_Click(object sender, EventArgs e)
        {
            if (!Program.playing)
            {
                isFinite = !isFinite;
                UpdateGrid();
                graphicsPanel1.Invalidate();
            }
        }

        private void buttonLineToggle_Click(object sender, EventArgs e)
        {
            drawLines = !drawLines;
            graphicsPanel1.Invalidate();
        }

        private void buttonAdjacentToggle_Click(object sender, EventArgs e)
        {
            drawAdjacent = !drawAdjacent;
            graphicsPanel1.Invalidate();
        }

        private void buttonHud_Click(object sender, EventArgs e)
        {
            drawHud = !drawHud;
            graphicsPanel1.Invalidate();
        }

        private void buttonStrip_Click(object sender, EventArgs e)
        {
            drawToolStrip = !drawToolStrip;
            hudScale = drawToolStrip ? 1 : 0;
            statusStrip1.Visible = drawToolStrip;

            slidingPanel[panelInd].Height = ClientSize.Height - panel1.Height - (statusStrip1.Height * hudScale) - 3;
            UpdatePanels();
        }

        private void buttonBackCol_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = cellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                backColor = dlg.Color;
            }
        }

        private void buttonCellCol_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = cellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                cellColor = dlg.Color;
            }
        }

        private void buttonLineCol_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = cellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                gridColor = dlg.Color;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetSettings();
            graphicsPanel1.Invalidate();
        }
    }
}
