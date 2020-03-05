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
        public int xOff { get; set; }
        public bool sliding { get; set; }
        public SplitContainer splitObj { get; set; }

        // Default constructor
        public SliderButton() : base()
        {
            xOff = 0;
            sliding = false;

        }

        /*public void sliderButton_MouseClick(object sender, MouseEventArgs e)
        {
            xOff = 0;
            sliding = true;

            splitObj.SplitterDistance += e.Location.X;
        }*/
    }
}
