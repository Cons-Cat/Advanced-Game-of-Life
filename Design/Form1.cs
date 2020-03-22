using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        public int seed { get; set; }
        static public int GameSpeed { get; set; }
        string cellText;

        // Sliding panels
        uint panelInd = 0;
        FlowLayoutPanel[] slidingPanel = new FlowLayoutPanel[2];

        public Form1()
        {
            InitializeComponent();
            seed = 0;
            GameSpeed = 100;
            TickLabel.Text = $"Tick Speed (ms) = {GameSpeed}";

            // Setup the timer
            sliderTimer.Interval = 100; // milliseconds
            sliderTimer.Tick += SliderTick;
            sliderTimer.Enabled = true; // start timer running

            // Initialize array
            slidingPanel[0] = flowLayoutPanelCore;
            slidingPanel[1] = flowLayoutPanelSettings;

            UpdateMainBar();
            UpdateSliderPanel();
        }

        // Calculate the next generation of cells
        public void UpdateTicks(int argTicks)
        {
            // Update status strip generations
            try
            {
                toolStripStatusLabelGenerations.Text = "Generations = " + argTicks.ToString();
            }
            catch (Exception ex)
            {
                // Visual Studio lies about an impossible
                // cross-threading error, so a Try-Catch
                // is necessary when executing the program
                // in Debug mode.

                // Compiled executables do not need this.
            }
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

        public void UpdateLoop()
        {
            graphicsPanel1.Invalidate();
        }
    }
}
