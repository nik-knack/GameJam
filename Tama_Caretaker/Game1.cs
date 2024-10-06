using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System;

namespace Tama_Caretaker
{
    enum GameState
    {
        TitleScreen,
        Instructions,
        Credits,
        TamagachiMenu,
        SleepMinigame,
        FeedMinigame,
        PlayMinigame,
        GameOver

    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont monogram;
        private GameState gameState;
        public int screenWidth;
        public int screenHeight;

        private KeyboardState kbState;
        private KeyboardState prevkbState;
        private MouseState mState;
        private MouseState prevMState;
        

        private Texture2D loadingBars;
        private Texture2D tamagotchiLarge;
        private Texture2D tamagotchiSmall;
        private Texture2D gameOver;
        private Texture2D potatoTex;
        private Texture2D carrotTex;
        private Texture2D cornTex;
        private Texture2D ghostTex;
        private Texture2D drumstickTex;
        private Texture2D sleepIcon;

        private Texture2D mainBackground;
        private Texture2D tamagotchiBackground;
        private Texture2D gameOverBackground;
        private Texture2D titleCard;
        private Texture2D howToPlayCard;
        private Texture2D creditsCard;


        private SoundEffect cancelFX;
        private SoundEffect menuFX;
        private SoundEffect minigameFX;
        private SoundEffect feedFX;
        private SoundEffect winFX;
        private SoundEffect failFX;
        private Song menuSong;

        private Nightmare nightmare;
        private Tamagotchi playerTamagochi;
        private SleepPlayer sleepPlayer;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            gameState = GameState.TitleScreen;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            screenWidth = GraphicsDevice.Viewport.Bounds.Width;
            screenHeight = GraphicsDevice.Viewport.Bounds.Height;

            loadingBars = Content.Load<Texture2D>("loading_bars");
            tamagotchiLarge = Content.Load<Texture2D>("tamagotchi");
            tamagotchiSmall = Content.Load<Texture2D>("tamagotchi_smallfr");
            gameOver = Content.Load<Texture2D>("game_over");
            potatoTex = Content.Load<Texture2D>("potato");
            carrotTex = Content.Load<Texture2D>("carrot");
            cornTex = Content.Load<Texture2D>("corn");
            ghostTex = Content.Load<Texture2D>("ghostSprite");
            drumstickTex = Content.Load<Texture2D>("drumstick");
            sleepIcon = Content.Load<Texture2D>("sleep");


            mainBackground = Content.Load<Texture2D>("main_background");
            tamagotchiBackground = Content.Load<Texture2D>("tamagotchi_background");
            gameOverBackground = Content.Load<Texture2D>("game_over_background");
            titleCard = Content.Load<Texture2D>("title");
            howToPlayCard = Content.Load<Texture2D>("how_to_play");
            creditsCard = Content.Load<Texture2D>("credits");


            monogram = Content.Load<SpriteFont>("monogram");

            cancelFX = Content.Load<SoundEffect>("cancel");
            menuFX = Content.Load<SoundEffect>("main_select");
            minigameFX = Content.Load<SoundEffect>("minigame_select");
            feedFX = Content.Load<SoundEffect>("feeding");
            winFX = Content.Load<SoundEffect>("win");
            failFX = Content.Load<SoundEffect>("fail");


            playerTamagochi = new Tamagotchi(loadingBars, cornTex, potatoTex, carrotTex, drumstickTex, sleepIcon,
                feedFX, winFX);
            nightmare = new Nightmare(ghostTex, new Rectangle(0, 0, ghostTex.Width, ghostTex.Height));
            sleepPlayer = new SleepPlayer(nightmare, playerTamagochi, tamagotchiSmall,
            new Rectangle(screenWidth / 2, screenHeight / 2, tamagotchiSmall.Width, tamagotchiSmall.Height),
            screenWidth, screenHeight);

            menuSong = Content.Load<Song>("panorama");

            gameState = GameState.TitleScreen;

            SoundEffect.MasterVolume = 0.5f;
            MediaPlayer.Volume -= 0.8f;
            MediaPlayer.Play(menuSong);
            MediaPlayer.IsRepeating = true;
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            kbState = Keyboard.GetState();


            mState = Mouse.GetState();

