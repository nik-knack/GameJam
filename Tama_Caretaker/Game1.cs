using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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


    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D loadingBars;
        private Texture2D tamagotchi;
        private Tamagotchi playerTamagochi;
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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            loadingBars = Content.Load<Texture2D>("loading_bars");
            tamagotchi = Content.Load<Texture2D>("tamagotchi");
            playerTamagochi = new Tamagotchi(tamagotchi, loadingBars);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            playerTamagochi.UpdateAnimations(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(loadingBars, new Rectangle(loadingBars.Width/8, 0,loadingBars.Width/8, loadingBars.Height/3 ), Color.White);
            _spriteBatch.Draw(tamagotchi, new Rectangle(
                (GraphicsDevice.Viewport.Bounds.Width - tamagotchi.Width)/2, 
                (GraphicsDevice.Viewport.Bounds.Height - tamagotchi.Height)/2, 
                tamagotchi.Width, 
                tamagotchi.Height), 
                Color.White);

            playerTamagochi.DrawBars(_spriteBatch);
            playerTamagochi.DrawBarOutline(_spriteBatch);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
