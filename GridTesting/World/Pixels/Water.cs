using System;
using System.Collections.Generic;
using System.Text;

namespace GridTesting.Pixels
{
    static class Water
    {
        /*public static void update (Block block, int i, int j)
        {
            bool L_clear = block.getLeftPixel(i, j) == Pixel.AIR;
            bool R_clear = block.getRightPixel(i, j) == Pixel.AIR;

            if (block.getTopPixel(i, j) == Pixel.WATER && (L_clear || R_clear))                               //The pixel above is water and the left or right sides are air
            {
                if (L_clear && Pixel.rand.Next(0, 10) <= 4)
                {
                    block.setThisPixel(i, j, Pixel.AIR, 0);
                    block.setLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                    block.setThisMarker (i, j, 1);
                    //block.setUpdated(i, j, 0, 0, false);
                    return;
                }
                else if (R_clear)
                {
                    block.setThisPixel(i, j, Pixel.AIR, 0);
                    block.setRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                    block.setThisMarker(i, j, 1);
                    //block.setUpdated(i, j, 0, 0, false);
                    return;
                }
            }

            if (block.getBottomPixel(i, j) == Pixel.AIR)                                               //The pixel below is air
            {
                block.setThisPixel(i, j, Pixel.AIR, 0);
                block.setBottomPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                block.setMarker(i, j, -1, 0);
                block.setMarker(i, j, -1, -1);
                block.setMarker(i, j, -1, 1);
                //block.setUpdated(i, j, 0, 0, false);
            }
            else                                                                         //The pixel below is NOT air
            {

                if (!L_clear && !R_clear)
                {
                    //block.setUpdated(i, j, 0, 0, false);
                    return;
                }

                bool BL_clear = block.getBottomLeftPixel(i, j) == Pixel.AIR;
                bool BR_clear = block.getBottomRightPixel(i, j) == Pixel.AIR;

                if (BL_clear && BR_clear && L_clear && R_clear)                                                                  //:The left and right bottom corners are air
                {
                    if (Pixel.rand.Next(0, 10) <= 4)                                                                                    //::Maybe the water falls left
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setBottomLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        block.setThisMarker(i, j, 1);
                        //block.setUpdated(i, j, 0, 0, false);
                    }
                    else                                                                                                          //::Otherwise the water falls right
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setBottomRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        block.setThisMarker(i, j, 1);
                        //block.setUpdated(i, j, 0, 0, false);
                    }
                }
                else if (BL_clear && L_clear)                                                                                //:The left bottom corner is air only
                {
                    block.setThisPixel(i, j, Pixel.AIR, 0);
                    block.setBottomLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                    block.setThisMarker(i, j, 1);
                    //block.setUpdated(i, j, 0, 0, false);
                }
                else if (BR_clear && R_clear)                                                                                //:The right bottom corner is air only
                {
                    block.setThisPixel(i, j, Pixel.AIR, 0);
                    block.setBottomRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                    block.setThisMarker(i, j, 1);
                    //block.setUpdated(i, j, 0, 0, false);
                }
                else                                                                                        //:No corners are air
                {
                    bool BL_water = block.getBottomLeftPixel(i, j) == Pixel.WATER;
                    bool BR_water = block.getBottomRightPixel(i, j) == Pixel.WATER;

                    bool fL_clear = block.getPixel(i, j, 0, -2) == Pixel.AIR;   //f = far
                    bool fR_clear = block.getPixel(i, j, 0, 2) == Pixel.AIR;    //f = far

                    if (L_clear && R_clear && fL_clear && fR_clear)
                    {
                        if (Pixel.rand.Next(0, 10) <= 4)                                           //::Maybe the water moves left
                        {
                            block.setThisPixel(i, j, Pixel.AIR, 0);
                            block.setLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                            block.setThisMarker(i, j, 1);
                            //block.setUpdated(i, j, 0, 0, false);
                        }
                        else                                                                       //::Otherwise the water moves right
                        {
                            block.setThisPixel(i, j, Pixel.AIR, 0);
                            block.setRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                            block.setThisMarker(i, j, 1);
                            //block.setUpdated(i, j, 0, 0, false);
                        }
                    }
                    else if (L_clear && fL_clear)
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        block.setThisMarker(i, j, 1);
                        //block.setUpdated(i, j, 0, 0, false);
                    }
                    else if (R_clear && fR_clear)
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        block.setThisMarker(i, j, 1);
                        //block.setUpdated(i, j, 0, 0, false);
                    }
                    else if (L_clear && R_clear && BL_water && BR_water)                                                                           //:The left and right bottom corners are water and both sides are air
                    {
                        if (Pixel.rand.Next(0, 10) <= 4)                                                                             //::Maybe the water moves left
                        {
                            block.setThisPixel(i, j, Pixel.AIR, 0);
                            block.setLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                            block.setThisMarker(i, j, 1);
                            //block.setUpdated(i, j, 0, 0, false);
                        }
                        else                                                                                                   //::Otherwise the water moves right
                        {
                            block.setThisPixel(i, j, Pixel.AIR, 0);
                            block.setRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                            block.setThisMarker(i, j, 1);
                            //block.setUpdated(i, j, 0, 0, false);
                        }
                    }
                    else if (BL_water && L_clear)                                                                            //:The left bottom corner is water only
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setLeftPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        block.setThisMarker(i, j, 1);
                        //block.setUpdated(i, j, 0, 0, false);
                    }
                    else if (BR_water && R_clear)                                                                             //:The right bottom corner is water only
                    {
                        block.setThisPixel(i, j, Pixel.AIR, 0);
                        block.setRightPixel(i, j, Pixel.WATER, block.getData(i, j, 0, 0));
                        block.setThisMarker(i, j, 1);
                        //block.setUpdated(i, j, 0, 0, false);
                    }
                    //else block.setUpdated(i, j, 0, 0, false);//Maybe add a move chance for the ground
                }
            }
        }*/


    }
}
