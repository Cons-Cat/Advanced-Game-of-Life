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
        Color backColor;
        Color cellColor;
        Color gridColor;

        // The Timer class
        System.Windows.Forms.Timer sliderTimer = new System.Windows.Forms.Timer();

        // Grid state
        uint gridShape = 0; // 0 - Square, 1 - Hexagon
        uint cellCount;
        uint generationsCount;
        public int Seed { get; set; }
        public static int GameSpeed { get; set; }
        public bool isFinite;

        string cellText;
        bool drawLines;
        bool drawAdjacent;
        bool drawToolStrip;
        bool drawHud;
        int stripScale;

        // Sliding panels
        uint panelInd = 0;
        FlowLayoutPanel[] slidingPanel = new FlowLayoutPanel[2];

        public Form1()
        {
            InitializeComponent();
            Seed = 0;
            cellCount = 0;
            generationsCount = 0;

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
            backColor = Color.White;
            gridColor = Color.Black;
            cellColor = Color.Gray;

            GameSpeed = 100;
            isFinite = true;

            drawLines = true;
            drawAdjacent = true;
            drawHud = true;

            drawToolStrip = true;
            statusStrip1.Visible = true;
            stripScale = 1;

            toolStripStatusLabelTickRate.Text = $"Tick Speed (ms) = {GameSpeed}";

            Program.ReSizeUniverse(25, 25);
            UpdatePanels();
        }

        private void ReloadSettings()
        {
            backColor = Properties.Settings.Default.backColor;
            gridColor = Properties.Settings.Default.gridColor;
            cellColor = Properties.Settings.Default.cellColor;

            GameSpeed = Properties.Settings.Default.GameSpeed;
            isFinite = Properties.Settings.Default.isFinite;

            drawLines = Properties.Settings.Default.drawLines;
            drawAdjacent = Properties.Settings.Default.drawAdjacent;
            drawHud = Properties.Settings.Default.drawHud;

            drawToolStrip = Properties.Settings.Default.drawToolStrip;
            statusStrip1.Visible = drawToolStrip;
            stripScale = drawToolStrip ? 1 : 0;

            toolStripStatusLabelTickRate.Text = $"Tick Speed (ms) = {GameSpeed}";

            Program.ReSizeUniverse(Properties.Settings.Default.gridWidth, Properties.Settings.Default.gridHeight);
            UpdatePanels();
        }

        // Calculate the next generation of cells
        public void UpdateTicksLabel(uint argTicks)
        {
            // Update status strip generations
            try
            {
                generationsCount = argTicks;
                toolStripStatusLabelGenerations.Text = "Generations = " + argTicks.ToString();
            }
            catch (Exception ex)
            {
                // Visual Studio asserts a bizarrea sort of
                // cross-threading error, so a Try-Catch
                // is necessary when executing the program
                // in Debug mode.

                // Compiled executables do not need this.
            }
        }

        // Update cell count
        private void UpdateCellCountLabel()
        {
            try
            {
                toolStripStatusLabelCells.Text = $"Cell Count = {cellCount}";
            }
            catch (Exception ex)
            {
                // Visual Studio asserts a bizarrea sort of
                // cross-threading error, so a Try-Catch
                // is necessary when executing the program
                // in Debug mode.

                // Compiled executables do not need this.
            }
        }

        // This is called whenever a panel's dimensions or co-ordinates have changed.
        private void UpdatePanels()
        {
            graphicsPanel1.Width = ClientRectangle.Width;
            graphicsPanel1.UpdateGridOffset(ClientSize.Width - panel1.Width, ClientRectangle.Height - (statusStrip1.Height * stripScale), gridShape);
            UpdateSizeLabel(Program.universe.GetLength(0), Program.universe.GetLength(1));

            slidingPanel[panelInd].Height = ClientRectangle.Height - panel1.Height - (statusStrip1.Height * stripScale);
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
                slidingPanel[i].Height = ClientRectangle.Height - panel1.Height - (statusStrip1.Height * stripScale);
            }

            graphicsPanel1.UpdateGridOffset(ClientSize.Width - panel1.Width, ClientRectangle.Height - (statusStrip1.Height * stripScale), gridShape);
        }

        // This is called each tick.
        public void UpdateLoop()
        {
            UpdateGrid();
            graphicsPanel1.Invalidate();
        }

        // Save settings.
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.backColor = backColor;
            Properties.Settings.Default.gridColor = gridColor;
            Properties.Settings.Default.cellColor = cellColor;
            Properties.Settings.Default.GameSpeed = GameSpeed;
            Properties.Settings.Default.isFinite = isFinite;
            Properties.Settings.Default.drawAdjacent = drawAdjacent;
            Properties.Settings.Default.drawToolStrip = drawToolStrip;
            Properties.Settings.Default.drawHud = drawHud;
            Properties.Settings.Default.drawLines = drawLines;
            Properties.Settings.Default.gridWidth = Program.universe.GetLength(0);
            Properties.Settings.Default.gridHeight = Program.universe.GetLength(1);

            Properties.Settings.Default.Save();
            Environment.Exit(0);
        }
    }
}
