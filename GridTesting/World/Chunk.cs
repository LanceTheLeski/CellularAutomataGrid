using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GridTesting
{
    class Chunk
    {
        Random rand = new Random();

        Block[,] blocks;
        public Chunk ()
        {
            Block nullBlock = new Block ();

            int[,] nullGrid = new int[8, 8] { { 11, 11, 11, 11, 11, 11, 11, 11 } , //0,
                                               { 11, 0, 0, 0, 0, 0, 0, 11 } , //1,
                                               { 11, 0, 0, 0, 0, 0, 0, 11 } , //2,
                                               { 11, 0, 0, 0, 0, 0, 0, 11 } , //3,
                                               { 11, 0, 0, 0, 0, 0, 0, 11 } , //4,
                                               { 11, 0, 0, 0, 0, 0, 0, 11 } , //5,
                                               { 11, 0, 0, 0, 0, 0, 0, 11 } , //6,
                                               { 11, 11, 11, 11, 11, 11, 11, 11 } };//7
            
            nullBlock = new Block(nullGrid);
            Block[] suppBlocks = new Block[8];
            for (int i = 0; i < suppBlocks.Length; i++) suppBlocks[i] = nullBlock;
            nullBlock.setBlocks(suppBlocks);

            int [,] pattern1 = new int[8, 8] { { 11, 5, 5, 10, 10, 5, 5, 5 } , //0,
                                               { 0, 11, 5, 10, 5, 5, 5, 0 } , //1,
                                               { 0, 0, 11, 5, 10, 0, 5, 0 } , //2,
                                               { 5, 0, 11, 11, 5, 0, 5, 5 } , //3,
                                               { 5, 0, 0, 11, 11, 5, 5, 5 } , //4,
                                               { 5, 10, 0, 11, 11, 11, 0, 5 } , //5,
                                               { 5, 5, 0, 10, 0, 0, 11, 0 } , //6,
                                               { 5, 5, 10, 10, 0, 0, 0, 11 } };//7

            int [,] pattern2 = new int[8, 8] { { 5, 5, 5, 5, 5, 0, 5, 5 } , //0,
                                               { 0, 5, 0, 5, 5, 5, 5, 0 } , //1,
                                               { 0, 0, 5, 0, 10, 0, 0, 0 } , //2,
                                               { 10, 10, 5, 5, 0, 0, 0, 5 } , //3,
                                               { 0, 0, 10, 5, 5, 0, 0, 5 } , //4,
                                               { 0, 10, 10, 5, 5, 5, 0, 5 } , //5,
                                               { 10, 0, 10, 10, 5, 0, 5, 0 } , //6,
                                               { 0, 0, 10, 10, 0, 5, 0, 5 } };//7

            blocks = new Block [16, 16];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (i <= 10)
                    {
                        if (i == j)
                            this.blocks[i, j] = new Block(pattern1);
                        else if (i + 3 == j)
                            this.blocks[i, j] = new Block(pattern1);
                        else if (rand.Next(0, 10) <= 4)
                            this.blocks[i, j] = new Block(pattern2);
                        else
                            this.blocks[i, j] = new Block();
                    }
                    else
                        this.blocks[i, j] = new Block();
                }
            }


            for (int j = 0; j < 16; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    Block[] blocks = new Block[8];
                    if (i == 0)//on top
                    {
                        blocks[0] = blocks[4] = blocks [5] = nullBlock;
                        if (j == 0)//on the left
                        {
                            blocks[1] = nullBlock;
                            blocks[6] = nullBlock;

                            blocks[2] = this.blocks[i, j + 1];
                            blocks[3] = this.blocks[i + 1, j];
                            blocks[7] = this.blocks[i + 1, j + 1];
                        }
                        else if (j == 15)//on the right 
                        {
                            blocks[2] = nullBlock;
                            blocks[7] = nullBlock;

                            blocks[1] = this.blocks[i, j - 1];
                            blocks[3] = this.blocks[i + 1, j];
                            blocks[6] = this.blocks[i + 1, j - 1];
                        }
                        else
                        {
                            blocks[1] = this.blocks[i, j - 1];
                            blocks[2] = this.blocks[i, j + 1];
                            blocks[3] = this.blocks[i + 1, j];
                            blocks[6] = this.blocks[i + 1, j - 1];
                            blocks[7] = this.blocks[i + 1, j + 1];
                        }
                    }
                    else if (i == 15)//on bottom
                    {
                        blocks[3] = blocks[6] = blocks [7] = nullBlock;
                        if (j == 0)//on the left
                        {
                            blocks[1] = nullBlock;
                            blocks[4] = nullBlock;

                            blocks[0] = this.blocks[i - 1, j];
                            blocks[2] = this.blocks[i, j + 1];
                            blocks[5] = this.blocks[i - 1, j + 1];
                        }
                        else if (j == 15)//on the right
                        {
                            blocks[2] = nullBlock;//
                            blocks[5] = nullBlock;//

                            blocks[0] = this.blocks[i - 1, j];
                            blocks[1] = this.blocks[i, j - 1];
                            blocks[4] = this.blocks[i - 1, j - 1];
                        }
                        else
                        {
                            blocks[0] = this.blocks[i - 1, j];
                            blocks[1] = this.blocks[i, j - 1];
                            blocks[2] = this.blocks[i, j + 1];
                            blocks[4] = this.blocks[i - 1, j - 1];
                            blocks[5] = this.blocks[i - 1, j + 1];
                        }
                    }
                    else if (j == 0)//on the left
                    {
                        blocks[1] = blocks[4] = blocks[6] = nullBlock;

                        blocks[0] = this.blocks[i - 1, j];
                        blocks[2] = this.blocks[i, j + 1];
                        blocks[3] = this.blocks[i + 1, j];
                        blocks[5] = this.blocks[i - 1, j + 1];
                        blocks[7] = this.blocks[i + 1, j + 1];
                    }
                    else if (j == 15)//on the right
                    {
                        blocks[2] = blocks[5] = blocks[7] = nullBlock;

                        blocks[0] = this.blocks[i - 1, j];
                        blocks[1] = this.blocks[i, j - 1];
                        blocks[3] = this.blocks[i + 1, j];
                        blocks[4] = this.blocks[i - 1, j - 1];
                        blocks[6] = this.blocks[i + 1, j - 1];
                    }
                    else//in the very middle
                    {
                        blocks[0] = this.blocks[i - 1, j];
                        blocks[1] = this.blocks[i, j - 1];
                        blocks[2] = this.blocks[i, j + 1];
                        blocks[3] = this.blocks[i + 1, j];
                        blocks[4] = this.blocks[i - 1, j - 1];
                        blocks[5] = this.blocks[i - 1, j + 1];
                        blocks[6] = this.blocks[i + 1, j - 1];
                        blocks[7] = this.blocks[i + 1, j + 1];
                    }

                    {
                        /*if (i == 0)//on top
                        {
                            blocks[3] = blocks[7] = blocks[6] = nullBlock;
                            if (j == 0)//on the left
                            {
                                blocks[2] = nullBlock;
                                blocks[5] = nullBlock;

                                blocks[1] = this.blocks[i, j + 1];
                                blocks[0] = this.blocks[i + 1, j];
                                blocks[4] = this.blocks[i + 1, j + 1];
                            }
                            else if (j == 15)//on the right 
                            {
                                blocks[1] = nullBlock;
                                blocks[4] = nullBlock;

                                blocks[2] = this.blocks[i, j - 1];
                                blocks[0] = this.blocks[i + 1, j];
                                blocks[5] = this.blocks[i + 1, j - 1];
                            }
                            else
                            {
                                blocks[2] = this.blocks[i, j - 1];
                                blocks[1] = this.blocks[i, j + 1];
                                blocks[0] = this.blocks[i + 1, j];
                                blocks[5] = this.blocks[i + 1, j - 1];
                                blocks[4] = this.blocks[i + 1, j + 1];
                            }
                        }
                        else if (i == 15)//on bottom
                        {
                            blocks[0] = blocks[5] = blocks[4] = nullBlock;
                            if (j == 0)//on the left
                            {
                                blocks[2] = nullBlock;
                                blocks[7] = nullBlock;

                                blocks[3] = this.blocks[i - 1, j];
                                blocks[1] = this.blocks[i, j + 1];
                                blocks[6] = this.blocks[i - 1, j + 1];
                            }
                            else if (j == 15)//on the right
                            {
                                blocks[1] = nullBlock;//
                                blocks[6] = nullBlock;//

                                blocks[3] = this.blocks[i - 1, j];
                                blocks[2] = this.blocks[i, j - 1];
                                blocks[7] = this.blocks[i - 1, j - 1];
                            }
                            else
                            {
                                blocks[3] = this.blocks[i - 1, j];
                                blocks[2] = this.blocks[i, j - 1];
                                blocks[1] = this.blocks[i, j + 1];
                                blocks[7] = this.blocks[i - 1, j - 1];
                                blocks[6] = this.blocks[i - 1, j + 1];
                            }
                        }
                        else if (j == 0)//on the left
                        {
                            blocks[2] = blocks[7] = blocks[5] = nullBlock;

                            blocks[3] = this.blocks[i - 1, j];
                            blocks[1] = this.blocks[i, j + 1];
                            blocks[0] = this.blocks[i + 1, j];
                            blocks[6] = this.blocks[i - 1, j + 1];
                            blocks[4] = this.blocks[i + 1, j + 1];
                        }
                        else if (j == 15)//on the right
                        {
                            blocks[1] = blocks[6] = blocks[4] = nullBlock;

                            blocks[3] = this.blocks[i - 1, j];
                            blocks[2] = this.blocks[i, j - 1];
                            blocks[0] = this.blocks[i + 1, j];
                            blocks[7] = this.blocks[i - 1, j - 1];
                            blocks[5] = this.blocks[i + 1, j - 1];
                        }
                        else//in the very middle
                        {
                            blocks[3] = this.blocks[i - 1, j];
                            blocks[2] = this.blocks[i, j - 1];
                            blocks[1] = this.blocks[i, j + 1];
                            blocks[0] = this.blocks[i + 1, j];
                            blocks[7] = this.blocks[i - 1, j - 1];
                            blocks[6] = this.blocks[i - 1, j + 1];
                            blocks[5] = this.blocks[i + 1, j - 1];
                            blocks[4] = this.blocks[i + 1, j + 1];
                        }*/
                    }

                    this.blocks[i, j].setBlocks(blocks);

                    this.blocks[i, j].debugBlocks(i, j); 
                }

                //top = blocks[0];
                //left = blocks[1];
                //right = blocks[2];
                //bottom = blocks[3];
                //cornerTL = blocks[4];
                //cornerTR = blocks[5];
                //cornerBL = blocks[6];
                //cornerBR = blocks[7];

                highlighted = false;
                altHighlighted = false;
                blockSpace = 40;
                updateLeft = true;
            }

        }

        bool updateLeft;
        public void update()
        {
            if (updateLeft)
                for (int j = 0; j < 16; j++)
                {
                    for (int i = 15; i >= 0; i--)
                    {
                        if (blocks[i, j].isActive()) blocks[i, j].update();
                    }
                }
            else
                for (int j = 15; j >= 0; j--)
                {
                    for (int i = 15; i >= 0; i--)
                    {
                        if (blocks[i, j].isActive()) blocks[i, j].update();
                    }
                }

            updateLeft = !updateLeft;
        }
        public void Draw (SpriteBatch _spriteBatch)
        {
            int y = 0;
            _spriteBatch.Begin();
            for (int i = 0; i < 16; i++)
            {
                int x = 0;
                for (int j = 0; j < 16; j++)
                {
                    blocks[i, j].Draw(x, y, 0, 0, 8, 8, _spriteBatch);
                    x += blockSpace;
                }
                y += blockSpace;
            }
            _spriteBatch.End();

        }

        public bool highlighted;
        public bool altHighlighted;
        public int blockSpace;

        public void highlight (int x, int y, int iDist, int jDist)
        {
            blocks[y / blockSpace + iDist, x / blockSpace + jDist].marked = !blocks[y / blockSpace + iDist, x / blockSpace + jDist].marked;
        }

        public void highlight ()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    blocks[i, j].marked = !blocks[i, j].marked;
                }
            }
            highlighted = !highlighted;
        }

        public void altHighlight()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    blocks[i, j].altMarked = !blocks[i, j].altMarked;
                }
            }
            altHighlighted = !altHighlighted;
        }

        public void forceMove (int x, int y)
        {
            Block block = blocks[y / blockSpace, x / blockSpace];
            //Pixel.updatePixel(block, y / 80, x / 80);
            Pixel.updatePixel(block, (y / 5) % (blockSpace / 5), (x / 5) % (blockSpace / 5));
        }
    }
}
