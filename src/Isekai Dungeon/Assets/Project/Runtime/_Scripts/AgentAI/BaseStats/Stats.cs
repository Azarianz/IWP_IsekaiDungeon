using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.STATS
{
    public interface Stats
    {
        float STAT_DAMAGE { get; set; }

        float STAT_HEALTH { get; set; }
        float STAT_MAXHEALTH { get; set; }
        float STAT_HEALTHREGEN { get; set; }

        float STAT_MANA { get; set; }
        float STAT_MAXMANA { get; set; }
        float STAT_MANAREGEN { get; set; }

        float STAT_DEFENSE { get; set; }
        float STAT_SPEED { get; set; }
        float STAT_ACCURACY { get; set; }
        float STAT_CRITCHANCE { get; set; }

        float STAT_MELEEPROFICIENCY { get; set; }
        float STAT_RANGEPROFICIENCY { get; set; }
        float STAT_MAGICPROFICIENCY { get; set; }

        enum ELEMENTAL_ATTACK
        {
            NONE = 0,
            FIRE = 1,
            WATER = 2,
            EARTH = 3,
            LIGHTNING = 4
        }

        ELEMENTAL_ATTACK ATTACK_ELEMENT { get; set; }

        float STAT_FIRE_RESISTANCE { get; set; }
        float STAT_WATER_RESISTANCE { get; set; }
        float STAT_EARTH_RESISTANCE { get; set; }
        float STAT_LIGHTNING_RESISTANCE { get; set; }
        float STAT_DEBUFF_RESISTANCE { get; set; }
        float STAT_DEATH_RESISTANCE { get; set; }
    }
}