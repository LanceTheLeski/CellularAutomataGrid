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
            bpressed = spressed = false;

            // TODO: use this.Content to load your game content here
        }

        bool pause;
        bool bpressed, spressed;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && spressed == false)
            {
                pause = !pause;
                spressed = true;
            }
            else spressed = false;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && bpressed == false)
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
            else bpressed = false;

            if (!pause) testChunk.update();

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
