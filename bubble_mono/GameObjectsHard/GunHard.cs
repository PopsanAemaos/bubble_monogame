using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace bubble_mono.GameObjectsHard
{
	class GunHard : _GameObjectsHard
	{
		private Random random = new Random();
		private BubbleHard BubbleOnGun;
		private int _Index;
		private float angle;
		private List<Texture2D> bubbleTextureList;

		public SoundEffectInstance _deadSFX, _stickSFX, _boomSFX, _thunderSFX;
		public GunHard(Texture2D texture, List<Texture2D> bubble) : base(texture) {
			bubbleTextureList = bubble;
            _Index = random.Next(0, 8);
        }

		public override void Update(GameTime gameTime, BubbleHard[,] gameObjects) {
			//int _Index = random.Next(0, 3);

			Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
			Singleton.Instance.MouseCurrent = Mouse.GetState();
			if (Singleton.Instance.MouseCurrent.Y < Singleton.Instance.Diemensions.Y - bubbleTextureList[_Index].Height) {
				angle = (float)Math.Atan2((Position.Y + _texture.Height / 2) - Singleton.Instance.MouseCurrent.Y, (Position.X + _texture.Width / 2) - Singleton.Instance.MouseCurrent.X);
				if (!Singleton.Instance.Shooting && Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released) {
                    BubbleOnGun = new BubbleHard(bubbleTextureList[_Index], bubbleTextureList) {
						Name = "Bubble",
						Position = new Vector2(Singleton.Instance.Diemensions.X / 2 - bubbleTextureList[_Index].Width / 2, Singleton.Instance.Diemensions.Y - bubbleTextureList[_Index].Height),
						deadSFX = _deadSFX,
						stickSFX = _stickSFX,
						thunderSFX = _thunderSFX,
						boomSFX = _boomSFX,
						Index = _Index,
                        color = Color.White,
						IsActive = true,
						Angle = angle + MathHelper.Pi,
						Speed = 1000,
					};
					_Index = random.Next(0, 8);
					Singleton.Instance.Shooting = true;
				}
			}
			if (Singleton.Instance.Shooting)
				BubbleOnGun.Update(gameTime, gameObjects);
		}
		public override void Draw(SpriteBatch spriteBatch) {
			spriteBatch.Draw(_texture, Position + new Vector2(50, 50), null, Color.White, angle + MathHelper.ToRadians(-90f), new Vector2(50, 50), 1.5f, SpriteEffects.None, 0f);
			if (Singleton.Instance.Shooting)
				BubbleOnGun.Draw(spriteBatch);
			else
				spriteBatch.Draw(bubbleTextureList[_Index], new Vector2(Singleton.Instance.Diemensions.X / 2 - bubbleTextureList[_Index].Width / 2, 700 - bubbleTextureList[_Index].Height), Color.White);
		}
	}
}
