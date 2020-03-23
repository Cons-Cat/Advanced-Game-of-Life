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
        public int Seed { get; set; }
        public static int GameSpeed { get; set; }

        string cellText;
        bool drawLines;
        bool drawAdjacent;
        bool drawHud;
        int hudScale;

        // Sliding panels
        uint panelInd = 0;
        FlowLayoutPanel[] slidingPanel = new FlowLayoutPanel[2];

        public Form1()
        {
            InitializeComponent();
            Seed = 0;
            GameSpeed = 100;
            toolStripStatusLabelTickRate.Text = $"Tick Speed (ms) = {GameSpeed}";
            hudScale = 1;

            // Setup the timer
            sliderTimer.Interval = 100; // milliseconds
            sliderTimer.Tick += SliderTick;
            sliderTimer.Enabled = true; // start timer running

            // Initialize array
            slidingPanel[0] = flowLayoutPanelCore;
            slidingPanel[1] = flowLayoutPanelSettings;

            drawLines = true;
            drawAdjacent = true;
            drawHud = true;

            UpdateSliderPanel();
            UpdateMainBar();
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
            graphicsPanel1.Width = ClientRectangle.Width;
            graphicsPanel1.UpdateGridOffset(ClientSize.Width - panel1.Width, ClientRectangle.Height - (statusStrip1.Height * hudScale), gridShape);

            sliderButton1.Location = new Point(sliderButton1.Location.X, graphicsPanel1.Height / 2 - sliderButton1.Height / 2);

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
                slidingPanel[i].Height = ClientRectangle.Height - panel1.Height - (statusStrip1.Height * hudScale);
            }

            graphicsPanel1.UpdateGridOffset(ClientSize.Width - panel1.Width, ClientRectangle.Height - (statusStrip1.Height * hudScale), gridShape);
        }

        public void UpdateLoop()
        {
            UpdateGrid();
            graphicsPanel1.Invalidate();
        }
    }
}
