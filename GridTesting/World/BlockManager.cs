using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;

namespace GridTesting.World
{
    static class BlockManager
    {
        /*
        * This method strictly updates a pixel at the given (i, j) coordinate. It does not 
        * check to see if that pixel was updated already however.
        */
        public static void update(Block block)
        {
            block.markers = new List<Point>();//(For debugging only)

            //Determines final start and end points for the impending update loop
            block.curStartEvent.i = Math.Max(block.curStartEvent.i, block.savedStartEvent.i);
            block.curStartEvent.j = Math.Min(block.curStartEvent.j, block.savedStartEvent.j);
            block.curEndEvent.i = Math.Min(block.curEndEvent.i, block.savedEndEvent.i);
            block.curEndEvent.j = Math.Max(block.curEndEvent.j, block.savedEndEvent.j);

            //Sets final start and end points for the imepnding update loop
            Point useStartEvent = block.curStartEvent;
            Point useEndEvent = block.curEndEvent;

            //Reset the current event markers (points) so they can mark where the event for the next update loop (after this one) for this block will start and end
            block.curStartEvent = new Point(0, 7);
            block.curEndEvent = new Point(7, 0);

            bool updated = false;
            for (int i = useStartEvent.i; i >= useEndEvent.i; i--)
            {
                for (int j = useStartEvent.j; j <= useEndEvent.j; j++)
                {
                    if (block.updated[i, j] == true) updated = true;
                    else if (PixelManager.updatePixel(block, i, j) == true) updated = true;
                }
            }

            if (updated == false) block.hasEvent = false;//:Literally nothing gets updated, then remove event

            //Saves any markers (points) which were added for the next update loop of this block
            block.savedStartEvent = block.curStartEvent;
            block.savedEndEvent = block.curEndEvent;

            block.updated = new bool[8, 8];
        }

        /*
         * Draws the enitre block in its current state. Additionally, adds 
         * 
         */
        public static void Draw(Block block, int x, int y, int i, int j, int iDist, int jDist, SpriteBatch _spriteBatch)
        {
            for (int iiter = i; iiter < i + iDist; iiter++)
            {
                for (int jiter = j; jiter < j + jDist; jiter++)
                {
                    Rectangle pixel = new Rectangle(x + (jiter * 5), y + (iiter * 5), 5, 5);

                    Color color = Color.Black;
                    
                    switch (block.getThisPixel(iiter, jiter))
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

                    _spriteBatch.Draw(Game1.whiteRectangle, pixel, color);
                }
            }

            if (block.showEvent && block.hasEvent)
                DrawEvent(block, x, y, i, j, iDist, jDist, _spriteBatch);

            if (block.showMarkers)
                DrawMarkers(block, x, y, i, j, iDist, jDist, _spriteBatch);
        }

        public static void DrawEvent(Block block, int x, int y, int i, int j, int iDist, int jDist, SpriteBatch _spriteBatch)
        {
            Rectangle bottom = new Rectangle(x + (block.curStartEvent.j * 5) + 2, y + (block.curStartEvent.i * 5) + 3, (block.curEndEvent.j - block.curStartEvent.j) * 5 + 3, 2);
            Rectangle left = new Rectangle(x + (block.curStartEvent.j * 5), y + (block.curEndEvent.i * 5), 2, (block.curStartEvent.i - block.curEndEvent.i) * 5 + 5);
            Rectangle top = new Rectangle(x + (block.curStartEvent.j * 5) + 2, y + (block.curEndEvent.i * 5), (block.curEndEvent.j - block.curStartEvent.j) * 5 + 3, 2);
            Rectangle right = new Rectangle(x + (block.curEndEvent.j * 5) + 3, y + (block.curEndEvent.i * 5), 2, (block.curStartEvent.i - block.curEndEvent.i) * 5 + 5);

            Rectangle startB = new Rectangle(x + (block.curStartEvent.j * 5), y + (block.curStartEvent.i * 5) + 2, 3, 3);
            Rectangle endB = new Rectangle(x + (block.curEndEvent.j * 5) + 2, y + (block.curEndEvent.i * 5), 3, 3);

            _spriteBatch.Draw(Game1.whiteRectangle, bottom, Color.Red);

            _spriteBatch.Draw(Game1.whiteRectangle, left, Color.Red);

            _spriteBatch.Draw(Game1.whiteRectangle, top, Color.Red);

            _spriteBatch.Draw(Game1.whiteRectangle, right, Color.Red);

            _spriteBatch.Draw(Game1.whiteRectangle, startB, Color.LightGreen);

            _spriteBatch.Draw(Game1.whiteRectangle, endB, Color.DarkGreen);
        }

        public static void DrawMarkers (Block block, int x, int y, int i, int j, int iDist, int jDist, SpriteBatch _spriteBatch)
        {
            for (int t = 0; t < block.markers.Count; t++)
            {
                Rectangle pixel = new Rectangle((x + block.markers[t].j * 5) + 1, y + (block.markers[t].i * 5) + 1, 3, 3);

                Color color = Color.Purple;

                _spriteBatch.Draw(Game1.whiteRectangle, pixel, color);
            }
        }
    }
}
