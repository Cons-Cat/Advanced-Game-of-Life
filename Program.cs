using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace GOLSource
{
    static class Program
    {
        // The universe array
        public static Cell[,] universe = new Cell[25, 25];
        public static bool playing;
        public static int ticks;
        public static Form1 form;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ticks = 0;
            playing = false;

            Thread t = new Thread(PlayThread);
            t.Start();

            ReSizeUniverse(25, 25);
            form = new Form1();

            Application.Run(form);
        }

        public static void Tick()
        {
            ticks++;
            form.UpdateTicksLabel(ticks);

            int w = universe.GetLength(0);
            int h = universe.GetLength(1);

            // Update cell states.
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (universe[i, j].AdjacentCount == 3)
                    {
                        // Reproduce.
                        universe[i, j].Active = true;
                    }
                    else if (universe[i, j].AdjacentCount != 2)
                    {
                        // Die.
                        universe[i, j].Active = false;
                    }
                }
            }

            form.UpdateLoop();
        }

        public static void PlayThread()
        {
            while (true)
            {
                if (playing)
                {
                    Tick();
                }

                Thread.Sleep(Form1.GameSpeed);
            }
        }

        public static void ReSizeUniverse(int argWidth, int argHeight)
        {
            if (argWidth > 0 && argHeight > 0)
            {
                universe = null;
                universe = new Cell[argWidth, argHeight];

                for (int i = 0; i < argWidth; i++)
                {
                    for (int j = 0; j < argHeight; j++)
                    {
                        universe[i, j] = new Cell
                        {
                            X = i,
                            Y = j
                        };

                        universe[i, j].AdjacentCount = 0;
                    }
                }
            }
        }
    }
}
