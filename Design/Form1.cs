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
        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        System.Windows.Forms.Timer sliderTimer = new System.Windows.Forms.Timer();

        // Grid state
        uint gridShape = 0; // 0 - Square, 1 - Hexagon
        string cellText;

        // Sliding panels
        uint panelInd = 0;
        FlowLayoutPanel[] slidingPanel = new FlowLayoutPanel[2];

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            sliderTimer.Interval = 100; // milliseconds
            sliderTimer.Tick += SliderTick;
            sliderTimer.Enabled = true; // start timer running

            // Initialize array
            slidingPanel[0] = flowLayoutPanelCore;
            slidingPanel[1] = flowLayoutPanelSettings;

            UpdateSliderPanel();

            for (int i = 0; i < slidingPanel.Length; i++)
            {
                slidingPanel[i].Location = new Point(slidingPanel[i].Location.X, panel1.Height);
                slidingPanel[i].Height = ClientRectangle.Height - panel1.Height - statusStrip1.Height;
            }
        }

        // Calculate the next generation of cells
        public void UpdateTicks(int argTicks)
        {
            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + argTicks.ToString();
        }

        private void UpdatePanels()
        {
            graphicsPanel1.Width = ClientRectangle.Width - panel1.Width + graphicsPanel1.XOff;
            graphicsPanel1.UpdateGrid(ClientRectangle.Height - statusStrip1.Height, gridShape);

            flowLayoutPanelCore.Update();
            graphicsPanel1.Update();
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            graphicsPanel1.Width = ClientRectangle.Width - panel1.Width + graphicsPanel1.XOff;

            graphicsPanel1.Location = new Point(
                 flowLayoutPanelCore.Width,
                 0
            );

            for (int i = 0; i < slidingPanel.Length; i++)
            {
                slidingPanel[i].Location = new Point(slidingPanel[i].Location.X, panel1.Height);
                slidingPanel[i].Height = ClientRectangle.Height - panel1.Height - statusStrip1.Height;
            }

            graphicsPanel1.UpdateGrid(ClientRectangle.Height - statusStrip1.Height, gridShape);
        }

        // Discrete tick.
        private void ButtonTick_Click(object sender, EventArgs e)
        {
            if (!Program.playing)
            {
                Program.Tick();
                graphicsPanel1.Invalidate();
            }
        }

        // Loop ticks.
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Program.playing = !Program.playing;
        }

        public void UpdateLoop()
        {
            graphicsPanel1.Invalidate();
        }

        private void buttonCore_Click(object sender, EventArgs e)
        {
            panelInd = 0;
            UpdateSliderPanel();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            panelInd = 1;
            UpdateSliderPanel();
        }

        private void UpdateSliderPanel()
        {
            for (int i = 0; i < slidingPanel.Length; i++)
            {
                if (i == panelInd)
                {
                    slidingPanel[i].Location = new Point(0, panel1.Height);
                    slidingPanel[i].Width = sliderButton1.Location.X + graphicsPanel1.Location.X;
                }
                else
                {
                    slidingPanel[i].Location = new Point(0 - slidingPanel[i].Width, panel1.Height);
                }

                slidingPanel[i].Height = ClientRectangle.Height - panel1.Height - statusStrip1.Height;

                slidingPanel[i].Update();
            }
        }
    }
}
