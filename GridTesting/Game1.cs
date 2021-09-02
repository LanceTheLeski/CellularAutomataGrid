using GridTesting.World;
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

        Chunk testChunk1;
        //Chunk testChunk2;
        //Chunk testChunk3;
        //Chunk testChunk4;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferHeight = 768 * 2 - 500;
            _graphics.PreferredBackBufferWidth = 1024 + 200;
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

            testChunk1 = new Chunk();
            /*testChunk2 = new Chunk();
            testChunk3 = new Chunk();
            testChunk4 = new Chunk();

            testChunk2.yoff = 640;

            testChunk3.xoff = 640;

            testChunk4.xoff = 640;
            testChunk4.yoff = 640;

            Chunk [] link4 = {null, null, testChunk2, testChunk3, null, null, null, testChunk4};
            Chunk [] link3 = { null, testChunk1, null, testChunk4, null, null, testChunk3, null };
            Chunk [] link2 = { testChunk1, null, testChunk4, null, null, testChunk2, null, null };
            Chunk [] link1 = { testChunk2, testChunk3, null, null, testChunk1, null, null, null };

            testChunk1.setLinkedChunks(link1);
            testChunk2.setLinkedChunks(link2);
            testChunk3.setLinkedChunks(link3);
            testChunk4.setLinkedChunks(link4);*/

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            testChunk1 = Controls.update(testChunk1);
            /*tChunk3 = Controls.update(testChunk3);
            testChunk4 = Controls.update(testChunk4);*/

            ChunkManager.update(testChunk1);
            //ChunkManager.update(testChunk2);
            //ChunkManager.update(testChunk3);
            //ChunkManager.update(testChunk4);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ChunkManager.Draw(testChunk1, _spriteBatch);
            //ChunkManager.Draw(testChunk2, _spriteBatch);
            //ChunkManager.Draw(testChunk3, _spriteBatch);
            //ChunkManager.Draw(testChunk4, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}
