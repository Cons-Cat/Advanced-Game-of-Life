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

            for (int i = 0; i < 9; i++)
            {
                if (i == 4) { continue; }
                Debug.WriteLine($"{argX + (i / 3) - 1}, {argY + i % 3 - 1}");

                if (argArr[argX + (i / 3) - 1, argY + i % 3 - 1].Active)
                {
                    AdjacentCount++;
                }
            }
        }

    }
}
