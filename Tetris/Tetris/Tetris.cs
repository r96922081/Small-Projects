using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Block
    {
        public Block() { this.x = 0; this.y = 0; }
        public Block(int x, int y) { this.x = x; this.y = y; }
        public static bool operator ==(Block a, Block b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Block a, Block b)
        {
            return !(a == b);
        }
        public int x;
        public int y;
        public ShapeType type;

        public void clone(Block c)
        {
            x = c.x;
            y = c.y;
            type = c.type;
        }
    }

    public class Tetris
    {
        int width;
        int height;
        Random random = new Random();
        DelegateFunction1 game_over_callback = null;
        public List<Block> fixed_blocks;
        Shape shape = null;
        int shape_index = 0;
        int clear_lines = 0;

        public Block[] moving_blocks
        {
            get
            {
                if (shape == null)
                    return null;

                return shape.blocks;
            }
        }

        public Tetris(int width, int height, DelegateFunction1 game_over_callback)
        {
            this.width = width;
            this.height = height;
            this.game_over_callback = game_over_callback;

            Reset();
        }

        public void Start()
        {
            NewShape();
        }

        public void Reset()
        {
            fixed_blocks = new List<Block>();
            shape = null;
            shape_index = 0;
            clear_lines = 0;
        }

        public bool IfValidShapePos(Block[] shape_blocks)
        {
            for (int i = 0; i < shape_blocks.Count(); i++)
            {
                if (shape_blocks[i].x < 0)
                    return false;
                if (shape_blocks[i].x >= width)
                    return false;
                if (shape_blocks[i].y < 0)
                    return false;
                if (shape_blocks[i].y >= height)
                    return false;

                foreach (var block in fixed_blocks)
                {
                    if (shape_blocks[i] == block)
                        return false;
                }
            }

            return true;
        }

        public bool ChangeShape()
        {
            if (shape == null)
                return false;

            return shape.ChangeShape();
        }

        public bool MoveLeft()
        {
            if (shape == null)
                return false;

            return shape.Move(-1, false);
        }

        public bool MoveRight()
        {
            if (shape == null)
                return false;

            return shape.Move(1, false);
        }

        public void ClearLine()
        {
            bool keep_going = true;
            while (keep_going)
            {
                keep_going = false;
                int current_y = -1;
                int block_of_this_y = 0;

                for (int i = 0; i < fixed_blocks.Count(); i++)
                {
                    var block = fixed_blocks[i];
                    if (block.y != current_y)
                    {
                        current_y = block.y;
                        block_of_this_y = 1;
                    }
                    else
                        block_of_this_y++;

                    if (block_of_this_y == width)
                    {
                        keep_going = true;
                        for (int j = i + 1; j < fixed_blocks.Count(); j++)
                        {
                            fixed_blocks[j].y++;
                        }
                        fixed_blocks.RemoveRange(i - block_of_this_y + 1, block_of_this_y);
                        clear_lines++;
                        break;
                    }
                }
            }
        }

        public bool MoveDown()
        {
            if (shape == null)
                return false;

            if (shape.Move(0, true) == false)
            {
                Block[] new_blocks = util.NewCoordinate(4);
                for (int i = 0; i < 4; i++)
                {
                    new_blocks[i].clone(shape.blocks[i]);
                    AddToFixedBlocks(new_blocks[i]);
                }

                ClearLine();
                NewShape();
            }

            return true;
        }

        public void NewShape(ShapeType shape_type)
        {
            if (shape_type == ShapeType.I)
            {
                shape = ShapeI.NewShape(this, shape_index++, width, height);
            }
            else if (shape_type == ShapeType.O)
            {
                shape = ShapeO.NewShape(this, shape_index++, width, height);
            }
            else if (shape_type == ShapeType.N1)
            {
                shape = ShapeN1.NewShape(this, shape_index++, width, height);
            }
            else if (shape_type == ShapeType.N2)
            {
                shape = ShapeN2.NewShape(this, shape_index++, width, height);
            }
            else if (shape_type == ShapeType.F1)
            {
                shape = ShapeF1.NewShape(this, shape_index++, width, height);
            }
            else if (shape_type == ShapeType.F2)
            {
                shape = ShapeF2.NewShape(this, shape_index++, width, height);
            }
            else if (shape_type == ShapeType.T)
            {
                shape = ShapeT.NewShape(this, shape_index++, width, height);
            }

            if (shape == null)
            {
                game_over_callback();
            }
        }

        public void NewShape()
        {
            NewShape((ShapeType)(random.Next() % 7));
        }

        public void Step()
        {
            MoveDown();
        }

        public int GetClearLines()
        {
            return clear_lines;
        }

        public void AddToFixedBlocks(Block block)
        {
            fixed_blocks.Add(block);
            for (int i = fixed_blocks.Count() - 1; i >= 1; i--)
            {
                var coordinateI = fixed_blocks[i];
                var coordinateI_minus_1 = fixed_blocks[i - 1];
                if (coordinateI.y > coordinateI_minus_1.y - 1)
                {
                    var temp = coordinateI;
                    fixed_blocks[i] = coordinateI_minus_1;
                    fixed_blocks[i - 1] = coordinateI;
                }
                else
                    break;

            }
        }
    }
}
