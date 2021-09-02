using System;
using System.Collections.Generic;
using System.Text;

namespace GridTesting
{
    static class PixelManager
    {
        public static Random rand = new Random();

        public const int AIR = 0;
        public const int SMOKE = 1;

        public const int WATER = 5;
        public const int ACID = 10;

        public const int SAND = 10;
        public const int METAL = 11;
        public const int CONCRETE = 12;
        public const int WOOD = 13;

        public const int EMBER_HOT = 20;
        public const int EMBER_WARM = 21;

        /*
         * Updates the corresponding pixel and returns true only if the pixel moves or changes state. Otherwise if the pixel remains in the same spot then return false;
         */
        public static bool updatePixel (Block block, int i, int j)
        {
            switch (block.getThisPixel(i, j))
            {
                case PixelManager.AIR:
                    {
                        return false;
                    }
                case PixelManager.SAND:
                    {
                        return Pixel.Sand.update(block, i, j);
                    }
                case PixelManager.WATER:
                    {
                        return Pixel.Water.updateOld(block, i, j);
                    }
                case PixelManager.METAL:
                    {
                        return false;
                    }
            }

            return false;
        }

        public static bool isGas (int pixelID)
        {
            return (pixelID >= 0 && pixelID <= 4);
        }

        public static bool isLiquid(int pixelID)
        {
            return (pixelID >= 5 && pixelID <= 9);
        }

        public static bool isSolid(int pixelID)
        {
            return (pixelID >= 10 && pixelID <= 19);
        }

        public static bool isParticle (int pixelID)
        {
            return (pixelID >= 20 && pixelID <= 29);
        }
    }


}
