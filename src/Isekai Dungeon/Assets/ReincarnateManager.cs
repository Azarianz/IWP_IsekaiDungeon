using AI;
using AI.STATS;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IsekaiDungeon
{
    public class ReincarnateManager : MonoBehaviour
    {
        public GameObject listing;
        public List<BaseStats> heroes;
        public TMP_InputField inputFieldName;
        public int startingLevel, startingStar;

        public int GetSelectedHero()
        {
            for (int i = 0; i < listing.transform.childCount; i++)
            {
                Toggle selectedHero = listing.transform.GetChild(i).GetComponent<Toggle>();

                if (selectedHero.isOn)
                {
                    return i;
                }
            }

            return -1;
        }

        public void OnChooseHero()
        {
            if (inputFieldName.text != null)
            {
                Agent_Data agent_data = new Agent_Data(heroes[GetSelectedHero()], startingLevel, startingStar);
                agent_data.unit_Name = inputFieldName.text;

                InventoryController.Inventory_Instance.AddUnit(agent_data);
                OnPressedPlay();
            }
        }

        //PLAY BUTTON
        public void OnPressedPlay()
        {
            SceneManager.LoadScene("Game_Town2.5D");
        }

    }
}
