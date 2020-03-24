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
        private void toolStripMenuItemBackCol_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = cellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                backColor = dlg.Color;
            }
        }

        private void toolStripMenuItemCellCol_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = cellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                cellColor = dlg.Color;
            }
        }

        private void toolStripMenuItemLineCol_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = cellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                gridColor = dlg.Color;
            }
        }

        private void toolStripMenuItemTogLine_Click(object sender, EventArgs e)
        {
            drawLines = !drawLines;
            graphicsPanel1.Invalidate();
        }

        private void toolStripMenuItemTogAdjacent_Click(object sender, EventArgs e)
        {
            drawAdjacent = !drawAdjacent;
            graphicsPanel1.Invalidate();
        }

        private void toolStripMenuItemTogHud_Click(object sender, EventArgs e)
        {
            drawHud = !drawHud;
            graphicsPanel1.Invalidate();
        }

        private void toolStripMenuItemTogStrip_Click(object sender, EventArgs e)
        {
            drawToolStrip = !drawToolStrip;
            hudScale = drawToolStrip ? 1 : 0;
            statusStrip1.Visible = drawToolStrip;

            slidingPanel[panelInd].Height = ClientSize.Height - panel1.Height - (statusStrip1.Height * hudScale) - 3;
            UpdatePanels();
        }
    }
}
