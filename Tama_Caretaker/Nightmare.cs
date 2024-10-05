using Microsoft.Xna.Framework;
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
        private Vector2 direction;
        private Random random = new Random();
        private Rectangle position;
        private Texture2D texture;

        public Nightmare(Texture2D texture, Rectangle position)
        {
            this.position = position;
            this.texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            int x = random.Next(-3, 2);
            int y = random.Next(-3, 2);

            position.X += (int)(speed * direction.X);
            position.Y += (int)(speed * direction.Y);

            if (position.Left < 0 || position.Right > 1280)
            {
                direction.X *= -1;
            }

            if (position.Top < 0 || position.Bottom > 720)
            {
                direction.Y *= -1;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
