using System;
using System.Collections.Generic;
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

        public void UpdateAdjacentCount()
        {

        }
    }
}
