﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GridTesting
{
    class Block
    {
        //Pixels.Sand sand = new Pixels.Sand();//For testing. Remove soon.
        Random rand = new Random();

        int[,] grid;
        int[,] data;
        bool[,] updated;

        public Block top, left, right, bottom, cornerTL, cornerTR, cornerBL, cornerBR;//!!Probably turn into an array for simplifying code :)
        Block[] blocks;

        /*
         * The empty constructor for a blank block.
         */
        public Block()
        {
            {/*
            grid = new int [8, 8];
            for (int i = 0; i < 8; i++)//from top row
            {
                for (int j = 0; j < 8; j++)//from left to right
                {
                    //grid[i, j] = 0;
                }    
            }
            */
            }

            //Better idea:
            //Also I'm labeling these based on Matrix notation. So vector (i, j) = grid [j, i]
            //j:
            grid = new int[8, 8] { { 0, 0, 0, 0, 0, 0, 0, 0 } , //0,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //1,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //2,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //3,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //4,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //5,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //6,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } };//7
                                                                  //i: 0, 1, 2, 3, 4, 5, 6, 7
                                                                  //j:
            data = new int[8, 8] { { 0, 0, 0, 0, 0, 0, 0, 0 } , //0,.
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //1,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //2,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //3,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //4,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //5,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //6,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } };//7
                                                                //i: 0, 1, 2, 3, 4, 5, 6, 7
                                                                //j:
            updated = new bool[8, 8] { { false, false, false, false, false, false, false, false } , //0,
                                       { false, false, false, false, false, false, false, false } , //1,
                                       { false, false, false, false, false, false, false, false } , //2,
                                       { false, false, false, false, false, false, false, false } , //3,
                                       { false, false, false, false, false, false, false, false } , //4,
                                       { false, false, false, false, false, false, false, false } , //5,
                                       { false, false, false, false, false, false, false, false } , //6,
                                       { false, false, false, false, false, false, false, false } };//7
                                                                                                    //i:  0,     1,     2,     3,     4,     5,     6,     7

            hasEvent = false;
            startEvent = new Vector2(0, 7);
            endEvent = new Vector2(7, 0);

            marked = false;//remove later
        }

        public Block(int[,] grid /*, int[,] data*/) //!!Different argument(s)..?
        {
            this.grid = new int[8, 8] { { 0, 0, 0, 0, 0, 0, 0, 0 } , //0,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //1,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //2,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //3,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //4,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //5,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } , //6,
                                     { 0, 0, 0, 0, 0, 0, 0, 0 } };//7

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.grid[i, j] = grid[i, j];
                }
            }

            //this.grid = grid;
            //this.data = data;
            data = new int[8, 8] { { 0, 0, 0, 0, 0, 0, 0, 0 } , //0,.
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //1,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //2,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //3,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //4,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //5,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } , //6,
                                   { 0, 0, 0, 0, 0, 0, 0, 0 } };//7
                                                                //i: 0, 1, 2, 3, 4, 5, 6, 7
                                                                //j:
            updated = new bool[8, 8] { { false, false, false, false, false, false, false, false } , //0,
                                       { false, false, false, false, false, false, false, false } , //1,
                                       { false, false, false, false, false, false, false, false } , //2,
                                       { false, false, false, false, false, false, false, false } , //3,
                                       { false, false, false, false, false, false, false, false } , //4,
                                       { false, false, false, false, false, false, false, false } , //5,
                                       { false, false, false, false, false, false, false, false } , //6,
                                       { false, false, false, false, false, false, false, false } };//7

            hasEvent = true;
            startEvent = new Vector2(0, 7);
            endEvent = new Vector2(7, 0);

            marked = false;//remove later
        }

        public void setBlocks (Block [] blocks)
        {
            this.blocks = blocks;

            top = blocks[0];
            left = blocks[1];
            right = blocks[2];
            bottom = blocks[3];
            cornerTL = blocks[4];
            cornerTR = blocks[5];
            cornerBL = blocks[6];
            cornerBR = blocks[7];
        }

        public bool isActive ()
        {
            return hasEvent;
        }

        public bool debugBlocks (int i, int j)
        {
            for (int ind = 0; ind < blocks.Length; ind++)
                if (this == blocks[ind]) throw new Exception("Block " + ind + " points to the same object that it is in for (" + i + ", " + j + ")!!");

            return false;
        }

        /*
        * This method strictly updates a pixel at the given (i, j) coordinate. It does not 
        * check to see if that pixel was updated already however.
        */
        public void update()
        {
            Vector2 startO = startEvent, endO = endEvent;
            Vector2 startN = new Vector2 (8, -1), endN = new Vector2 (-1, 8);

            for (int i = (int) startO.Y; i >= endO.Y; i--)
            {

                for (int j = (int)startO.X; j <= endO.X; j++)
                {
                    if (updated[i, j] == true)
                    {
                        if (startO.Y == startEvent.Y && i > startN.Y)
                            startN.Y = i;
                        if (endO.X == endEvent.Y && i < endN.Y)
                            endN.Y = i;

                        if (startO.X == startEvent.X && j < startN.X)
                            startN.X = j;
                        if (endO.X == endEvent.X && j > endN.X)
                            endN.X = j;

                        updated[i, j] = false;
                    }
                    else
                        updatePixel(i, j);
                }
            }
            
            if (startO.X != startEvent.X)
                startN.X = startEvent.X;
            if (startO.Y != startEvent.Y)
                startN.Y = startEvent.Y;
            if (endO.X != endEvent.X)
                endN.X = endEvent.X;
            if (endO.Y != endEvent.Y)
                endN.Y = endEvent.Y;

            if (startN.X == 8 && startN.Y == -1 && endN.X == -1 && endN.Y == 8)
            {
                hasEvent = false;
                return;
            }
            /*else if (startN.X == 8 || startN.Y == -1) //Probably don't need
                startN = startEvent;
            else if (endN.X == -1 || endN.Y == 8)
                endN = endEvent;*/

            //startEvent = startN;
            //endEvent = endN;
            
            //Probably remove..
            {
            /*
            bool c1 = startO.X != startEvent.X;
            bool c2 = startO.Y != startEvent.Y;
            bool c3 = endO.X != endEvent.X;
            bool c4 = endO.Y != endEvent.Y;

            if (c1 && c2 && c3 && c4)
                return;
            else
            {
                bool top = true, left = true, right = true, bottom = true;
                for (int j = (int) startEvent.Y; j >= endEvent.Y; j--)
                {
                    for (int i = (int)startEvent.X; i <= endEvent.X; i++)
                    {
                        if (startEvent.X == endEvent.X || startEvent.Y == endEvent.Y)
                        {
                            hasEvent = false;
                            return;
                        }

                        if (bottom && j == startEvent.Y && !c2)
                        {
                            if (updated[j, i] == true)
                            {
                                bottom = false;
                                i = (int)endEvent.X - 1;
                            }
                            else if (i == endEvent.X)
                                startEvent.Y -= 1;

                        }
                        else if (top && j == endEvent.Y && !c4)
                        {
                            if (updated[j, i] == true)
                            {
                                top = false;
                                i = (int) endEvent.X - 1;
                            }
                            else if (i == endEvent.X)
                            {
                                endEvent.Y += 1;
                                j = (int) endEvent.Y + 1;
                            }
                        }

                        if (left && i == startEvent.X && !c1)
                        {
                            if (updated[j, i] == true)
                                left = false;
                            else if (j == endEvent.Y)
                            {
                                startEvent.X += 1;
                                j = (int) startEvent.Y + 1;
                            }
                        }
                        else if (right && i == endEvent.X && !c3)
                        {
                            if (updated[j, i] == true)
                                right = false;
                            else if (j == endEvent.Y)
                            {
                                endEvent.X -= 1;
                                j = (int) startEvent.Y + 1;
                            }
                        }
                    }
                }
            }
            */}
        }

        public bool marked;//remove later
        public void Draw (int x, int y, int i, int j, int iDist, int jDist, SpriteBatch _spriteBatch)//REDONE
        {
            for (int iiter = i; iiter < i + iDist; iiter ++)
            {
                for (int jiter = j; jiter < j + jDist; jiter ++)
                {
                    Rectangle pixel = new Rectangle (x + (jiter * 5), y + (iiter * 5), 5, 5);

                    Color color = Color.Black;

                    switch (grid [iiter, jiter])
                    {
                        case 0:
                            if (marked == true) color = Color.Red;
                            else color = Color.White;
                            break;
                        case 1:
                            color = Color.Brown;
                            break;
                        case 2:
                            color = Color.Tan;
                            break;
                        case 3:
                            color = Color.Blue;
                            break;
                    }

                    _spriteBatch.Draw (Game1.whiteRectangle, pixel, color);
                }
            }
        }

        /*
         * This method strictly updates a pixel at the given (i, j) coordinate. It does not 
         * check to see if that pixel was updated already however.
         */
        public void updatePixel(int i, int j)//!!Maybe change the update policy from above to actually allow this to be the only place which updates pixels //REDONE
        {
            if (i >= 8 || i < 0) throw new Exception("i: " + i + ", was out of bounds in updatePixel");
            if (j >= 8 || j < 0) throw new Exception("j: " + j + ", was out of bounds in updatePixel");

            switch (grid[i, j])
            {
                case 0:
                    //Air
                    {
                        updated[i, j] = false;
                        return;//Maybe we need to do more?
                    }
                    break;
                case 1:
                    //Metal
                    {
                        updated[i, j] = false;
                        return;//This will certainly require more thought. But for now I want metal to be indestructable
                    }
                    break;
                case 2:
                    //Sand
                    {
                        if (getPixel(i, j, 1, 0) == 0)                                              //The pixel below is air
                        {
                            grid[i, j] = 0;
                            setPixel(i, j, 1, 0, 2, data[i, j]);
                            updated[i, j] = false;
                        }
                        else if (getPixel(i, j, 1, 0) == 1) updated[i, j] = false;                  //The pixel below is metal (As of now, this does nothing)
                        else if (getPixel(i, j, 1, 0) == 2)                                         //The pixel below is sand
                        {
                            bool s1 = getPixel(i, j, 0, -1) == 0 || getPixel(i, j, 0, -1) == 3;
                            bool s2 = getPixel(i, j, 0, 1) == 0 || getPixel(i, j, 0, 1) == 3;

                            if (!s1 && !s2)
                            {
                                updated[i, j] = false;
                                return;
                            }

                            bool c1 = (getPixel(i, j, 1, -1) == 0 || getPixel(i, j, 1, -1) == 3);
                            bool c2 = (getPixel(i, j, 1, 1) == 0 || getPixel(i, j, 1, 1) == 3);

                            if (c1 && c2 && s1 && s2)                                                                  //:The left and right bottom corners are air
                            {
                                if (getPixel(i, j, -1, 2) == 0 && (rand.Next(0, 10) <= 4))                              //:The left side is air (two pixels down), then maybe the sand falls to that side
                                {
                                    grid[i, j] = getPixel(i, j, 1, -1);
                                    setPixel(i, j, 1, -1, 2, data[i, j]);
                                    if (grid[i, j] == 0) updated[i, j] = false;
                                    else updated[i, j] = true;
                                }
                                else if (getPixel(i, j, 1, 2) == 0)                                                     //:The right side is air (two pixels down), then the sand definitely falls to that side
                                {
                                    grid[i, j] = getPixel(i, j, 1, 1);
                                    setPixel(i, j, 1, 1, 2, data[i, j]);
                                    if (grid[i, j] == 0) updated[i, j] = false;
                                    else updated[i, j] = true;
                                }
                                else updated[i, j] = true;
                            }
                            else if (c1 && s1 && (getPixel(i, j, -1, 2) == 0 || getPixel(i, j, -1, 2) == 3))                                                                                //:The left bottom corner is air only
                            {
                                
                                grid[i, j] = getPixel(i, j, 1, -1);
                                setPixel(i, j, 1, -1, 2, data[i, j]);
                                if (grid[i, j] == 0) updated[i, j] = false;
                                else updated[i, j] = true;
                            }
                            else if (c2 && s2 && (getPixel(i, j, 1, 2) == 0 || getPixel(i, j, 1, 2) == 3))                                                                                //:The right bottom corner is air only
                            {
                                grid[i, j] = getPixel(i, j, 1, 1);
                                setPixel(i, j, 1, 1, 2, data[i, j]);
                                if (grid[i, j] == 0) updated[i, j] = false;
                                else updated[i, j] = true;
                            }
                        }
                        else if (getPixel(i, j, 1, 0) == 3)                                         //The pixel below is water
                        {
                            grid[i, j] = 3;
                            setPixel(i, j, 1, 0, 2, data[i, j]);
                            updated [i, j] = true;//true adds weird results
                        }
                        else updated [i, j] = true;
                    }
                    break;
                case 3:
                    //Water
                    {
                        bool s1 = getPixel(i, j, 0, -1) == 0;
                        bool s2 = getPixel(i, j, 0, 1) == 0;


                        if (getPixel(i, j, -1, 0) == 3 && (s1 || s2))                               //The pixel above is water and the left or right sides are air
                        {
                            if (s1 && rand.Next(0, 10) <= 4)
                            {
                                grid[i, j] = 0;
                                setPixel(i, j, 0, -1, 3, data[i, j]);
                                updated[i, j] = false;
                                return;
                            }
                            else if (s2)
                            {
                                grid[i, j] = 0;
                                setPixel(i, j, 0, 1, 3, data[i, j]);
                                updated[i, j] = false;
                                return;
                            }
                        }
                        
                        if (getPixel(i, j, 1, 0) == 0)                                               //The pixel below is air
                        {
                            grid[i, j] = 0;
                            setPixel(i, j, 1, 0, 3, data[i, j]);
                            updated[i, j] = false;
                        }
                        else                                                                         //The pixel below is NOT air
                        {
                            
                            if (!s1 && !s2)
                            {
                                updated [i, j] = false;
                                return;
                            }

                            bool c1 = getPixel(i, j, 1, -1) == 0;
                            bool c2 = getPixel(i, j, 1, 1) == 0;

                            if (c1 && c2 && s1 && s2)                                                                  //:The left and right bottom corners are air
                            {
                                if (rand.Next(0, 10) <= 4)                                                                                    //::Maybe the water falls left
                                {
                                    grid[i, j] = 0;
                                    setPixel(i, j, 1, -1, 3, data[i, j]);
                                    updated[i, j] = false;
                                }
                                else                                                                                                          //::Otherwise the water falls right
                                {
                                    grid[i, j] = 0;
                                    setPixel(i, j, 1, 1, 3, data[i, j]);
                                    updated[i, j] = false;
                                }
                            }
                            else if (c1 && s1)                                                                                //:The left bottom corner is air only
                            {
                                grid[i, j] = 0;
                                setPixel(i, j, 1, -1, 3, data[i, j]);
                                updated[i, j] = false;
                            }
                            else if (c2 && s2)                                                                                //:The right bottom corner is air only
                            {
                                grid[i, j] = 0;
                                setPixel(i, j, 1, 1, 3, data[i, j]);
                                updated[i, j] = false;
                            }
                            else                                                                                        //:No corners are air
                            {
                                bool c3 = getPixel(i, j, 1, -1) == 3;
                                bool c4 = getPixel(i, j, 1, 1) == 3;

                                bool c5 = getPixel(i, j, 0, -2) == 3;
                                bool c6 = getPixel(i, j, 0, 2) == 3;

                                if (s1 && s2 && c5 && c6)
                                {
                                    if (rand.Next(0, 10) <= 4)                                                                             //::Maybe the water moves left
                                    {
                                        grid[i, j] = 0;
                                        setPixel(i, j, 0, -1, 3, data[i, j]);
                                        updated[i, j] = false;
                                    }
                                    else                                                                                                   //::Otherwise the water moves right
                                    {
                                        grid[i, j] = 0;
                                        setPixel(i, j, 0, 1, 3, data[i, j]);
                                        updated[i, j] = false;
                                    }
                                }
                                else if (s1 && c5)
                                {
                                    grid[i, j] = 0;
                                    setPixel(i, j, 0, -1, 3, data[i, j]);
                                    updated[i, j] = false;
                                }
                                else if (s2 && c6)
                                {
                                    grid[i, j] = 0;
                                    setPixel(i, j, 0, 1, 3, data[i, j]);
                                    updated[i, j] = false;
                                }
                                else if (c3 && c4 && s1 && s2)                                                                           //:The left and right bottom corners are water and both sides are air
                                {
                                    if (rand.Next(0, 10) <= 4)                                                                             //::Maybe the water moves left
                                    {
                                        grid[i, j] = 0;
                                        setPixel(i, j, 0, -1, 3, data[i, j]);
                                        updated[i, j] = false;
                                    }
                                    else                                                                                                   //::Otherwise the water moves right
                                    {
                                        grid[i, j] = 0;
                                        setPixel(i, j, 0, 1, 3, data[i, j]);
                                        updated[i, j] = false;
                                    }
                                }
                                else if (c3 && s1)                                                                            //:The left bottom corner is water only
                                {
                                    grid[i, j] = 0;
                                    setPixel(i, j, 0, -1, 3, data[i, j]);
                                    updated[i, j] = false;
                                }
                                else if (c4 && s2)                                                                             //:The right bottom corner is water only
                                {
                                    grid[i, j] = 0;
                                    setPixel(i, j, 0, 1, 3, data[i, j]);
                                    updated[i, j] = false;
                                }
                                else updated[i, j] = false;//Maybe add a move chance for the ground
                            }
                        }
                    }
                    break;
            }
        }

        /*
         * This method takes an (i, j) coord on the graph and returns a pixel at the 
         * end of the (iDist, jDist) vector, even if that pixel is in an adjacent block.
         * 
         * i and j are 0 to 7
         * iDist and jDist are -8 to 8
         */
        public int getPixel(int i, int j, int iDist, int jDist)//REDONE
        {
            if (i >= 8 || i < 0) throw new Exception ("i: " + i + ", was out of bounds in get");
            if (j >= 8 || j < 0) throw new Exception ("j: " + j + ", was out of bounds in get");
            if (iDist >= 16 || iDist < -16) throw new Exception("iDist: " + iDist + ", was suggestively out of bounds in get");
            if (jDist >= 16 || jDist < -16) throw new Exception("jDist: " + jDist + ", was suggestively out of bounds in get");

            bool d1;    //true if positive (down) direction
            bool d2;    //true if positive (right) direction
            bool c1;    //true if the target pixel is within the block vertically
            bool c2;    //true if the target pixel is within the block horizontally

            d1 = iDist >= 0;
            d2 = jDist >= 0;

            if (d1) c1 = iDist <= 7 - i;
            else c1 = -iDist <= i;

            if (d2) c2 = jDist <= 7 - j;
            else c2 = -jDist <= j;

            if (c1 && c2)       //Everything is within the block
                return grid[i + iDist, j + jDist];
            else if (!c1 && !c2) //In a outside block digonally
            {
                if (d1 && d2)       //:Bottom right block
                    return cornerBR.getPixel(0, 0, (iDist - 1) - (7 - i), (jDist - 1) - (7 - j));
                else if (d2)        //:Top right block
                    return cornerTR.getPixel(7, 0, (iDist + 1) + i, (jDist - 1) - (7 - j));
                else if (d1)        //:Bottom left block
                    return cornerBL.getPixel(0, 7, (iDist - 1) - (7 - i), (jDist + 1) + j);
                else                //:Top left block
                    return cornerTL.getPixel(7, 7, (iDist + 1) + i, (jDist + 1) + j);
            }
            else if (c1)        //In a outside block vertically
            {
                if (d2)             //:Right block
                    return right.getPixel(i, 0, iDist, (jDist - 1) - (7 - j));
                else                //:Left block
                    return left.getPixel(i, 7, iDist, (jDist + 1) + j);
            }
            else                //In a outside block horizontally
            {
                if (d1)             //:Bottom block
                    return bottom.getPixel(0, j, (iDist - 1) - (7 - i), jDist);
                else                //:Top block
                    return top.getPixel(7, j, (iDist + 1) + i, jDist);
            }
        }

        bool hasEvent;
        Vector2 startEvent, endEvent;
        public void setPixel (int i, int j, int iDist, int jDist, int pixel, int newData)//REDONE
        {
            if (i >= 8 || i < 0) throw new Exception("i: " + i + ", was out of bounds in set");
            if (j >= 8 || j < 0) throw new Exception("j: " + j + ", was out of bounds in set");

            bool d1;    //true if positive (down) direction
            bool d2;    //true if positive (tight) direction
            bool c1;    //true if the target pixel is within the block vertically
            bool c2;    //true if the target pixel is within the block horizontally

            d1 = iDist >= 0;
            d2 = jDist >= 0;

            if (d1) c1 = iDist <= 7 - i;
            else c1 = -iDist <= i;

            if (d2) c2 = jDist <= 7 - j;
            else c2 = -jDist <= j;

            if (c1 && c2)       //Everything is within the block
            {
                grid [i + iDist, j + jDist] = pixel;
                data [i + iDist, j + jDist] = newData;
                
                updated [i + iDist, j + jDist] = true;
                //MOVE THIS
                if (hasEvent == false)  //:Makes a new event in the block if one does not exist already
                {
                    hasEvent = true;
                    startEvent = endEvent = new Vector2 (i + iDist, j + jDist);
                }
                else                    //:Adds to an existing event in a block (if needed)
                {
                    bool s1 = i + iDist > startEvent.Y;
                    bool s2 = i + iDist < endEvent.Y;
                    bool s3 = j + jDist < startEvent.X;
                    bool s4 = j + jDist > endEvent.X;

                    if (s1 || s2 || s3 || s4)
                    {
                        if (s1)
                            startEvent.Y = i + iDist;
                        else if (s2)
                            endEvent.Y = i + iDist;
                        if (s3)
                            startEvent.X = j + jDist;
                        else if (s4)
                            endEvent.X = j + jDist;
                    }
                }
            }
            else if (!c1 && !c2) //In a outside block digonally
            {
                if (d1 && d2)       //:Bottom right block
                    cornerBR.setPixel (0, 0, (iDist - 1) - (7 - i), (jDist - 1) - (7 - j), pixel, newData);
                else if (d2)        //:Top right block
                    cornerTR.setPixel (7, 0, (iDist + 1) + i, (jDist - 1) - (7 - j), pixel, newData);
                else if (d1)        //:Bottom left block
                    cornerBL.setPixel (0, 7, (iDist - 1) - (7 - i), (jDist + 1) + j, pixel, newData);
                else                //:Top left block
                    cornerTL.setPixel (7, 7, (iDist + 1) + i, (jDist + 1) + j, pixel, newData);
            }
            else if (c1)        //In a outside block vertically
            {
                if (d2)             //:Right block
                    right.setPixel (i, 0, iDist, (jDist - 1) - (7 - j), pixel, newData);
                else                //:Left block
                    left.setPixel (i, 7, iDist, (jDist + 1) + j, pixel, newData);
            }
            else                //In a outside block horizontally
            {
                if (d1)             //:Bottom block
                    bottom.setPixel (0, j, (iDist - 1) - (7 - i), jDist, pixel, newData);
                else                //:Top block
                    top.setPixel (7, j, (iDist + 1) + i, jDist, pixel, newData);
            }
        }
    }
}
