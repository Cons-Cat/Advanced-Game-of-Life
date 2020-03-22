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
        private void button2_Click(object sender, EventArgs e)
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
        private void button5_Click(object sender, EventArgs e)
        {
            if (gridShape == 0)
            {
                gridShape = 1;
                button5.Text = "Square";
            }
            else
            {
                gridShape = 0;
                button5.Text = "Hexagon";
            }

            for (int k = 0; k <= 1; k++)
            {
                for (int i = 0; i < Program.universe.GetLength(0); i++)
                {
                    for (int j = 0; j < Program.universe.GetLength(1); j++)
                    {
                        if (k == 0)
                        {
                            Program.universe[i, j].AdjacentCount = 0;
                        }
                        else
                        {
                            if (Program.universe[i, j].Active)
                            {
                                Program.universe[i, j].CountAdjacent(i, j, gridShape, graphicsPanel1.GridWidth, graphicsPanel1.GridHeight);
                            }
                        }
                    }
                }
            }

            graphicsPanel1.UpdateGrid(ClientRectangle.Height - statusStrip1.Height, gridShape);
            graphicsPanel1.Invalidate();
        }
    }
}
