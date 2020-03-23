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
        private void buttonNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)";
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
                    sw.WriteLine(Program.universe.GetLength(0));
                    sw.WriteLine(Program.universe.GetLength(1));

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
                    int w;
                    int h;

                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        w = int.Parse(sr.ReadLine());
                        h = int.Parse(sr.ReadLine());

                        Program.ReSizeUniverse(w, h);

                        for (int j = 0; j < h; j++)
                        {
                            for (int i = 0; i < w; i++)
                            {
                                Program.universe[i, j].Active = (sr.Read() == '1' ? true : false);
                            }

                            sr.ReadLine();
                        }

                        sr.ReadToEnd();
                    }

                    graphicsPanel1.UpdateGridOffset(w, h, gridShape);
                    UpdatePanels();
                }
            }
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
    }
}
