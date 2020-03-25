using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GOLSource
{
    public partial class Form1 : Form
    {
        private void buttonNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|Plaintext files (*.cells)|*.cells";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < Program.universe.GetLength(0); i++)
                {
                    for (int j = 0; j < Program.universe.GetLength(1); j++)
                    {
                        Program.universe[i, j].Active = false;
                    }
                }

                Stream stream;
                stream = saveFileDialog1.OpenFile();
                string ext = Path.GetExtension(saveFileDialog1.FileName).ToLower();

                if (ext == ".txt")
                {
                    WriteFileTXT(stream);
                }
                else if (ext == ".cells")
                {
                    WriteFileCELLS(stream, Path.GetFileNameWithoutExtension(saveFileDialog1.FileName));
                }
            }

            UpdateGrid();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|Plaintext files (*.cells)|*.cells";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream;
                stream = saveFileDialog1.OpenFile();
                string ext = Path.GetExtension(saveFileDialog1.FileName).ToLower();

                if (ext == ".txt")
                {
                    WriteFileTXT(stream);
                }
                else if (ext == ".cells")
                {
                    WriteFileCELLS(stream, Path.GetFileNameWithoutExtension(saveFileDialog1.FileName));
                }
            }
        }

        private void WriteFileTXT(Stream argStream)
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

        public void UpdateSizeLabel(int argWidth, int argHeight)
        {
            toolStripStatusLabelGrid.Text = "Universe Size = {" + $"{argWidth}, {argHeight}" + '}';
        }

        private void WriteFileCELLS(Stream argStream, string argName)
        {
            if (argStream != null)
            {
                // Write each directory name to a file.
                using (StreamWriter sw = new StreamWriter(argStream))
                {
                    sw.Write('!');
                    sw.WriteLine(argName);
                    sw.WriteLine('!');

                    for (int j = 0; j < Program.universe.GetLength(1); j++)
                    {
                        for (int i = 0; i < Program.universe.GetLength(0); i++)
                        {
                            sw.Write(Program.universe[i, j].Active ? 'O' : '.');
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
                openFileDialog.Filter = "txt files (*.txt)|*.txt|Plaintext files (*.cells)|*.cells";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();
                    string ext = Path.GetExtension(openFileDialog.FileName).ToLower();

                    int w = Program.universe.GetLength(0);
                    int h = Program.universe.GetLength(1);

                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        if (ext == ".txt")
                        {
                            w = int.Parse(sr.ReadLine());
                            h = int.Parse(sr.ReadLine());
                        }
                        else if (ext == ".cells")
                        {
                            w = Program.universe.GetLength(0);
                            h = Program.universe.GetLength(1);

                            // Skip first two lines.
                            sr.ReadLine();
                            sr.ReadLine();
                        }

                        Program.ReSizeUniverse(w, h);

                        for (int j = 0; j < h; j++)
                        {
                            for (int i = 0; i < w; i++)
                            {
                                if (ext == ".txt")
                                {
                                    Program.universe[i, j].Active = (sr.Read() == '1' ? true : false);
                                }
                                else if (ext == ".cells")
                                {
                                    char chr = (char)sr.Read();

                                    if (chr != '\n')
                                    {
                                        Program.universe[i, j].Active = (chr == 'O' ? true : false);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }

                            if (ext == ".txt")
                            {
                                sr.ReadLine();
                            }
                        }

                        sr.ReadToEnd();
                    }

                    graphicsPanel1.UpdateGridOffset(w, h, gridShape);
                    UpdatePanels();
                    UpdateGrid();
                }
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|Plaintext files (*.cells)|*.cells";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();
                    string ext = Path.GetExtension(openFileDialog.FileName).ToLower();

                    int w = Program.universe.GetLength(0);
                    int h = Program.universe.GetLength(1);

                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        if (ext == ".txt")
                        {
                            w = int.Parse(sr.ReadLine());
                            h = int.Parse(sr.ReadLine());
                        }
                        else if (ext == ".cells")
                        {
                            w = Program.universe.GetLength(0);
                            h = Program.universe.GetLength(1);

                            // Skip first two lines.
                            sr.ReadLine();
                            sr.ReadLine();
                        }

                        for (int j = 0; j < Program.universe.GetLength(1); j++)
                        {
                            if (j < h)
                            {
                                for (int i = 0; i < Program.universe.GetLength(0); i++)
                                {
                                    if (i < w)
                                    {
                                        if (ext == ".txt")
                                        {
                                            if (sr.Read() == '1')
                                            {
                                                Program.universe[i, j].Active = true;
                                            }
                                        }
                                        else if (ext == ".cells")
                                        {
                                            char chr = (char)sr.Read();

                                            if (chr == 'O')
                                            {
                                                Program.universe[i, j].Active = true;
                                            }
                                            else if (chr == '\n')
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                if (ext == ".txt")
                                {
                                    sr.ReadLine();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }

                        sr.ReadToEnd();
                    }

                    graphicsPanel1.UpdateGridOffset(w, h, gridShape);
                    UpdatePanels();
                    UpdateGrid();
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
