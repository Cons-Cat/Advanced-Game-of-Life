﻿using System;
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
        // The universe array
        bool[,] universe = new bool[5, 5];

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
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellSize;
                    cellRect.Y = y * cellSize;
                    cellRect.Width = cellSize;
                    cellRect.Height = cellSize;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
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

                if (x < universe.GetLength(0) && y < universe.GetLength(1))
                {
                    // Toggle the cell's state
                    universe[x, y] = !universe[x, y];

                    // Tell Windows you need to repaint
                    graphicsPanel1.Invalidate();
                }
            }
        }

        private void sliderButton_MouseClick(object sender, MouseEventArgs e)
        {
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
                }
            }
        }
    }
}
