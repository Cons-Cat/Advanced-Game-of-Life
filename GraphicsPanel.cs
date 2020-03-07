using System;
using System.Windows.Forms;

namespace GOLSource
{
    public class GraphicsPanel : Panel
    {
        public float YOff { get; set; }
        public float CellSize { get; set; }
        public int GridHeight { get; set; }
        public int GridWidth { get; set; }

        // Default constructor
        public GraphicsPanel()
        {
            YOff = 0;
            GridHeight = Program.universe.GetLength(1);
            GridWidth = Program.universe.GetLength(0);

            // Turn on double buffering.
            DoubleBuffered = true;

            // Allow repainting when the window is resized.
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void Update(int argWidth, int argHeight, ref FlowLayoutPanel argPanelLeft)
        {
            Width = argWidth - argPanelLeft.Width;
            Height = argHeight;

            CellSize = Math.Min((float)Width / GridWidth, (float)Height / GridHeight);
            YOff = (Height - (CellSize * GridHeight)) / 2;

            if (CellSize < 10)
            {
                int a = 0;
            }
        }
    }
}