            switch (gameState)
            {
                case GameState.TitleScreen:
                   
                    if(SingleKeyPress(Keys.Space, kbState, prevkbState))
                    {
                        menuFX.Play();
                        playerTamagochi.Reset();
                        gameState = GameState.TamagachiMenu;
                    }

                    if (SingleKeyPress(Keys.C, kbState, prevkbState))
                    {
                        menuFX.Play();
                        gameState = GameState.Credits;
                    }

                    if (SingleKeyPress(Keys.I, kbState, prevkbState))
                    {
                        menuFX.Play();
                        gameState = GameState.Instructions;
                    }

                    break;

                case GameState.Instructions:
                    if (SingleKeyPress(Keys.Q, kbState, prevkbState))
                    {
                        cancelFX.Play();
                        gameState = GameState.TitleScreen;
                    }

                    if (SingleKeyPress(Keys.Space, kbState, prevkbState))
                    {
                        menuFX.Play();
                        playerTamagochi.Reset();
                        gameState = GameState.TamagachiMenu;
                    }
                    break;

                case GameState.Credits:
                    if (SingleKeyPress(Keys.Q, kbState, prevkbState))
                    {
                        cancelFX.Play();
                        gameState = GameState.TitleScreen;
                    }
                    break;

                case GameState.TamagachiMenu:
                    if (playerTamagochi.isAlive)
                    {
                        if (SingleKeyPress(Keys.S, kbState, prevkbState))
                        {
                            minigameFX.Play();
                            gameState = GameState.SleepMinigame;
                        }

                        if (SingleKeyPress(Keys.P, kbState, prevkbState))
                        {
                            minigameFX.Play();
                            gameState = GameState.PlayMinigame;
                        }

                        if (SingleKeyPress(Keys.F, kbState, prevkbState))
                        {
                            minigameFX.Play();
                            gameState = GameState.FeedMinigame;
                        }

                        playerTamagochi.UpdateBarAnimations(gameTime);
                    }
                    else
                    {
                        gameState = GameState.GameOver;
                        break;
                    }

                    break;

                case GameState.SleepMinigame:

                    nightmare.Update(gameTime);
                    nightmare.UpdateGhostAnimations(gameTime);

                    sleepPlayer.Update(gameTime, kbState);
                    if (SingleKeyPress(Keys.Q, kbState, prevkbState))
                    {
                        cancelFX.Play();
                        gameState = GameState.TamagachiMenu;
                    }
                    
                    break;

                case GameState.FeedMinigame:
                    playerTamagochi.FeedUpdate(gameTime, mState, prevMState);

                    if (SingleKeyPress(Keys.Q, kbState, prevkbState))
                    {
                        if(playerTamagochi.completed)
                        {
                            winFX.Play();
                        }
                        else
                        {
                            cancelFX.Play();
                        }
                        playerTamagochi.FeedReset();
                        gameState = GameState.TamagachiMenu;
                    }
                    break;

                case GameState.PlayMinigame:
                    if (SingleKeyPress(Keys.Q, kbState, prevkbState))
                    {
                        cancelFX.Play();
                        gameState = GameState.TamagachiMenu;
                    }

                    break;

                case GameState.GameOver:
                    if (SingleKeyPress(Keys.Space, kbState, prevkbState))
                    {
                        playerTamagochi.Reset();
                        gameState = GameState.TamagachiMenu;
                    }
                    if (SingleKeyPress(Keys.Q, kbState, prevkbState))
                    {
                        gameState = GameState.TitleScreen;
                    }
                    break;
            }

            prevkbState = kbState;
            prevMState = mState;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Magenta);

            _spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.TitleScreen:
                    _spriteBatch.Draw(mainBackground, new Rectangle(0, 0,
                        mainBackground.Width, mainBackground.Height), Color.White);

                    _spriteBatch.Draw(titleCard, new Rectangle(
                        (screenWidth - titleCard.Width) / 2,
                        100,
                        titleCard.Width, 
                        titleCard.Height), 
                        Color.White);

                    _spriteBatch.DrawString(monogram, "Press Space to Start!",
                        new Vector2((screenWidth / 2) - 230, (screenHeight / 2) + 100),
                        Color.Brown);
                    _spriteBatch.DrawString(monogram, "Press I for instructions",
                        new Vector2((screenWidth / 2) - 230, (screenHeight / 2) + 200),
                        Color.Brown);
                    _spriteBatch.DrawString(monogram, "Press C for credits",
                        new Vector2((screenWidth / 2) - 230, (screenHeight / 2) + 240),
                        Color.Brown);
                    break;

                case GameState.Instructions:
                    _spriteBatch.Draw(mainBackground, new Rectangle(0, 0,
                        mainBackground.Width, mainBackground.Height), Color.White);

                    _spriteBatch.Draw(howToPlayCard, 
                        new Rectangle(
                            (screenWidth - howToPlayCard.Width)/2,
                            100,
                            howToPlayCard.Width, 
                            howToPlayCard.Height), 
                        Color.White);
                    _spriteBatch.DrawString(monogram, "How To Play:\n" +
                        "Take care of your Tama!\n" +
                        "Keep your Tama well rested\n" +
                        "and feed.\n" +
                        "Play minigames to fill up your\n" +
                        "Sleepiness and Hunger meters.", 
                        new Vector2((screenWidth / 2) - 230, screenHeight / 2- 50), Color.Brown);
                    
