using System;
using System.Collections.Generic;
using System.Text;

namespace GridTesting.Pixel
{
    static class Sand
    {
        public static bool update (Block block, int i, int j)
        {
            int bottom = block.getBottomPixel(i, j);

            if (PixelManager.isGas(bottom))//:The bottom is gas
            {
                block.setThisPixel(i, j, bottom, 0);
                block.setBottomPixel(i, j, PixelManager.SAND, 0);
                return true;
            }
            else if (PixelManager.isLiquid(bottom))//:The bottom is liquid
            {
                block.setThisPixel(i, j, bottom, 0);
                block.setBottomPixel(i, j, PixelManager.SAND, 0);

                //block.setUpdated(i, j, 0, 0, true);
                return true;
            }
            else if (PixelManager.isSolid(bottom))//:The bottom is solid
            {
                bool L_clear = block.getLeftPixel(i, j) == PixelManager.AIR || block.getLeftPixel(i, j) == PixelManager.WATER;
                bool R_clear = block.getRightPixel(i, j) == PixelManager.AIR || block.getRightPixel(i, j) == PixelManager.WATER;

                if (!L_clear && !R_clear) return false;//:Both sides are water

                bool BL_clear = block.getBottomLeftPixel(i, j) == PixelManager.AIR || block.getBottomLeftPixel(i, j) == PixelManager.WATER;
                bool BR_clear = block.getBottomRightPixel(i, j) == PixelManager.AIR || block.getBottomRightPixel(i, j) == PixelManager.WATER;

                if (BL_clear && BR_clear && L_clear && R_clear)
                {
                    if (block.getPixel(i, j, -1, 2) == PixelManager.AIR && (PixelManager.rand.Next(0, 10) <= 4))//:The left side is air (two pixels down), then maybe the sand falls to that side
                    {
                        block.setThisPixel(i, j, block.getBottomLeftPixel(i, j), block.getData(i, j, 0, 0));
                        block.setBottomLeftPixel(i, j, PixelManager.SAND, block.getData(i, j, 0, 0));
                        return true;
                    }
                    else if (block.getPixel(i, j, 1, 2) == PixelManager.AIR)//:The right side is air (two pixels down), then the sand definitely falls to that side
                    {
                        block.setThisPixel(i, j, block.getBottomRightPixel(i, j), block.getData(i, j, 0, 0));
                        block.setBottomRightPixel(i, j, PixelManager.SAND, block.getData(i, j, 0, 0));
                        return true;
                    }
                }
                else if (BL_clear && L_clear && (PixelManager.isGas(block.getPixel(i, j, -1, 2)) || PixelManager.isLiquid(block.getPixel(i, j, -1, 2))))//:The left side is clear                                            //:The left bottom corner is air only
                {
                    block.setThisPixel(i, j, block.getBottomLeftPixel(i, j), block.getData(i, j, 0, 0));
                    block.setBottomLeftPixel(i, j, PixelManager.SAND, block.getData(i, j, 0, 0));
                    return true;
                }
                else if (BR_clear && R_clear && (PixelManager.isGas(block.getPixel(i, j, 1, 2)) || PixelManager.isLiquid(block.getPixel(i, j, 1, 2))))//:The right side is clear                                                                          //:The right bottom corner is air only
                {
                    block.setThisPixel(i, j, block.getBottomRightPixel(i, j), block.getData(i, j, 0, 0));
                    block.setBottomRightPixel(i, j, PixelManager.SAND, block.getData(i, j, 0, 0));
                    return true;
                }
            }
            return false;
        }

    }
}
