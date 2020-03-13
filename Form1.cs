using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace GOLSource
{
    public partial class Form1 : Form
    {
        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        System.Windows.Forms.Timer sliderTimer = new System.Windows.Forms.Timer();

        // Grid state
        uint gridShape = 0; // 0 - Square, 1 - Hexagon

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            sliderTimer.Interval = 100; // milliseconds
            sliderTimer.Tick += SliderTick;
            sliderTimer.Enabled = true; // start timer running
        }

        // Calculate the next generation of cells
        public void UpdateTicks(int argTicks)
        {
            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + argTicks.ToString();
        }

        private void GraphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Prevent initialized CellSize of 0.
            if (graphicsPanel1.CellSize == 0)
            {
                graphicsPanel1.Width = ClientRectangle.Width - flowLayoutPanel1.Width;

                graphicsPanel1.Location = new Point(
                     flowLayoutPanel1.Width,
                     0
                );

                graphicsPanel1.UpdateGrid(ClientRectangle.Width, ClientRectangle.Height - statusStrip1.Height, ref flowLayoutPanel1);
            }

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // A rectangle to represent each cell in pixels
            RectangleF cellRect = RectangleF.Empty;
            //PointF p1 = new PointF();
            //PointF p2 = new PointF();
            //PointF p3 = new Point();
            //PointF p4 = new PointF();
            //PointF p5 = new PointF();
            //PointF p6 = new PointF();
            PointF[] cellHex = new PointF[6];

            // Iterate through the universe in the x, left to right
            for (int x = 0; x < graphicsPanel1.GridWidth; x++)
            {
                // Iterate through the universe in the y, top to bottom
                for (int y = 0; y < graphicsPanel1.GridHeight; y++)
                {
                    if (gridShape == 0)
                    {
                        cellRect.X = (x * graphicsPanel1.CellSize);
                        cellRect.Y = (y * graphicsPanel1.CellSize) + graphicsPanel1.YOff;

                        cellRect.Width = graphicsPanel1.CellSize;
                        cellRect.Height = graphicsPanel1.CellSize;
                    }
                    else
                    {
                        //float height = graphicsPanel1.HexRadius;
                        //float width = HexWidth(height);

                        float hexX = (x + 0.5F) * graphicsPanel1.HexRadius * 2 + ((y % 2) * (graphicsPanel1.HexRadius));
                        float hexY = (y + 0.85F) * graphicsPanel1.HexRadius * 1.75F + graphicsPanel1.YOff;

                        for (int i = 0; i < 6; i++)
                        {
                            cellHex[i] = new PointF(
                                hexX + graphicsPanel1.HexRadius * 1.15F * (float)Math.Cos((i * 60 + 30) * Math.PI / 180F),
                                hexY + graphicsPanel1.HexRadius * 1.15F * (float)Math.Sin((i * 60 + 30) * Math.PI / 180F)
                            );
                        }
                    }

                    // Fill the cell with a brush if alive
                    if (Program.universe[x, y].Active)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    if (gridShape == 0)
                    {
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    }
                    else
                    {
                        e.Graphics.DrawPolygon(gridPen, cellHex);
                    }

                    TextFormatFlags flags = TextFormatFlags.HorizontalCenter |
                    TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;

                    if (graphicsPanel1.CellSize > 12)
                    {
                        TextRenderer.DrawText(e.Graphics, $"{Program.universe[x, y].AdjacentCount}", this.Font, Rectangle.Round(cellRect), SystemColors.ControlText, flags);
                    }
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        // Return the width of a hexagon.
        //private float HexWidth(float argHeight)
        //{
        //    return (float)(4 * (argHeight / 2 / Math.Sqrt(3)));
        //}

        private void GraphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Program.playing)
            {
                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = (int)(e.X / graphicsPanel1.CellSize);
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = (int)((e.Y - graphicsPanel1.YOff) / graphicsPanel1.CellSize);

                if (x >= 0 && y >= 0 && x < graphicsPanel1.GridWidth && y < graphicsPanel1.GridHeight)
                {
                    if (
                        (e.Button == MouseButtons.Left && !Program.universe[x, y].Active)
                        || (e.Button == MouseButtons.Right && Program.universe[x, y].Active)
                        )
                    {
                        // If the left mouse button was clicked
                        if (e.Button == MouseButtons.Left)
                        {
                            // Enable cell.
                            Program.universe[x, y].Active = true;
                        }
                        if (e.Button == MouseButtons.Right)
                        {
                            // Disable cell.
                            Program.universe[x, y].Active = false;
                        }

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
        }

        private void SliderButton1_MouseDown(object sender, MouseEventArgs e)
        {
            sliderButton1.Sliding = true;
            sliderButton1.XOff = PointToClient(Cursor.Position).X - flowLayoutPanel1.Width;

            sliderButton1.MoveState = 0;
            sliderButton1.SubTicks = 0;
            sliderButton1.ClickCount++;
        }

        private void SliderButton1_MouseUp(object sender, MouseEventArgs e)
        {
            if (sliderButton1.Sliding)
            {
                if (flowLayoutPanel1.Width > 0 && flowLayoutPanel1.Width < sliderButton1.XStart)
                {
                    //sliderButton1.MoveState = 1;
                    //sliderButton1.MovePercent = 0;
                    //sliderButton1.MoveDist = flowLayoutPanel1.Width;
                }
            }

            sliderButton1.XOff = 0;
            sliderButton1.Sliding = false;

            if (sliderButton1.ClickCount == 2)
            {
                sliderButton1.ClickCount = 0;
                sliderButton1.MoveState = 1;
                sliderButton1.MovePercent = 0;

                sliderButton1.XMoveFrom = flowLayoutPanel1.Width;

                if (flowLayoutPanel1.Width == sliderButton1.XStart)
                {
                    // Fold.
                    sliderButton1.MoveDist = sliderButton1.XStart;
                }
                else
                {
                    // To origin.
                    sliderButton1.MoveDist = sliderButton1.XMoveFrom - sliderButton1.XStart;
                }
            }
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            int mouseX = PointToClient(Cursor.Position).X;

            if (sliderButton1.Sliding)
            {
                sliderButton1.ClickCount = 0;

                if (mouseX - sliderButton1.XOff >= 1)
                {
                    flowLayoutPanel1.Width = mouseX - sliderButton1.XOff;

                    graphicsPanel1.Location = new Point(
                         flowLayoutPanel1.Width,
                         0
                    );

                    UpdatePanels();
                }
            }
        }

        // The event called by the timer every Interval milliseconds.
        private void SliderTick(object sender, EventArgs e)
        {
            if (sliderButton1.SubTicks++ >= 10) // Every 10 ticks (1 second).
            {
                sliderButton1.SubTicks = 0;
                sliderButton1.ClickCount = 0;
            }

            if (sliderButton1.MoveState == 1)
            {
                if (sliderButton1.MovePercent < 1)
                {
                    sliderButton1.MovePercent += 0.1;
                }
                else
                {
                    sliderButton1.MovePercent = 1;
                    sliderButton1.MoveState = 0;
                }

                flowLayoutPanel1.Width = sliderButton1.XMoveFrom - (int)(sliderButton1.MoveDist * (Math.Pow(sliderButton1.MovePercent - 1, 3) + 1));

                graphicsPanel1.Location = new Point(
                    flowLayoutPanel1.Width,
                    0
                );

                UpdatePanels();
            }
        }

        private void UpdatePanels()
        {
            graphicsPanel1.Width = ClientRectangle.Width - flowLayoutPanel1.Width;
            graphicsPanel1.UpdateGrid(ClientRectangle.Width, ClientRectangle.Height - statusStrip1.Height, ref flowLayoutPanel1);

            flowLayoutPanel1.Update();
            graphicsPanel1.Update();
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            graphicsPanel1.Width = ClientRectangle.Width - flowLayoutPanel1.Width;

            graphicsPanel1.Location = new Point(
                 flowLayoutPanel1.Width,
                 0
            );

            graphicsPanel1.UpdateGrid(ClientRectangle.Width, ClientRectangle.Height - statusStrip1.Height, ref flowLayoutPanel1);
        }

        // Discrete tick.
        private void Button1_Click(object sender, EventArgs e)
        {
            if (!Program.playing)
            {
                Program.Tick();
                graphicsPanel1.Invalidate();
            }
        }

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

        // Loop ticks.
        private void button3_Click(object sender, EventArgs e)
        {
            Program.playing = !Program.playing;
        }
        public void UpdateLoop()
        {
            graphicsPanel1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

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

            graphicsPanel1.Invalidate();
        }
    }
}
