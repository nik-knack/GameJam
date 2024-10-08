﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Tama_Caretaker
{
    internal class SleepPlayer
    {
        Texture2D texture;
        Rectangle position;
        Nightmare nightmare;
        Tamagotchi tamagotchiCom;
        int screenWidth;
        int screenHeight;

        public bool hit = false;

        public SleepPlayer(Nightmare nightmare, Tamagotchi tamagotchiCom, Texture2D texture, Rectangle position,
            int screenWidth, int screenHeight)
        {
            this.texture = texture;
            this.position = position;
            this.nightmare = nightmare;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.tamagotchiCom = tamagotchiCom;
            
        }

        public void Update(GameTime gametime, KeyboardState kbState)
        {

            tamagotchiCom.sleepTimer -= gametime.ElapsedGameTime.TotalSeconds;

            if (kbState.IsKeyDown(Keys.W))
                position.Y -= 4;

            if(kbState.IsKeyDown(Keys.S))
                position.Y += 4;

            if (kbState.IsKeyDown(Keys.A))
                position.X -= 4;

            if (kbState.IsKeyDown(Keys.D))
                position.X += 4;

            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            if (position.Y > screenHeight-texture.Height)
            {
                position.Y = screenHeight-texture.Height;
            }
            if (position.X > screenWidth - texture.Width)
            {
                position.X= screenWidth-texture.Width;
            }

            if (tamagotchiCom.sleepTimer < 0)
            {
                tamagotchiCom.completed = true;
                tamagotchiCom.SleepFrame = 1;
            }

            if (CheckColisions(nightmare.position))
            {
                hit = true;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public bool CheckColisions(Rectangle nightmarePos)
        {
            return position.Intersects(nightmarePos);
        }

        public void Reset()
        {
            position.X = screenWidth/2;
            position.Y = screenHeight/2;

            hit = false;
        }
    }
}
