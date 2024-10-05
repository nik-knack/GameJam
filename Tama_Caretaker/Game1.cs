using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;

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
        private int screenWidth;
        private int screenHeight;

        private KeyboardState kbState;
        private KeyboardState prevkbState;
        

        private Texture2D loadingBars;
        private Texture2D tamagotchi;
        private Texture2D gameOver;
        private Tamagotchi playerTamagochi;
        private SoundEffect cancelFX;
        private SoundEffect menuFX;
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

            gameState = GameState.TitleScreen;
            loadingBars = Content.Load<Texture2D>("loading_bars");
            tamagotchi = Content.Load<Texture2D>("tamagotchi");
            playerTamagochi = new Tamagotchi(tamagotchi, loadingBars);
            monogram = Content.Load<SpriteFont>("monogram");
            gameOver = Content.Load<Texture2D>("game_over");
            cancelFX = Content.Load<SoundEffect>("cancel");
            menuFX = Content.Load<SoundEffect>("main_select");

            

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            kbState = Keyboard.GetState();

            switch (gameState)
            {
                case GameState.TitleScreen:
                    if(SingleKeyPress(Keys.Space, kbState, prevkbState))
                    {
                        menuFX.Play();
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
                    if (SingleKeyPress(Keys.S, kbState, prevkbState))
                    {
                        gameState = GameState.SleepMinigame;
                    }

                    if (SingleKeyPress(Keys.P, kbState, prevkbState))
                    {
                        gameState = GameState.PlayMinigame;
                    }

                    if (SingleKeyPress(Keys.F, kbState, prevkbState))
                    {
                        gameState = GameState.FeedMinigame;
                    }

                    playerTamagochi.UpdateAnimations(gameTime);

                    break;

                case GameState.SleepMinigame:
                    if (SingleKeyPress(Keys.Q, kbState, prevkbState))
                    {
                        cancelFX.Play();
                        gameState = GameState.TamagachiMenu;
                    }
                    break;

                case GameState.FeedMinigame:
                    if (SingleKeyPress(Keys.Q, kbState, prevkbState))
                    {
                        cancelFX.Play();
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
                        gameState = GameState.TamagachiMenu;
                    }
                    if (SingleKeyPress(Keys.Q, kbState, prevkbState))
                    {
                        gameState = GameState.TitleScreen;
                    }
                    break;


            }


            prevkbState = kbState;

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
                    _spriteBatch.DrawString(monogram, "Welcome to Tama-Caretaker!",
                        new Vector2((screenWidth/2) - 150, (screenHeight/2) -100), 
                        Color.White);
                    break;

                case GameState.Instructions:
                    _spriteBatch.DrawString(monogram, "How To Play:", new Vector2(screenWidth / 2, screenHeight / 2),
                        Color.White, 0f, new Vector2(0, 0), new Vector2(4.0f, 4.0f), SpriteEffects.None, 0f);
                    break;

                case GameState.Credits:
                    break;

                case GameState.TamagachiMenu:
                    _spriteBatch.Draw(tamagotchi, new Rectangle(
                    (screenWidth - tamagotchi.Width) / 2,
                    (screenHeight - tamagotchi.Height) / 2,
                    tamagotchi.Width*10,
                    tamagotchi.Height*10),
                    Color.White);

                    playerTamagochi.DrawBars(_spriteBatch);
                    playerTamagochi.DrawBarOutline(_spriteBatch);
                    break;

                case GameState.SleepMinigame:
                    break;

                case GameState.FeedMinigame:
                    break;

                case GameState.PlayMinigame:
                    break;

                case GameState.GameOver:

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
