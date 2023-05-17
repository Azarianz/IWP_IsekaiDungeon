using AI;
using AI.STATS;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Agent_Data))]
public class Agent_StatInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Agent_Data data = (Agent_Data)target;
        CharacterStats stats = data.GetAgentStats();

        if(stats != null)
        {
            //Display all variables in the CharacterStat class
            EditorGUILayout.LabelField("Character Stats", EditorStyles.boldLabel);
            stats.STAT_HEALTH = EditorGUILayout.FloatField("Health", stats.STAT_HEALTH);
            stats.STAT_MAXHEALTH = EditorGUILayout.FloatField("Max Health", stats.STAT_MAXHEALTH);
            stats.STAT_HEALTHREGEN = EditorGUILayout.FloatField("Health Regen", stats.STAT_HEALTHREGEN);
            stats.STAT_MANA = EditorGUILayout.FloatField("Mana", stats.STAT_MANA);
            stats.STAT_MAXMANA = EditorGUILayout.FloatField("Max Mana", stats.STAT_MAXMANA);
            stats.STAT_MANAREGEN = EditorGUILayout.FloatField("Mana Regen", stats.STAT_MANAREGEN);
            stats.STAT_DEFENSE = EditorGUILayout.FloatField("Defense", stats.STAT_DEFENSE);
            stats.STAT_SPEED = EditorGUILayout.FloatField("Speed", stats.STAT_SPEED);
            stats.STAT_ACCURACY = EditorGUILayout.FloatField("Accuracy", stats.STAT_ACCURACY);
            stats.STAT_CRITCHANCE = EditorGUILayout.FloatField("Crit Chance", stats.STAT_CRITCHANCE);
            stats.STAT_MELEEPROFICIENCY = EditorGUILayout.FloatField("Melee Proficiency", stats.STAT_MELEEPROFICIENCY);
            stats.STAT_RANGEPROFICIENCY = EditorGUILayout.FloatField("Range Proficiency", stats.STAT_RANGEPROFICIENCY);
            stats.STAT_MAGICPROFICIENCY = EditorGUILayout.FloatField("Magic Proficiency", stats.STAT_MAGICPROFICIENCY);
            stats.ATTACK_ELEMENT = (Stats.ELEMENTAL_ATTACK)EditorGUILayout.EnumPopup("Attack Element", stats.ATTACK_ELEMENT);
            stats.STAT_FIRE_RESISTANCE = EditorGUILayout.FloatField("Fire Resist", stats.STAT_FIRE_RESISTANCE);
            stats.STAT_WATER_RESISTANCE = EditorGUILayout.FloatField("Water Resist", stats.STAT_WATER_RESISTANCE);
            stats.STAT_EARTH_RESISTANCE = EditorGUILayout.FloatField("Earth Resist", stats.STAT_EARTH_RESISTANCE);
            stats.STAT_LIGHTNING_RESISTANCE = EditorGUILayout.FloatField("Lightning Resist", stats.STAT_LIGHTNING_RESISTANCE);
            stats.STAT_DEBUFF_RESISTANCE = EditorGUILayout.FloatField("Debuff Resist", stats.STAT_DEBUFF_RESISTANCE);
            stats.STAT_DEATH_RESISTANCE = EditorGUILayout.FloatField("Death Resist", stats.STAT_DEATH_RESISTANCE);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
