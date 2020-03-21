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
        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        System.Windows.Forms.Timer sliderTimer = new System.Windows.Forms.Timer();

        // Grid state
        uint gridShape = 0; // 0 - Square, 1 - Hexagon
        string cellText;

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

                graphicsPanel1.UpdateGrid(ClientRectangle.Height - statusStrip1.Height, gridShape);
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
                        float hexY = (y + 0.5F) * graphicsPanel1.HexRadius * 1.75F + graphicsPanel1.YOff;

                        for (int i = 0; i < 6; i++)
                        {
                            cellHex[i] = new PointF(
                                hexX + graphicsPanel1.HexRadius * 1.15F * (float)Math.Cos((i * 60 + 30) * Math.PI / 180F),
                                hexY + graphicsPanel1.HexRadius * 1.15F * (float)Math.Sin((i * 60 + 30) * Math.PI / 180F)
                            );
                        }

                        Program.universe[x, y].hexPoly = cellHex;
                        Program.universe[x, y].hexPoint = new PointF(hexX, hexY);
                    }

                    TextFormatFlags flags = TextFormatFlags.HorizontalCenter |
                    TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
                    cellText = $"{Program.universe[x, y].AdjacentCount}";

                    // Outline the cell with a pen
                    if (gridShape == 0)
                    {
                        if (Program.universe[x, y].Active)
                        {
                            e.Graphics.FillRectangle(cellBrush, cellRect);
                        }

                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                        if (graphicsPanel1.CellSize > 12)
                        {
                            TextRenderer.DrawText(e.Graphics, cellText, this.Font, Rectangle.Round(cellRect), SystemColors.ControlText, flags);
                        }
                    }
                    else
                    {
                        if (Program.universe[x, y].Active)
                        {
                            e.Graphics.FillPolygon(cellBrush, cellHex);
                        }

                        e.Graphics.DrawPolygon(gridPen, cellHex);

                        if (graphicsPanel1.HexRadius > 6)
                        {
                            SizeF textSize = e.Graphics.MeasureString(cellText, this.Font);

                            e.Graphics.DrawString(cellText, this.Font, new SolidBrush(Color.Black), ((x + 0.5F) * 2F + (y % 2)) * graphicsPanel1.HexRadius - (textSize.Width / 2) + 1, (y + 0.5F) * graphicsPanel1.HexRadius * 1.75F - (textSize.Height / 2) + graphicsPanel1.YOff + 1);
                        }
                    }
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        public static bool IsInPolygon(PointF[] poly, Point point)
        {
            var coef = poly.Skip(1).Select((p, i) =>
                                            (point.Y - poly[i].Y) * (p.X - poly[i].X)
                                          - (point.X - poly[i].X) * (p.Y - poly[i].Y))
                                    .ToList();

            if (coef.Any(p => p == 0))
                return true;

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                    return false;
            }

            return true;
        }

        private void GraphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Program.playing)
            {
                int x = 0;
                int y = 0;

                // Calculate the cell that was clicked in
                if (gridShape == 0)
                {
                    // CELL X = MOUSE X / CELL WIDTH
                    x = (int)(e.X / graphicsPanel1.CellSize);
                    // CELL Y = MOUSE Y / CELL HEIGHT
                    y = (int)((e.Y - graphicsPanel1.YOff) / graphicsPanel1.CellSize);
                }
                else if (gridShape == 1)
                {
                    x = (int)(e.X / (graphicsPanel1.HexRadius * 2F));
                    y = (int)(e.Y / (graphicsPanel1.HexRadius * 1.75F));

                    int ax = x - 1;
                    int ay = y - 1;

                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            if (ax >= 0 && ay >= 0 && ax + i < Program.universe.GetLength(0) && ay + j < Program.universe.GetLength(1))
                            {
                                if (IsInPolygon(Program.universe[ax + i, ay + j].hexPoly, e.Location))
                                {
                                    x = ax + i;
                                    y = ay + j;

                                    break;
                                }
                            }
                        }
                    }
                }

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

                        Program.universe[x, y].CountAdjacent(x, y, gridShape, graphicsPanel1.GridWidth, graphicsPanel1.GridHeight);
                    }
                }

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
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
            sliderButton1.SubTicks = 0;

            if (sliderButton1.ClickCount == 2)
            {
                //sliderButton1.ClickCount = 0;
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
            sliderButton1.SubTicks++;
            Debug.WriteLine(sliderButton1.ClickCount);

            if (sliderButton1.SubTicks >= 50)
            {
                sliderButton1.SubTicks = 0;
                sliderButton1.ClickCount = 0;
            }

            if (sliderButton1.MoveState == 1)
            {
                sliderButton1.ClickCount = 2;

                if (sliderButton1.MovePercent < 1)
                {
                    sliderButton1.MovePercent += 0.1;
                }
                else
                {
                    sliderButton1.MovePercent = 1;
                    sliderButton1.MoveState = 0;
                    sliderButton1.ClickCount = 0;
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
            graphicsPanel1.UpdateGrid(ClientRectangle.Height - statusStrip1.Height, gridShape);

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

            graphicsPanel1.UpdateGrid(ClientRectangle.Height - statusStrip1.Height, gridShape);
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
