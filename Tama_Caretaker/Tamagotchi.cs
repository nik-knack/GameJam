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

        

        int frame;
        double timeCounter;
        float fps;
        float timePerFrame;

        public Tamagotchi(Texture2D tamagotchiTex, Texture2D loadingBars)
        {
            sleepBarFrame = 7;
            hungerBarFrame = 7;
            this.tamagotchiTex = tamagotchiTex;
            this.loadingBars = loadingBars;
            frame = 0;
            fps = 6.0f;
            timePerFrame = 1.0f / fps;
        }

        public void UpdateAnimations(GameTime gameTime)
        {
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeCounter >= timePerFrame)
            {
                frame += 1;

                if (frame > BARFRAMECOUNTER)
                    frame = 1;

                timeCounter -= timePerFrame;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loadingBars,new Rectangle(0,0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
                new Rectangle(frame * BARFRAMEWIDTH - BARFRAMEWIDTH, 0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1.0f);
        }

    }
}
