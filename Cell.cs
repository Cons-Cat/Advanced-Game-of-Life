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

        public Cell()
        {
            Active = false;
        }

        public void UpdateAdjacentCount(ref Cell[,] argArr, int argX, int argY)
        {
            AdjacentCount = 0;
            int adjX;
            int adjY;

            for (int i = 0; i < 9; i++)
            {
                adjX = argX + (i / 3) - 1;
                adjY = argY + (i % 3) - 1;

                if (
                    i == 4
                    || adjX >= 5
                    || adjY >= 5
                    || adjX < 0
                    || adjY < 0
                    )
                {
                    continue;
                }

                if (argArr[argX + (i / 3) - 1, argY + (i % 3) - 1].Active)
                {
                    AdjacentCount++;
                }
            }
        }
    }
}
