﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tama_Caretaker
{
    internal class Nightmare
    {
        private int speed = 2;
        private int x;
        private int y;
        private Random random = new Random();
        public Rectangle position;
        private Texture2D texture;

        private bool randCheck = false;

        //int frame;
        //double timeCounter;
        //float fps;
        //float timePerFrame;
        //const int GHOSTFRAMES = 4;
        const int GHOSTWIDTH = 96;
        const int GHOSTHEIGHT = 96;

        public Nightmare(Texture2D texture, Rectangle position)
        {
            this.position = position;
            this.texture = texture;

            //this.frame = 0;
            //this.fps = 6.0f;
            //timeCounter = 0;
            //this.timePerFrame = 1.0f / fps;


        }

        public void Update(GameTime gameTime)
        {
            if(!randCheck)
            {
                x = random.Next(1, 3);
                y = random.Next(1, 3);
                randCheck = true;
            }

            position.X += speed * x;
            position.Y += speed * y;

            if (position.Left < 0 || position.Right > 1280)
            {
                x *= -1;
            }

            if (position.Top < 0 || position.Bottom > 720)
            {
                y *= -1;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
        public void NightmareReset()
        {
            position.X = 0;
            position.Y = 0;
            randCheck = false;
        }
    }
}
