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
        private void SliderButton1_MouseDown(object sender, MouseEventArgs e)
        {
            sliderButton1.Sliding = true;
            sliderButton1.XOff = PointToClient(Cursor.Position).X - flowLayoutPanel1.Width;

            sliderButton1.MoveState = 0;
            sliderButton1.SubTicks = 0;
            sliderButton1.ClickCount++;
        }

        private void SliderButton1_MouseUp(object sender, MouseEventArgs e)
        {
            sliderButton1.XOff = 0;
            sliderButton1.Sliding = false;
            sliderButton1.SubTicks = 0;

            if (sliderButton1.ClickCount == 2)
            {
                sliderButton1.MoveState = 1;
                sliderButton1.MovePercent = 0;

                sliderButton1.XMoveFrom = flowLayoutPanel1.Width;

                if (flowLayoutPanel1.Width == sliderButton1.XStart)
                {
                    // Fold.
                    sliderButton1.MoveDist = sliderButton1.XStart;
                }
                else
                {
                    // To origin.
                    sliderButton1.MoveDist = sliderButton1.XMoveFrom - sliderButton1.XStart;
                }
            }
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            int mouseX = PointToClient(Cursor.Position).X;

            if (sliderButton1.Sliding)
            {
                sliderButton1.ClickCount = 0;

                if (mouseX - sliderButton1.XOff >= 1)
                {
                    flowLayoutPanel1.Width = mouseX - sliderButton1.XOff;
                    UpdateMainBar();

                    graphicsPanel1.Location = new Point(
                         flowLayoutPanel1.Width,
                         0
                    );

                    UpdatePanels();
                }
            }
        }

        // The event called by the timer every Interval milliseconds.
        private void SliderTick(object sender, EventArgs e)
        {
            sliderButton1.SubTicks++;
            Debug.WriteLine(sliderButton1.ClickCount);

            if (sliderButton1.SubTicks >= 50)
            {
                sliderButton1.SubTicks = 0;
                sliderButton1.ClickCount = 0;
            }

            if (sliderButton1.MoveState == 1)
            {
                sliderButton1.ClickCount = 2;

                if (sliderButton1.MovePercent < 1)
                {
                    sliderButton1.MovePercent += 0.1;
                }
                else
                {
                    sliderButton1.MovePercent = 1;
                    sliderButton1.MoveState = 0;
                    sliderButton1.ClickCount = 0;
                }

                flowLayoutPanel1.Width = sliderButton1.XMoveFrom - (int)(sliderButton1.MoveDist * (Math.Pow(sliderButton1.MovePercent - 1, 3) + 1));
                UpdateMainBar();

                graphicsPanel1.Location = new Point(
                    flowLayoutPanel1.Width,
                    0
                );

                UpdatePanels();
            }
        }

        // Main Bar Update
        private void UpdateMainBar()
        {
            if (flowLayoutPanel1.Width >= 155)
            {
                panel1.Width = flowLayoutPanel1.Width;
                panel1.Update();
            }
            else if (flowLayoutPanel1.Width != 155)
            {
                panel1.Width = 155;
                panel1.Update();
            }

            graphicsPanel1.XOff = Math.Abs(panel1.Width - flowLayoutPanel1.Width);
            graphicsPanel1.Invalidate();
        }
    }
}
