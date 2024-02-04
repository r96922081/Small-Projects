using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tetris
{
    class Unittest
    {
        public void Check(bool b)
        {
            if (!b)
            {
                Debug.Assert(false);
            }
        }

        public void dummy_callback()
        {

        }

        public void TestIfValidShapePos()
        {
            Tetris tetris = new Tetris(10, 10, new DelegateFunction1(dummy_callback));
            var blocks = util.NewCoordinate(4);
            blocks[0].x = -1;
            Check(tetris.IfValidShapePos(blocks) == false);
            blocks[0].x = 0;
            Check(tetris.IfValidShapePos(blocks) == true);
            blocks[0].y = 10;
            Check(tetris.IfValidShapePos(blocks) == false);
            blocks[0].y = 9;
            Check(tetris.IfValidShapePos(blocks) == true);

            tetris = new Tetris(1, 1, new DelegateFunction1(dummy_callback));
            blocks = util.NewCoordinate(4);
            Check(tetris.IfValidShapePos(blocks) == true);

            tetris = new Tetris(0, 1, new DelegateFunction1(dummy_callback));
            blocks = util.NewCoordinate(4);
            Check(tetris.IfValidShapePos(blocks) == false);

            tetris = new Tetris(1, 1, new DelegateFunction1(dummy_callback));
            blocks = util.NewCoordinate(4);
            Check(tetris.IfValidShapePos(blocks) == true);
            tetris.fixed_blocks = new List<Block>();
            tetris.fixed_blocks.Add(new Block(0, 0));
            Check(tetris.IfValidShapePos(blocks) == false);
        }

        public void RunUt()
        {
            TestIfValidShapePos();

        }
    }
}
