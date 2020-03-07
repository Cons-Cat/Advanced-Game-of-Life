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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            ticks = 0;
            playing = false;
            Thread t = new Thread(PlayThread);
            t.Start();

            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    universe[i, j] = new Cell
                    {
                        X = i,
                        Y = j
                    };
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            Application.Run(form);
        }

        public static void Tick()
        {
            ticks++;
            form.UpdateTicks(ticks);

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

            // Evaluate new adjacent counts.
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    universe[i, j].UpdateAdjacentCount();
                }
            }
        }

        public static void PlayThread()
        {
            while (true)
            {
                if (playing)
                {
                    Tick();
                    form.UpdateLoop();

                    Thread.Sleep(100);
                }
            }
        }
    }
}
