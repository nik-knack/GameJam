using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tama_Caretaker
{
    internal class Tamagotchi
    {
        private const int SLEEPDECREMENT = 7;
        private const int HUNGERDECREMENT = 5;

        public int sleepBarFrame;
        public int hungerBarFrame;


        public Tamagotchi()
        {
            sleepBarFrame = 7;
            hungerBarFrame = 7;
        }
    }
}
