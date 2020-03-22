using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        private void buttonNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream;

                for (int i = 0; i < Program.universe.GetLength(0); i++)
                {
                    for (int j = 0; j < Program.universe.GetLength(1); j++)
                    {
                        Program.universe[i, j].Active = false;
                    }
                }

                stream = saveFileDialog1.OpenFile();
                WriteFile(stream);
            }

            UpdateGrid();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream;
                stream = saveFileDialog1.OpenFile();

                WriteFile(stream);
            }
        }

        private void WriteFile(Stream argStream)
        {
            if (argStream != null)
            {
                // Write each directory name to a file.
                using (StreamWriter sw = new StreamWriter(argStream))
                {
                    for (int j = 0; j < Program.universe.GetLength(1); j++)
                    {
                        for (int i = 0; i < Program.universe.GetLength(0); i++)
                        {
                            sw.Write((Program.universe[i, j].Active ? 1 : 0).ToString());
                        }

                        sw.WriteLine();
                    }

                    argStream.Flush();
                }

                argStream.Close();
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        for (int j = 0; j < Program.universe.GetLength(1); j++)
                        {
                            for (int i = 0; i < Program.universe.GetLength(0); i++)
                            {
                                Program.universe[i, j].Active = (sr.Read() == '1' ? true : false);
                            }

                            sr.ReadLine();
                        }

                        sr.ReadToEnd();
                    }

                    UpdateGrid();
                    graphicsPanel1.Invalidate();
                }
            }
        }
    }
}
