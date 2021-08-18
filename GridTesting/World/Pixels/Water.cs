using System;
using System.Collections.Generic;
using System.Text;

namespace GridTesting.Pixels
{
    static class Water
    {
        public static bool updateOld (Block block, int i, int j)
        {
            bool L_clear = block.getLeftPixel(i, j) == Pixel.AIR;
            bool R_clear = block.getRightPixel(i, j) == Pixel.AIR;

            if (block.getTopPixel(i, j) == Pixel.WATER && (L_clear || R_clear))//:The pixel above is water and the left or right sides are air
            {
                bool BL_clear = block.getBottomLeftPixel(i, j) == Pixel.AIR;
                bool BR_clear = block.getBottomRightPixel(i, j) == Pixel.AIR;

                if (BL_clear && BR_clear && L_clear && R_clear)//:The left and right bottom corners are air
                {
                    if (Pixel.rand.Next(0, 10) <= 4)//:Maybe the water falls left
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setBottomLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        return true;
                    }
                    else//:Otherwise the water falls right
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setBottomRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        return true;
                    }
                }
                else if (BL_clear && L_clear)//:The left bottom corner is air only
                {
                    block.setThisPixel(i, j, Pixel.AIR, 0);
                    block.setBottomLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                    return true;
                }
                else if (BR_clear && R_clear)//:The right bottom corner is air only
                {
                    block.setThisPixel(i, j, Pixel.AIR, 0);
                    block.setBottomRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                    return true;
                }
                else if (L_clear && R_clear)
                {
                    if (Pixel.rand.Next(0, 10) <= 4)//:Maybe the water moves left
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        return true;
                    }
                    else//:Otherwise the water moves right
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        return true;
                    }
                }
                else if (L_clear)//:The left side is air only
                {
                    block.setThisPixel(i, j, Pixel.AIR, 0);
                    block.setLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                    return true;
                }
                else//:The right side is air only
                {
                    block.setThisPixel(i, j, Pixel.AIR, 0);
                    block.setRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                    return true;
                }
            }
            else if (block.getBottomPixel(i, j) == Pixel.AIR)//:The pixel below is air
            {
                block.setThisPixel(i, j, Pixel.AIR, 0);
                block.setBottomPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                return true;
            } 
            else//:The pixel below is NOT air
            {
                bool BL_clear = block.getBottomLeftPixel(i, j) == Pixel.AIR;
                bool BR_clear = block.getBottomRightPixel(i, j) == Pixel.AIR;

                if (BL_clear && BR_clear && L_clear && R_clear)//:The left and right bottom corners are air
                {
                    if (Pixel.rand.Next(0, 10) <= 4)//:Maybe the water falls left
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setBottomLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        return true;
                    }
                    else//:Otherwise the water falls right
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setBottomRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        return true;
                    }
                }
                else if (BL_clear && L_clear)//:The left bottom corner is air only
                {
                    block.setThisPixel(i, j, Pixel.AIR, 0);
                    block.setBottomLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                    return true;
                }
                else if (BR_clear && R_clear)//:The right bottom corner is air only
                {
                    block.setThisPixel(i, j, Pixel.AIR, 0);
                    block.setBottomRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                    return true;
                }
                else//:No corners are air
                {
                    bool BL_water = block.getBottomLeftPixel(i, j) == Pixel.WATER;
                    bool BR_water = block.getBottomRightPixel(i, j) == Pixel.WATER;

                    bool fL_clear = block.getPixel(i, j, 0, -2) == Pixel.AIR;   //f = far
                    bool fR_clear = block.getPixel(i, j, 0, 2) == Pixel.AIR;    //f = far

                    if (L_clear && R_clear && fL_clear && fR_clear)
                    {
                        if (Pixel.rand.Next(0, 10) <= 4)//:Maybe the water moves left
                        {
                            block.setThisPixel(i, j, Pixel.AIR, 0);
                            block.setLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                            return true;
                        }
                        else//:Otherwise the water moves right
                        {
                            block.setThisPixel(i, j, Pixel.AIR, 0);
                            block.setRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                            return true;
                        }
                    }
                    else if (L_clear && fL_clear)//:The far left is air only
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        return true;
                    }
                    else if (R_clear && fR_clear)//:The far right is air only
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        return true;
                    }
                    else if (L_clear && R_clear && BL_water && BR_water)//:The left and right bottom corners are water and both sides are air
                    {
                        if (Pixel.rand.Next(0, 10) <= 4)//:Maybe the water moves left
                        {
                            block.setThisPixel(i, j, Pixel.AIR, 0);
                            block.setLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                            return true;
                        }
                        else//:Otherwise the water moves right
                        {
                            block.setThisPixel(i, j, Pixel.AIR, 0);
                            block.setRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                            return true;
                        }
                    }
                    else if (BL_water && L_clear)//:The left bottom corner is water only
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        return true;
                    }
                    else if (BR_water && R_clear)//:The right bottom corner is water only
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
