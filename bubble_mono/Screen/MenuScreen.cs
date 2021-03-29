using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using bubble_mono.Managers;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace bubble_mono.Screen
{
    class MenuScreen : _GameScreen
    {
        //GraphicsDeviceManager graphics;

        private Color _Color = new Color(250, 250, 250, 0);
        private Texture2D BG, Black;
        private Texture2D StartButtonSelect, ExitButtonSelect, AboutButtonClick, OptionButtonClick;
        private SpriteFont Arial, Arcanista, KM;
        private Texture2D CheckBoxYes, CheckBox, Apply, ApplyClick, Back, BackClick, Arrow;
        private Texture2D Normal, NormalClick, Hard, HardClick;

        private SoundEffectInstance SoundClickUI, SoundEnterGameGame, SoundSelectUI;

        private bool showOption = false, showAbout = false, showStart = false;
        private bool mhStart = false, mhOption = false, mhAbout = false, mhExit = false, mhBack = false, mhApply;
        private bool mhNormal = false, mhHard = false;
        private bool mhsStart = false, mhsOption = false, mhsAbout = false, mhsExit = false;
        private bool mainScreen = true;
        private Song BG_Sound;

        private Vector2 fontSize;

        // Varible On Option Screen
        private bool FullScreen = Singleton.Instance.IsFullScreen;
        private bool ShowFPS = Singleton.Instance.cmdShowFPS;
        private int MasterBGM = Singleton.Instance.BGM_MasterVolume;
        private int MasterSFX = Convert.ToInt32(Singleton.Instance.SFX_MasterVolume * 100);

        public void Initialize()
        {
            BG_Sound = content.Load<Song>("Audios/BG_music");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = Singleton.Instance.BGM_MasterVolume/500f;
            MediaPlayer.Play(BG_Sound);
            //BG_Sound.Volume = 0.1f;
            //BG_Sound.Play();
            //Console.WriteLine(graphics.PreferredBackBufferWidth);
            //Console.WriteLine(graphics.PreferredBackBufferHeight);
            //graphics.ApplyChanges();
        }
        public override void LoadContent()
        {
            base.LoadContent();
            // Texture2D
            BG = content.Load<Texture2D>("MenuScreen/MenuPage");
            StartButtonSelect = content.Load<Texture2D>("MenuScreen/StartButtonClickv2");
            ExitButtonSelect = content.Load<Texture2D>("MenuScreen/ExitButtonClickv2");
            AboutButtonClick = content.Load<Texture2D>("MenuScreen/AboutButtonClickv2");
            OptionButtonClick = content.Load<Texture2D>("MenuScreen/OptionButtonClickv2");
            Black = content.Load<Texture2D>("SplashScreen/Black");
            CheckBoxYes = content.Load<Texture2D>("MenuScreen/CheckBox_yes");
            CheckBox = content.Load<Texture2D>("MenuScreen/CheckBox");
            Apply = content.Load<Texture2D>("MenuScreen/Apply");
            ApplyClick = content.Load<Texture2D>("MenuScreen/ApplyClick");
            Back = content.Load<Texture2D>("MenuScreen/Back");
            BackClick = content.Load<Texture2D>("MenuScreen/Backclick");
            Arrow = content.Load<Texture2D>("MenuScreen/Arrow");
            // Level
            Hard = content.Load<Texture2D>("MenuScreen/Hard");
            HardClick = content.Load<Texture2D>("MenuScreen/HardClick");
            Normal = content.Load<Texture2D>("MenuScreen/Normal");
            NormalClick = content.Load<Texture2D>("MenuScreen/NormalClick");

            // Fonts
            Arial = content.Load<SpriteFont>("Fonts/Arial");
            Arcanista = content.Load<SpriteFont>("Fonts/Arcanista");
            KM = content.Load<SpriteFont>("Fonts/KH-Metropolis");

            //OptionH = content.Load<Texture2D>("MenuScreen/option");
            // Sounds
            SoundClickUI = content.Load<SoundEffect>("Audios/UI_SoundPack8_Error_v1").CreateInstance();
            SoundEnterGameGame = content.Load<SoundEffect>("Audios/transition t07 two-step 007").CreateInstance();
            SoundSelectUI = content.Load<SoundEffect>("Audios/UI_SoundPack11_Select_v14").CreateInstance();
            // Call Init
            Initialize();
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            SoundSelectUI.Volume = Singleton.Instance.SFX_MasterVolume;
            SoundClickUI.Volume = Singleton.Instance.SFX_MasterVolume;
            SoundEnterGameGame.Volume = Singleton.Instance.SFX_MasterVolume;

            Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
            Singleton.Instance.MouseCurrent = Mouse.GetState();
            if (mainScreen)
            {
                // Click start game
                if ((Singleton.Instance.MouseCurrent.X > 534 && Singleton.Instance.MouseCurrent.Y > 334) && (Singleton.Instance.MouseCurrent.X < 763 && Singleton.Instance.MouseCurrent.Y < 411))
                {
                    mhStart = true;
                    if (!mhsStart)
                    {
                        mhsStart = true;
                    }
                    if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                    {
                        showStart = true;
                        mainScreen = false;
                        SoundClickUI.Play();
                    }
                }
                else
                {
                    mhStart = false;
                    mhsStart = false;
                }
                // Click Exit
                if ((Singleton.Instance.MouseCurrent.X > 534 && Singleton.Instance.MouseCurrent.Y > 600) && (Singleton.Instance.MouseCurrent.X < 763 && Singleton.Instance.MouseCurrent.Y < 677))
                {
                    mhExit = true;
                    if (!mhsExit)
                    {
                        SoundSelectUI.Play();
                        mhsExit = true;
                    }
                    if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                    {
                        SoundEnterGameGame.Play();
                        Singleton.Instance.cmdExit = true;
                    }
                }
                else
                {
                    mhExit = false;
                    mhsExit = false;
                }
                // Click Option
                if ((Singleton.Instance.MouseCurrent.X > 93 && Singleton.Instance.MouseCurrent.Y > 161) && (Singleton.Instance.MouseCurrent.X < 256 && Singleton.Instance.MouseCurrent.Y < 674))
                {
                    mhOption = true;
                    if (!mhsOption)
                    {
                        SoundSelectUI.Play();
                        mhsOption = true;
                    }
                    if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                    {
                        showOption = true;
                        mainScreen = false;
                        SoundClickUI.Play();
                    }
                }
                else
                {
                    mhsOption = false;
                    mhOption = false;
                }
                // Click About
                if ((Singleton.Instance.MouseCurrent.X > 1044 && Singleton.Instance.MouseCurrent.Y > 178) && (Singleton.Instance.MouseCurrent.X < 1218 && Singleton.Instance.MouseCurrent.Y < 694))
                {
                    mhAbout = true;
                    if (!mhsAbout)
                    {
                        SoundSelectUI.Play();
                        mhsAbout = true;
                    }
                    if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                    {
                        showAbout = true;
                        mainScreen = false;
                        SoundClickUI.Play();
                    }
                }
                else
                {
                    mhsAbout = false;
                    mhAbout = false;
                }

            }
            else
            {
                // Click Back
                if ((Singleton.Instance.MouseCurrent.X > (1230 - Back.Width) && Singleton.Instance.MouseCurrent.Y > 50) && (Singleton.Instance.MouseCurrent.X < 1230 && Singleton.Instance.MouseCurrent.Y < (50 + Back.Height)))
                {
                    mhBack = true;
                    if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                    {
                        mainScreen = true;
                        showAbout = false;
                        showOption = false;
                        SoundClickUI.Play();
                    }
                }
                else
                {
                    mhBack = false;
                }
                if (showOption)
                {
                    // Click change CheckBox FullScreen
                    if ((Singleton.Instance.MouseCurrent.X > 800 && Singleton.Instance.MouseCurrent.Y > 425) && (Singleton.Instance.MouseCurrent.X < (800 + CheckBox.Width) && Singleton.Instance.MouseCurrent.Y < (425 + CheckBox.Height)))
                    {
                        if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                        {
                            FullScreen = !FullScreen;
                        }
                    }
                    // Click change CheckBox ShowFPS
                    if ((Singleton.Instance.MouseCurrent.X > 800 && Singleton.Instance.MouseCurrent.Y > 500) && (Singleton.Instance.MouseCurrent.X < (800 + CheckBox.Width) && Singleton.Instance.MouseCurrent.Y < (500 + CheckBox.Height)))
                    {
                        if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                        {
                            ShowFPS = !ShowFPS;
                        }
                    }

                    // Click Arrow BGM
                    if ((Singleton.Instance.MouseCurrent.X > 700 && Singleton.Instance.MouseCurrent.Y > 240) && (Singleton.Instance.MouseCurrent.X < (700 + Arrow.Width) && Singleton.Instance.MouseCurrent.Y < (240 + Arrow.Height)))
                    {
                        if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                        {
                            if (MasterBGM > 0) MasterBGM -= 5;
                            SoundSelectUI.Play();
                        }
                    }
                    else if ((Singleton.Instance.MouseCurrent.X > 900 && Singleton.Instance.MouseCurrent.Y > 240) && (Singleton.Instance.MouseCurrent.X < (900 + Arrow.Width) && Singleton.Instance.MouseCurrent.Y < (240 + Arrow.Height)))
                    {
                        if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                        {
                            if (MasterBGM < 100) MasterBGM += 5;
                        }
                    }
                    // Click Arrow SFX
                    if ((Singleton.Instance.MouseCurrent.X > 700 && Singleton.Instance.MouseCurrent.Y > 315) && (Singleton.Instance.MouseCurrent.X < (700 + Arrow.Width) && Singleton.Instance.MouseCurrent.Y < (315 + Arrow.Height)))
                    {
                        if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                        {
                            if (MasterSFX > 0) MasterSFX -= 5;
                        }
                    }
                    else if ((Singleton.Instance.MouseCurrent.X > 900 && Singleton.Instance.MouseCurrent.Y > 315) && (Singleton.Instance.MouseCurrent.X < (900 + Arrow.Width) && Singleton.Instance.MouseCurrent.Y < (315 + Arrow.Height)))
                    {
                        if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                        {
                            if (MasterSFX < 100) MasterSFX += 5;
                        }
                    }
                    // Apply Option to Game
                    if ((Singleton.Instance.MouseCurrent.X > (1100 - Apply.Width) && Singleton.Instance.MouseCurrent.Y > 625) && (Singleton.Instance.MouseCurrent.X < 1100 && Singleton.Instance.MouseCurrent.Y < (625 + Back.Height)))
                    {
                        mhApply = true;
                        if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                        {
                            if (Singleton.Instance.IsFullScreen != FullScreen) Singleton.Instance.cmdFullScreen = true;
                            SoundClickUI.Play();
                            Singleton.Instance.cmdShowFPS = ShowFPS;
                            Singleton.Instance.BGM_MasterVolume = MasterBGM;
                            Singleton.Instance.SFX_MasterVolume = MasterSFX;
                        }
                    }
                    else
                    {
                        mhApply = false;
                    }
                }
                if (showStart)
                {
                    //Click Normal Mode
                    if ((Singleton.Instance.MouseCurrent.X > 565 && Singleton.Instance.MouseCurrent.Y > 260 && Singleton.Instance.MouseCurrent.X < 807 && Singleton.Instance.MouseCurrent.Y < 321))
                    {
                        mhNormal = true;
                        if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                        {
                            SoundClickUI.Play();
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.PlayScreenNormal);
                        }
                    }
                    else
                    {
                        mhNormal = false;
                    }
                    //Click Hard Mode
                    if ((Singleton.Instance.MouseCurrent.X > 565 && Singleton.Instance.MouseCurrent.Y > 380 && Singleton.Instance.MouseCurrent.X < 807 && Singleton.Instance.MouseCurrent.Y < 441))
                    {
                        mhHard = true;
                        if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                        {
                            SoundClickUI.Play();
                            //Ready.Play();
                            ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.PlayScreenHard);
                        }
                    }
                    else
                    {
                        mhHard = false;
                    }
                }

            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BG, Vector2.Zero, Color.White);
            if (mhStart)
            {
                spriteBatch.Draw(StartButtonSelect, new Vector2(530, 330), Color.White);
            }
            if (mhExit)
            {
                spriteBatch.Draw(ExitButtonSelect, new Vector2(530, 595), Color.White);
            }
            if (mhOption)
            {
                spriteBatch.Draw(OptionButtonClick, new Vector2(101, 362), Color.White);
            }
            if (mhAbout)
            {
                spriteBatch.Draw(AboutButtonClick, new Vector2(1047, 366), Color.White);
            }
            if (!mainScreen)
            {
                spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
                if (mhBack)
                {
                    spriteBatch.Draw(BackClick, new Vector2(1230 - BackClick.Width, 50), Color.White);
                }
                else
                {
                    spriteBatch.Draw(Back, new Vector2(1230 - Back.Width, 50), Color.White);
                }
                // Draw Option Screen
                if (showOption)
                {
                    fontSize = KM.MeasureString("Option");
                    spriteBatch.DrawString(Arcanista, "Option", new Vector2(Singleton.Instance.Diemensions.X / 2 - fontSize.X / 2, 125), Color.White);

                    spriteBatch.DrawString(Arcanista, "BGM Volume", new Vector2(300, 250), Color.White);
                    spriteBatch.Draw(Arrow, new Vector2(700, 240), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
                    spriteBatch.DrawString(Arcanista, MasterBGM.ToString(), new Vector2(800, 250), Color.White);
                    spriteBatch.Draw(Arrow, new Vector2(900, 240), Color.White);

                    spriteBatch.DrawString(Arcanista, "SFX Volume", new Vector2(300, 325), Color.White);
                    spriteBatch.Draw(Arrow, new Vector2(700, 315), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
                    spriteBatch.DrawString(Arcanista, MasterSFX.ToString(), new Vector2(800, 325), Color.White);
                    spriteBatch.Draw(Arrow, new Vector2(900, 315), Color.White);

                    spriteBatch.DrawString(Arcanista, "Full Screen", new Vector2(300, 425), Color.White);
                    if (FullScreen)
                    {
                        spriteBatch.Draw(CheckBox, new Vector2(800, 425), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(CheckBoxYes, new Vector2(800, 425), Color.White);
                    }

                    spriteBatch.DrawString(Arcanista, "Show FPS", new Vector2(300, 500), Color.White);
                    if (!ShowFPS)
                    {
                        spriteBatch.Draw(CheckBox, new Vector2(800, 500), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(CheckBoxYes, new Vector2(800, 500), Color.White);
                    }

                    if (mhApply)
                    {
                        spriteBatch.Draw(ApplyClick, new Vector2(1100 - ApplyClick.Width, 625), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(Apply, new Vector2(1100 - Apply.Width, 625), Color.White);
                    }
                }
                // Draw About Screen
                if (showAbout)
                {
                    fontSize = KM.MeasureString("About");
                    spriteBatch.DrawString(KM, "About", new Vector2(Singleton.Instance.Diemensions.X / 2 - fontSize.X / 2, 125), Color.White);

                    //spriteBatch.DrawString(Arcanista, "Graphics", new Vector2(200, 250), Color.NavajoWhite);
                    //spriteBatch.DrawString(Arcanista, "- We create", new Vector2(160, 350), Color.White);
                    //spriteBatch.DrawString(Arcanista, "All Graphics", new Vector2(150, 425), Color.White);

                    //spriteBatch.DrawString(Arcanista, "Audios", new Vector2(600, 250), Color.NavajoWhite);
                    //spriteBatch.DrawString(Arcanista, "- www.sonniss.com", new Vector2(520, 350), Color.White);
                    //spriteBatch.DrawString(Arcanista, "Free Audios Bundle", new Vector2(510, 425), Color.White);

                    //spriteBatch.DrawString(Arcanista, "Fonts", new Vector2(1000, 250), Color.NavajoWhite);
                    //spriteBatch.DrawString(Arcanista, "- Arial", new Vector2(985, 350), Color.White);
                    //spriteBatch.DrawString(Arcanista, "- Arcanista", new Vector2(950, 425), Color.White);
                    //spriteBatch.DrawString(Arcanista, "- KH-Metropolis", new Vector2(920, 500), Color.White);

                    spriteBatch.DrawString(Arial, "Reference: https://github.com/pepodev-archive/egypt-bubble.git", new Vector2(50, 590), Color.White);

                    spriteBatch.DrawString(Arial, "FPS Counter Script : https://stackoverflow.com/questions/20676185", new Vector2(50, 630), Color.White);
                    spriteBatch.DrawString(Arial, "/xna-monogame-getting-the-frames-per-second", new Vector2(350, 660), Color.White);
                }
                // Draw Leader board Screen
                // Draw Start Screen
                if (showStart)
                {
                    spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
                    spriteBatch.Draw(Normal, new Vector2(565, 260), Color.White);

                    if (mhNormal)
                    {
                        spriteBatch.Draw(NormalClick, new Vector2(565, 260), Color.White);
                    }
                    spriteBatch.Draw(Hard, new Vector2(565, 380), Color.White);
                    if (mhHard)
                    {
                        spriteBatch.Draw(HardClick, new Vector2(565, 380), Color.White);
                    }
                }

            }


        }
    }
}
