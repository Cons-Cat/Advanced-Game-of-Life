using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOLSource
{
    static class Program
    {
        // The universe array
        public static Cell[,] universe = new Cell[25, 25];

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    universe[i, j] = new Cell();
                    universe[i, j].X = i;
                    universe[i, j].Y = j;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void Tick()
        {
            int w = universe.GetLength(0);
            int h = universe.GetLength(1);

            // Update cell states.
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (universe[i, j].AdjacentCount < 2)
                    {
                        // Starve.
                        universe[i, j].Active = false;
                    }
                    else if (universe[i, j].AdjacentCount > 3)
                    {
                        // Overpopulate.
                        universe[i, j].Active = false;
                    }
                    else if (universe[i, j].AdjacentCount == 3)
                    {
                        // Reproduce.
                        universe[i, j].Active = true;
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
    }
}
