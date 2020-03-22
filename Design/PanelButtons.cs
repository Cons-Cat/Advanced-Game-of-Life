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
        // Clear grid.
        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (!Program.playing)
            {
                for (int i = 0; i < graphicsPanel1.GridWidth; i++)
                {
                    for (int j = 0; j < graphicsPanel1.GridHeight; j++)
                    {
                        Program.universe[i, j].Active = false;
                        Program.universe[i, j].AdjacentCount = 0;
                    }
                }

                Program.ticks = 0;
                UpdateTicks(0);

                graphicsPanel1.Invalidate();
            }
        }

        // Toggle grid shape.
        private void buttonShape_Click(object sender, EventArgs e)
        {
            if (gridShape == 0)
            {
                gridShape = 1;
                buttonShape.Text = "Square";
            }
            else
            {
                gridShape = 0;
                buttonShape.Text = "Hexagon";
            }

            UpdateGrid();
            graphicsPanel1.UpdateGrid(ClientRectangle.Height - statusStrip1.Height, gridShape);
            graphicsPanel1.Invalidate();
        }
    }
}
