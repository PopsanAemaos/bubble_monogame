using bubble_mono.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace bubble_mono.Managers
{
	public class ScreenManager
	{
		public ContentManager Content { private set; get; }
		public enum GameScreenName
		{
			SplashScreen,
			MenuScreen,
			PlayScreenHard,
			PlayScreenNormal
		}
		private _GameScreen CurrentGameScreen;

		public ScreenManager()
		{
			CurrentGameScreen = new SplashScreen();
		}
		public void LoadScreen(GameScreenName _ScreenName)
		{
			switch (_ScreenName)
			{
				case GameScreenName.MenuScreen:
					CurrentGameScreen = new MenuScreen();
					break;
				case GameScreenName.PlayScreenNormal:
					CurrentGameScreen = new PlayScreenNormal();
					break;
				case GameScreenName.PlayScreenHard:
					CurrentGameScreen = new PlayScreenHard();
					break;
			}
			CurrentGameScreen.LoadContent();
		}
		public void LoadContent(ContentManager Content)
		{
			this.Content = new ContentManager(Content.ServiceProvider, "Content");
			CurrentGameScreen.LoadContent();
		}
		public void UnloadContent()
		{
			CurrentGameScreen.UnloadContent();
		}

		public void Update(GameTime gameTime)
		{
			CurrentGameScreen.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			CurrentGameScreen.Draw(spriteBatch);
		}
		private static ScreenManager instance;
		public static ScreenManager Instance
		{
			get
			{
				if (instance == null)
					instance = new ScreenManager();
				return instance;
			}
		}
	}
}
