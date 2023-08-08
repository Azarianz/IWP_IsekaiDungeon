using AI;
using AI.STATS;
using Codice.CM.Client.Differences.Graphic;
using IsekaiDungeon;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(DungeonManager))]
public class DungeonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DungeonManager dungeonManager = (DungeonManager)target;
        DungeonJsonReader reader = dungeonManager.reader;
        Dungeon_Data dungeonData = reader.ReadDungeonData();


        if (dungeonManager != null && reader != null && dungeonData != null && dungeonData.floors != null)
        {
            foreach (Floor floor in dungeonData.floors)
            {
                GUILayout.Label("Floor Number: " + floor.floorNumber);

                if (floor.stats != null)
                {
                    foreach (KeyValuePair<string, CharacterStats> kvp in floor.stats)
                    {
                        GUILayout.Label("Unit Type: " + kvp.Key);
                        GUILayout.Label("STAT_DAMAGE: " + kvp.Value.STAT_DAMAGE);
                        GUILayout.Label("STAT_HEALTH: " + kvp.Value.STAT_HEALTH);
                        GUILayout.Label("STAT_HEALTHREGEN: " + kvp.Value.STAT_HEALTHREGEN);
                        GUILayout.Label("STAT_MANA: " + kvp.Value.STAT_MANA);
                        GUILayout.Label("STAT_MANAREGEN: " + kvp.Value.STAT_MANAREGEN);
                        GUILayout.Label("STAT_DEFENSE: " + kvp.Value.STAT_DEFENSE);
                        GUILayout.Label("STAT_SPEED: " + kvp.Value.STAT_SPEED);
                        GUILayout.Label("STAT_ACCURACY: " + kvp.Value.STAT_ACCURACY);
                        GUILayout.Label("STAT_CRITCHANCE: " + kvp.Value.STAT_CRITCHANCE);
                        GUILayout.Label("STAT_MELEEPROFICIENCY: " + kvp.Value.STAT_MELEEPROFICIENCY);
                        GUILayout.Label("STAT_RANGEPROFICIENCY: " + kvp.Value.STAT_RANGEPROFICIENCY);
                        GUILayout.Label("STAT_MAGICPROFICIENCY: " + kvp.Value.STAT_MAGICPROFICIENCY);
                        GUILayout.Label("ATTACK_ELEMENT: " + kvp.Value.ATTACK_ELEMENT);
                        GUILayout.Label("STAT_FIRE_RESISTANCE: " + kvp.Value.STAT_FIRE_RESISTANCE);
                        GUILayout.Label("STAT_WATER_RESISTANCE: " + kvp.Value.STAT_WATER_RESISTANCE);
                        GUILayout.Label("STAT_EARTH_RESISTANCE: " + kvp.Value.STAT_EARTH_RESISTANCE);
                        GUILayout.Label("STAT_LIGHTNING_RESISTANCE: " + kvp.Value.STAT_LIGHTNING_RESISTANCE);
                        GUILayout.Label("STAT_DEBUFF_RESISTANCE: " + kvp.Value.STAT_DEBUFF_RESISTANCE);
                        GUILayout.Label("GOLD_REWARD: " + kvp.Value.GOLD_REWARD);
                        GUILayout.Label("EXP_REWARD: " + kvp.Value.EXP_REWARD);
                        // Add other stats properties you want to display
                        GUILayout.Space(5);
                    }
                }
                else
                {
                    GUILayout.Label("Stats dictionary is null for this floor.");
                }

                if (floor.waves != null)
                {
                    foreach (Wave wave in floor.waves)
                    {
                        GUILayout.Label("Wave Number: " + wave.waveNumber);

                        if (wave.enemies != null)
                        {
                            foreach (Agent_Data enemyData in wave.enemies)
                            {
                                GUILayout.Label("Enemy Unit Type: " + enemyData.unitType);
                                GUILayout.Label("Enemy Position: " + enemyData.position.x + ", " + enemyData.position.y + ", " + enemyData.position.z);
                                GUILayout.Space(5);
                            }
                        }
                        else
                        {
                            GUILayout.Label("Enemies list is null for this wave.");
                        }

                        GUILayout.Space(5);
                    }
                }
                else
                {
                    GUILayout.Label("Waves list is null for this floor.");
                }

                GUILayout.Space(10);
            }
        }
        else
        {
            GUILayout.Label("Dungeon Manager, DungeonJsonReader, or Dungeon Data is null.");
        }



        // Draw the default inspector GUI (optional, you can remove this if not needed)
        DrawDefaultInspector();
    }
}