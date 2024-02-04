using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public enum ShapeType { I, O, N1, N2, F1, F2, T };

    public abstract class Shape
    {
        public Tetris tetris;
        public Block[] blocks;
        public int shape_index;

        public abstract bool ChangeShape();
        public bool Move(int xDiff, bool moveY)
        {
            Block[] new_blocks = util.NewCoordinate(4);
            for (int i = 0; i < 4; i++)
                new_blocks[i].clone(blocks[i]);

            if (moveY)
            {
                for (int i = 0; i < 4; i++)
                    new_blocks[i].y++;

                if (tetris.IfValidShapePos(new_blocks))
                {
                    blocks = new_blocks;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                for (int i = 0; i < new_blocks.Count(); i++)
                {
                    new_blocks[i].x += xDiff;
                }

                if (tetris.IfValidShapePos(new_blocks))
                {
                    blocks = new_blocks;
                    return true;
                }
                else
                    return false;
            }
        }
    }

    public class ShapeN1 : Shape
    {
        public static Shape NewShape(Tetris tetris, int shape_index, int width, int height)
        {
            ShapeN1 ret = new ShapeN1();
            ret.blocks = util.NewCoordinate(4);
            ret.shape_index = shape_index;
            ret.tetris = tetris;

            if (shape_index % 2 == 0)
            {
                ret.blocks[0].y = 0;
                ret.blocks[1].y = 0;
                ret.blocks[2].y = 1;
                ret.blocks[3].y = 1;

                ret.blocks[0].x = width / 2 - 1;
                ret.blocks[1].x = width / 2;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2 + 1;
            }
            else
            {
                ret.blocks[0].x = width / 2;
                ret.blocks[1].x = width / 2;
                ret.blocks[2].x = width / 2 + 1;
                ret.blocks[3].x = width / 2 + 1;

                ret.blocks[0].y = 1;
                ret.blocks[1].y = 2;
                ret.blocks[2].y = 0;
                ret.blocks[3].y = 1;
            }

            if (tetris.IfValidShapePos(ret.blocks))
            {
                for (int i = 0; i < 4; i++)
                    ret.blocks[i].type = ShapeType.N1;
                return ret;
            }
            else
                return null;
        }

        public override bool ChangeShape()
        {
            var new_blocks = util.NewCoordinate(4);
            if (shape_index % 2 == 0)
            {
                new_blocks[0].y = blocks[0].y;
                new_blocks[1].y = blocks[1].y + 1;
                new_blocks[2].y = blocks[2].y - 2;
                new_blocks[3].y = blocks[3].y - 1;

                new_blocks[0].x = blocks[0].x + 1;
                new_blocks[1].x = blocks[1].x;
                new_blocks[2].x = blocks[2].x + 1;
                new_blocks[3].x = blocks[3].x;
            }
            else
            {
                new_blocks[0].y = blocks[0].y;
                new_blocks[1].y = blocks[1].y - 1;
                new_blocks[2].y = blocks[2].y + 2;
                new_blocks[3].y = blocks[3].y + 1;

                new_blocks[0].x = blocks[0].x - 1;
                new_blocks[1].x = blocks[1].x;
                new_blocks[2].x = blocks[2].x - 1;
                new_blocks[3].x = blocks[3].x;
            }

            if (tetris.IfValidShapePos(new_blocks))
            {
                shape_index++;
                blocks = new_blocks;
                for (int i = 0; i < 4; i++)
                    blocks[i].type = ShapeType.N1;
                return true;
            }
            else
                return false;
        }
    }

    public class ShapeN2 : Shape
    {
        public static Shape NewShape(Tetris tetris, int shape_index, int width, int height)
        {
            ShapeN2 ret = new ShapeN2();
            ret.blocks = util.NewCoordinate(4);
            ret.shape_index = shape_index;
            ret.tetris = tetris;

            if (shape_index % 2 == 0)
            {
                ret.blocks[0].y = 1;
                ret.blocks[1].y = 1;
                ret.blocks[2].y = 0;
                ret.blocks[3].y = 0;

                ret.blocks[0].x = width / 2 - 1;
                ret.blocks[1].x = width / 2;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2 + 1;
            }
            else
            {
                ret.blocks[0].x = width / 2 - 1;
                ret.blocks[1].x = width / 2 - 1;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2;

                ret.blocks[0].y = 0;
                ret.blocks[1].y = 1;
                ret.blocks[2].y = 1;
                ret.blocks[3].y = 2;
            }

            if (tetris.IfValidShapePos(ret.blocks))
            {
                for (int i = 0; i < 4; i++)
                    ret.blocks[i].type = ShapeType.N2;
                return ret;
            }
            else
                return null;
        }

        public override bool ChangeShape()
        {
            var new_blocks = util.NewCoordinate(4);
            if (shape_index % 2 == 0)
            {
                new_blocks[0].y = blocks[0].y - 2;
                new_blocks[1].y = blocks[1].y - 1;
                new_blocks[2].y = blocks[2].y;
                new_blocks[3].y = blocks[3].y + 1;

                new_blocks[0].x = blocks[0].x;
                new_blocks[1].x = blocks[1].x - 1;
                new_blocks[2].x = blocks[2].x;
                new_blocks[3].x = blocks[3].x - 1;
            }
            else
            {
                new_blocks[0].y = blocks[0].y + 2;
                new_blocks[1].y = blocks[1].y + 1;
                new_blocks[2].y = blocks[2].y;
                new_blocks[3].y = blocks[3].y - 1;

                new_blocks[0].x = blocks[0].x;
                new_blocks[1].x = blocks[1].x + 1;
                new_blocks[2].x = blocks[2].x;
                new_blocks[3].x = blocks[3].x + 1;
            }

            if (tetris.IfValidShapePos(new_blocks))
            {
                shape_index++;
                blocks = new_blocks;
                for (int i = 0; i < 4; i++)
                    blocks[i].type = ShapeType.N2;
                return true;
            }
            else
                return false;
        }
    }

    public class ShapeF1 : Shape
    {
        public static Shape NewShape(Tetris tetris, int shape_index, int width, int height)
        {
            ShapeF1 ret = new ShapeF1();
            ret.blocks = util.NewCoordinate(4);
            ret.shape_index = shape_index;
            ret.tetris = tetris;

            if (shape_index % 4 == 0)
            {
                ret.blocks[0].y = 0;
                ret.blocks[1].y = 1;
                ret.blocks[2].y = 1;
                ret.blocks[3].y = 1;

                ret.blocks[0].x = width / 2 - 1;
                ret.blocks[1].x = width / 2 - 1;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2 + 1;
            }
            else if (shape_index % 4 == 1)
            {
                ret.blocks[0].y = 0;
                ret.blocks[1].y = 0;
                ret.blocks[2].y = 1;
                ret.blocks[3].y = 2;

                ret.blocks[0].x = width / 2 + 1;
                ret.blocks[1].x = width / 2;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2;
            }
            else if (shape_index % 4 == 2)
            {
                ret.blocks[0].y = 1;
                ret.blocks[1].y = 0;
                ret.blocks[2].y = 0;
                ret.blocks[3].y = 0;

                ret.blocks[0].x = width / 2 + 1;
                ret.blocks[1].x = width / 2 + 1;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2 - 1;
            }
            else if (shape_index % 4 == 3)
            {
                ret.blocks[0].y = 2;
                ret.blocks[1].y = 2;
                ret.blocks[2].y = 1;
                ret.blocks[3].y = 0;

                ret.blocks[0].x = width / 2 - 1;
                ret.blocks[1].x = width / 2;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2;
            }

            if (tetris.IfValidShapePos(ret.blocks))
            {
                for (int i = 0; i < 4; i++)
                    ret.blocks[i].type = ShapeType.F1;
                return ret;
            }
            else
                return null;
        }

        public override bool ChangeShape()
        {
            var new_blocks = util.NewCoordinate(4);
            if (shape_index % 4 == 0)
            {
                new_blocks[0].y = blocks[0].y - 1;
                new_blocks[1].y = blocks[1].y - 2;
                new_blocks[2].y = blocks[2].y - 1;
                new_blocks[3].y = blocks[3].y;

                new_blocks[0].x = blocks[0].x + 2;
                new_blocks[1].x = blocks[1].x + 1;
                new_blocks[2].x = blocks[2].x;
                new_blocks[3].x = blocks[3].x - 1;
            }
            else if (shape_index % 4 == 1)
            {
                new_blocks[0].y = blocks[0].y + 2;
                new_blocks[1].y = blocks[1].y + 1;
                new_blocks[2].y = blocks[2].y;
                new_blocks[3].y = blocks[3].y - 1;

                new_blocks[0].x = blocks[0].x;
                new_blocks[1].x = blocks[1].x + 1;
                new_blocks[2].x = blocks[2].x;
                new_blocks[3].x = blocks[3].x - 1;
            }
            else if (shape_index % 4 == 2)
            {
                new_blocks[0].y = blocks[0].y;
                new_blocks[1].y = blocks[1].y + 1;
                new_blocks[2].y = blocks[2].y;
                new_blocks[3].y = blocks[3].y - 1;

                new_blocks[0].x = blocks[0].x - 2;
                new_blocks[1].x = blocks[1].x - 1;
                new_blocks[2].x = blocks[2].x;
                new_blocks[3].x = blocks[3].x + 1;
            }
            else if (shape_index % 4 == 3)
            {
                new_blocks[0].y = blocks[0].y - 1;
                new_blocks[1].y = blocks[1].y;
                new_blocks[2].y = blocks[2].y + 1;
                new_blocks[3].y = blocks[3].y + 2;

                new_blocks[0].x = blocks[0].x;
                new_blocks[1].x = blocks[1].x - 1;
                new_blocks[2].x = blocks[2].x;
                new_blocks[3].x = blocks[3].x + 1;
            }

            if (tetris.IfValidShapePos(new_blocks))
            {
                shape_index++;
                blocks = new_blocks;
                for (int i = 0; i < 4; i++)
                    blocks[i].type = ShapeType.F1;
                return true;
            }
            else
                return false;
        }
    }

    public class ShapeF2 : Shape
    {
        public static Shape NewShape(Tetris tetris, int shape_index, int width, int height)
        {
            ShapeF2 ret = new ShapeF2();
            ret.blocks = util.NewCoordinate(4);
            ret.shape_index = shape_index;
            ret.tetris = tetris;

            if (shape_index % 4 == 0)
            {
                ret.blocks[0].y = 0;
                ret.blocks[1].y = 1;
                ret.blocks[2].y = 1;
                ret.blocks[3].y = 1;

                ret.blocks[0].x = width / 2 + 1;
                ret.blocks[1].x = width / 2 + 1;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2 - 1;
            }
            else if (shape_index % 4 == 1)
            {
                ret.blocks[0].y = 2;
                ret.blocks[1].y = 2;
                ret.blocks[2].y = 1;
                ret.blocks[3].y = 0;

                ret.blocks[0].x = width / 2 + 1;
                ret.blocks[1].x = width / 2;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2;
            }
            else if (shape_index % 4 == 2)
            {
                ret.blocks[0].y = 1;
                ret.blocks[1].y = 0;
                ret.blocks[2].y = 0;
                ret.blocks[3].y = 0;

                ret.blocks[0].x = width / 2 - 1;
                ret.blocks[1].x = width / 2 - 1;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2 + 1;
            }
            else if (shape_index % 4 == 3)
            {
                ret.blocks[0].y = 0;
                ret.blocks[1].y = 0;
                ret.blocks[2].y = 1;
                ret.blocks[3].y = 2;

                ret.blocks[0].x = width / 2 - 1;
                ret.blocks[1].x = width / 2;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2;
            }

            if (tetris.IfValidShapePos(ret.blocks))
            {
                for (int i = 0; i < 4; i++)
                    ret.blocks[i].type = ShapeType.F2;
                return ret;
            }
            else
                return null;
        }

        public override bool ChangeShape()
        {
            var new_blocks = util.NewCoordinate(4);
            if (shape_index % 4 == 0)
            {
                new_blocks[0].y = blocks[0].y + 1;
                new_blocks[1].y = blocks[1].y;
                new_blocks[2].y = blocks[2].y - 1;
                new_blocks[3].y = blocks[3].y - 2;

                new_blocks[0].x = blocks[0].x;
                new_blocks[1].x = blocks[1].x - 1;
                new_blocks[2].x = blocks[2].x;
                new_blocks[3].x = blocks[3].x + 1;
            }
            else if (shape_index % 4 == 1)
            {
                new_blocks[0].y = blocks[0].y;
                new_blocks[1].y = blocks[1].y - 1;
                new_blocks[2].y = blocks[2].y;
                new_blocks[3].y = blocks[3].y + 1;

                new_blocks[0].x = blocks[0].x - 2;
                new_blocks[1].x = blocks[1].x - 1;
                new_blocks[2].x = blocks[2].x;
                new_blocks[3].x = blocks[3].x + 1;
            }
            else if (shape_index % 4 == 2)
            {
                new_blocks[0].y = blocks[0].y - 2;
                new_blocks[1].y = blocks[1].y - 1;
                new_blocks[2].y = blocks[2].y;
                new_blocks[3].y = blocks[3].y + 1;

                new_blocks[0].x = blocks[0].x;
                new_blocks[1].x = blocks[1].x + 1;
                new_blocks[2].x = blocks[2].x;
                new_blocks[3].x = blocks[3].x - 1;
            }
            else if (shape_index % 4 == 3)
            {
                new_blocks[0].y = blocks[0].y + 1;
                new_blocks[1].y = blocks[1].y + 2;
                new_blocks[2].y = blocks[2].y + 1;
                new_blocks[3].y = blocks[3].y;

                new_blocks[0].x = blocks[0].x + 2;
                new_blocks[1].x = blocks[1].x + 1;
                new_blocks[2].x = blocks[2].x;
                new_blocks[3].x = blocks[3].x - 1;
            }

            if (tetris.IfValidShapePos(new_blocks))
            {
                shape_index++;
                blocks = new_blocks;
                for (int i = 0; i < 4; i++)
                    blocks[i].type = ShapeType.F2;
                return true;
            }
            else
                return false;
        }
    }

    public class ShapeT : Shape
    {
        public static Shape NewShape(Tetris tetris, int shape_index, int width, int height)
        {
            ShapeT ret = new ShapeT();
            ret.blocks = util.NewCoordinate(4);
            ret.shape_index = shape_index;
            ret.tetris = tetris;

            if (shape_index % 4 == 0)
            {
                ret.blocks[0].y = 1;
                ret.blocks[1].y = 1;
                ret.blocks[2].y = 1;
                ret.blocks[3].y = 0;

                ret.blocks[0].x = width / 2 - 1;
                ret.blocks[1].x = width / 2;
                ret.blocks[2].x = width / 2 + 1;
                ret.blocks[3].x = width / 2;
            }
            else if (shape_index % 4 == 1)
            {
                ret.blocks[0].y = 0;
                ret.blocks[1].y = 1;
                ret.blocks[2].y = 2;
                ret.blocks[3].y = 1;

                ret.blocks[0].x = width / 2;
                ret.blocks[1].x = width / 2;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2 + 1;
            }
            else if (shape_index % 4 == 2)
            {
                ret.blocks[0].y = 0;
                ret.blocks[1].y = 0;
                ret.blocks[2].y = 0;
                ret.blocks[3].y = 1;

                ret.blocks[0].x = width / 2 + 1;
                ret.blocks[1].x = width / 2;
                ret.blocks[2].x = width / 2 - 1;
                ret.blocks[3].x = width / 2;
            }
            else if (shape_index % 4 == 3)
            {
                ret.blocks[0].y = 2;
                ret.blocks[1].y = 1;
                ret.blocks[2].y = 0;
                ret.blocks[3].y = 1;

                ret.blocks[0].x = width / 2;
                ret.blocks[1].x = width / 2;
                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = width / 2 - 1;
            }

            if (tetris.IfValidShapePos(ret.blocks))
            {
                for (int i = 0; i < 4; i++)
                    ret.blocks[i].type = ShapeType.T;
                return ret;
            }
            else
                return null;
        }

        public override bool ChangeShape()
        {
            var new_blocks = util.NewCoordinate(4);
            if (shape_index % 4 == 0)
            {
                new_blocks[0].y = blocks[0].y - 1;
                new_blocks[1].y = blocks[1].y;
                new_blocks[2].y = blocks[2].y + 1;
                new_blocks[3].y = blocks[3].y + 1;

                new_blocks[0].x = blocks[0].x + 1;
                new_blocks[1].x = blocks[1].x;
                new_blocks[2].x = blocks[2].x - 1;
                new_blocks[3].x = blocks[3].x + 1;
            }
            else if (shape_index % 4 == 1)
            {
                new_blocks[0].y = blocks[0].y + 1;
                new_blocks[1].y = blocks[1].y;
                new_blocks[2].y = blocks[2].y - 1;
                new_blocks[3].y = blocks[3].y + 1;

                new_blocks[0].x = blocks[0].x + 1;
                new_blocks[1].x = blocks[1].x;
                new_blocks[2].x = blocks[2].x - 1;
                new_blocks[3].x = blocks[3].x - 1;
            }
            else if (shape_index % 4 == 2)
            {
                new_blocks[0].y = blocks[0].y + 1;
                new_blocks[1].y = blocks[1].y;
                new_blocks[2].y = blocks[2].y - 1;
                new_blocks[3].y = blocks[3].y - 1;

                new_blocks[0].x = blocks[0].x - 1;
                new_blocks[1].x = blocks[1].x;
                new_blocks[2].x = blocks[2].x + 1;
                new_blocks[3].x = blocks[3].x - 1;
            }
            else if (shape_index % 4 == 3)
            {
                new_blocks[0].y = blocks[0].y - 1;
                new_blocks[1].y = blocks[1].y;
                new_blocks[2].y = blocks[2].y + 1;
                new_blocks[3].y = blocks[3].y - 1;

                new_blocks[0].x = blocks[0].x - 1;
                new_blocks[1].x = blocks[1].x;
                new_blocks[2].x = blocks[2].x + 1;
                new_blocks[3].x = blocks[3].x + 1;
            }

            if (tetris.IfValidShapePos(new_blocks))
            {
                shape_index++;
                blocks = new_blocks;
                for (int i = 0; i < 4; i++)
                    blocks[i].type = ShapeType.T;
                return true;
            }
            else
                return false;
        }
    }

    public class ShapeI : Shape
    {
        public static Shape NewShape(Tetris tetris, int shape_index, int width, int height)
        {
            ShapeI ret = new ShapeI();
            ret.blocks = util.NewCoordinate(4);
            ret.shape_index = shape_index;
            ret.tetris = tetris;

            if (shape_index % 2 == 0)
            {
                for (int i = 0; i < 4; i++)
                    ret.blocks[i].y = 0;

                ret.blocks[2].x = width / 2;
                ret.blocks[3].x = ret.blocks[2].x + 1;
                ret.blocks[1].x = ret.blocks[2].x - 1;
                ret.blocks[0].x = ret.blocks[2].x - 2;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    ret.blocks[i].x = width / 2;

                for (int i = 0; i < 4; i++)
                    ret.blocks[i].y = i;
            }

            if (tetris.IfValidShapePos(ret.blocks))
            {
                for (int i = 0; i < 4; i++)
                    ret.blocks[i].type = ShapeType.I;
                return ret;
            }
            else
                return null;
        }

        public override bool ChangeShape()
        {
            var new_blocks = util.NewCoordinate(4);
            if (shape_index % 2 == 0)
            {
                for (int i = 0; i < 4; i++)
                    new_blocks[i].x = blocks[2].x;

                new_blocks[0].y = blocks[2].y;
                new_blocks[1].y = new_blocks[0].y + 1;
                new_blocks[2].y = new_blocks[0].y + 2;
                new_blocks[3].y = new_blocks[0].y + 3;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    new_blocks[i].y = blocks[0].y;

                new_blocks[0].x = blocks[2].x - 2;
                new_blocks[1].x = new_blocks[0].x + 1;
                new_blocks[2].x = new_blocks[0].x + 2;
                new_blocks[3].x = new_blocks[0].x + 3;
            }

            if (tetris.IfValidShapePos(new_blocks))
            {
                shape_index++;
                blocks = new_blocks;
                for (int i = 0; i < 4; i++)
                    blocks[i].type = ShapeType.I;
                return true;
            }
            else
                return false;
        }
    }

    public class ShapeO : Shape
    {
        public static Shape NewShape(Tetris tetris, int shape_index, int width, int height)
        {
            ShapeO ret = new ShapeO();
            ret.blocks = util.NewCoordinate(4);
            ret.tetris = tetris;

            ret.blocks[0].y = 0;
            ret.blocks[1].y = 0;
            ret.blocks[2].y = 1;
            ret.blocks[3].y = 1;

            ret.blocks[0].x = width / 2 - 1;
            ret.blocks[1].x = width / 2;
            ret.blocks[2].x = width / 2 - 1;
            ret.blocks[3].x = width / 2;

            if (tetris.IfValidShapePos(ret.blocks))
            {
                for (int i = 0; i < 4; i++)
                    ret.blocks[i].type = ShapeType.O;
                return ret;
            }
            else
                return null;
        }

        public override bool ChangeShape()
        {
            return true;
        }
    }
}
