
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using bubble_mono.Managers;
using System;
namespace bubble_mono.Screen
{
	class _GameScreen
	{

		protected ContentManager content;

		public virtual void LoadContent()
		{
			content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
		}
		public virtual void UnloadContent()
		{
			content.Unload();
		}
		public virtual void Update(GameTime gameTime)
		{

		}
		public virtual void Draw(SpriteBatch spriteBatch)
		{

		}
	}
}
