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
        // Mouse co-ordinate holders used for the context form.
        int tempEX;
        int tempEY;

        // Variables related to grid input.
        int curX;
        int curY;
        int tempX;
        int tempY;
        int incLineX = -1;
        int incLineY = -1;
        bool offGrid = false;
        bool fixOffGridLine = false;

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

                graphicsPanel1.UpdateGridOffset(ClientSize.Width - panel1.Width, ClientRectangle.Height - (statusStrip1.Height * hudScale), gridShape);
            }

            // A pen for drawing the HUD.
            Pen hudPen = new Pen(Color.FromArgb(122, 153, 0, 153), 2);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);
            Brush emptyBrush = new SolidBrush(backColor);

            // A rectangle to represent each cell in pixels
            RectangleF cellRect = RectangleF.Empty;
            PointF[] cellHex = new PointF[6];

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;

            // Iterate through the universe in the x, left to right
            for (int x = 0; x < Program.universe.GetLength(0); x++)
            {
                // Iterate through the universe in the y, top to bottom
                for (int y = 0; y < Program.universe.GetLength(1); y++)
                {
                    // Calculate the co-ordinates of each cell.
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

                    cellText = $"{Program.universe[x, y].AdjacentCount}";

                    // Draw each cell.
                    if (gridShape == 0)
                    {
                        if (Program.universe[x, y].Active)
                        {
                            e.Graphics.FillRectangle(cellBrush, cellRect);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(emptyBrush, cellRect);
                        }

                        if (drawLines)
                        {
                            e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                        }

                        if (drawAdjacent && Program.universe[x, y].AdjacentCount > 0)
                        {
                            if (graphicsPanel1.CellSize > 12)
                            {
                                TextRenderer.DrawText(e.Graphics, cellText, this.Font, Rectangle.Round(cellRect), SystemColors.ControlText, flags);
                            }
                        }
                    }
                    else
                    {
                        if (Program.universe[x, y].Active)
                        {
                            e.Graphics.FillPolygon(cellBrush, cellHex);
                        }
                        else
                        {
                            e.Graphics.FillPolygon(emptyBrush, cellHex);
                        }

                        if (drawLines)
                        {
                            e.Graphics.DrawPolygon(gridPen, cellHex);
                        }

                        if (drawAdjacent && Program.universe[x, y].AdjacentCount > 0)
                        {
                            if (graphicsPanel1.HexRadius > 6)
                            {
                                SizeF textSize = e.Graphics.MeasureString(cellText, this.Font);

                                e.Graphics.DrawString(cellText, this.Font, new SolidBrush(Color.Black), ((x + 0.5F) * 2F + (y % 2)) * graphicsPanel1.HexRadius - (textSize.Width / 2) + 1 + graphicsPanel1.XOff, (y + 0.5F) * graphicsPanel1.HexRadius * 1.75F - (textSize.Height / 2) + graphicsPanel1.YOff + 1);
                            }
                        }
                    }
                }
            }

            // Draw HUD
            if (drawHud)
            {
                flags = TextFormatFlags.Left |
                TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
                TextRenderer.DrawText(e.Graphics, "Universe Size: {" + $"{Program.universe.GetLength(0)}, {Program.universe.GetLength(1)}" + '}', this.Font, new Point(graphicsPanel1.XOff, graphicsPanel1.Location.Y + graphicsPanel1.Height - 20), hudPen.Color, flags);
                string wrapStr = "Finite";
                TextRenderer.DrawText(e.Graphics, $"Boundary Type: {wrapStr}", this.Font, new Point(graphicsPanel1.XOff, graphicsPanel1.Location.Y + graphicsPanel1.Height - 30), hudPen.Color, flags);
                TextRenderer.DrawText(e.Graphics, $"Cell Count: {cellCount}", this.Font, new Point(graphicsPanel1.XOff, graphicsPanel1.Location.Y + graphicsPanel1.Height - 40), hudPen.Color, flags);
                TextRenderer.DrawText(e.Graphics, $"Generations: {generationsCount}", this.Font, new Point(graphicsPanel1.XOff, graphicsPanel1.Location.Y + graphicsPanel1.Height - 50), hudPen.Color, flags);
            }

            // Cleaning up pens and brushes
            hudPen.Dispose();
            gridPen.Dispose();
            cellBrush.Dispose();
            emptyBrush.Dispose();
        }

        public static bool IsInPolygon(PointF[] poly, Point point)
        {
            var coef = poly.Skip(1).Select((p, i)
            => (point.Y - poly[i].Y) * (p.X - poly[i].X)
            - (point.X - poly[i].X) * (p.Y - poly[i].Y)
            ).ToList();

            if (coef.Any(p => p == 0))
            {
                return true;
            }

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                {
                    return false;
                }
            }

            return true;
        }

        // Clicking on a single cell.
        private void graphicsPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateMouseGridCoords(e);

            if (curX < 0) { incLineX = 0; }
            if (curY < 0) { incLineY = 0; }
            if (curX > Program.universe.GetLength(0) - 1) { incLineX = Program.universe.GetLength(0) - 1; }
            if (curY > Program.universe.GetLength(1) - 1) { incLineY = Program.universe.GetLength(1) - 1; }

            if (curX >= 0 && curY >= 0 && curX < Program.universe.GetLength(0) && curY < Program.universe.GetLength(1))
            {
                if (e.Button == MouseButtons.Left)
                {
                    CellInput(e);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    tempEX = e.X;
                    tempEY = e.Y;
                }
            }
            else
            {
                offGrid = true;
            }
        }

        // Open context menu.
        private void graphicsPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            fixOffGridLine = false;
            incLineX = -1;
            incLineY = -1;

            if (e.Button == MouseButtons.Right)
            {
                if (e.X == tempEX && e.Y == tempEY)
                {
                    // If the right mouse button is released without dragging.
                    rightClickMenuStrip.Show(this, new Point(e.X + slidingPanel[panelInd].Width, e.Y));
                }
            }
        }

        // Painting across multiple cells.
        private void GraphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            CellInput(e);
        }

        private void UpdateMouseGridCoords(MouseEventArgs e)
        {
            // Calculate the cell that was clicked in.
            if (gridShape == 0)
            {
                // CELL X = MOUSE X / CELL WIDTH
                curX = (int)((e.X - graphicsPanel1.XOff) / graphicsPanel1.CellSize);
                // CELL Y = MOUSE Y / CELL HEIGHT
                curY = (int)((e.Y - graphicsPanel1.YOff) / graphicsPanel1.CellSize);
            }
            else if (gridShape == 1)
            {
                curY = (int)((e.Y - graphicsPanel1.YOff) / (graphicsPanel1.HexRadius * 1.75F));
                curX = (int)((e.X - graphicsPanel1.XOff - ((curY % 2) * graphicsPanel1.HexRadius)) / (graphicsPanel1.HexRadius * 2F));

                int ax = curX - 1;
                int ay = curY - 1;

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (ax >= 0 && ay >= 0 && ax + i < Program.universe.GetLength(0) && ay + j < Program.universe.GetLength(1))
                        {
                            if (IsInPolygon(Program.universe[ax + i, ay + j].hexPoly, e.Location))
                            {
                                curX = ax + i;
                                curY = ay + j;

                                break;
                            }
                        }
                    }
                }
            }
        }

        public void CellInput(MouseEventArgs e)
        {
            if (!Program.playing)
            {
                UpdateMouseGridCoords(e);

                if (curX >= 0 && curY >= 0 && curX < Program.universe.GetLength(0) && curY < Program.universe.GetLength(1))
                {
                    offGrid = false;
                }

                if (!offGrid)
                {
                    if (
                        (e.Button == MouseButtons.Left)
                        || (e.Button == MouseButtons.Right)
                        )
                    {
                        bool active = true;

                        if (e.Button == MouseButtons.Left)
                        {
                            active = true;
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            active = false;
                        }

                        // Prevent erratic line from being drawn when
                        // the mouse leaves the grid, then comes back
                        // inside.
                        if (fixOffGridLine)
                        {
                            tempX = curX;
                            tempY = curY;

                            if (incLineX != -1)
                            {
                                tempX = incLineX;
                            }
                            if (incLineY != -1)
                            {
                                tempY = incLineY;
                            }

                            fixOffGridLine = false;
                        }

                        incLineX = -1;
                        incLineY = -1;

                        // Clamp co-ordinates.
                        if (curX < 0) { curX = 0; offGrid = true; incLineX = 0; }
                        if (curY < 0) { curY = 0; offGrid = true; incLineY = 0; }
                        if (curX > Program.universe.GetLength(0) - 1) { curX = Program.universe.GetLength(0) - 1; offGrid = true; incLineX = Program.universe.GetLength(0) - 1; }
                        if (curY > Program.universe.GetLength(1) - 1) { curY = Program.universe.GetLength(1) - 1; offGrid = true; incLineY = Program.universe.GetLength(1) - 1; }

                        fixOffGridLine = offGrid;

                        // Assign cell state.
                        if (curX == tempX)
                        {
                            Program.universe[curX, curY].Active = active;
                        }
                        else
                        {
                            InterpolateCells(curX, curY, tempX, tempY, active);
                        }

                        //Program.universe[curX, curY].CountAdjacent(curX, curY, gridShape, Program.universe.GetLength(0), Program.universe.GetLength(1));
                        UpdateGrid();
                        UpdateCellCountLabel();
                    }

                    tempX = curX;
                    tempY = curY;
                }

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void InterpolateCells(int x0, int y0, int x1, int y1, bool argActive)
        {
            int dx = Math.Abs(x1 - x0);
            int sx = x0 < x1 ? 1 : -1;
            int dy = -Math.Abs(y1 - y0);
            int sy = y0 < y1 ? 1 : -1;
            int err = dx + dy;
            int e2;

            while (true)
            {
                if (x0 < Program.universe.GetLength(0)
                    && y0 < Program.universe.GetLength(1)
                    && x0 >= 0
                    && y0 >= 0
                    )
                {
                    Program.universe[x0, y0].Active = argActive;
                }

                if (x0 == x1 && y0 == y1) { break; }

                e2 = 2 * err;

                if (e2 >= dy)
                {
                    err += dy;
                    x0 += sx;
                }
                if (e2 <= dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }

        private void UpdateGrid()
        {
            cellCount = 0;

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
                                Program.universe[i, j].CountAdjacent(i, j, gridShape, Program.universe.GetLength(0), Program.universe.GetLength(1));
                                cellCount++;
                            }
                        }
                    }
                }
            }

            UpdateCellCountLabel();
        }
    }
}
