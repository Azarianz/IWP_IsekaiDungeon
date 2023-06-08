using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int maxWave = 3, currentWave = 0;
    public List<Flock> waves;

    public static DungeonManager instance;

    public TextMeshProUGUI waveText;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentWave = 0;

        Debug.Log("Wave " + currentWave + " : " + waves[currentWave].IsFlockDead());
        waveText.text = "Wave " + currentWave + 1;
    }

    void Update()
    {
        if (waves[currentWave].IsFlockDead() && currentWave < maxWave)
        {
            ClearWave();
        }
        else if(currentWave < maxWave)
        {
            Debug.Log("Wave " + currentWave + " : " + waves[currentWave].IsFlockDead());
        }

    }

    public void ClearWave()
    {
        Debug.Log("ClearWave");
        waves[currentWave].gameObject.SetActive(false);

        currentWave++;

        if (currentWave >= maxWave)
        {
            currentWave = maxWave;
            GameManager.Instance.ChangeState(GameState.Win);
        }
        else
        {
            Invoke("SpawnWave", 2f);
        }
    }

    public void SpawnWave()
    {
        Debug.Log("SpawnWave");
        waves[currentWave].gameObject.SetActive(true);
        waveText.text = "Wave " + currentWave + 1;
    }
}
