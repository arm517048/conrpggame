using System;
using System.Collections.Generic;

namespace conrpggame.Game
{
    public class Dice
    {
        public int RollDice(List<Die> DiceToRoll)
        {
            var randomRoller = new Random();
            var total = 0;
            foreach(var die in DiceToRoll)
            {
                total += randomRoller.Next(1, (int)die);
            }
            return total;
        }
    }
    public enum Die
    {
        D1 = 1,
        D2 = 2,
        D4 = 4,
        D8 = 8,
        D16 = 16,
        D32 = 32,
        D64 = 64,
        D128 = 128,
    }
}
