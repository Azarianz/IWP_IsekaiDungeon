using AI;
using AI.STATS;
using IsekaiDungeon;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    [SerializeField]
    private int currentFloor = 0, maxWave = 3, currentWave = 1;

    [SerializeField]
    private List<Flock> wave_flocks;

    [SerializeField]
    public List<Wave> waves;

    [SerializeField]
    private TextMeshProUGUI waveText;

    [SerializeField]
    public DungeonJsonReader reader;

    [SerializeField]
    private GameObject enemyPrefab;

    public bool hasDungeonEnded;

    public int DeathCount = 0, KillCount = 0, expEarned = 0, goldEarned = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        currentFloor = PlayerManager.instance.PROGRESSION_DUNGEON_CURRENTFLOOR;
        waveText.text = "Wave " + currentWave;

        string dungeonJSON = "Dungeon_Data";
        reader.SetJSON(Resources.Load<TextAsset>(dungeonJSON));
        Debug.Log("Current Floor Data: " + GetCurrentFloorData());

        if (GetCurrentFloorData())
            SetupDungeon();
    }

    void Update()
    {
        if (!hasDungeonEnded && wave_flocks[currentWave - 1].IsFlockDead())
        {
            if(currentWave == maxWave)
            {
                PlayerManager.instance.PROGRESSION_DUNGEON_CURRENTFLOOR += 1;
                GameManager.Instance.ChangeState(GameState.Win);
                hasDungeonEnded = true;
            }
            else
            {
                ClearWave();
            }

        }
    }

    public void ClearWave()
    {
        Debug.Log("ClearWave");
        wave_flocks[currentWave - 1].enabled = false;

        currentWave++;
        Invoke("SpawnWave", 2f);
    }

    public void SpawnWave()
    {
        Debug.Log("SpawnWave");

        Flock currentFlock = wave_flocks[currentWave - 1];

        currentFlock.Initialize();
        currentFlock.enabled = true;

        waveText.text = "Wave " + (currentWave);
    }

    public void SetupDungeon()
    {
        Debug.Log("SetupDungeon():");

        maxWave = waves.Count;

        for (int i = 0; i < maxWave; i++)
        {
            List<Agent_Data> _wave = waves[i].enemies;

            foreach (Agent_Data enemyData in _wave)
            {
                Vector3 spawnPos = enemyData.position;

                // Instantiate an enemy game object
                GameObject enemyGO = Instantiate(enemyPrefab, wave_flocks[i].transform);
                enemyGO.transform.localPosition = spawnPos;
                enemyGO.GetComponent<Agent_AI>().SetUnitData(enemyData);

                Debug.Log(enemyData.GetAgentStats());
            }
        }
    }

    public bool GetCurrentFloorData()
    {
        waves = reader.ReadWaveData(currentFloor);

        if (waves != null)
        {
            return true;
        }
        else
        {
            Debug.LogError("Current Floor is out of range or dungeon data is invalid.");
            return false;
        }
    }

}
