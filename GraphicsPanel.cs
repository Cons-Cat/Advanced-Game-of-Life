using System;
using System.Windows.Forms;

namespace GOLSource
{
    public class GraphicsPanel : Panel
    {
        public int XOff { get; set; }
        public float YOff { get; set; }
        public float CellSize { get; set; }
        public float HexRadius { get; set; }
        public int GridHeight { get; set; }
        public int GridWidth { get; set; }

        // Default constructor
        public GraphicsPanel()
        {
            XOff = 0;
            YOff = 0;
            GridHeight = Program.universe.GetLength(1);
            GridWidth = Program.universe.GetLength(0);

            // Turn on double buffering.
            DoubleBuffered = true;

            // Allow repainting when the window is resized.
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void UpdateGrid(int argHeight, uint argGridState)
        {
            //Width = argWidth - argPanelLeft.Width;
            Height = argHeight;

            CellSize = Math.Min((float)Width / GridWidth, (float)Height / GridHeight);
            HexRadius = Math.Min((float)Width / (GridWidth + 0.5F) / 2F, (float)Height / (GridHeight + 0.5F) / 1.75F);

            if (argGridState == 0)
            {
                YOff = (Height - (CellSize * GridHeight)) / 2;
            }
            else
            {
                YOff = (Height - (HexRadius * 1.75F * GridHeight)) / 2;
            }
        }
    }
}
