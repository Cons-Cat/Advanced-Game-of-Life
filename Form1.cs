using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOLSource
{
    public partial class Form1 : Form
    {
        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = true; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void GraphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            graphicsPanel1.RecalcCellSize(ref splitContainer1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the x, left to right
            for (int x = 0; x < Program.universe.GetLength(0); x++)
            {
                // Iterate through the universe in the y, top to bottom
                for (int y = 0; y < Program.universe.GetLength(1); y++)
                {
                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = RectangleF.Empty;
                    cellRect.X = (x * graphicsPanel1.CellSize);
                    cellRect.Y = (y * graphicsPanel1.CellSize) + graphicsPanel1.YOff;

                    cellRect.Width = graphicsPanel1.CellSize;
                    cellRect.Height = graphicsPanel1.CellSize;

                    // Fill the cell with a brush if alive
                    if (Program.universe[x, y].Active)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    if (graphicsPanel1.CellSize > 12)
                    {
                        TextRenderer.DrawText(e.Graphics, $"{Program.universe[x, y].AdjacentCount}", this.Font, new Point((int)cellRect.X, (int)cellRect.Y), SystemColors.ControlText);
                    }
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void GraphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                //float cellSize = Math.Min((float)splitContainer1.Panel2.Width / Program.universe.GetLength(0), (float)splitContainer1.Panel2.Height / Program.universe.GetLength(1));

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = (int)(e.X / graphicsPanel1.CellSize);
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = (int)((e.Y - graphicsPanel1.YOff) / graphicsPanel1.CellSize);

                if (x >= 0 && y >= 0 && x < graphicsPanel1.GridWidth && y < graphicsPanel1.GridHeight)
                {
                    // Toggle the cell's state
                    Program.universe[x, y].Active = !Program.universe[x, y].Active;

                    int adjX;
                    int adjY;

                    for (int i = 0; i < 9; i++)
                    {
                        adjX = x + (i / 3) - 1;
                        adjY = y + (i % 3) - 1;

                        if (
                            i == 4
                            || adjX >= graphicsPanel1.GridWidth
                            || adjY >= graphicsPanel1.GridHeight
                            || adjX < 0
                            || adjY < 0
                            )
                        {
                            continue;
                        }

                        if (Program.universe[x, y].Active)
                        {
                            Program.universe[adjX, adjY].AdjacentCount++;
                        }
                        else
                        {
                            Program.universe[adjX, adjY].AdjacentCount--;
                        }
                    }

                    // Tell Windows you need to repaint
                    graphicsPanel1.Invalidate();
                }
            }
        }

        private void SliderButton1_MouseDown(object sender, MouseEventArgs e)
        {
            sliderButton1.XOff = PointToClient(Cursor.Position).X - splitContainer1.SplitterDistance;
            sliderButton1.Sliding = true;
        }

        private void SliderButton1_MouseUp(object sender, MouseEventArgs e)
        {
            sliderButton1.XOff = 0;
            sliderButton1.Sliding = false;
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            int mouseX = PointToClient(Cursor.Position).X;

            if (sliderButton1.Sliding)
            {
                if (mouseX - sliderButton1.XOff >= 1)
                {
                    splitContainer1.SplitterDistance = mouseX - sliderButton1.XOff;
                    graphicsPanel1.RecalcY(ref splitContainer1);

                    splitContainer1.Panel2.Invalidate();
                    graphicsPanel1.Invalidate();
                }
            }
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            graphicsPanel1.RecalcY(ref splitContainer1);
            graphicsPanel1.RecalcCellSize(ref splitContainer1);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Program.Tick();
            graphicsPanel1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < graphicsPanel1.GridWidth; i++)
            {
                for (int j = 0; j < graphicsPanel1.GridHeight; j++)
                {
                    Program.universe[i, j].Active = false;
                    Program.universe[i, j].AdjacentCount = 0;
                }
            }

            graphicsPanel1.Invalidate();
        }
    }
}
