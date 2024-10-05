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
        private Rectangle position;
        public Food(Texture2D tex, Rectangle position)
        {
            texture = tex;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public bool CheckCollision(Rectangle position, MouseState mouse)
        {
            return position.Contains(mouse.Position);
        }
    }
}
