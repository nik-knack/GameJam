using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
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
        private Texture2D potatoTex;
        private Texture2D carrotTex;
        private Texture2D cornTex;
        private Texture2D drumstickIcon;
        private Texture2D sleepIcon;

        private const int BARFRAMECOUNTER = 7;

        private const int BARFRAMEHEIGHT = 80;
        private const int BARFRAMEWIDTH = 240;

        public int sleepBarFrame;
        public int hungerBarFrame;

        public bool isAlive = true;
        private bool isPopulated = false;
        public bool completed = false;

        private List<Food> foodList;
        private Random random = new Random();

        private SoundEffect feedFX;

        int feedFrame;
        int sleepFrame;
        int playFrame;

        public int FeedFrame
        {
            get { return feedFrame; }
            set { feedFrame = value; }
        }

        public int SleepFrame
        {
            get { return sleepFrame; }
            set { sleepFrame = value; }
        }

        public int PlayFrame
        {
            get { return playFrame; }
            set { playFrame = value; }
        }

        double timeCounterSleepBar;
        double timeCounterHungerBar;
        double timeCounterPlayBar;

        float sleepFps;
        float feedFps;
        float playFps;

        float timePerSleepFrame;
        float timePerFeedFrame;
        float timePerPlayFrame;

        public Tamagotchi(Texture2D loadingBars,
            Texture2D cornTex, Texture2D potatoTex, Texture2D carrotTex,
            Texture2D drumstickTex, Texture2D sleepIcon,
            SoundEffect feedFX, SoundEffect winFX)
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

            this.feedFX = feedFX;
            this.potatoTex = potatoTex;
            this.carrotTex = carrotTex;
            this.cornTex = cornTex;
            this.drumstickIcon = drumstickTex;
            this.sleepIcon = sleepIcon;

            foodList = new List<Food>();

            this.timePerSleepFrame = 1.0f / sleepFps;
            this.timePerFeedFrame = 1.0f / feedFps;
            this.timePerPlayFrame = 1.0f / playFps;
        }
        
        public void UpdateBarAnimations(GameTime gameTime)
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
            /*
            if (timeCounterPlayBar >= timePerPlayFrame)
            {
                playFrame += 1;

                if (playFrame > BARFRAMECOUNTER)
                    playFrame = 2;

                timeCounterPlayBar -= timePerPlayFrame;
            }
            */
           if (playFrame >= 7 || feedFrame >= 7 || playFrame >= 7)
           {
                isAlive = false;
           }
        }
        
        public void FeedUpdate(GameTime gameTime, MouseState mState, MouseState prevMState)
        {
            if (!isPopulated)
            {
                for (int i = 0; i < 10; i++)
                {
                    int randNum = random.Next(0, 10);
                    int positionY = random.Next(50, 620);
                    int positionX = random.Next(50, 1180);

                    if (randNum <= 2)
                    {
                        foodList.Add(new Food(cornTex, new Rectangle(positionX, positionY, cornTex.Width, cornTex.Height)));
                    }
                    else if (randNum < 6 && randNum > 2)
                    {
                        foodList.Add(new Food(potatoTex, new Rectangle(positionX, positionY, potatoTex.Width, potatoTex.Height)));
                    }
                    else
                    {
                        foodList.Add(new Food(carrotTex, new Rectangle(positionX, positionY, carrotTex.Width, carrotTex.Height)));
                    }
                }
                isPopulated = true;
            }

            for (int i = 0; i < foodList.Count; i++)
            {
                if (SingleLeftMousePress(mState, prevMState) && foodList[i].CheckCollision(mState))
                {
                    feedFX.Play();
                    foodList.RemoveAt(i);
                }
            }

            if (foodList.Count == 0)
            {
                FeedFrame = 1;
                completed = true;
            }
        }

        public void FeedDraw(SpriteBatch sb)
        {
            for(int i = 0;i < foodList.Count; i++)
            {
                foodList[i].Draw(sb);
            }
        }

        public void SleepUpdate (GameTime gameTime)
        {

        }


        public void DrawBarOutline(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loadingBars, new Vector2(0, 0),
            new Rectangle(BARFRAMEWIDTH - BARFRAMEWIDTH, 0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.Coral, 0, new Vector2(0, 0), new Vector2(1.0f, 1.0f), SpriteEffects.None, 1.0f);

            spriteBatch.Draw(loadingBars, new Vector2(0, 70),
            new Rectangle(BARFRAMEWIDTH - BARFRAMEWIDTH, 0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.Coral, 0, new Vector2(0, 0), new Vector2(1.0f, 1.0f), SpriteEffects.None, 1.0f);
            /*
            spriteBatch.Draw(loadingBars, new Vector2(0, 140),
            new Rectangle(BARFRAMEWIDTH - BARFRAMEWIDTH, 0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.Coral, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);
            */
        }
        public void DrawBars(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loadingBars,new Vector2(0,0),
                new Rectangle(sleepFrame * BARFRAMEWIDTH, BARFRAMEHEIGHT*0, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(1.0f, 1.0f), SpriteEffects.None, 1.0f);

            spriteBatch.Draw(loadingBars, new Vector2(0, 70),
            new Rectangle(feedFrame * BARFRAMEWIDTH, BARFRAMEHEIGHT*1, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(1.0f, 1.0f), SpriteEffects.None, 1.0f);
            /*
            spriteBatch.Draw(loadingBars, new Vector2(0, 140),
            new Rectangle(playFrame * BARFRAMEWIDTH, BARFRAMEHEIGHT * 2, BARFRAMEWIDTH, BARFRAMEHEIGHT),
            Color.White, 0, new Vector2(0, 0), new Vector2(5.0f, 5.0f), SpriteEffects.None, 1.0f);
            */
        }

        public void DrawIcons(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(drumstickIcon,
                new Rectangle(BARFRAMEWIDTH,BARFRAMEHEIGHT, drumstickIcon.Width, drumstickIcon.Height)
                , Color.White);
            spriteBatch.Draw(sleepIcon,
                new Rectangle(BARFRAMEWIDTH, 0, sleepIcon.Width, sleepIcon.Height)
                , Color.White);
        }        
        public void Reset()
        {
            isAlive = true;
            sleepFrame = 1;
            playFrame = 1;
            feedFrame = 1;
        }

        public void FeedReset()
        {
            completed = false;
            isPopulated = false;
            foodList.Clear();
        }

        private bool SingleLeftMousePress(MouseState mouseState, MouseState prevMouseState)
        {
            return (mouseState.LeftButton == ButtonState.Pressed) && (prevMouseState.LeftButton == ButtonState.Released);
        }



    }
}
