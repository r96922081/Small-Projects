using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{

    public delegate void DelegateFunction1();

    class util
    {
        public static Block[] NewCoordinate(int n)
        {
            Block[] ret = new Block[n];
            for (int i = 0; i < n; i++)
            {
                ret[i] = new Block();
            }

            return ret;
        }
    }
}