                    _spriteBatch.DrawString(monogram, "Press Q to go back",
                        new Vector2((screenWidth / 2) - 230, (screenHeight / 2) + 250),
                        Color.Brown);
                    break;

                case GameState.Credits:
                    _spriteBatch.Draw(mainBackground, new Rectangle(0, 0,
                        mainBackground.Width, mainBackground.Height), Color.White);

                    _spriteBatch.Draw(creditsCard, new Rectangle((screenWidth - creditsCard.Width)/2, 100,
                        creditsCard.Width, creditsCard.Height), Color.White);

                    _spriteBatch.DrawString(monogram, "Programming: Filiberto Nieves IV\n" +
                        "Artwork: Dania Macias\n" +
                        "Borrowed Game Assets on itch.io:\n" +
                        "- Fonts: datagoblin\n" +
                        "- Loading Bars: BDragon1727\n" +
                        "- Food Sprites: LLENJIN\n" +
                        "- SFX: JDWasabi\n" +
                        "- Songs: Zakiro\n",
                        new Vector2((screenWidth / 2) - 230, (screenHeight / 2)-50),
                        Color.Brown);

                    _spriteBatch.DrawString(monogram, "Press Q to go back",
                        new Vector2((screenWidth / 2) - 230, (screenHeight / 2) + 250),
                        Color.Brown);
                    break;

                case GameState.TamagachiMenu:
                    _spriteBatch.Draw(tamagotchiBackground, new Rectangle(0, 0,
                        tamagotchiBackground.Width, tamagotchiBackground.Height), Color.White);
                    
                    _spriteBatch.Draw(tamagotchiLarge, new Rectangle(
                    (screenWidth - tamagotchiLarge.Width) / 2,
                    (screenHeight - tamagotchiLarge.Height) / 2,
                    tamagotchiLarge.Width,
                    tamagotchiLarge.Height),
                    Color.White);
                    
                    playerTamagochi.DrawBars(_spriteBatch);
                    playerTamagochi.DrawBarOutline(_spriteBatch);
                    playerTamagochi.DrawIcons(_spriteBatch);

                    _spriteBatch.DrawString(monogram, "Press F to feed",
                       new Vector2((screenWidth / 2) + 200, (screenHeight / 2) + 250),
                       Color.Brown);
                    _spriteBatch.DrawString(monogram, "Press S to sleep",
                       new Vector2((screenWidth / 2) -400, (screenHeight / 2) + 250),
                       Color.Brown);
                    break;

                case GameState.SleepMinigame:

                    _spriteBatch.Draw(tamagotchiBackground, new Rectangle(0, 0,
                        tamagotchiBackground.Width, tamagotchiBackground.Height), Color.White);

                    nightmare.Draw(_spriteBatch);
                    sleepPlayer.Draw(_spriteBatch);

                    _spriteBatch.DrawString(monogram,
                    $"Timer:{String.Format("{0:0.00}", playerTamagochi.sleepTimer)}",
                   new Vector2(0, 0), Color.Brown);
                    break;

                case GameState.FeedMinigame:

                    _spriteBatch.Draw(tamagotchiBackground, new Rectangle(0, 0,
                        tamagotchiBackground.Width, tamagotchiBackground.Height), Color.White);
                    playerTamagochi.FeedDraw(_spriteBatch);

                    _spriteBatch.DrawString(monogram,
                    $"Timer:{String.Format("{0:0.00}", playerTamagochi.feedTimer)}",
                    new Vector2(0, 0), Color.Brown);
                    break;

                case GameState.PlayMinigame:
                    _spriteBatch.Draw(tamagotchiBackground, new Rectangle(0, 0,
                        tamagotchiBackground.Width, tamagotchiBackground.Height), Color.White);
                    break;

                case GameState.GameOver: 
                    _spriteBatch.Draw(gameOverBackground, new Rectangle(0, 0,
                        gameOverBackground.Width, gameOverBackground.Height), Color.White);
                    _spriteBatch.Draw(gameOver, new Rectangle(0, 0,
                        gameOver.Width, gameOver.Height), Color.White);
                    _spriteBatch.DrawString(monogram, "Press Space to restart!",
                       new Vector2((screenWidth / 2) - 150, (screenHeight / 2) + 250),
                       Color.Brown);

                    break;

            }

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private bool SingleKeyPress(Keys key, KeyboardState kbState, KeyboardState prevKbState)
        {
            return kbState.IsKeyDown(key) && prevkbState.IsKeyUp(key);
        }
    }
}
