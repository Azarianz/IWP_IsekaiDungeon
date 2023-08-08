using AI.STATS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.STATS
{
    [Serializable]
    public class CharacterStats : Stats, IDamageable
    {
        public CharacterStats() { }

        public CharacterStats(CharacterStats import) 
        {
            STAT_DAMAGE = import.STAT_DAMAGE;
            STAT_HEALTH = import.STAT_HEALTH;
            STAT_MAXHEALTH = import.STAT_HEALTH;
            STAT_HEALTHREGEN = import.STAT_HEALTHREGEN;
            STAT_MANA = import.STAT_MANA;
            STAT_MAXMANA = import.STAT_MANA;
            STAT_MANAREGEN = import.STAT_MANAREGEN;
            STAT_DEFENSE = import.STAT_DEFENSE;
            STAT_SPEED = import.STAT_SPEED;
            STAT_ACCURACY = import.STAT_ACCURACY;
            STAT_CRITCHANCE = import.STAT_CRITCHANCE;
            STAT_MELEEPROFICIENCY = import.STAT_MELEEPROFICIENCY;
            STAT_RANGEPROFICIENCY = import.STAT_RANGEPROFICIENCY;
            STAT_MAGICPROFICIENCY = import.STAT_MAGICPROFICIENCY;
            ATTACK_ELEMENT = (Stats.ELEMENTAL_ATTACK)import.ATTACK_ELEMENT;
            STAT_FIRE_RESISTANCE = import.STAT_FIRE_RESISTANCE;
            STAT_WATER_RESISTANCE = import.STAT_WATER_RESISTANCE;
            STAT_EARTH_RESISTANCE = import.STAT_EARTH_RESISTANCE;
            STAT_LIGHTNING_RESISTANCE = import.STAT_LIGHTNING_RESISTANCE;
            STAT_DEBUFF_RESISTANCE = import.STAT_DEBUFF_RESISTANCE;
            GOLD_REWARD = import.GOLD_REWARD;
            EXP_REWARD = import.EXP_REWARD;
        }

        //Constructor (Takes in baseStat which stores base classes/race stat)
        public CharacterStats(BaseStats baseStat)
        {
            //Debug.Log("Character Stat Constructor");
            UNIT_RACE = baseStat.BASE_RACE;
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
            STAT_DEBUFF_RESISTANCE = baseStat.BASE_DEBUFF_RESISTANCE();

            baseStatREF = baseStat;
        }

        //STAT VARIABLES
        public string UNIT_NAME { get; set; }   //for enemies
        public string UNIT_RACE { get; set; }
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
        public BaseStats baseStatREF;
        public int GOLD_REWARD { get; set; }
        public int EXP_REWARD { get; set; }

        //UNIT VARIABLES
        public void LoadBaseStats(string unitType)
        {
            // Load the base stats for the enemy type based on the unitType
            BaseStats baseStat = Resources.Load<BaseStats>("Base Enemies/" + unitType);

            if (baseStat != null)
            {
                UNIT_RACE = baseStat.BASE_RACE;
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
                STAT_DEBUFF_RESISTANCE = baseStat.BASE_DEBUFF_RESISTANCE();

                baseStatREF = baseStat;
            }
        }

        public void LoadStats(CharacterStats readData)
        {
            UNIT_RACE = readData.UNIT_RACE;
            STAT_DAMAGE = readData.STAT_DAMAGE;
            STAT_HEALTH = readData.STAT_HEALTH;
            STAT_MAXHEALTH = readData.STAT_MAXHEALTH;
            STAT_HEALTHREGEN = readData.STAT_HEALTHREGEN;
            STAT_MANA = readData.STAT_MANA;
            STAT_MAXMANA = readData.STAT_MAXMANA;
            STAT_MANAREGEN = readData.STAT_MANAREGEN;
            STAT_DEFENSE = readData.STAT_DEFENSE;
            STAT_SPEED = readData.STAT_SPEED;
            STAT_ACCURACY = readData.STAT_ACCURACY;
            STAT_CRITCHANCE = readData.STAT_CRITCHANCE;
            STAT_MELEEPROFICIENCY = readData.STAT_MELEEPROFICIENCY;
            STAT_RANGEPROFICIENCY = readData.STAT_RANGEPROFICIENCY;
            STAT_MAGICPROFICIENCY = readData.STAT_MAGICPROFICIENCY;
            ATTACK_ELEMENT = (Stats.ELEMENTAL_ATTACK)readData.ATTACK_ELEMENT;
            STAT_FIRE_RESISTANCE = readData.STAT_FIRE_RESISTANCE;
            STAT_WATER_RESISTANCE = readData.STAT_WATER_RESISTANCE;
            STAT_EARTH_RESISTANCE = readData.STAT_EARTH_RESISTANCE;
            STAT_LIGHTNING_RESISTANCE = readData.STAT_LIGHTNING_RESISTANCE;
            STAT_DEBUFF_RESISTANCE = readData.STAT_DEBUFF_RESISTANCE;
            GOLD_REWARD = readData.GOLD_REWARD;
            EXP_REWARD = readData.EXP_REWARD;
        }


        public float CalculateDamage()
        {
            float minDamage = STAT_DAMAGE * 0.9f;
            float maxDamage = STAT_DAMAGE * 1.1f;

            float finalDamage = UnityEngine.Random.Range(minDamage, maxDamage);

            if(UnityEngine.Random.Range(0, 100) >= STAT_CRITCHANCE)
            {
                finalDamage *= 1.5f;   //x1.5 crit dmg modifier
            }

            //Debug.Log("" + UNIT_NAME + ": " + finalDamage);
            return finalDamage;
        }

        public void ReceiveDamage(float dmg)
        {
            float finalDmg = dmg * (100 / (100 + STAT_DEFENSE));
            STAT_HEALTH -= finalDmg;
        }

        public void ReceiveHeal(float val)
        {
            STAT_HEALTH += val;
        }

        public void UpdateStats(LevelSystem levelData, int awakenLevel)
        {
            if(awakenLevel != 0)
            {
                STAT_MAXHEALTH = baseStatREF.BASE_HEALTH * (levelData.GetLevel() * awakenLevel);
                STAT_MAXMANA = baseStatREF.BASE_MANA * (levelData.GetLevel() * awakenLevel);
                STAT_DAMAGE = baseStatREF.BASE_DAMAGE * (levelData.GetLevel() * awakenLevel / 1.5f);  //to balance ltr 
                STAT_DEFENSE = baseStatREF.BASE_DEFENSE * (levelData.GetLevel() * awakenLevel);
            }
            else
            {
                STAT_MAXHEALTH = baseStatREF.BASE_HEALTH * (levelData.GetLevel());
                STAT_MAXMANA = baseStatREF.BASE_MANA * (levelData.GetLevel());
                STAT_DAMAGE = baseStatREF.BASE_DAMAGE * (levelData.GetLevel() / 1.5f);  //to balance ltr 
                STAT_DEFENSE = baseStatREF.BASE_DEFENSE * (levelData.GetLevel());
            }

            STAT_HEALTHREGEN = baseStatREF.BASE_HEALTHREGEN;
            STAT_MANAREGEN = baseStatREF.BASE_MANAREGEN;
            STAT_DEBUFF_RESISTANCE = baseStatREF.BASE_DEBUFF_RESISTANCE() * (levelData.GetLevel() / 5);
        }
    }
}
