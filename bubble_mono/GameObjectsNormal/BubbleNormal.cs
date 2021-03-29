using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace bubble_mono.GameObjectsNormal
{
	public class BubbleNormal : _GameObjectsNormal
    {
		public float Speed;
		public float Angle;
		private List<Texture2D> bubbleTextureList;

        public SoundEffectInstance deadSFX, stickSFX, thunderSFX, boomSFX;

		public BubbleNormal(Texture2D texture, List<Texture2D> bubble) : base(texture) {
			bubbleTextureList = bubble;

		}

		public override void Update(GameTime gameTime, BubbleNormal[,] gameObjects) {
            if (IsActive) {
				Velocity.X = (float)Math.Cos(Angle) * Speed;
				Velocity.Y = (float)Math.Sin(Angle) * Speed;
				Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
				DetectCollision(gameObjects);
				if (Position.Y <= 40) {
					IsActive = false;
                    if (Position.X > 880)
                    {
                        gameObjects[0, 7] = this;
                        Position = new Vector2((7 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                        CheckSpecialBubble(gameObjects, Index, new Vector2(7,0));
                    }
                    else if (Position.X > 800)
                    {
                        gameObjects[0, 6] = this;
                        Position = new Vector2((6 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                        CheckSpecialBubble(gameObjects, Index, new Vector2(6, 0));
                    }
                    else if (Position.X > 720)
                    {
                        gameObjects[0, 5] = this;
                        Position = new Vector2((5 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                        CheckSpecialBubble(gameObjects, Index, new Vector2(5, 0));
                    }
                    else if (Position.X > 640)
                    {
                        gameObjects[0, 4] = this;
                        Position = new Vector2((4 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                        CheckSpecialBubble(gameObjects, Index, new Vector2(4, 0));
                    }
                    else if (Position.X > 560)
                    {
                        gameObjects[0, 3] = this;
                        Position = new Vector2((3 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                        CheckSpecialBubble(gameObjects, Index, new Vector2(3, 0));
                    }
                    else if (Position.X > 480)
                    {
                        gameObjects[0, 2] = this;
                        Position = new Vector2((2 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                        CheckSpecialBubble(gameObjects, Index, new Vector2(2, 0));
                    }
                    else if (Position.X > 400)
                    {
                        gameObjects[0, 1] = this;
                        Position = new Vector2((1 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                        CheckSpecialBubble(gameObjects, Index, new Vector2(1, 0));

                    }
                    else if (Position.X > 320)
                    {
                        gameObjects[0, 0] = this;
                        Position = new Vector2((0 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                        CheckSpecialBubble(gameObjects, Index, new Vector2(0, 0));
                    }
                    Singleton.Instance.Shooting = false;
					
					stickSFX.Volume = Singleton.Instance.SFX_MasterVolume;
					stickSFX.Play();
				}

				if (Position.X <= 325) {
					Angle = -Angle;
					Angle += MathHelper.ToRadians(180);
				}

				if (Position.X + _texture.Width >= 960) {
					Angle = -Angle;
					Angle += MathHelper.ToRadians(180);
				}
			}
		}

		private void DetectCollision(BubbleNormal[,] gameObjects) {
				for (int i = 0; i < 9; i++) {
					for (int j = 0; j < 8; j++) {
						// บอลที่ตั้งอยู่เป็นค่าว่างไหม
						if (gameObjects[i, j] != null && !gameObjects[i, j].IsActive)
                        {
                            if (calDistance(gameObjects[i, j]) <= 70 )
                            {                          
                                if (Position.X >= gameObjects[i, j].Position.X)
                                {
									if (i % 2 == 0)
                                    {
                                        if (j == 7)
                                        {
                                            gameObjects[i + 1, j - 1] = this;
                                            gameObjects[i + 1, j - 1].Position = new Vector2(((j - 1) * 80) + (((i + 1) % 2) == 0 ? 320 : 360), ((i + 1) * 70) + 40);
                                        CheckSpecialBubble(gameObjects, Index, new Vector2(j - 1, i + 1));
                                            CheckRemoveBubble(gameObjects, Index, new Vector2(j - 1, i + 1));
                                            
                                        }
                                        else
                                        {
                                            gameObjects[i + 1, j] = this;
                                            gameObjects[i + 1, j].Position = new Vector2((j * 80) + (((i + 1) % 2) == 0 ? 320 : 360), ((i + 1) * 70) + 40);
                                        CheckSpecialBubble(gameObjects, Index, new Vector2(j, i + 1));                                           
                                            CheckRemoveBubble(gameObjects, Index, new Vector2(j, i + 1));
                                            
										}
									}
                                    else
                                    {
										gameObjects[i + 1, j + 1] = this;
										gameObjects[i + 1, j + 1].Position = new Vector2(((j + 1) * 80) + (((i + 1) % 2) == 0 ? 320 : 360), ((i + 1) * 70) + 40);
                                    CheckSpecialBubble(gameObjects, Index, new Vector2(j + 1, i + 1));                                       
                                        CheckRemoveBubble(gameObjects, Index, new Vector2(j + 1, i + 1));
									}
								}
                                else
                                {
                                    if (i % 2 == 0)
                                    {
										gameObjects[i + 1, j - 1] = this;
                                    gameObjects[i + 1, j - 1].Position = new Vector2(((j - 1) * 80) + (((i + 1) % 2) == 0 ? 320 : 360), ((i + 1) * 70) + 40);

                                    CheckSpecialBubble(gameObjects, Index, new Vector2(j - 1, i + 1));                                                                    
                                        CheckRemoveBubble(gameObjects, Index, new Vector2(j - 1, i + 1));
                                        
									}
                                    else
                                    {
										gameObjects[(i + 1), j] = this;
										gameObjects[(i + 1), j].Position = new Vector2((j * 80) + (((i + 1) % 2) == 0 ? 320 : 360), ((i + 1) * 70) + 40);
                                    CheckSpecialBubble(gameObjects, Index, new Vector2(j, i + 1));                                        
                                        CheckRemoveBubble(gameObjects, Index, new Vector2(j, i + 1));
                                        
									}
								}
								IsActive = false;
								if (Singleton.Instance.removeBubble.Count >= 3 || Singleton.Instance.IsSpecialBallVector)
                                {
									Singleton.Instance.Score += Singleton.Instance.removeBubble.Count * 100;
									Singleton.Instance.IsSpecialBallVector = false;
                                    if (Singleton.Instance.soundboom == true)
                                    {
                                        boomSFX.Volume = Singleton.Instance.SFX_MasterVolume / 2;
                                        boomSFX.Play();
                                        Singleton.Instance.soundboom = false;
                                    }
                                    else if (Singleton.Instance.soundthunder == true)
                                    {
                                        thunderSFX.Volume = Singleton.Instance.SFX_MasterVolume / 2;
                                        thunderSFX.Play();
                                        Singleton.Instance.soundthunder = false;
                                    }
                                    else
                                    {
                                        deadSFX.Volume = Singleton.Instance.SFX_MasterVolume;
                                        deadSFX.Play();
                                    }
                                } else if (Singleton.Instance.removeBubble.Count > 0) {
									stickSFX.Volume = Singleton.Instance.SFX_MasterVolume;
									stickSFX.Play();
									foreach (Vector2 v in Singleton.Instance.removeBubble)
									{
										gameObjects[(int)v.Y, (int)v.X] = new BubbleNormal(bubbleTextureList[Index], bubbleTextureList)
										{
											Name = "Bubble",
											Index = Index,
											Position = new Vector2((v.X * 80) + ((v.Y % 2) == 0 ? 320 : 360), (v.Y * 70) + 40),
											color = Color.White,
											IsActive = false,
										};
									}
								}
								Singleton.Instance.removeBubble.Clear();
								Singleton.Instance.Shooting = false;
								return;
							}
						}
					}
				}
		}
		public override void Draw(SpriteBatch spriteBatch) {
			spriteBatch.Draw(_texture, Position, Color.White);
			base.Draw(spriteBatch);
		}

		public int calDistance(BubbleNormal other) {
			// หาระยะทางห่างระหว่างบอล
            return (int)Math.Sqrt(Math.Pow(Position.X - other.Position.X, 2) + Math.Pow(Position.Y - other.Position.Y, 2));
		}
		public void CheckRemoveBubble(BubbleNormal[,] gapositionObjects, int IndexTarget, Vector2 position)
		{
            if ((position.X >= 0 && position.Y >= 0) && (position.X <= 7 && position.Y <= 8) && (gapositionObjects[(int)position.Y, (int)position.X] != null && gapositionObjects[(int)position.Y, (int)position.X].Index == IndexTarget))
			{
                Singleton.Instance.removeBubble.Add(position);
				gapositionObjects[(int)position.Y, (int)position.X] = null;

			}
			else
			{
                return;
			}
			CheckRemoveBubble(gapositionObjects, IndexTarget, new Vector2(position.X + 1, position.Y)); // Right
			CheckRemoveBubble(gapositionObjects, IndexTarget, new Vector2(position.X - 1, position.Y)); // Left
			if (position.Y % 2 == 0)
			{
				CheckRemoveBubble(gapositionObjects, IndexTarget, new Vector2(position.X, position.Y - 1)); // Top Right
				CheckRemoveBubble(gapositionObjects, IndexTarget, new Vector2(position.X - 1, position.Y - 1)); // Top Left
				CheckRemoveBubble(gapositionObjects, IndexTarget, new Vector2(position.X, position.Y + 1)); // Bot Right
				CheckRemoveBubble(gapositionObjects, IndexTarget, new Vector2(position.X - 1, position.Y + 1)); // Bot Left
			}
			else
			{
				CheckRemoveBubble(gapositionObjects, IndexTarget, new Vector2(position.X + 1, position.Y - 1)); // Top Right
				CheckRemoveBubble(gapositionObjects, IndexTarget, new Vector2(position.X, position.Y - 1)); // Top Left
				CheckRemoveBubble(gapositionObjects, IndexTarget, new Vector2(position.X + 1, position.Y + 1)); // Bot Right
				CheckRemoveBubble(gapositionObjects, IndexTarget, new Vector2(position.X, position.Y + 1)); // Bot 		}
			}
		}

        public void CheckSpecialBubble(BubbleNormal[,] gapositionObjects, int IndexTarget, Vector2 position)
        {
            if (gapositionObjects[(int)position.Y, (int)position.X].Index == 5)
            {
                Singleton.Instance.soundthunder = true;
                Singleton.Instance.SpecialBallVector = position;
                    Singleton.Instance.IsSpecialBallVector = true;
                    for (int i = (int)position.X + 1; i < 8; i++)
                    {
                        if (gapositionObjects[(int)position.Y, i] != null)
                        {
                            Singleton.Instance.removeBubble.Add(new Vector2(i, position.Y));
                            gapositionObjects[(int)position.Y, i] = null;
                        }
                    }
                    for (int i = (int)position.X - 1; i >= 0; i--)
                    {
                        if (gapositionObjects[(int)position.Y, i] != null)
                        {
                            Singleton.Instance.removeBubble.Add(new Vector2(i, position.Y));
                            gapositionObjects[(int)position.Y, i] = null;
                        }
                    }
                Singleton.Instance.removeBubble.Add(position);
                gapositionObjects[(int)position.Y, (int)position.X] = null;
                return;

            }
                if (gapositionObjects[(int)position.Y, (int)position.X].Index == 4)
                {
                // ระเบิดรอบตัว
                Singleton.Instance.soundboom = true;
                Singleton.Instance.SpecialBallVector = position;
                    Singleton.Instance.IsSpecialBallVector = true;
                    // ซ้าย
                    if ((int)position.X - 1 >= 0)
                    {
                        if (gapositionObjects[(int)position.Y, (int)position.X - 1] != null)
                        {
                            Singleton.Instance.removeBubble.Add(new Vector2(position.X - 1, position.Y));
                            gapositionObjects[(int)position.Y, (int)position.X - 1] = null;
                        }
                    }
                    // ขวา
                    if ((int)position.X + 1 < 8)
                    {
                        if (gapositionObjects[(int)position.Y, (int)position.X + 1] != null)
                        {
                            Singleton.Instance.removeBubble.Add(new Vector2(position.X + 1, position.Y));
                            gapositionObjects[(int)position.Y, (int)position.X + 1] = null;
                        }
                    }
                    // บน
                    if ((int)position.Y - 1 >= 0)
                    {
                        if (gapositionObjects[(int)position.Y - 1, (int)position.X] != null)
                        {
                            Singleton.Instance.removeBubble.Add(new Vector2(position.X, position.Y - 1));

                            gapositionObjects[(int)position.Y - 1, (int)position.X] = null;

                        }
                    }
                    // ล่าง
                    if ((int)position.Y + 1 < 9)
                    {
                        if (gapositionObjects[(int)position.Y + 1, (int)position.X] != null)
                        {
                            Singleton.Instance.removeBubble.Add(new Vector2(position.X, position.Y + 1));
                            gapositionObjects[(int)position.Y + 1, (int)position.X] = null;
                        }
                    }


                    if (position.Y % 2 == 0)
                    {
                        if ((int)position.X - 1 >= 0 && (int)position.Y - 1 >= 0)
                        {
                            if (gapositionObjects[(int)position.Y - 1, (int)position.X - 1] != null)
                            {
                                Singleton.Instance.removeBubble.Add(new Vector2(position.X - 1, position.Y - 1));
                                gapositionObjects[(int)position.Y - 1, (int)position.X - 1] = null;
                            }
                        }
                        if ((int)position.X - 1 >= 0 && (int)position.Y + 1 < 9)
                        {
                            if (gapositionObjects[(int)position.Y + 1, (int)position.X - 1] != null)
                            {
                                Singleton.Instance.removeBubble.Add(new Vector2(position.X - 1, position.Y - 1));
                                gapositionObjects[(int)position.Y + 1, (int)position.X - 1] = null;
                            }
                        }
                    }
                    else
                    {
                        if ((int)position.X + 1 < 8 && (int)position.Y - 1 >= 0)
                        {
                            if (gapositionObjects[(int)position.Y - 1, (int)position.X + 1] != null)
                            {
                                Singleton.Instance.removeBubble.Add(new Vector2(position.X + 1, position.Y - 1));
                                gapositionObjects[(int)position.Y - 1, (int)position.X + 1] = null;
                            }
                        }
                        if ((int)position.X + 1 < 8 && (int)position.Y + 1 < 9)
                        {
                            if (gapositionObjects[(int)position.Y + 1, (int)position.X + 1] != null)
                            {
                                Singleton.Instance.removeBubble.Add(new Vector2(position.X + 1, position.Y - 1));
                                gapositionObjects[(int)position.Y + 1, (int)position.X + 1] = null;
                            }
                        }

                    }
                Singleton.Instance.removeBubble.Add(position);
                gapositionObjects[(int)position.Y, (int)position.X] = null;
                return;
            }

        }
    }
}