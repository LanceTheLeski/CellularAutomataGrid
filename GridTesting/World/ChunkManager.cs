using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;

namespace GridTesting.World
{
    static class ChunkManager
    {
        public static Random rand = new Random();

        public static int blockSpace = 40;
        public static void update(Chunk chunk)
        {
            for (int j = 0; j < 16; j++)
                for (int i = 15; i >= 0; i--)
                    if (chunk.blocks[i, j].hasEvent) BlockManager.update(chunk.blocks[i, j]);
        }

        public static void Draw(Chunk chunk, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();

            int y = 0;
            for (int i = 0; i < 16; i++)
            {
                int x = 0;
                for (int j = 0; j < 16; j++)
                {
                    BlockManager.Draw(chunk.blocks[i, j], x + chunk.xoff, y + chunk.yoff, 0, 0, 8, 8, _spriteBatch);
                    x += blockSpace;
                }
                y += blockSpace;
            }

            _spriteBatch.End();
        }

        public static void DrawEvents(Chunk chunk)
        {
            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                    chunk.blocks[i, j].showEvent = !chunk.blocks[i, j].showEvent;

            chunk.showEvents = !chunk.showEvents;
        }

        public static void DrawMarkers(Chunk chunk)
        {
            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                    chunk.blocks[i, j].showMarkers = !chunk.blocks[i, j].showMarkers;

            chunk.showMarkers = !chunk.showMarkers;
        }
    }
}
