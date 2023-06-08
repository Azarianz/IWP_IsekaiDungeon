using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.STATS
{
    [CreateAssetMenu(menuName = "Add Base Unit Stats")]
    public class BaseStats : ScriptableObject
    {
        public Sprite sprite;

        public float BASE_DAMAGE;

        [Header("BASE")]
        public float BASE_HEALTH;
        public float BASE_HEALTHREGEN;

        public float BASE_MANA;
        public float BASE_MANAREGEN;

        public float BASE_DEFENSE;
        public float BASE_SPEED;
        public float BASE_ACCURACY;
        public float BASE_CRITCHANCE;

        [Header("PROFICIENCY")]
        public float BASE_MELEEPROFICIENCY;
        public float BASE_RANGEPROFICIENCY;
        public float BASE_MAGICPROFICIENCY;

        public enum ELEMENTAL_ATTACK
        {
            NONE = 0,
            FIRE = 1,
            WATER = 2,
            EARTH = 3,
            LIGHTNING = 4
        }

        [Header("ATTACK ELEMENT")]
        public ELEMENTAL_ATTACK ATTACK_ELEMENT;

        [Header("RESISTANCES")]
        public float BASE_FIRE_RESISTANCE;
        public float BASE_WATER_RESISTANCE;
        public float BASE_EARTH_RESISTANCE;
        public float BASE_LIGHTNING_RESISTANCE;

        public enum DEBUFF_RESIST
        {
            NONE = 0,       //0%
            LOW = 1,        //15%
            MEDIUM = 2,     //30%
            HIGH = 3,       //50%
            VERYHIGH = 4,   //70%
            IMMUNE = 5      //100%
        }

        private DEBUFF_RESIST BASE_DEBUFF_RESISTANCE_SETTING;

        public float BASE_DEBUFF_RESISITANCE()
        {
            switch (BASE_DEBUFF_RESISTANCE_SETTING)
            {
                case DEBUFF_RESIST.NONE: return 0;
                case DEBUFF_RESIST.LOW: return 20;
                case DEBUFF_RESIST.MEDIUM: return 30;
                case DEBUFF_RESIST.HIGH: return 40;
                case DEBUFF_RESIST.VERYHIGH: return 50;
                case DEBUFF_RESIST.IMMUNE: return 100;
                default: return 0;
            }
        }

        public float BASE_DEATH_RESISTANCE;
    }
}