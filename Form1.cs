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

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            int cellSize = Math.Min(splitContainer1.Panel2.Width / Program.universe.GetLength(0), splitContainer1.Panel2.Height / Program.universe.GetLength(1));

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < Program.universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < Program.universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellSize;
                    cellRect.Y = y * cellSize;
                    cellRect.Width = cellSize;
                    cellRect.Height = cellSize;

                    // Fill the cell with a brush if alive
                    if (Program.universe[x, y].Active)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    //TextRenderer.DrawText(e.Graphics, "0", this.Font, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height, SystemColors.ControlText);
                    TextRenderer.DrawText(e.Graphics, $"{Program.universe[x, y].AdjacentCount}", this.Font, new Point(cellRect.X, cellRect.Y), SystemColors.ControlText);
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellSize = Math.Min(splitContainer1.Panel2.Width / Program.universe.GetLength(0), splitContainer1.Panel2.Height / Program.universe.GetLength(1));

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellSize;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellSize;

                if (x < Program.universe.GetLength(0) && y < Program.universe.GetLength(1))
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
                            || adjX >= 5
                            || adjY >= 5
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

        private void sliderButton1_MouseDown(object sender, MouseEventArgs e)
        {
            sliderButton1.xOff = PointToClient(Cursor.Position).X - splitContainer1.SplitterDistance;
            sliderButton1.sliding = true;
        }

        private void sliderButton1_MouseUp(object sender, MouseEventArgs e)
        {
            sliderButton1.xOff = 0;
            sliderButton1.sliding = false;
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            int mouseX = PointToClient(Cursor.Position).X;

            if (sliderButton1.sliding)
            {
                if (mouseX - sliderButton1.xOff >= 1)
                {
                    splitContainer1.SplitterDistance += mouseX - sliderButton1.xOff - splitContainer1.SplitterDistance;
                    //graphicsPanel1.Invalidate();
                }
            }
        }
    }
}
