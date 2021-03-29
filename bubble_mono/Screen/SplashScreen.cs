using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using bubble_mono.Managers;
using Microsoft.Xna.Framework.Input;

namespace bubble_mono.Screen
{
	class SplashScreen : _GameScreen
	{
		private Vector2 fontSize;
		private Color _Color; // for update color alpha
		private SpriteFont Arcanista, Splash, KH;
		private Texture2D Logo, GameName, Black;
		private int alpha; // Value of alpha in color for fade logo and text
		private int index; // order of index to display splash screen
		private float _timer; // Elapsed time in game
		private float _timePerUpdate; // Will do update function when _timer > _timePerUpdate
		private bool Show; // true will fade in and false will fade out
		public SplashScreen()
		{
			Show = true;
			_timePerUpdate = 0.03f;
			index = 0;
			alpha = 210;
			_Color = new Color(255, 255, 255, alpha);
		}
		public override void LoadContent()
		{
			base.LoadContent();
			Arcanista = content.Load<SpriteFont>("Fonts/Arcanista");
			Splash = content.Load<SpriteFont>("Fonts/Splash");
			KH = content.Load<SpriteFont>("Fonts/KH-Metropolis");
			Logo = content.Load<Texture2D>("SplashScreen/Logo");
			GameName = content.Load<Texture2D>("SplashScreen/Logo");
			Black = content.Load<Texture2D>("SplashScreen/Black");
		}
		public override void UnloadContent() { base.UnloadContent(); }
		public override void Update(GameTime gameTime)
		{
			if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released) ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
			// TODO: Add your update logic here
			_timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
			if (_timer >= _timePerUpdate)
			{
				if (Show)
				{
					alpha -= 5;
					// when fade in finish
					if (alpha <= 0)
					{
						Show = false;
						if (index == 1)
						{
							ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
						}

					}
				}
				else
				{
					// fade out
					alpha += 5;
					// whene fade out finish
					if (alpha >= 210)
					{
						Show = true;
						index++;
					}
				}
				_timer -= _timePerUpdate;
				_Color.A = (byte)alpha;
				base.Update(gameTime);
			}
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			//spriteBatch.Draw(Black, Vector2.Zero, _Color);
			//fontSize = KH.MeasureString("Prejudice Bubble");
			spriteBatch.DrawString(KH, "Prejudice Bubble", new Vector2((Singleton.Instance.Diemensions.X - fontSize.X) / 2, (Singleton.Instance.Diemensions.Y - fontSize.Y) / 2), _Color);
            switch (index)
            {
                case 0:
                    //spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
                    spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
                    fontSize = KH.MeasureString("Prejudice Bubble");
                    spriteBatch.DrawString(KH, "Prejudice Bubble", new Vector2((Singleton.Instance.Diemensions.X - fontSize.X) / 2, (Singleton.Instance.Diemensions.Y - fontSize.Y) / 2), _Color);
                    break;
                case 1:
                    spriteBatch.Draw(Black, Vector2.Zero, _Color);
                    break;
            }

        }
	}
}
