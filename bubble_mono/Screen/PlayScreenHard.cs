
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using bubble_mono.Managers;
using bubble_mono.GameObjectsHard;
using Microsoft.Xna.Framework.Media;

namespace bubble_mono.Screen
{
    class PlayScreenHard : _GameScreen
    {
        private Texture2D BG, Black, GunTexture, LoveBall, AngryBall, GreedBall, LustBall, HungryBall, SadBall, BombBall, ElectroBall;
        private SpriteFont Arial, Arcanista;
        private BubbleHard[,] bubble = new BubbleHard[9, 8];
        private Color _Color;
        private Random random = new Random();
        private GunHard gunHard;
        private Vector2 fontSize;
        private float _timer = 0f;
        private float __timer = 0f;
        private float Timer = 0f;
        private float timerPerUpdate = 0.05f;
        private float tickPerUpdate = 30f;
        private int alpha = 255;
        private bool fadeFinish = false;
        private bool gameOver = false;
        private bool gameWin = false;
        private SoundEffectInstance Ball_broken, Shooting, explotion, Thunder_Sound, game_over, Devil_sound;
        private List<Texture2D> BubleList = new List<Texture2D>();


        public void Initial()
        {
            BubleList.Add(LustBall);
            BubleList.Add(AngryBall);
            BubleList.Add(LoveBall);
            BubleList.Add(GreedBall);
            BubleList.Add(HungryBall);
            BubleList.Add(SadBall);
            BubleList.Add(BombBall);
            BubleList.Add(ElectroBall);

            _Color = new Color(255, 255, 255, alpha);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8 - (i % 2); j++)
                {
                    int index = random.Next(0, 6);
                    bubble[i, j] = new BubbleHard(BubleList[index], BubleList)
                    {
                        Name = "Bubble",
                        Index = index,
                        Position = new Vector2((j * 80) + ((i % 2) == 0 ? 320 : 360), (i * 70) + 40),
                        color = Color.White,
                        IsActive = false,
                    };
                }
            }
            Shooting.Volume = Singleton.Instance.SFX_MasterVolume;
            Ball_broken.Volume = Singleton.Instance.SFX_MasterVolume;
            Shooting.Volume = Singleton.Instance.SFX_MasterVolume;
            Ball_broken.Volume = Singleton.Instance.SFX_MasterVolume;
            gunHard = new GunHard(GunTexture, BubleList)
            {
                Name = "GunHard",
                Position = new Vector2(Singleton.Instance.Diemensions.X / 2 - GunTexture.Width / 2, Singleton.Instance.Diemensions.Y - GunTexture.Height),
                color = Color.White,
                _deadSFX = Ball_broken,
                _stickSFX = Shooting,
                _boomSFX = explotion,
                _thunderSFX = Thunder_Sound,
                IsActive = true,
            };
        }

        public bool CheckWin(BubbleHard[,] bubble)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8 - (i % 2); j++)
                {
                    if (bubble[i, j] != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void LoadContent()
        {
            base.LoadContent();
            LustBall = content.Load<Texture2D>("Bubble/LustBall");
            AngryBall = content.Load<Texture2D>("Bubble/AngryBall");
            LoveBall = content.Load<Texture2D>("Bubble/LoveBall");
            GreedBall = content.Load<Texture2D>("Bubble/GreedBall");
            SadBall = content.Load<Texture2D>("Bubble/Sadball");
            HungryBall = content.Load<Texture2D>("Bubble/HungryBall");
            BombBall = content.Load<Texture2D>("Bubble/BombBall");
            ElectroBall = content.Load<Texture2D>("Bubble/ElectroBall");
            BG = content.Load<Texture2D>("PlayScreen/BGGame");
            Black = content.Load<Texture2D>("SplashScreen/Black");
            GunTexture = content.Load<Texture2D>("PlayScreen/Gun_Brain");
            Arial = content.Load<SpriteFont>("Fonts/Arial");
            Arcanista = content.Load<SpriteFont>("Fonts/Arcanista");
            Shooting = content.Load<SoundEffect>("Audios/Shooting").CreateInstance();
            Ball_broken = content.Load<SoundEffect>("Audios/Ball_broken").CreateInstance();
            Thunder_Sound = content.Load<SoundEffect>("Audios/Thunder_Sound").CreateInstance();
            explotion = content.Load<SoundEffect>("Audios/explotion").CreateInstance();
            game_over = content.Load<SoundEffect>("Audios/game_over").CreateInstance();
            Devil_sound = content.Load<SoundEffect>("Audios/Devil_sound").CreateInstance();

            Initial();
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (!gameOver && !gameWin)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (bubble[i, j] != null)
                            bubble[i, j].Update(gameTime, bubble);
                    }
                }
                gunHard.Update(gameTime, bubble);
                Timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                for (int i = 0; i < 8; i++)
                {
                    if (bubble[8, i] != null)
                    {
                        gameOver = true;
                    }
                }
                //Check ball flying
                for (int i = 1; i < 9; i++)
                {
                    for (int j = 1; j < 7 - (i % 2); j++)
                    {
                        if (i % 2 != 0)
                        {
                            if (bubble[i - 1, j] == null && bubble[i - 1, j + 1] == null)
                            {
                                bubble[i, j] = null;
                            }
                            if (bubble[i, 1] == null && bubble[i - 1, 0] == null && bubble[i - 1, 1] == null)
                            {
                                bubble[i, 0] = null;
                            }
                            if (bubble[i, 5] == null && bubble[i - 1, 7] == null && bubble[i - 1, 6] == null)
                            {
                                bubble[i, 6] = null;
                            }
                        }
                        else
                        {
                            if (bubble[i - 1, j - 1] == null && bubble[i - 1, j] == null)
                            {
                                bubble[i, j] = null;
                            }
                            if (bubble[i - 1, 0] == null && bubble[i, 1] == null)
                            {
                                bubble[i, 0] = null;
                            }
                            if (bubble[i - 1, 6] == null && bubble[i, 6] == null)
                            {
                                bubble[i, 7] = null;
                            }
                        }
                    }
                }

                __timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                if (__timer >= tickPerUpdate)
                {
                    // Check game over before scroll
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 0; j < 8 - (i % 2); j++)
                        {
                            if (bubble[i, j] != null)
                            {
                                gameOver = true;
                            }
                        }
                    }
                    // Scroll position 
                    for (int i = 5; i >= 0; i--)
                    {
                        for (int j = 0; j < 8 - (i % 2); j++)
                        {
                            bubble[i + 2, j] = bubble[i, j];
                        }
                    }
                    // Draw new scroll position
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 8 - (i % 2); j++)
                        {
                            if (bubble[i, j] != null)
                            {
                                bubble[i, j].Position = new Vector2((j * 80) + ((i % 2) == 0 ? 320 : 360), (i * 70) + 40);
                            }
                        }
                    }
                    //Random ball after scroll
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 8 - (i % 2); j++)
                        {
                            int index = random.Next(0, 6);
                            bubble[i, j] = new BubbleHard(BubleList[index], BubleList)
                            {
                                Name = "Bubble",
                                Index = index,
                                Position = new Vector2((j * 80) + ((i % 2) == 0 ? 320 : 360), (i * 70) + 40),
                                color = Color.White,
                                IsActive = false,
                            };
                        }
                    }

                    __timer -= tickPerUpdate;
                }

                gameWin = CheckWin(bubble);
                if(gameWin == true)
                {
                    Devil_sound.Volume = Singleton.Instance.SFX_MasterVolume / 2;
                    Devil_sound.Play();
                }
                if (gameOver == true)
                {
                    game_over.Volume = Singleton.Instance.SFX_MasterVolume / 2;
                    game_over.Play();
                }


            }
            else
            {
                Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
                Singleton.Instance.MouseCurrent = Mouse.GetState();
                if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                {
                    Singleton.Instance.Score = 0;
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                }
            }
            // fade out
            if (!fadeFinish)
            {
                _timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                if (_timer >= timerPerUpdate)
                {
                    alpha -= 5;
                    _timer -= timerPerUpdate;
                    if (alpha <= 5)
                    {
                        fadeFinish = true;
                    }
                    _Color.A = (byte)alpha;
                }
            }



            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BG, Vector2.Zero, Color.White);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (bubble[i, j] != null)
                        bubble[i, j].Draw(spriteBatch);
                }
            }
            gunHard.Draw(spriteBatch);

            spriteBatch.DrawString(Arial, "Score : " + Singleton.Instance.Score, new Vector2(850, 10), _Color);
            spriteBatch.DrawString(Arial, "Time : " + Timer.ToString("F"), new Vector2(600, 10), _Color);
            spriteBatch.DrawString(Arial, "Next Time : " + (tickPerUpdate - __timer).ToString("F"), new Vector2(350, 10), _Color);

            if (gameOver)
            {
                spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
                fontSize = Arial.MeasureString("GameOver !!");
                spriteBatch.DrawString(Arial, "GameOver !!", Singleton.Instance.Diemensions / 2 - fontSize / 2, _Color);
            }

            if (gameWin)
            {
                spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
                fontSize = Arial.MeasureString("GameWin !!");
                spriteBatch.DrawString(Arial, "GameWin !!", Singleton.Instance.Diemensions / 2 - fontSize / 2, _Color);
            }

            // Draw fade out
            if (!fadeFinish)
            {
                spriteBatch.Draw(Black, Vector2.Zero, _Color);
            }
        }
    }
}
