using GridTesting.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GridTesting
{
    class Controls
    {
        static bool pause = false;
        static bool bpressed = false, spressed = false, rpressed = false, fpressed = false, qpressed = false, ppressed = false, lpressed = false;
        public static Chunk update (Chunk chunk)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Space) && spressed == false)
            {
                pause = !pause;
                spressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Space) && spressed == true)
                spressed = false;

            if (Keyboard.GetState().IsKeyDown(Keys.E) && ppressed == false)
            {
                if (ChunkManager.blockSpace == 40) ChunkManager.blockSpace = 45;
                else ChunkManager.blockSpace = 40;
                ppressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.E) && ppressed == true)
                ppressed = false;

            /*if (Mouse.GetState().LeftButton == ButtonState.Pressed && bpressed == false && Mouse.GetState().X < 720 && Mouse.GetState().X > 0 && Mouse.GetState().Y < 720 && Mouse.GetState().Y > 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.A)) chunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, -1, -1);
                else if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.D)) chunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, -1, 1);
                else if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.S)) chunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 1, -1);
                else if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.D)) chunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                else if (Keyboard.GetState().IsKeyDown(Keys.W)) chunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, -1, 0);
                else if (Keyboard.GetState().IsKeyDown(Keys.A)) chunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 0, -1);
                else if (Keyboard.GetState().IsKeyDown(Keys.S)) chunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 1, 0);
                else if (Keyboard.GetState().IsKeyDown(Keys.D)) chunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 0, 1);
                else chunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 0, 0);

                bpressed = true;
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released && bpressed == true) 
                bpressed = false;*/

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && bpressed == false && Mouse.GetState().X < 720 && Mouse.GetState().X > 0 && Mouse.GetState().Y < 720 && Mouse.GetState().Y > 0)
            {
                chunk.forceMove(Mouse.GetState().X, Mouse.GetState().Y);
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released && bpressed == true)
                bpressed = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Q) && qpressed == false)
            {
                ChunkManager.DrawEvents(chunk);
                qpressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Q) && qpressed == true)
                qpressed = false;

            if (Keyboard.GetState().IsKeyDown(Keys.R) && lpressed == false)
            {
                ChunkManager.DrawMarkers(chunk);
                lpressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.R) && lpressed == true)
                lpressed = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Tab) && rpressed == false)
            {
                bool temp = chunk.showEvents;
                chunk = new Chunk();
                if (temp) ChunkManager.DrawEvents(chunk);

            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Tab) && rpressed == true)
                rpressed = false;

            if (!pause) ChunkManager.update(chunk);
            else if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && fpressed == false)
            {
                ChunkManager.update(chunk);
                fpressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.LeftAlt) && fpressed == true)
                fpressed = false;

            return chunk;
        }

    }
}
