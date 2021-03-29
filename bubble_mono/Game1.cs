using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using bubble_mono.Managers;

namespace bubble_mono
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont Arial;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = (int)Singleton.Instance.Diemensions.X;
            graphics.PreferredBackBufferHeight = (int)Singleton.Instance.Diemensions.Y;
            graphics.ApplyChanges();

            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            //Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2), (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));
            graphics.ApplyChanges();

            //_graphics = new GraphicsDeviceManager(this);
            //Content.RootDirectory = "Content";
            //IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //BGM = Content.Load<Song>("Audios/Spirit_of_the_Dead");
            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.Volume = Singleton.Instance.BGM_MasterVolume;
            //MediaPlayer.Play(BGM);

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //_spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Instance.LoadContent(Content);
            Arial = Content.Load<SpriteFont>("Fonts/Arial");

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            ScreenManager.Instance.UnloadContent();
        }


        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            //// TODO: Add your update logic here

            //base.Update(gameTime);
            ScreenManager.Instance.Update(gameTime);
            Singleton.Instance.IsFullScreen = graphics.IsFullScreen;
            if (Singleton.Instance.cmdExit)
            {
                Exit();
            }
            //Singleton.Instance.cmdFullScreen = false;
            if (Singleton.Instance.cmdFullScreen)
            {
                graphics.ToggleFullScreen();
                graphics.ApplyChanges();
                Singleton.Instance.cmdFullScreen = false;
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //// TODO: Add your drawing code here

            //base.Draw(gameTime);
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            ScreenManager.Instance.Draw(spriteBatch);
            if (Singleton.Instance.cmdShowFPS)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                FrameCounter.Instance.Update(deltaTime);
                spriteBatch.DrawString(Arial, string.Format("FPS: " + FrameCounter.Instance.AverageFramesPerSecond.ToString("F")), new Vector2(1090, 10), Color.Yellow);
            }

            spriteBatch.End();
            base.Draw(gameTime);

        }

    }
}
