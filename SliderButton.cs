using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        /*public void sliderButton_MouseClick(object sender, MouseEventArgs e)
        {
            xOff = 0;
            sliding = true;

            splitObj.SplitterDistance += e.Location.X;
        }*/
    }
}
