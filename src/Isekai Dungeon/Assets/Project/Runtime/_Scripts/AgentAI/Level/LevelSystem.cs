using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.STATS
{
    [Serializable]
    public class LevelSystem
    {
        private Agent_Data agent_data;
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

        public LevelSystem(Agent_Data data)    
        {
            LVL = 0;
            XP = 0;
            XPToLevelUp = BaseXPToLevelUp;
            agent_data = data;
        }

        public void AddExperience(int amt)
        {
            XP += amt;

            while (XP >= XPToLevelUp)
            {
                // Enough experience to level up
                LevelUp();
            }
             
            if (XP < 0)
            {
                //XP does not fall below 0
                XP = 0;
            }
        }

        public void SetLevel(int number)
        {
            LVL = number;

            for(int i = 0; i < number; i++)
            {
                XPToLevelUp = GetXPToNextLevel();
            }
        }
        
        public void LevelUp()
        {
            XP -= XPToLevelUp;
            int nextLvl = LVL + 1;
            SetLevel(nextLvl);
            agent_data.LevelUpStats();
        }

    }
}

