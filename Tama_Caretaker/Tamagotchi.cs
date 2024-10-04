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

        public int sleepBarFrame;
        public int hungerBarFrame;

        public Tamagotchi(Texture2D tamagotchiTex, Texture2D loadingBars)
        {
            sleepBarFrame = 7;
            hungerBarFrame = 7;
            this.tamagotchiTex = tamagotchiTex;
            this.loadingBars = loadingBars;
        }




    }
}
