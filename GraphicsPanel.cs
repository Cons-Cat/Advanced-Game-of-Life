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
        // Default constructor
        public GraphicsPanel()
        {
            // Turn on double buffering.
            DoubleBuffered = true;

            // Allow repainting when the window is resized.
            SetStyle(ControlStyles.ResizeRedraw, true);
        }
    }
}
