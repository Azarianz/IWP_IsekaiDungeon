using AI.STATS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Agent_Data : MonoBehaviour
    {
        [SerializeField] private LevelSystem AgentLevel;
        [SerializeField] private CharacterStats AgentStats;
        [SerializeField] private BaseStats AgentBaseStat;

        private void Start()
        {
            AgentLevel = new LevelSystem();
            AgentStats = new CharacterStats(AgentBaseStat);
        }

        public CharacterStats GetAgentStats()
        {
            return AgentStats;
        }

    }
}
