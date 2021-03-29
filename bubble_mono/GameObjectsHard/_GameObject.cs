using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace bubble_mono.GameObjectsHard
{
	public class _GameObjectsHard
	{
		protected Texture2D _texture;

		public Vector2 Position;
		public float Rotation;
		public Vector2 Scale;
		public Color color;
		public Vector2 Velocity;

		public string Name;
		public int Index;

		public bool IsActive;

		public Rectangle Rectangle {
			get {
				return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
			}
		}

		public _GameObjectsHard(Texture2D texture) {
			_texture = texture;
			Position = Vector2.Zero;
			Scale = Vector2.One;
			Rotation = 0f;
			IsActive = true;
		}

		public virtual void Update(GameTime gameTime, BubbleHard[,] gameObjects) {
		}
		public virtual void Draw(SpriteBatch spriteBatch) {
		}
	}
}
