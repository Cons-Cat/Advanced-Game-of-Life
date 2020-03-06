using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOLSource
{
    class GraphicsPanel : Panel
    {
        public float YOff { get; set; }
        public float CellSize { get; set; }
        public int GridHeight { get; set; }
        public int GridWidth { get; set; }

        // Default constructor
        public GraphicsPanel()
        {
            YOff = 0;
            GridHeight = 5;
            GridWidth = 5;

            // Turn on double buffering.
            DoubleBuffered = true;

            // Allow repainting when the window is resized.
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void RecalcY(ref SplitContainer argContainer)
        {
            YOff = (argContainer.Panel2.Height - (CellSize * GridHeight)) / 2;
        }

        public void RecalcCellSize(ref SplitContainer argContainer)
        {
            CellSize = Math.Min((float)argContainer.Panel2.Width / Program.universe.GetLength(0), (float)argContainer.Panel2.Height / Program.universe.GetLength(1));
        }
    }
}
