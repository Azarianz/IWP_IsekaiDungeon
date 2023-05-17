using AI.STATS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.STATS
{
    public class CharacterStats : Stats
    {
        //Constructor (Takes in baseStat which stores base classes/race stat)
        public CharacterStats(BaseStats baseStat)
        {
            STAT_DAMAGE = baseStat.BASE_DAMAGE;
            STAT_HEALTH = baseStat.BASE_HEALTH;
            STAT_MAXHEALTH = baseStat.BASE_HEALTH;
            STAT_HEALTHREGEN = baseStat.BASE_HEALTHREGEN;
            STAT_MANA = baseStat.BASE_MANA;
            STAT_MAXMANA = baseStat.BASE_MANA;
            STAT_MANAREGEN = baseStat.BASE_MANAREGEN;
            STAT_DEFENSE = baseStat.BASE_DEFENSE;
            STAT_SPEED = baseStat.BASE_SPEED;
            STAT_ACCURACY = baseStat.BASE_ACCURACY;
            STAT_CRITCHANCE = baseStat.BASE_CRITCHANCE;
            STAT_MELEEPROFICIENCY = baseStat.BASE_MELEEPROFICIENCY;
            STAT_RANGEPROFICIENCY = baseStat.BASE_RANGEPROFICIENCY;
            STAT_MAGICPROFICIENCY = baseStat.BASE_MAGICPROFICIENCY;
            ATTACK_ELEMENT = (Stats.ELEMENTAL_ATTACK)baseStat.ATTACK_ELEMENT;
            STAT_FIRE_RESISTANCE = baseStat.BASE_FIRE_RESISTANCE;
            STAT_WATER_RESISTANCE = baseStat.BASE_WATER_RESISTANCE;
            STAT_EARTH_RESISTANCE = baseStat.BASE_EARTH_RESISTANCE;
            STAT_LIGHTNING_RESISTANCE = baseStat.BASE_LIGHTNING_RESISTANCE;
            STAT_DEBUFF_RESISTANCE = baseStat.BASE_DEBUFF_RESISITANCE();
        }

        //STAT VARIABLES
        public float STAT_DAMAGE { get; set; }
        public float STAT_HEALTH { get; set; }
        public float STAT_MAXHEALTH { get; set; }
        public float STAT_HEALTHREGEN { get; set; }
        public float STAT_MANA { get; set; }
        public float STAT_MAXMANA { get; set; }
        public float STAT_MANAREGEN { get; set; }
        public float STAT_DEFENSE { get; set; }
        public float STAT_SPEED { get; set; }
        public float STAT_ACCURACY { get; set; }
        public float STAT_CRITCHANCE { get; set; }
        public float STAT_MELEEPROFICIENCY { get; set; }
        public float STAT_RANGEPROFICIENCY { get; set; }
        public float STAT_MAGICPROFICIENCY { get; set; }
        public Stats.ELEMENTAL_ATTACK ATTACK_ELEMENT { get; set; }
        public float STAT_FIRE_RESISTANCE { get; set; }
        public float STAT_WATER_RESISTANCE { get; set; }
        public float STAT_EARTH_RESISTANCE { get; set; }
        public float STAT_LIGHTNING_RESISTANCE { get; set; }
        public float STAT_DEBUFF_RESISTANCE { get; set; }
        public float STAT_DEATH_RESISTANCE { get; set; }
    }
}
