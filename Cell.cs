using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLSource
{
    class Cell
    {
        public bool Active { get; set; }
        public uint AdjacentCount { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Cell()
        {
            Active = false;
        }

        public void UpdateAdjacentCount()
        {
            AdjacentCount = 0;
            int adjX;
            int adjY;

            for (int i = 0; i < 9; i++)
            {
                adjX = X + (i / 3) - 1;
                adjY = Y + (i % 3) - 1;

                if (
                    i == 4
                    || adjX >= Program.universe.GetLength(0)
                    || adjY >= Program.universe.GetLength(1)
                    || adjX < 0
                    || adjY < 0
                    )
                {
                    continue;
                }

                if (Program.universe[X + (i / 3) - 1, Y + (i % 3) - 1].Active)
                {
                    AdjacentCount++;
                }
            }
        }
    }
}
