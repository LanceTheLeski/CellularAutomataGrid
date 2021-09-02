using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GridTesting
{
    class Block
    {
        //All relevant data that makes up the block
        private int[,] grid;
        private int[,] data;
        public bool[,] updated;

        //For tracking where events within the block are occuring:
        public bool hasEvent;
        public Point curStartEvent, curEndEvent;
        public Point savedStartEvent, savedEndEvent;

        //For linking to surrounding blocks
        public Block top, left, right, bottom, cornerTL, cornerTR, cornerBL, cornerBR;

        //For debugging:
        public bool showEvent;
        public bool showMarkers;
        public List<Point> markers;

        //Empty constructor:
        public Block()
        {
            //Initialize our matricies:
            grid = new int[8, 8];
            data = new int[8, 8];
            updated = new bool[8, 8];

            //Initialize our Event markers:
            hasEvent = false;
            curStartEvent = new Point(0, 7);
            curEndEvent = new Point(7, 0);
            savedStartEvent = new Point(7, 0);
            savedEndEvent = new Point(0, 7);

            //Initialize our debugging tools:
            showEvent = false;
            showMarkers = false;
            markers = new List<Point>();
        }

        //Constructor for a grid without any conplexity
        public Block(int[,] grid)
        {
            //Initialize our matricies:
            this.grid = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    this.grid[i, j] = grid[i, j];
            data = new int[8, 8];
            updated = new bool[8, 8];

            //Initialize our Event markers:
            hasEvent = true;
            curStartEvent = new Point(0, 7);
            curEndEvent = new Point(7, 0);
            savedStartEvent = new Point(7, 0);
            savedEndEvent = new Point(0, 7);

            //Initialize our debugging tools:
            showEvent = false;
            showMarkers = false;
            markers = new List<Point>();
        }

        //Constructor for a grid with complexity (i.e. physics or structures)
        public Block(int[,] grid, int[,] data, bool[,] updated)
        {
            //Initialize our matricies:
            this.grid = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    this.grid[i, j] = grid[i, j];
            this.data = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    this.data[i, j] = data[i, j];
            this.updated = new bool[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    this.updated[i, j] = updated[i, j];

            //Initialize our Event markers:
            hasEvent = true;
            curStartEvent = new Point(0, 7);
            curEndEvent = new Point(7, 0);
            savedStartEvent = new Point(7, 0);
            savedEndEvent = new Point(0, 7);

            //Initialize our debugging tools:
            showEvent = false;
            showMarkers = false;
            markers = new List<Point>();
        }

        /* 
         * !Important!
         * This method indicates the pattern that the block recieves for blocks linked around it. 
         * I.e. the first block in an array will always be the block directly above the current one.
         */
        public void setLinkedBlocks(Block[] blocks)
        {
            top = blocks[0];
            left = blocks[1];
            right = blocks[2];
            bottom = blocks[3];
            cornerTL = blocks[4];
            cornerTR = blocks[5];
            cornerBL = blocks[6];
            cornerBR = blocks[7];
        }

        public Block [] getLinkedBlocks()
        {
            Block [] ret = {top, left, right, bottom, cornerTL, cornerTR, cornerBL, cornerBR};
            return ret;
        }

        /*
         * This method takes an (i, j) coord on the graph and returns a pixel at the 
         * end of the (iDist, jDist) vector, even if that pixel is in an adjacent block.
         * 
         * i and j are 0 to 7
         * iDist and jDist are -8 to 8 (ideally)
         */
        public int getPixel(int i, int j, int iDist, int jDist)
        {
            //Might need for debugging in the future:
            //if (i >= 8 || i < 0) throw new Exception ("i: " + i + ", was out of bounds in get");
            //if (j >= 8 || j < 0) throw new Exception ("j: " + j + ", was out of bounds in get");
            //if (iDist >= 16 || iDist < -16) throw new Exception("iDist: " + iDist + ", was suggestively out of bounds in get");
            //if (jDist >= 16 || jDist < -16) throw new Exception("jDist: " + jDist + ", was suggestively out of bounds in get");

            bool moveIdown;    //true if positive (down) direction
            bool moveJright;   //true if positive (right) direction
            bool inIbounds;    //true if the target pixel is within the block vertically
            bool inJbounds;    //true if the target pixel is within the block horizontally

            moveIdown = iDist >= 0;
            moveJright = jDist >= 0;

            if (moveIdown) inIbounds = iDist <= 7 - i;
            else inIbounds = -iDist <= i;

            if (moveJright) inJbounds = jDist <= 7 - j;
            else inJbounds = -jDist <= j;

            if (inIbounds && inJbounds)//:Everything is within the block (Best case scenario)
                return this.grid[i + iDist, j + jDist];
            else if (!inIbounds && !inJbounds)//:In a outside block digonally
            {
                if (moveIdown && moveJright)//:Bottom right block
                    return cornerBR.getPixel(0, 0, (iDist - 1) - (7 - i), (jDist - 1) - (7 - j));
                else if (moveJright)//:Top right block
                    return cornerTR.getPixel(7, 0, (iDist + 1) + i, (jDist - 1) - (7 - j));
                else if (moveIdown)//:Bottom left block
                    return cornerBL.getPixel(0, 7, (iDist - 1) - (7 - i), (jDist + 1) + j);
                else//:Top left block
                    return cornerTL.getPixel(7, 7, (iDist + 1) + i, (jDist + 1) + j);
            }
            else if (inIbounds)//:In a outside block vertically
            {
                if (moveJright)//:Right block
                    return right.getPixel(i, 0, iDist, (jDist - 1) - (7 - j));
                else//:Left block
                    return left.getPixel(i, 7, iDist, (jDist + 1) + j);
            }
            else//:In a outside block horizontally
            {
                if (moveIdown)//:Bottom block
                    return bottom.getPixel(0, j, (iDist - 1) - (7 - i), jDist);
                else//:Top block
                    return top.getPixel(7, j, (iDist + 1) + i, jDist);
            }
        }

        /*
         * Ideal for getting (0, 0)
         * Shortcut for when you are certain that the pixel you need to get is within 
         * the block
         * 
         * i and j are 0 to 7
         */
        public int getThisPixel(int i, int j)
        { return this.grid[i, j]; }

        public int getTopPixel(int i, int j)
        { return this.getPixel(i, j, -1, 0); }

        public int getLeftPixel(int i, int j)
        { return this.getPixel(i, j, 0, -1); }

        public int getRightPixel(int i, int j)
        { return this.getPixel(i, j, 0, 1); }

        public int getBottomPixel(int i, int j)
        { return this.getPixel(i, j, 1, 0); }

        public int getTopLeftPixel(int i, int j)
        { return this.getPixel(i, j, -1, -1); }

        public int getTopRightPixel(int i, int j)
        { return this.getPixel(i, j, -1, 1); }

        public int getBottomLeftPixel(int i, int j)
        { return this.getPixel(i, j, 1, -1); }

        public int getBottomRightPixel(int i, int j)
        { return this.getPixel(i, j, 1, 1); }

        /*
         * This method takes an (i, j) coord on the graph and sets a pixel at the 
         * end of the (iDist, jDist) vector, even if that pixel is in an adjacent block.
         * 
         * Note 1: Automatically marks that pixel, with a radius of 1, to be included in 
         *         the next Event (i.e. it will be iterated through).
         * Note 2: Any pixel moved is automatically considered updated, and will not be 
         *         iterated be updated again until the chunk (that the set pixel is in) 
         *         calls the next update.
         * 
         * i and j are 0 to 7
         * iDist and jDist are -8 to 8 (ideally)
         */
        public void setPixel (int i, int j, int iDist, int jDist, int pixel, int data)
        {
            bool moveIdown;    //true if positive (down) direction
            bool moveJright;   //true if positive (tight) direction
            bool inIbounds;    //true if the target pixel is within the block vertically
            bool inJbounds;    //true if the target pixel is within the block horizontally

            moveIdown = iDist >= 0;
            moveJright = jDist >= 0;

            if (moveIdown) inIbounds = iDist <= 7 - i;
            else inIbounds = -iDist <= i;

            if (moveJright) inJbounds = jDist <= 7 - j;
            else inJbounds = -jDist <= j;

            if (inIbounds && inJbounds)//:Everything is within the block (Best case scenario)
            {
                this.grid [i + iDist, j + jDist] = pixel;
                this.data [i + iDist, j + jDist] = data;
                
                if (!moveJright && (!moveIdown || iDist == 0) && !(iDist == 0 && jDist == 0))//:Only updates pixels that haven't been iterated through yet
                    this.updated[i + iDist, j + jDist] = true;

                if (iDist != 0 || jDist != 0)//Automatically sets marker(s) for a potential event
                    setMarkerRadius(i, j, 1);
                setMarkerRadius (i + iDist, j + jDist, 1);
            }
            else if (!inIbounds && !inJbounds)//:In a outside block digonally
            {
                if (moveIdown && moveJright)//:Bottom right block
                    cornerBR.setPixel (0, 0, (iDist - 1) - (7 - i), (jDist - 1) - (7 - j), pixel, data);
                else if (moveJright)//:Top right block
                    cornerTR.setPixel (7, 0, (iDist + 1) + i, (jDist - 1) - (7 - j), pixel, data);
                else if (moveIdown)//:Bottom left block
                    cornerBL.setPixel (0, 7, (iDist - 1) - (7 - i), (jDist + 1) + j, pixel, data);
                else//:Top left block
                    cornerTL.setPixel (7, 7, (iDist + 1) + i, (jDist + 1) + j, pixel, data);
            }
            else if (inIbounds)//:In a outside block vertically
            {
                if (moveJright)//:Right block
                    right.setPixel (i, 0, iDist, (jDist - 1) - (7 - j), pixel, data);
                else//:Left block
                    left.setPixel (i, 7, iDist, (jDist + 1) + j, pixel, data);
            }
            else//:In a outside block horizontally
            {
                if (moveIdown)//:Bottom block
                    bottom.setPixel (0, j, (iDist - 1) - (7 - i), jDist, pixel, data);
                else//:Top block
                    top.setPixel (7, j, (iDist + 1) + i, jDist, pixel, data);
            }
        }

        /*
         * Ideal for setting (0, 0)
         * Shortcut for when you are certain that the pixel you need to set is within 
         * the block
         * 
         * Automatically marks the pixel, with a radius of 1, to the blocks event box
         * 
         * i and j are 0 to 7
         */
        public void setThisPixel(int i, int j, int pixel, int data)
        {
            this.grid[i, j] = pixel;
            this.data[i, j] = data;

            setMarkerRadius(i, j, 1);
        }

        public void setTopPixel(int i, int j, int pixel, int data)
        { this.setPixel(i, j, -1, 0, pixel, data); }

        public void setLeftPixel(int i, int j, int pixel, int data)
        { this.setPixel(i, j, 0, -1, pixel, data); }

        public void setRightPixel(int i, int j, int pixel, int data)
        { this.setPixel(i, j, 0, 1, pixel, data); }

        public void setBottomPixel(int i, int j, int pixel, int data)
        { this.setPixel(i, j, 1, 0, pixel, data); }

        public void setTopLeftPixel(int i, int j, int pixel, int data)
        { this.setPixel(i, j, -1, -1, pixel, data); }

        public void setTopRightPixel(int i, int j, int pixel, int data)
        { this.setPixel(i, j, -1, 1, pixel, data); }

        public void setBottomLeftPixel(int i, int j, int pixel, int data)
        { this.setPixel(i, j, 1, -1, pixel, data); }

        public void setBottomRightPixel(int i, int j, int pixel, int data)
        { this.setPixel(i, j, 1, 1, pixel, data); }

        /*
         * This method takes an (i, j) coord on the graph and returns the encoded data 
         * (i.e. an integer representing additional information about a pixel) at the 
         * end of the (iDist, jDist) vector, even if the data is in an adjacent block.
         * 
         * i and j are 0 to 7
         * iDist and jDist are -8 to 8 (ideally)
         */
        public int getData(int i, int j, int iDist, int jDist)
        {
            //We may need this in the future for debugging:
            //if (i >= 8 || i < 0) throw new Exception("i: " + i + ", was out of bounds in get");
            //if (j >= 8 || j < 0) throw new Exception("j: " + j + ", was out of bounds in get");
            //if (iDist >= 16 || iDist < -16) throw new Exception("iDist: " + iDist + ", was suggestively out of bounds in get");
            //if (jDist >= 16 || jDist < -16) throw new Exception("jDist: " + jDist + ", was suggestively out of bounds in get");

            bool moveIdown;    //true if positive (down) direction
            bool moveJright;   //true if positive (right) direction
            bool inIbounds;    //true if the target pixel is within the block vertically
            bool inJbounds;    //true if the target pixel is within the block horizontally

            moveIdown = iDist >= 0;
            moveJright = jDist >= 0;

            if (moveIdown) inIbounds = iDist <= 7 - i;
            else inIbounds = -iDist <= i;

            if (moveJright) inJbounds = jDist <= 7 - j;
            else inJbounds = -jDist <= j;

            if (inIbounds && inJbounds)//:Everything is within the block
                return data[i + iDist, j + jDist];
            else if (!inIbounds && !inJbounds) //:In a outside block digonally
            {
                if (moveIdown && moveJright)//:Bottom right block
                    return cornerBR.getPixel(0, 0, (iDist - 1) - (7 - i), (jDist - 1) - (7 - j));
                else if (moveJright)//:Top right block
                    return cornerTR.getPixel(7, 0, (iDist + 1) + i, (jDist - 1) - (7 - j));
                else if (moveIdown)//:Bottom left block
                    return cornerBL.getPixel(0, 7, (iDist - 1) - (7 - i), (jDist + 1) + j);
                else//:Top left block
                    return cornerTL.getPixel(7, 7, (iDist + 1) + i, (jDist + 1) + j);
            }
            else if (inIbounds)//:In a outside block vertically
            {
                if (moveJright)//:Right block
                    return right.getPixel(i, 0, iDist, (jDist - 1) - (7 - j));
                else//:Left block
                    return left.getPixel(i, 7, iDist, (jDist + 1) + j);
            }
            else//:In a outside block horizontally
            {
                if (moveIdown)//:Bottom block
                    return bottom.getPixel(0, j, (iDist - 1) - (7 - i), jDist);
                else//:Top block
                    return top.getPixel(7, j, (iDist + 1) + i, jDist);
            }
        }

        /*
         * Ideal for getting (0, 0)
         * Shortcut for when you are certain that the data you need to get is within 
         * the block
         * 
         * i and j are 0 to 7
         */
        public int getThisData(int i, int j)
        { return this.data[i, j]; }

        /*
         * Ideal for setting a radius at (0, 0).
         * 
         * Ensures that the event surrounding the given position (i, j) is marked for the next update.
         * There is also an argument for updating outward in a square radius.
         * 
         * Intuitively, this is done because if a pixel within our block moves, then we might want to
         * check surrounding pixels the next time we update. Thus the butterfly effect of natural forces
         * can ripple out from block to block (if need be).
         * 
         * i and j are 0 to 7
         * radius can be any number
         */
        public void setMarkerRadius(int i, int j, int radius)
        {
            if (hasEvent == false)//:Makes a new event in the block if one does not exist already
            {
                hasEvent = true;
                curStartEvent = new Point(i, j);
                curEndEvent = new Point(i, j);

                markers.Add(new Point(i, j));
            }
            else//:Adds to an existing event in a block (if needed)
            {
                if (i >= curStartEvent.i) curStartEvent.i = i;
                if (i <= curEndEvent.i) curEndEvent.i = i;
                if (j <= curStartEvent.j) curStartEvent.j = j;
                if (j >= curEndEvent.j) curEndEvent.j = j;

                markers.Add(new Point(i, j));
            }

            for (int t = radius; t > 0; t--)
            {
                setMarker(i, j, -t, -t);
                setMarker(i, j, -t, t);
                setMarker(i, j, t, -t);
                setMarker(i, j, t, t);
            }
        }

        /*
         * This method takes an (i, j) coord on the graph and ensures that the pixel at 
         * the end of the (iDist, jDist) vector is marked within the event space, even 
         * if that pixel is in an adjacent block.
         * 
         * i and j are 0 to 7
         * iDist and jDist are -8 to 8 (ideally)
         */
        public void setMarker(int i, int j, int iDist, int jDist)
        {
            bool moveIdown;    //true if positive (down) direction
            bool moveJright;   //true if positive (right) direction
            bool inIbounds;    //true if the target pixel is within the block vertically
            bool inJbounds;    //true if the target pixel is within the block horizontally

            moveIdown = iDist >= 0;
            moveJright = jDist >= 0;

            if (moveIdown) inIbounds = iDist <= 7 - i;
            else inIbounds = -iDist <= i;

            if (moveJright) inJbounds = jDist <= 7 - j;
            else inJbounds = -jDist <= j;

            if (inIbounds && inJbounds)//:Everything is within the block (Best case scenario)
            {
                if (hasEvent == false)//:Makes a new event in the block if one does not exist already
                {
                    hasEvent = true;
                    curStartEvent = new Point(i + iDist, j + jDist);
                    curEndEvent = new Point(i + iDist, j + jDist);

                    markers.Add(new Point(i + iDist, j + jDist));
                }
                else//:Adds to an existing event in a block (if needed)
                {
                    if (i + iDist >= curStartEvent.i) curStartEvent.i = i + iDist;
                    if (i + iDist <= curEndEvent.i) curEndEvent.i = i + iDist;
                    if (j + jDist <= curStartEvent.j) curStartEvent.j = j + jDist;
                    if (j + jDist >= curEndEvent.j) curEndEvent.j = j + jDist;

                    markers.Add(new Point(i + iDist, j + jDist));
                }
            }
            else if (!inIbounds && !inJbounds)//:In a outside block digonally
            {
                if (moveIdown && moveJright)//:Bottom right block
                    cornerBR.setMarker(0, 0, (iDist - 1) - (7 - i), (jDist - 1) - (7 - j));
                else if (moveJright)//:Top right block
                    cornerTR.setMarker(7, 0, (iDist + 1) + i, (jDist - 1) - (7 - j));
                else if (moveIdown)//:Bottom left block
                    cornerBL.setMarker(0, 7, (iDist - 1) - (7 - i), (jDist + 1) + j);
                else//:Top left block
                    cornerTL.setMarker(7, 7, (iDist + 1) + i, (jDist + 1) + j);
            }
            else if (inIbounds)//:In a outside block vertically
            {
                if (moveJright)//:Right block
                    right.setMarker(i, 0, iDist, (jDist - 1) - (7 - j));
                else//:Left block
                    left.setMarker(i, 7, iDist, (jDist + 1) + j);
            }
            else//:In a outside block horizontally
            {
                if (moveIdown)//:Bottom block
                    bottom.setMarker(0, j, (iDist - 1) - (7 - i), jDist);
                else//:Top block
                    top.setMarker(7, j, (iDist + 1) + i, jDist);
            }
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
