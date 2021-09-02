using GridTesting.World;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GridTesting
{
    class Chunk
    {
        //Array of every block within the chunk (current chunk size is 16 x 16)
        public Block[,] blocks;

        public Block NullBlock;//MOVE SOMEWHERE - LITERALLY ANYWHERE

        //For debugging:
        public bool showEvents;
        public bool showMarkers;

        public Chunk top, left, right, bottom, cornerTL, cornerTR, cornerBL, cornerBR;

        public int xoff, yoff;

        public Chunk ()//Much of this should probably get moved to either ChunkManager (as things like the NullBlock should be available to everyone)
        {
            NullBlock = new Block(new int[8, 8] { { 11, 11, 11, 11, 11, 11, 11, 11 } ,
                                                         { 11, 0,  0,  0,  0,  0,  0,  11 } ,
                                                         { 11, 0,  0,  0,  0,  0,  0,  11 } ,
                                                         { 11, 0,  0,  0,  0,  0,  0,  11 } ,
                                                         { 11, 0,  0,  0,  0,  0,  0,  11 } ,
                                                         { 11, 0,  0,  0,  0,  0,  0,  11 } ,
                                                         { 11, 0,  0,  0,  0,  0,  0,  11 } ,
                                                         { 11, 11, 11, 11, 11, 11, 11, 11 } });
            Block[] suppBlocks = new Block[8];
            for (int i = 0; i < suppBlocks.Length; i++) suppBlocks[i] = NullBlock;
            NullBlock.setLinkedBlocks(suppBlocks);

            int [,] testPattern1 = new int[8, 8] { { 11, 5, 5, 10, 10, 5, 5, 5 } ,
                                                   { 0, 11, 5, 10, 5, 5, 5, 0 } ,
                                                   { 0, 0, 11, 5, 10, 0, 5, 0 } ,
                                                   { 5, 0, 11, 11, 5, 0, 5, 5 } ,
                                                   { 5, 0, 0, 11, 11, 5, 5, 5 } ,
                                                   { 5, 10, 0, 11, 11, 11, 0, 5 } ,
                                                   { 5, 5, 0, 10, 0, 0, 11, 0 } ,
                                                   { 5, 5, 10, 10, 0, 0, 0, 11 } };

            int [,] testPattern2 = new int[8, 8] { { 5, 5, 5, 5, 5, 0, 5, 5 } ,
                                                   { 0, 5, 0, 5, 5, 5, 5, 0 } ,
                                                   { 0, 0, 5, 0, 10, 0, 0, 0 } ,
                                                   { 10, 10, 5, 5, 0, 0, 0, 5 } ,
                                                   { 0, 0, 10, 5, 5, 0, 0, 5 } ,
                                                   { 0, 10, 10, 5, 5, 5, 0, 5 } ,
                                                   { 10, 0, 10, 10, 5, 0, 5, 0 } ,
                                                   { 0, 0, 10, 10, 0, 5, 0, 5 } };

            blocks = new Block [16, 16];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (i <= 10)
                    {
                        if (i == j)
                            this.blocks[i, j] = new Block(testPattern1);
                        else if (i + 3 == j)
                            this.blocks[i, j] = new Block(testPattern1);
                        else if (ChunkManager.rand.Next(0, 10) <= 4)
                            this.blocks[i, j] = new Block(testPattern2);
                        else
                            this.blocks[i, j] = new Block();
                    }
                    else
                        this.blocks[i, j] = new Block();
                }
            }

            linkBlocks();

            xoff = 0;
            yoff = 0;
        }

        public Chunk (Block [,] blocks)
        {
            blocks = new Block[16, 16];
            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                    this.blocks[i,j] = blocks[i,j];

            linkBlocks();
        }

        public void setLinkedChunks (Chunk [] chunks)
        {
            //For when we link Chunks for an open world.
            //Will have to link blocks which are on the outside edges of the chunks.
            //I.e. the ones currently linked to the NullBlock.
            top = chunks[0];
            left = chunks[1];
            right = chunks[2];
            bottom = chunks[3];
            cornerTL = chunks[4];
            cornerTR = chunks[5];
            cornerBL = chunks[6];
            cornerBR = chunks[7];

            for (int it = 0; it < chunks.Length; it++)
            {
                if (chunks [it] != null)
                {
                    switch (it)
                    {
                        case 0:
                            {
                                for (int jt = 0; jt < 16; jt++)
                                {
                                    Block[] newLinked = blocks[0, jt].getLinkedBlocks();
                                    newLinked[0] = chunks[0].blocks[15, jt];
                                    blocks[0, jt].setLinkedBlocks(newLinked);
                                }
                                break;
                            }
                        case 1:
                            {
                                for (int jt = 0; jt < 16; jt++)
                                {
                                    Block[] newLinked = blocks[0, jt].getLinkedBlocks();
                                    newLinked[1] = chunks[1].blocks[jt, 15];
                                    blocks[0, jt].setLinkedBlocks(newLinked);
                                }
                                break;
                            }
                        case 2:
                            {
                                for (int jt = 0; jt < 16; jt++)
                                {
                                    Block[] newLinked = blocks[15, jt].getLinkedBlocks();
                                    newLinked[2] = chunks[2].blocks[jt, 0];
                                    blocks[15, jt].setLinkedBlocks(newLinked);
                                }
                                break;
                            }
                        case 3:
                            {
                                for (int jt = 0; jt < 16; jt++)
                                {
                                    Block[] newLinked = blocks[15, jt].getLinkedBlocks();
                                    newLinked[3] = chunks[3].blocks[0, jt];
                                    blocks[15, jt].setLinkedBlocks(newLinked);
                                }
                                break;
                            }
                        case 4:
                            {
                                Block[] newLinked = blocks[0, 0].getLinkedBlocks();
                                newLinked[4] = chunks[4].blocks[15, 15];
                                blocks[0, 0].setLinkedBlocks(newLinked);
                                break;
                            }
                        case 5:
                            {
                                Block[] newLinked = blocks[0, 15].getLinkedBlocks();
                                newLinked[5] = chunks[5].blocks[15, 0];
                                blocks[0, 15].setLinkedBlocks(newLinked);
                                break;
                            }
                        case 6:
                            {
                                Block[] newLinked = blocks[15, 0].getLinkedBlocks();
                                newLinked[6] = chunks[6].blocks[0, 15];
                                blocks[15, 0].setLinkedBlocks(newLinked);
                                break;
                            }
                        case 7:
                            {
                                Block[] newLinked = blocks[15, 15].getLinkedBlocks();
                                newLinked[7] = chunks[7].blocks[0, 0];
                                blocks[15, 15].setLinkedBlocks(newLinked);
                                break;
                            }
                    }
                }
            }
        }

        public void linkBlocks ()
        {
            for (int j = 0; j < 16; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    Block[] blocks = new Block[8];
                    if (i == 0)//on top
                    {
                        blocks[0] = blocks[4] = blocks[5] = NullBlock;
                        if (j == 0)//on the left
                        {
                            blocks[1] = NullBlock;
                            blocks[6] = NullBlock;

                            blocks[2] = this.blocks[i, j + 1];
                            blocks[3] = this.blocks[i + 1, j];
                            blocks[7] = this.blocks[i + 1, j + 1];
                        }
                        else if (j == 15)//on the right 
                        {
                            blocks[2] = NullBlock;
                            blocks[7] = NullBlock;

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
                        blocks[3] = blocks[6] = blocks[7] = NullBlock;
                        if (j == 0)//on the left
                        {
                            blocks[1] = NullBlock;
                            blocks[4] = NullBlock;

                            blocks[0] = this.blocks[i - 1, j];
                            blocks[2] = this.blocks[i, j + 1];
                            blocks[5] = this.blocks[i - 1, j + 1];
                        }
                        else if (j == 15)//on the right
                        {
                            blocks[2] = NullBlock;
                            blocks[5] = NullBlock;

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
                        blocks[1] = blocks[4] = blocks[6] = NullBlock;

                        blocks[0] = this.blocks[i - 1, j];
                        blocks[2] = this.blocks[i, j + 1];
                        blocks[3] = this.blocks[i + 1, j];
                        blocks[5] = this.blocks[i - 1, j + 1];
                        blocks[7] = this.blocks[i + 1, j + 1];
                    }
                    else if (j == 15)//on the right
                    {
                        blocks[2] = blocks[5] = blocks[7] = NullBlock;

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

                    this.blocks[i, j].setLinkedBlocks(blocks);
                }

                showEvents = false;
                showMarkers = false;
            }
        }

        public void forceMove (int x, int y)
        {
            Block block = blocks[y / ChunkManager.blockSpace, x / ChunkManager.blockSpace];
            PixelManager.updatePixel(block, (y / 5) % (ChunkManager.blockSpace / 5), (x / 5) % (ChunkManager.blockSpace / 5));
        }
    }
}
