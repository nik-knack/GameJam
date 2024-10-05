using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tama_Caretaker
{
    internal class Tamagotchi
    {
        private Texture2D tamagotchiTex;
        private Texture2D loadingBars;

        private const int SLEEPDECREMENT = 7;
        private const int HUNGERDECREMENT = 5;
        private const int BARFRAMECOUNTER = 7;

        private const int BARFRAMEHEIGHT = 16;
        private const int BARFRAMEWIDTH = 48;

        public int sleepBarFrame;
        public int hungerBarFrame;

        

        int hungerFrame;
        int sleepFrame;
        double timeCounter;
        double timeCounterSleepBar;
        double timeCounterHungerBar;
        float fps;
        float timePerFrame;

        public Tamagotchi(Texture2D tamagotchiTex, Texture2D loadingBars)
        {
            sleepBarFrame = 7;
            hungerBarFrame = 7;
            this.tamagotchiTex = tamagotchiTex;
            this.loadingBars = loadingBars;
            sleepFrame = 1;
            hungerFrame = 1;
            fps = 0.14f;
            timePerFrame = 1.0f / fps;
        }
        
        public void UpdateAnimations(GameTime gameTime)
        {
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeCounter >= timePerFrame)
            {
                sleepFrame += 1;

                if (sleepFrame > BARFRAMECOUNTER)
                    sleepFrame = 2;

                timeCounter -= timePerFrame;
            }
        }
        


        public void DrawBarOutline(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loadingBars, new Vector2(500, 100),
            new Rectangle(BARFRAMEWIDTH - BARFRAMEWIDTH, 0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);

            spriteBatch.Draw(loadingBars, new Vector2(500, 170),
            new Rectangle(BARFRAMEWIDTH - BARFRAMEWIDTH, 0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);
        }
        public void DrawBars(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loadingBars,new Vector2(500,100),
                new Rectangle(sleepFrame * BARFRAMEWIDTH, BARFRAMEHEIGHT*0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);

            spriteBatch.Draw(loadingBars, new Vector2(500, 170),
            new Rectangle(hungerFrame * BARFRAMEWIDTH - BARFRAMEWIDTH, BARFRAMEHEIGHT*1, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);
        }



    }
}
