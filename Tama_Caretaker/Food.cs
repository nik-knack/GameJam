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
    internal class Food
    {
        private Texture2D texture;
        public Rectangle position;
        private Vector2 grrr;

        public Food(Texture2D tex, Rectangle position)
        {
            texture = tex;
            this.position = position;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sb)
        { 
            sb.Draw(texture, position, Color.White);
        }
        public bool CheckCollision(MouseState mouse)
        {
            return position.Contains(mouse.Position);
        }
    }
}
