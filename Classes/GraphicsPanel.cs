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

        // Default constructor
        public GraphicsPanel()
        {
            XOff = 0;
            YOff = 0;

            // Turn on double buffering.
            DoubleBuffered = true;

            // Allow repainting when the window is resized.
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void UpdateGridOffset(int argWidth, int argHeight, uint argGridState)
        {
            Height = argHeight;

            CellSize = Math.Min((float)argWidth / Program.universe.GetLength(0), (float)Height / Program.universe.GetLength(1));
            HexRadius = Math.Min((float)argWidth / (Program.universe.GetLength(0) + 0.5F) / 2F, (float)Height / (Program.universe.GetLength(1) + 0.5F) / 1.75F);

            if (argGridState == 0)
            {
                YOff = (Height - (CellSize * Program.universe.GetLength(1))) / 2;
            }
            else
            {
                YOff = (Height - (HexRadius * 1.75F * Program.universe.GetLength(1))) / 2;
            }
        }
    }
}
