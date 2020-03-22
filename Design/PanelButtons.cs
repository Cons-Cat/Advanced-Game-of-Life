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
        // A single tick.
        private void ButtonTick_Click(object sender, EventArgs e)
        {
            if (!Program.playing)
            {
                Program.Tick();
                graphicsPanel1.Invalidate();
            }
        }

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
            }
        }

        // Toggle grid shape.
        private void buttonShape_Click(object sender, EventArgs e)
        {
            if (!Program.playing)
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

        // Loop ticks.
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Program.playing = !Program.playing;
        }

        // Randomize grid life.
        private void buttonRandom_Click(object sender, EventArgs e)
        {
            if (!Program.playing)
            {
                RandomizeGrid(Guid.NewGuid().GetHashCode());
            }
        }

        private void buttonRandSeed_Click(object sender, EventArgs e)
        {
            InputForm dlg = new InputForm();
            dlg.seed = 0;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                seed = dlg.seed;
                RandomizeGrid(seed);
            }
        }

        // Randomize grid life.
        private void RandomizeGrid(int argSeed)
        {
            Random rnd = new Random(argSeed);

            // Activate approximately one third of cells.
            for (int i = 0; i < Program.universe.GetLength(0); i++)
            {
                for (int j = 0; j < Program.universe.GetLength(1); j++)
                {
                    Program.universe[i, j].Active = (rnd.Next(0, 3) == 2) ? true : false;
                }
            }

            UpdateGrid();
            graphicsPanel1.Invalidate();
        }
    }
}
