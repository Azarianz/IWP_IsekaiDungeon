using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.STATS
{
    public class LevelSystem
    {
        private int LVL;
        private int XP;
        private int XPToLevelUp;
        private int BaseXPToLevelUp = 100;
        private float XPToLevelUpMultiplier = 3.6f;

        public int GetLevel() { return LVL; }
        public int GetXP() { return XP; }
        public int GetXPToNextLevel() 
        { 
            // LevelUp Formula (Exponential Curve)
            return (int)((LVL * BaseXPToLevelUp) * XPToLevelUpMultiplier); 
        }

        public LevelSystem()    
        {
            LVL = 0;
            XP = 0;
            XPToLevelUp = BaseXPToLevelUp;  
        }

        public void AddExperience(int amt)
        {
            XP += amt;

            while (XP >= XPToLevelUp)
            {
                // Enough experience to level up
                LVL++;
                XP -= XPToLevelUp;
            }
             
            if (XP < 0)
            {
                //XP does not fall below 0
                XP = 0;
            }
        }

    }
}

