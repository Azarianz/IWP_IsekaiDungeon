using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using AI;

namespace IsekaiDungeon
{
    public class TestUI : MonoBehaviour
    {
        public TextMeshProUGUI gameStateText, inventorySize;
        public Button StartBtn;

        //Win Canvas
        public TextMeshProUGUI killCountTxt, deathCountTxt, GoldTxt, xpTxt;

        public TextMeshProUGUI speedTxt;

        public GameObject gameCanvas, loseCanvas, winCanvas, setupCanvas, setupGrid, statisticCanvas;

        public GameObject playerTeam, inventoryContent;

        public List<GameObject> grids;

        public Placement_Inventory inventory;

        private bool squadRewardGiven = false;

        // Update is called once per frame
        void Update()
        {
            gameStateText.text = GameManager.Instance.State.ToString();

            inventorySize.text = inventoryContent.transform.childCount.ToString() + " / 30";

            if(GameManager.Instance.State == GameState.PlayerSetup)
            {
                if(playerTeam.transform.childCount > 0)
                {
                    StartBtn.interactable = true;
                }
                else
                {
                    StartBtn.interactable = false;
                }
            }

            if (GameManager.Instance.State == GameState.GameStart)
            {
                setupCanvas.SetActive(false);
                setupGrid.SetActive(false);
                StartBtn.gameObject.SetActive(false);
            }
            if (GameManager.Instance.State == GameState.Lose)
            {
                Time.timeScale = 1.0f;
                gameCanvas.SetActive(false);
                loseCanvas.SetActive(true);
                statisticCanvas.SetActive(true);
                killCountTxt.text = DungeonManager.instance.KillCount.ToString() + " KILLS";
                deathCountTxt.text = DungeonManager.instance.DeathCount.ToString() + " DEATHS";
                GoldTxt.text = DungeonManager.instance.goldEarned.ToString() + " GOLD";
                xpTxt.text = DungeonManager.instance.expEarned.ToString() + " XP";
            }
            else if (GameManager.Instance.State == GameState.Win)
            {
                SquadClearReward();

                Time.timeScale = 1.0f;
                gameCanvas.SetActive(false);
                winCanvas.SetActive(true);
                statisticCanvas.SetActive(true);
                killCountTxt.text = DungeonManager.instance.KillCount.ToString() + " KILLS";
                deathCountTxt.text = DungeonManager.instance.DeathCount.ToString() + " DEATHS";
                GoldTxt.text = DungeonManager.instance.goldEarned.ToString() + " GOLD";
                xpTxt.text = DungeonManager.instance.expEarned.ToString() + " XP";
            }

        }

        public void ChangeScene(string nextScene)
        {
            SceneManager.LoadScene(nextScene);
        }

        public void ResetFormation()
        {
            foreach(GameObject grid in grids)
            {
                grid.GetComponent<Image>().color = Color.white;
                grid.SetActive(true);
            }

            foreach(Transform unit in playerTeam.transform)
            {
                Destroy(unit.gameObject);
            }

            inventory.RefreshUnitList();
        }

        public void SquadClearReward()
        {
            if (squadRewardGiven)
                return;

            List<Agent_AI> survivingFlock = playerTeam.GetComponent<Flock>().GetAgents;

            int modifierXP = (PlayerManager.instance.PROGRESSION_DUNGEON_CURRENTFLOOR + 9) / 10;
            int rewardXP = modifierXP * 100;

            foreach (Agent_AI agent in survivingFlock)
            {
                agent.AgentData.GetLevelSystem().AddExperience(rewardXP);
            }

            Debug.Log("FLOOR CLEAR EXP REWARD: " + rewardXP);

            squadRewardGiven = true;
        }

        public void SpeedUpTime()
        {
            switch(Time.timeScale)
            {
                case 1:
                    Time.timeScale = 2.0f;
                    speedTxt.text = "2X";
                    break;
                case 2:
                    Time.timeScale = 2.5f;
                    speedTxt.text = "2.5X";
                    break;
                case 2.5f:
                    Time.timeScale = 1.0f;
                    speedTxt.text = "1X";
                    break;
                default: 
                    break;
            } 
            
        }

        public void Retreat()
        {
            //TODO
            Time.timeScale = 0.0f;
            gameCanvas.SetActive(false);
            loseCanvas.SetActive(true);
            statisticCanvas.SetActive(true);
            killCountTxt.text = DungeonManager.instance.KillCount.ToString() + " KILLS";
            deathCountTxt.text = DungeonManager.instance.DeathCount.ToString() + " DEATHS";
            GoldTxt.text = DungeonManager.instance.goldEarned.ToString() + " GOLD";
            xpTxt.text = DungeonManager.instance.expEarned.ToString() + " XP";
        }

    }
}
