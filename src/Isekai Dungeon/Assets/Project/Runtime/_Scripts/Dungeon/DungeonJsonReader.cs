using AI;
using AI.STATS;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using UnityEngine.Windows;

namespace IsekaiDungeon
{
    [System.Serializable]
    public class Wave
    {
        public int waveNumber;
        public List<Agent_Data> enemies;
    }

    [System.Serializable]
    public class Floor
    {
        public int floorNumber;
        public Dictionary<string, CharacterStats> stats;
        public Dictionary<string, ClassSystem> classes;
        public List<Wave> waves;
    }

    [System.Serializable]
    public class Dungeon_Data
    {
        public List<Floor> floors;
    }

    public class DungeonJsonReader : MonoBehaviour
    {
        [SerializeField]
        private TextAsset jsonFile; // Reference to your JSON 
        private Dungeon_Data dungeonData; // Cache the deserialized data

        public void SetJSON(TextAsset _file)
        {
            jsonFile = _file;
            // When the JSON file changes, reset the cached data
            dungeonData = null;
        }

        public Dungeon_Data ReadDungeonData()
        {
            if (dungeonData == null)
            {
                // Read the JSON data from the text asset and deserialize it only if not already done
                string jsonData = jsonFile.text;
                dungeonData = JsonConvert.DeserializeObject<Dungeon_Data>(jsonData);
            }

            // Return the cached data
            return dungeonData;
        }

        public Floor ReadFloorData(int floorNum)
        {
            Dungeon_Data dungeonData = ReadDungeonData();

            if (dungeonData != null && floorNum >= 0 && floorNum < dungeonData.floors.Count)
            {
                Floor floor = dungeonData.floors[floorNum];

                if (floor.stats == null)
                {
                    // Stats dictionary is missing, create an empty one
                    floor.stats = new Dictionary<string, CharacterStats>();
                }
                else
                {
                    // Add debug log to check the stats dictionary size
                    Debug.Log("Stats Dictionary Size for Floor " + floor.floorNumber + ": " + floor.stats.Count);
                }

                return floor;
            }
            else
            {
                Debug.LogError("Floor Number is out of range or dungeon data is invalid.");
                return null;
            }
        }

        public List<Wave> ReadWaveData(int floorNum)
        {
            Dungeon_Data dungeonData = ReadDungeonData();

            if (dungeonData != null)
            {
                Floor floor = dungeonData.floors[floorNum];
                //Debug.Log("Floor Number: " + floor.floorNumber);
                //Debug.Log("Floor: " + floor);

                foreach (Wave wave in floor.waves)
                {
                    foreach (Agent_Data agentData in wave.enemies)
                    {

                        //Code For Assigning the Character Stat Values
                        if (floor.stats != null)
                        {
                            // Get the unit type from the "unitType" property
                            string unitType = agentData.unitType;

                            // Check if the unitType exists in the stats dictionary
                            if (floor.stats.ContainsKey(unitType))
                            {
                                CharacterStats stats = new CharacterStats(floor.stats[unitType]);
                                agentData.SetAgentStats(stats);
                                agentData.SetEnemyAgent(unitType);
                            }
                            else
                            {
                                Debug.LogError("CharacterStats not found for unit: " + unitType);
                            }
                        }
                        else
                        {
                            Debug.LogError("Stats dictionary is null for floor: " + floor.floorNumber);
                        }

                        //Code For Assigning the Character Classes
                        if(floor.classes != null)
                        {
                            // Get the unit type from the "unitType" property
                            string unitClass = agentData.unitClass;
                            // Check if the unitType exists in the stats dictionary
                            if (floor.classes.ContainsKey(unitClass))
                            {
                                ClassSystem _unitClass = new ClassSystem(floor.classes[unitClass]);
                                foreach(string abilityRef in _unitClass.Abilities)
                                {
                                    Ability abilityToAdd = GetAbility(unitClass, abilityRef);
                                    _unitClass.AddAbility(abilityToAdd);
                                }

                                agentData.SetAgentClass(_unitClass);
                            }
                            else
                            {
                                Debug.LogError("Class not found for: " + unitClass);
                            }
                        }
                        else
                        {
                            Debug.LogError("Classes dictionary is null for floor: " + floor.floorNumber);
                        }

                    }
                }


                return floor.waves;
            }
            else
            {
                Debug.LogError("Invalid floor number or dungeon data is null.");
                return null;
            }
        }

        public bool ContainsUnitType(string unitType)
        {
            Dungeon_Data dungeonData = ReadDungeonData();

            foreach (Floor floor in dungeonData.floors)
            {
                if (floor.stats.ContainsKey(unitType))
                {
                    return true;
                }
            }

            return false;
        }

        public BaseStats GetBaseEnemyType(string unitType)
        {
            string unitTypePath = "Base Enemies/" + unitType;
            BaseStats baseStats = Resources.Load<BaseStats>(unitTypePath);
            if (baseStats == null)
            {
                Debug.LogError($"Cannot find enemy type: {unitType}");
            }
            return baseStats;
        }

        public Ability GetAbility(string unitClass, string abilityName)
        {
            string classRef = Regex.Replace(unitClass, @"\d+", "");   //Remove all numbers in string eg. Warrior2,3,4,etc.
            string unitClassPath = ($"Classes/{classRef}/{abilityName}");
            Debug.Log(unitClassPath);
            Ability _ability = Resources.Load<Ability>(unitClassPath);
            if (_ability == null)
            {
                Debug.LogError($"Cannot find {unitClass} ability: {abilityName}");
                return null;
            }

            return _ability;
        }
    }
}
