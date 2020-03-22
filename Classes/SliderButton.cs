using System.Drawing;
using System.Windows.Forms;

namespace GOLSource
{
    class SliderButton : Button
    {
        public bool Sliding { get; set; }
        public int XOff { get; set; }
        public int XStart { get; set; }
        public int ClickCount { get; set; }
        public int MoveState { get; set; }
        public int SubTicks { get; set; }
        public int MoveDist { get; set; }
        public double MovePercent { get; set; }

        public SplitContainer SplitObj { get; set; }
        public int XMoveFrom { get; set; }

        // Default constructor
        public SliderButton(int argWidth) : base()
        {
            Sliding = false;
            XOff = 0;
            XStart = 68;
            ClickCount = 0;
            MoveState = 0;
            SubTicks = 0;

            Location = new Point(
                 XStart,
                 0
            );
        }
    }
}
