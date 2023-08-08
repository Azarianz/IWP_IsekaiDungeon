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
        private float attackRange = (float)AttackRange.Sword;  //Default Melee Range
        public float getAttackRange { get { return attackRange; } }
        public int getRewardXP { get { return AgentStats.EXP_REWARD; } }
        public int getRewardGold { get { return AgentStats.GOLD_REWARD; } }

        [SerializeField]
        private float currentXP { get { return AgentLevel.GetXP(); } }

        [SerializeField]
        public Vector3 position { get; set; }

        [SerializeField]
        public string unitType { get; set; }

        [SerializeField]
        public string unitClass { get; set; }

        //Identification Variables
        public Guid unitID;
        public string unit_class;
        public string unit_Name;

        //Animations
        public Sprite unit_icon;
        public RuntimeAnimatorController animationSet;

        //Core Systems
        [SerializeField] private ClassSystem AgentClass;        //Attack & Abilities
        [SerializeField] private LevelSystem AgentLevel;        //Exp System
        [SerializeField] private CharacterStats AgentStats;     //Stat Data
        [SerializeField] private BaseStats AgentBaseStat;       //Stat Reference
        [SerializeField] public Agent_Animator AgentAnimator;   //Sprite & FX

        public int awaken_level;
        public InventoryItem equipItem_1, equipItem_2;


        public Agent_Data()
        {
            unitID = Guid.NewGuid();
        }

        public Agent_Data(BaseStats baseStat, ClassSystem charClass)
        {
            unitID = Guid.NewGuid();

            AgentBaseStat = baseStat;
            AgentStats = new CharacterStats(AgentBaseStat);

            unit_icon = AgentBaseStat.sprite;
            animationSet = baseStat.animator;

            unit_class = charClass.className;
            AgentClass = baseStat.agentClass;
            attackRange = charClass.GetAttackRange();

            AgentLevel = new LevelSystem(this);
        }

        public Agent_Data(BaseStats baseStat, int level, int star)
        {
            unitID = Guid.NewGuid();

            AgentBaseStat = baseStat;
            AgentStats = new CharacterStats(AgentBaseStat);

            awaken_level = star;
            unit_icon = AgentBaseStat.sprite;
            animationSet = baseStat.animator;

            AgentClass = baseStat.agentClass;
            unit_class = AgentClass.className;
            attackRange = AgentClass.GetAttackRange();

            AgentLevel = new LevelSystem(this);
            AgentLevel.SetLevel(level);

            AgentStats.UpdateStats(AgentLevel, awaken_level);
        }

        public Agent_Animator GetAgentAnimator()
        {
            return AgentAnimator;
        }

        public CharacterStats GetAgentStats()
        {
            return AgentStats;
        }

        public void SetAgentStats(CharacterStats stats)
        {
            AgentStats = stats;
        }


        public LevelSystem GetLevelSystem()
        {
            return AgentLevel;
        }

        public ClassSystem GetAgentClass()
        {
            return AgentClass;
        }


        public void SetAgentClass(ClassSystem unitClass)
        {
            AgentClass = unitClass;
        }

        public int GetAgentLevel()
        {
            int lvl = AgentLevel.GetLevel();
            return lvl;
        }

        public void LevelUpStats()
        {
            AgentStats.UpdateStats(AgentLevel, awaken_level);
            AgentAnimator.Animate_LevelUp();
            AgentStats.STAT_HEALTH = AgentStats.STAT_MAXHEALTH;
            AgentStats.STAT_MANA = AgentStats.STAT_MAXMANA;
        }

        public void SetAnimations(BaseStats _ref)
        {
            Sprite spr = _ref.sprite;
            animationSet = _ref.animator;

            AgentAnimator.Initialize(spr, animationSet);
        }

        public void SetEnemyAnimations(BaseStats _ref)
        {
            unit_icon = _ref.sprite;
            Sprite spr = _ref.sprite;
            animationSet = _ref.animator;

            AgentAnimator.Initialize_Data(spr, animationSet);
        }

        public void SetEnemyAgent(string unitType)
        {
            //Debug.Log("Base Animations: " + unitType);
            // Load the base stats for the enemy type based on the unitType
            BaseStats baseStat = Resources.Load<BaseStats>("Base_Enemies/" + unitType);
            //Debug.Log("Load Resource: Base_Enemies/" + unitType + ": " + baseStat);

            if (baseStat != null)
            {
                AgentAnimator = new Agent_Animator();
                AgentStats.baseStatREF = baseStat;
                AgentBaseStat = baseStat;
                SetEnemyAnimations(baseStat);
                AgentClass = baseStat.agentClass;
            }
            else
            {
                Debug.LogError("BaseStats not found for unit type: " + unitType);
            }
        }
    }
}
