using System.Windows.Forms;

namespace GOLSource
{
    class SliderButton : Button
    {
        public int XOff { get; set; }
        public bool Sliding { get; set; }
        public SplitContainer SplitObj { get; set; }

        // Default constructor
        public SliderButton() : base()
        {
            XOff = 0;
            Sliding = false;

        }
    }
}
