using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GridTesting
{
    class Block
    {
        //Pixels.Sand sand = new Pixels.Sand();//For testing. Remove soon.
        
        //Random rand = new Random();

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
            startEvent = new Point(0, 7);
            endEvent = new Point(7, 0);
            startN = new Point(7, 0);
            endN = new Point(0, 7);

            marked = false;//remove later
            altMarked = false;
            markers = new List<Point>();
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
            startEvent = new Point(0, 7);
            endEvent = new Point(7, 0);
            startN = new Point(7, 0);
            endN = new Point(0, 7);

            marked = false;//remove later
            altMarked = false;
            markers = new List<Point>();
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

        bool hasEvent;
        Point startEvent, endEvent;
        Point startN, endN;
        public void update()
        {
            markers = new List<Point>();

            startEvent.i = Math.Max(startEvent.i, startN.i);
            startEvent.j = Math.Min(startEvent.j, startN.j);
            endEvent.i = Math.Min(endEvent.i, endN.i);
            endEvent.j = Math.Max(endEvent.j, endN.j);

            Point startO = startEvent, endO = endEvent;
            
            startEvent = new Point(0, 7);
            endEvent = new Point(7, 0);
            bool updating = false;

            for (int i = startO.i; i >= endO.i; i--)
            {

                for (int j = startO.j; j <= endO.j; j++)
                {
                    if (this.updated[i, j] == true)
                    {
                        updating = true;
                        this.updated[i, j] = false;
                    }
                    else if (Pixel.updatePixel(this, i, j) == true) updating = true;
                }
            }

            if (updating == false) hasEvent = false;

            startN = startEvent;
            endN = endEvent;

            updated = new bool[8, 8] { { false, false, false, false, false, false, false, false } , //0,
                                       { false, false, false, false, false, false, false, false } , //1,
                                       { false, false, false, false, false, false, false, false } , //2,
                                       { false, false, false, false, false, false, false, false } , //3,
                                       { false, false, false, false, false, false, false, false } , //4,
                                       { false, false, false, false, false, false, false, false } , //5,
                                       { false, false, false, false, false, false, false, false } , //6,
                                       { false, false, false, false, false, false, false, false } };//7

            //startEvent = new Point(0, 7);
            //endEvent = new Point(7, 0);

            //if (startN.j == 8 && startN.i == -1 && endN.j == -1 && endN.i == 8)
            {
                //hasEvent = false;
                //return;
            }
            /*else if (startN.j == 8 || startN.i == -1) //Probably don't need
                startN = startEvent;
            else if (endN.j == -1 || endN.i == 8)
                endN = endEvent;*/

            //startEvent = startN;
            //endEvent = endN;
            
            //Probably remove..
            {
            /*
            bool c1 = startO.j != startEvent.j;
            bool c2 = startO.i != startEvent.i;
            bool c3 = endO.j != endEvent.j;
            bool c4 = endO.i != endEvent.i;

            if (c1 && c2 && c3 && c4)
                return;
            else
            {
                bool top = true, left = true, right = true, bottom = true;
                for (int j =  startEvent.i; j >= endEvent.i; j--)
                {
                    for (int i = startEvent.j; i <= endEvent.j; i++)
                    {
                        if (startEvent.j == endEvent.j || startEvent.i == endEvent.i)
                        {
                            hasEvent = false;
                            return;
                        }

                        if (bottom && j == startEvent.i && !c2)
                        {
                            if (updated[j, i] == true)
                            {
                                bottom = false;
                                i = endEvent.j - 1;
                            }
                            else if (i == endEvent.j)
                                startEvent.i -= 1;

                        }
                        else if (top && j == endEvent.i && !c4)
                        {
                            if (updated[j, i] == true)
                            {
                                top = false;
                                i =  endEvent.j - 1;
                            }
                            else if (i == endEvent.j)
                            {
                                endEvent.i += 1;
                                j =  endEvent.i + 1;
                            }
                        }

                        if (left && i == startEvent.j && !c1)
                        {
                            if (updated[j, i] == true)
                                left = false;
                            else if (j == endEvent.i)
                            {
                                startEvent.j += 1;
                                j =  startEvent.i + 1;
                            }
                        }
                        else if (right && i == endEvent.j && !c3)
                        {
                            if (updated[j, i] == true)
                                right = false;
                            else if (j == endEvent.i)
                            {
                                endEvent.j -= 1;
                                j =  startEvent.i + 1;
                            }
                        }
                    }
                }
            }
            */}
        }

        public void setMarkerRadius (int i, int j, int radius)
        {
            if (hasEvent == false)  //:Makes a new event in the block if one does not exist already
            {
                hasEvent = true;
                startEvent = new Point(i, j);
                endEvent = new Point(i, j);

                markers.Add(new Point(i, j));
            }
            else                    //:Adds to an existing event in a block (if needed)
            {
                if (i >= startEvent.i) startEvent.i = i;
                if (i <= endEvent.i) endEvent.i = i;
                if (j <= startEvent.j) startEvent.j = j;
                if (j >= endEvent.j) endEvent.j = j;

                markers.Add(new Point(i, j));
            }

            for (int t = radius; t > 0; t--)
            {
                setMarker(i, j, -t, -t);
                setMarker(i, j, -t, t);
                setMarker(i, j, t, -t);
                setMarker(i, j, t, t);
                //setMarker(i, j, radius, 0);
                //setMarker(i, j, -radius, 0);
                //setMarker(i, j, 0, radius);
                //setMarker(i, j, 0, -radius);
            }
            //setMarker(i, j, 0, 0);
        }

        public bool marked;//remove later
        public bool altMarked;
        List<Point> markers;
        public void Draw (int x, int y, int i, int j, int iDist, int jDist, SpriteBatch _spriteBatch)
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
                            color = Color.White;
                            break;
                        case 11:
                            color = Color.Brown;
                            break;
                        case 10:
                            color = Color.Tan;
                            break;
                        case 5:
                            color = Color.Blue;
                            break;
                    }

                    _spriteBatch.Draw (Game1.whiteRectangle, pixel, color);
                }
            }

            if (marked && hasEvent)
            {
                Rectangle bottom = new Rectangle(x + ( startEvent.j * 5) + 2, y + ( startEvent.i * 5) + 3, ( endEvent.j -  startEvent.j) * 5 + 3, 2);
                Rectangle left = new Rectangle(x + (startEvent.j * 5), y + (endEvent.i * 5), 2, (startEvent.i - endEvent.i) * 5 + 5);
                Rectangle top = new Rectangle(x + (startEvent.j * 5) + 2, y + (endEvent.i * 5), (endEvent.j - startEvent.j) * 5 + 3, 2);
                Rectangle right = new Rectangle(x + (endEvent.j * 5) + 3, y + (endEvent.i * 5), 2, (startEvent.i - endEvent.i) * 5 + 5);

                Rectangle startB = new Rectangle(x + (startEvent.j * 5), y + (startEvent.i * 5) + 2, 3, 3);
                Rectangle endB = new Rectangle(x + (endEvent.j * 5) + 2, y + (endEvent.i * 5), 3, 3);

                _spriteBatch.Draw(Game1.whiteRectangle, bottom, Color.Red);
                
                _spriteBatch.Draw(Game1.whiteRectangle, left, Color.Red);
                
                _spriteBatch.Draw(Game1.whiteRectangle, top, Color.Red);
                
                _spriteBatch.Draw(Game1.whiteRectangle, right, Color.Red);

                _spriteBatch.Draw(Game1.whiteRectangle, startB, Color.LightGreen);

                _spriteBatch.Draw(Game1.whiteRectangle, endB, Color.DarkGreen);
            }

            if (altMarked)
            {
                for (int t = 0; t < markers.Count; t++)
                {
                    Rectangle pixel = new Rectangle((x + markers[t].j * 5) + 1, y + (markers[t].i * 5) + 1, 3, 3);

                    Color color = Color.Purple;

                    _spriteBatch.Draw(Game1.whiteRectangle, pixel, color);
                }
            }
        }

        /*
         * This method strictly updates a pixel at the given (i, j) coordinate. It does not 
         * check to see if that pixel was updated already however.
         */
        /*public void updatePixel(int i, int j)//!!Maybe change the update policy from above to actually allow this to be the only place which updates pixels
        {
            if (i >= 8 || i < 0) throw new Exception("i: " + i + ", was out of bounds in updatePixel");
            if (j >= 8 || j < 0) throw new Exception("j: " + j + ", was out of bounds in updatePixel");

            
            switch (grid[i, j])
            {
                case Pixel.AIR:
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
        }*/

        /*
         * This method takes an (i, j) coord on the graph and returns a pixel at the 
         * end of the (iDist, jDist) vector, even if that pixel is in an adjacent block.
         * 
         * i and j are 0 to 7
         * iDist and jDist are -8 to 8
         */
        public int getPixel(int i, int j, int iDist, int jDist)
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
                return this.grid[i + iDist, j + jDist];
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

        public void setPixel (int i, int j, int iDist, int jDist, int pixel, int data)
        {
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
                this.grid [i + iDist, j + jDist] = pixel;
                this.data [i + iDist, j + jDist] = data;
                
                if (d2 && (!d1 || iDist == 0) && !(iDist == 0 && jDist == 0)) this.updated [i + iDist, j + jDist] = true;

                //setMarker(i, j, 0, 0);
                //setMarker(i, j, iDist, jDist);
                if (iDist != 0 || jDist != 0) setMarkerRadius (i, j, 1);
                setMarkerRadius (i + iDist, j + jDist, 1);
            }
            else if (!c1 && !c2) //In a outside block digonally
            {
                if (d1 && d2)       //:Bottom right block
                    cornerBR.setPixel (0, 0, (iDist - 1) - (7 - i), (jDist - 1) - (7 - j), pixel, data);
                else if (d2)        //:Top right block
                    cornerTR.setPixel (7, 0, (iDist + 1) + i, (jDist - 1) - (7 - j), pixel, data);
                else if (d1)        //:Bottom left block
                    cornerBL.setPixel (0, 7, (iDist - 1) - (7 - i), (jDist + 1) + j, pixel, data);
                else                //:Top left block
                    cornerTL.setPixel (7, 7, (iDist + 1) + i, (jDist + 1) + j, pixel, data);
            }
            else if (c1)        //In a outside block vertically
            {
                if (d2)             //:Right block
                    right.setPixel (i, 0, iDist, (jDist - 1) - (7 - j), pixel, data);
                else                //:Left block
                    left.setPixel (i, 7, iDist, (jDist + 1) + j, pixel, data);
            }
            else                //In a outside block horizontally
            {
                if (d1)             //:Bottom block
                    bottom.setPixel (0, j, (iDist - 1) - (7 - i), jDist, pixel, data);
                else                //:Top block
                    top.setPixel (7, j, (iDist + 1) + i, jDist, pixel, data);
            }
        }

        public int getData(int i, int j, int iDist, int jDist)
        {
            if (i >= 8 || i < 0) throw new Exception("i: " + i + ", was out of bounds in get");
            if (j >= 8 || j < 0) throw new Exception("j: " + j + ", was out of bounds in get");
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
                return data[i + iDist, j + jDist];
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

        public void setMarker(int i, int j, int iDist, int jDist)
        {
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
            {
                if (hasEvent == false)  //:Makes a new event in the block if one does not exist already
                {
                    hasEvent = true;
                    startEvent = new Point(i + iDist, j + jDist);//0, 7 initially
                    endEvent = new Point(i + iDist, j + jDist);//7, 0 initially

                    markers.Add(new Point(i + iDist, j + jDist));
                }
                else                    //:Adds to an existing event in a block (if needed)
                {
                    if (i + iDist >= startEvent.i) startEvent.i = i + iDist;
                    if (i + iDist <= endEvent.i) endEvent.i = i + iDist;
                    if (j + jDist <= startEvent.j) startEvent.j = j + jDist;
                    if (j + jDist >= endEvent.j) endEvent.j = j + jDist;

                    markers.Add(new Point(i + iDist, j + jDist));
                }
            }
            else if (!c1 && !c2) //In a outside block digonally
            {
                if (d1 && d2)       //:Bottom right block
                    cornerBR.setMarker(0, 0, (iDist - 1) - (7 - i), (jDist - 1) - (7 - j));
                else if (d2)        //:Top right block
                    cornerTR.setMarker(7, 0, (iDist + 1) + i, (jDist - 1) - (7 - j));
                else if (d1)        //:Bottom left block
                    cornerBL.setMarker(0, 7, (iDist - 1) - (7 - i), (jDist + 1) + j);
                else                //:Top left block
                    cornerTL.setMarker(7, 7, (iDist + 1) + i, (jDist + 1) + j);
            }
            else if (c1)        //In a outside block vertically
            {
                if (d2)             //:Right block
                    right.setMarker(i, 0, iDist, (jDist - 1) - (7 - j));//(jDist - 1) - (7 - j)Also aplied to corners above
                else                //:Left block
                    left.setMarker(i, 7, iDist, (jDist + 1) + j);
            }
            else                //In a outside block horizontally
            {
                if (d1)             //:Bottom block
                    bottom.setMarker(0, j, (iDist - 1) - (7 - i), jDist);//(iDist - 1) - (7 - i)
                else                //:Top block
                    top.setMarker(7, j, (iDist + 1) + i, jDist);
            }
        }

        public void setUpdated(int i, int j, int iDist, int jDist, bool updated)
        {
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
                this.updated[i + iDist, j + jDist] = updated;
                //if (updated == true) this.setThisMarker(i, j, 2);
            }
            else if (!c1 && !c2) //In a outside block digonally
            {
                if (d1 && d2)       //:Bottom right block
                    cornerBR.setUpdated(0, 0, (iDist - 1) - (7 - i), (jDist - 1) - (7 - j), updated);
                else if (d2)        //:Top right block
                    cornerTR.setUpdated(7, 0, (iDist + 1) + i, (jDist - 1) - (7 - j), updated);
                else if (d1)        //:Bottom left block
                    cornerBL.setUpdated(0, 7, (iDist - 1) - (7 - i), (jDist + 1) + j, updated);
                else                //:Top left block
                    cornerTL.setUpdated(7, 7, (iDist + 1) + i, (jDist + 1) + j, updated);
            }
            else if (c1)        //In a outside block vertically
            {
                if (d2)             //:Right block
                    right.setUpdated(i, 0, iDist, (jDist - 1) - (7 - j), updated);
                else                //:Left block
                    left.setUpdated(i, 7, iDist, (jDist + 1) + j, updated);
            }
            else                //In a outside block horizontally
            {
                if (d1)             //:Bottom block
                    bottom.setUpdated(0, j, (iDist - 1) - (7 - i), jDist, updated);
                else                //:Top block
                    top.setUpdated(7, j, (iDist + 1) + i, jDist, updated);
            }
        }

        public int getThisPixel (int i, int j)
        {
            return this.grid[i, j];
        }

        public int getThisData (int i, int j)
        {
            return this.data[i, j];
        }

        public int getTopPixel (int i, int j)
        {
            return this.getPixel(i, j, -1, 0);
        }

        public int getLeftPixel(int i, int j)
        {
            return this.getPixel(i, j, 0, -1);
        }

        public int getRightPixel(int i, int j)
        {
            return this.getPixel(i, j, 0, 1);
        }

        public int getBottomPixel(int i, int j)
        {
            return this.getPixel(i, j, 1, 0);
        }

        public int getTopLeftPixel(int i, int j)
        {
            return this.getPixel(i, j, -1, -1);
        }

        public int getTopRightPixel(int i, int j)
        {
            return this.getPixel(i, j, -1, 1);
        }

        public int getBottomLeftPixel(int i, int j)
        {
            return this.getPixel(i, j, 1, -1);
        }

        public int getBottomRightPixel(int i, int j)
        {
            return this.getPixel(i, j, 1, 1);
        }

        public void setThisPixel (int i, int j, int pixel, int data)
        {
            this.grid[i, j] = pixel;
            this.data[i, j] = data;
            //updated[i, j] = true;
            setMarkerRadius (i, j, 1);
        }

        public void setTopPixel(int i, int j, int pixel, int data)
        {
            this.setPixel(i, j, -1, 0, pixel, data);
        }

        public void setLeftPixel(int i, int j, int pixel, int data)
        {
            this.setPixel(i, j, 0, -1, pixel, data);
        }

        public void setRightPixel(int i, int j, int pixel, int data)
        {
            this.setPixel(i, j, 0, 1, pixel, data);
        }

        public void setBottomPixel(int i, int j, int pixel, int data)
        {
            this.setPixel(i, j, 1, 0, pixel, data);
        }

        public void setTopLeftPixel(int i, int j, int pixel, int data)
        {
            this.setPixel(i, j, -1, -1, pixel, data);
        }

        public void setTopRightPixel(int i, int j, int pixel, int data)
        {
            this.setPixel(i, j, -1, 1, pixel, data);
        }

        public void setBottomLeftPixel(int i, int j, int pixel, int data)
        {
            this.setPixel(i, j, 1, -1, pixel, data);
        }

        public void setBottomRightPixel(int i, int j, int pixel, int data)
        {
            this.setPixel(i, j, 1, 1, pixel, data);
        }
    }

    class Point
    {
        public int i, j;
        public Point (int i, int j)
        {
            this.i = i;
            this.j = j;
        }
    }
}
