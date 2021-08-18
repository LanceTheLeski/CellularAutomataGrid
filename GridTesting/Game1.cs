using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GridTesting
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Texture2D whiteRectangle;

        Chunk testChunk;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.PreferredBackBufferWidth = 1024;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });

            testChunk = new Chunk();
            pause = false;
            bpressed = spressed = rpressed = fpressed = qpressed = false;

            // TODO: use this.Content to load your game content here
        }

        bool pause;
        bool bpressed, spressed, rpressed, fpressed, qpressed, ppressed, lpressed;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && spressed == false)
            {
                pause = !pause;
                spressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp (Keys.Space) && spressed == true)
                spressed = false;

            if (Keyboard.GetState().IsKeyDown(Keys.E) && ppressed == false)
            {
                if (testChunk.blockSpace == 40) testChunk.blockSpace = 45;
                else testChunk.blockSpace = 40;
                ppressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.E) && ppressed == true)
                ppressed = false;

            /*if (Mouse.GetState().LeftButton == ButtonState.Pressed && bpressed == false && Mouse.GetState().X < 720 && Mouse.GetState().X > 0 && Mouse.GetState().Y < 720 && Mouse.GetState().Y > 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.A)) testChunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, -1, -1);
                else if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.D)) testChunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, -1, 1);
                else if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.S)) testChunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 1, -1);
                else if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.D)) testChunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                else if (Keyboard.GetState().IsKeyDown(Keys.W)) testChunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, -1, 0);
                else if (Keyboard.GetState().IsKeyDown(Keys.A)) testChunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 0, -1);
                else if (Keyboard.GetState().IsKeyDown(Keys.S)) testChunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 1, 0);
                else if (Keyboard.GetState().IsKeyDown(Keys.D)) testChunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 0, 1);
                else testChunk.highlight(Mouse.GetState().X, Mouse.GetState().Y, 0, 0);

                bpressed = true;
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released && bpressed == true) 
                bpressed = false;*/

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && bpressed == false && Mouse.GetState().X < 720 && Mouse.GetState().X > 0 && Mouse.GetState().Y < 720 && Mouse.GetState().Y > 0)
            {
                testChunk.forceMove(Mouse.GetState().X, Mouse.GetState().Y);
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released && bpressed == true)
                bpressed = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Q) && qpressed == false)
            {
                testChunk.highlight();
                qpressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Q) && qpressed == true)
                qpressed = false;

            if (Keyboard.GetState().IsKeyDown(Keys.R) && lpressed == false)
            {
                testChunk.altHighlight();
                lpressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.R) && lpressed == true)
                lpressed = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Tab) && rpressed == false)
            {
                bool temp = testChunk.highlighted;
                testChunk = new Chunk();
                if(temp) testChunk.highlight();

            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Tab) && rpressed == true)
                rpressed = false;

            if (!pause) testChunk.update();
            else if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && fpressed == false)
            {
                testChunk.update();
                fpressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.LeftAlt) && fpressed == true)
                fpressed = false;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            testChunk.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
