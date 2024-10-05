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
        private Texture2D loadingBars;

        private const int BARFRAMECOUNTER = 7;

        private const int BARFRAMEHEIGHT = 16;
        private const int BARFRAMEWIDTH = 48;

        public int sleepBarFrame;
        public int hungerBarFrame;

        public bool isAlive = true;

        private List<Food> foodList;
        private Random random;
        

        int feedFrame;
        int sleepFrame;
        int playFrame;

        double timeCounterSleepBar;
        double timeCounterHungerBar;
        double timeCounterPlayBar;

        float sleepFps;
        float feedFps;
        float playFps;

        float timePerSleepFrame;
        float timePerFeedFrame;
        float timePerPlayFrame;

        public Tamagotchi(Texture2D loadingBars)
        {
            sleepBarFrame = 7;
            hungerBarFrame = 7;
            this.loadingBars = loadingBars;
            sleepFrame = 1;
            feedFrame = 1;
            playFrame = 1;
            sleepFps = 0.12f;
            feedFps = 0.10f;
            playFps = 0.18f;
            this.timePerSleepFrame = 1.0f / sleepFps;
            this.timePerFeedFrame = 1.0f / feedFps;
            this.timePerPlayFrame = 1.0f / playFps;
        }
        
        public void UpdateAnimations(GameTime gameTime)
        {
            timeCounterSleepBar += gameTime.ElapsedGameTime.TotalSeconds;
            timeCounterHungerBar += gameTime.ElapsedGameTime.TotalSeconds;
            timeCounterPlayBar += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeCounterSleepBar >= timePerSleepFrame)
            {
                sleepFrame += 1;

                if (sleepFrame > BARFRAMECOUNTER)
                    sleepFrame = 2;

                timeCounterSleepBar -= timePerSleepFrame;
            }
            if (timeCounterHungerBar >= timePerFeedFrame)
            {
                feedFrame += 1;

                if (feedFrame > BARFRAMECOUNTER)
                    feedFrame = 2;

                timeCounterHungerBar -= timePerFeedFrame;
            }
            if (timeCounterPlayBar >= timePerPlayFrame)
            {
                playFrame += 1;

                if (playFrame > BARFRAMECOUNTER)
                    playFrame = 2;

                timeCounterPlayBar -= timePerPlayFrame;
            }

           if (playFrame >= 7 || feedFrame >= 7 || playFrame >= 7)
            {
                isAlive = false;
            }
        }
        
        public void FeedUpdate(GameTime gameTime)
        {
            foodList = new List<Food>(10);

            for (int i = 0; i < foodList.Count; i++)
            {
                int randNum = random.Next(0, 10);

                int randPos = 

                if (randNum <= 2)
                {
                    foodList[i] = new Food()
                }
                else if (randNum < 6 && randNum > 2)
                {

                }
                else
                {

                }
            }
        }


        public void DrawBarOutline(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loadingBars, new Vector2(0, 0),
            new Rectangle(BARFRAMEWIDTH - BARFRAMEWIDTH, 0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);

            spriteBatch.Draw(loadingBars, new Vector2(0, 70),
            new Rectangle(BARFRAMEWIDTH - BARFRAMEWIDTH, 0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);

            spriteBatch.Draw(loadingBars, new Vector2(0, 140),
            new Rectangle(BARFRAMEWIDTH - BARFRAMEWIDTH, 0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);
        }
        public void DrawBars(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loadingBars,new Vector2(0,0),
                new Rectangle(sleepFrame * BARFRAMEWIDTH, BARFRAMEHEIGHT*0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);

            spriteBatch.Draw(loadingBars, new Vector2(0, 70),
            new Rectangle(feedFrame * BARFRAMEWIDTH, BARFRAMEHEIGHT*1, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);

            spriteBatch.Draw(loadingBars, new Vector2(0, 140),
            new Rectangle(playFrame * BARFRAMEWIDTH, BARFRAMEHEIGHT * 2, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);
        }
        
        public void Reset()
        {
            isAlive = true;
            sleepFrame = 1;
            playFrame = 1;
            feedFrame = 1;
        }



    }
}
