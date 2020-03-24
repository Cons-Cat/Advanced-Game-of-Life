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
        Color gridColor;
        Color cellColor;

        // The Timer class
        System.Windows.Forms.Timer sliderTimer = new System.Windows.Forms.Timer();

        // Grid state
        uint gridShape = 0; // 0 - Square, 1 - Hexagon
        uint cellCount;
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
            cellCount = 0;

            // Setup the timer
            sliderTimer.Interval = 100; // milliseconds
            sliderTimer.Tick += SliderTick;
            sliderTimer.Enabled = true; // start timer running

            // Initialize array
            slidingPanel[0] = flowLayoutPanelCore;
            slidingPanel[1] = flowLayoutPanelSettings;

            ReloadSettings();

            UpdateSliderPanel();
            UpdateMainBar();
        }

        // Set settings to default values.
        private void ResetSettings()
        {
            gridColor = Color.Black;
            cellColor = Color.Gray;

            GameSpeed = 100;
            hudScale = 1;

            drawLines = true;
            drawAdjacent = true;
            drawHud = true;

            statusStrip1.Visible = true;
            toolStripStatusLabelTickRate.Text = $"Tick Speed (ms) = {GameSpeed}";

            Program.ReSizeUniverse(25, 25);
            UpdatePanels();
        }

        private void ReloadSettings()
        {
            gridColor = Properties.Settings.Default.gridColor;
            cellColor = Properties.Settings.Default.cellColor;

            GameSpeed = Properties.Settings.Default.GameSpeed;

            drawLines = Properties.Settings.Default.drawLines;
            drawAdjacent = Properties.Settings.Default.drawAdjacent;

            drawHud = Properties.Settings.Default.drawHud;
            statusStrip1.Visible = drawHud;
            hudScale = drawHud ? 1 : 0;

            toolStripStatusLabelTickRate.Text = $"Tick Speed (ms) = {GameSpeed}";

            Program.ReSizeUniverse(Properties.Settings.Default.gridWidth, Properties.Settings.Default.gridHeight);
            UpdatePanels();
        }

        // Calculate the next generation of cells
        public void UpdateTicksLabel(int argTicks)
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

        // Update cell count
        private void UpdateCellCountLabel()
        {
            toolStripStatusLabelCells.Text = $"Cell Count = {cellCount}";
        }

        private void UpdatePanels()
        {
            graphicsPanel1.Width = ClientRectangle.Width;
            graphicsPanel1.UpdateGridOffset(ClientSize.Width - panel1.Width, ClientRectangle.Height - (statusStrip1.Height * hudScale), gridShape);
            UpdateSizeLabel(Program.universe.GetLength(0), Program.universe.GetLength(1));

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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.gridColor = gridColor;
            Properties.Settings.Default.cellColor = cellColor;
            Properties.Settings.Default.GameSpeed = GameSpeed;
            Properties.Settings.Default.drawAdjacent = drawAdjacent;
            Properties.Settings.Default.drawHud = drawHud;
            Properties.Settings.Default.drawLines = drawLines;
            Properties.Settings.Default.gridWidth = Program.universe.GetLength(0);
            Properties.Settings.Default.gridHeight = Program.universe.GetLength(1);

            Properties.Settings.Default.Save();
        }
    }
}
