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
        private void GraphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Prevent initialized CellSize of 0.
            if (graphicsPanel1.CellSize == 0)
            {
                graphicsPanel1.Width = ClientRectangle.Width - panel1.Width + graphicsPanel1.XOff;

                graphicsPanel1.Location = new Point(
                     flowLayoutPanelCore.Width,
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
            PointF[] cellHex = new PointF[6];

            // Iterate through the universe in the x, left to right
            for (int x = 0; x < graphicsPanel1.GridWidth; x++)
            {
                // Iterate through the universe in the y, top to bottom
                for (int y = 0; y < graphicsPanel1.GridHeight; y++)
                {
                    if (gridShape == 0)
                    {
                        cellRect.X = (x * graphicsPanel1.CellSize) + graphicsPanel1.XOff;
                        cellRect.Y = (y * graphicsPanel1.CellSize) + graphicsPanel1.YOff;

                        cellRect.Width = graphicsPanel1.CellSize;
                        cellRect.Height = graphicsPanel1.CellSize;
                    }
                    else
                    {
                        float hexX = (x + 0.5F) * graphicsPanel1.HexRadius * 2 + ((y % 2) * (graphicsPanel1.HexRadius));
                        float hexY = (y + 0.5F) * graphicsPanel1.HexRadius * 1.75F + graphicsPanel1.YOff;

                        for (int i = 0; i < 6; i++)
                        {
                            cellHex[i] = new PointF(
                                hexX + graphicsPanel1.HexRadius * 1.15F * (float)Math.Cos((i * 60 + 30) * Math.PI / 180F) + graphicsPanel1.XOff,
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

                            e.Graphics.DrawString(cellText, this.Font, new SolidBrush(Color.Black), ((x + 0.5F) * 2F + (y % 2)) * graphicsPanel1.HexRadius - (textSize.Width / 2) + 1 + graphicsPanel1.XOff, (y + 0.5F) * graphicsPanel1.HexRadius * 1.75F - (textSize.Height / 2) + graphicsPanel1.YOff + 1);
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
                    x = (int)((e.X - graphicsPanel1.XOff) / graphicsPanel1.CellSize);
                    // CELL Y = MOUSE Y / CELL HEIGHT
                    y = (int)((e.Y - graphicsPanel1.YOff) / graphicsPanel1.CellSize);
                }
                else if (gridShape == 1)
                {
                    x = (int)((e.X - graphicsPanel1.XOff) / (graphicsPanel1.HexRadius * 2F));
                    y = (int)((e.Y - graphicsPanel1.YOff) / (graphicsPanel1.HexRadius * 1.75F));

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
    }
}
