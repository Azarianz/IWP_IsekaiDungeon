using AI.STATS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [Serializable]
    public class Agent_Data
    {
        public Guid unitID;
        public string unit_class;
        public Sprite unit_icon;

        [SerializeField] private ClassSystem AgentClass;
        [SerializeField] private LevelSystem AgentLevel;
        [SerializeField] private CharacterStats AgentStats;
        [SerializeField] private BaseStats AgentBaseStat;

        public InventoryItem equipItem_1, equipItem_2;

        public Agent_Data()
        {
            unitID = Guid.NewGuid();
        }

        public Agent_Data(BaseStats baseStat, ClassSystem charClass)
        {
            unitID = Guid.NewGuid();
            AgentBaseStat = baseStat;
            unit_icon = AgentBaseStat.sprite;
            AgentClass = charClass;

            AgentLevel = new LevelSystem();
            AgentStats = new CharacterStats(AgentBaseStat);
        }

        public CharacterStats GetAgentStats()
        {
            return AgentStats;
        }

        public int GetAgentLevel()
        {
            int lvl = AgentLevel.GetLevel();
            return lvl;
        }
    }
}
