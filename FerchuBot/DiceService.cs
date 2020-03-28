﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FerchuBot
{
    public class DiceService
    {
        private ThreadSafeRandom randomGenerator;

        public DiceService()
        {
            randomGenerator = new ThreadSafeRandom();
        }

        public async Task<int> RollTheDice(int times, DiceType diceType, int modifier)
        {
            int amount = 0;

            if (times == 0)
                return amount;
            
            for (int i = 0; i < times; i++)
            {
                amount += await randomGenerator.Next((int)diceType);
            }

            return amount + modifier;
        }
    }

    public enum DiceType
    {
        d2 = 2,
        d6 = 6,
        d8 = 8,
        d10 = 10,
        d12 = 12,
        d20 = 20,
        d100 = 100
    }
}
