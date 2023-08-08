using AI;
using AI.STATS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IsekaiDungeon
{
    public class ShopRecruit : MonoBehaviour
    {
        public FantasyNameGenerator nameGenerator { get { return GetComponent<FantasyNameGenerator>(); } }  
        public List<BaseStats> races;
        public RuntimeAnimatorController[] animations;

        public GameObject listing, slotPrefab, priceBtn_info, recruitBtn_Text, recruitBtn;
        public TextMeshProUGUI priceBtn_Text, refreshTxt;

        public Image RefreshImage, GoldRefreshCostImage;

        public int maxStar = 2, maxLevel = 10;

        public int refreshCount = 5, maxRefresh = 5, refreshCost = 50;

        bool autoRefresh = false;

        public void RefreshListing()
        {
            if (autoRefresh)
                return;

            // Clear previous listing items
            foreach (Transform child in listing.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < 4; i++)
            {
                int randomRaceIndex = Random.Range(0, races.Count);
                BaseStats randomRace = races[randomRaceIndex];

                int randLevel = Random.Range(1, maxLevel);
                int randStar = Random.Range(0, maxStar);

                Agent_Data agent_data = new Agent_Data(randomRace, randLevel, randStar);
                agent_data.unit_Name = nameGenerator.GenerateRandomName(agent_data.GetAgentStats().UNIT_RACE);

                Agent_AI agent = new Agent_AI(agent_data);
                GameObject go = Instantiate(slotPrefab, listing.transform);

                RecruitCharacter recruitCharacter = go.GetComponent<RecruitCharacter>();
                recruitCharacter.Init(agent, i + 1, randStar);

                go.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => { UpdatePrice(); });
            }

            recruitBtn_Text.SetActive(true);
            priceBtn_info.SetActive(false);
            priceBtn_Text.text = "" + 0;

            autoRefresh = true;
        }

        public void ManualRefresh()
        {
            int newCount = refreshCount - 1;

            if (newCount >= 0)
            {
                string displayTxt;

                if(newCount == 0)
                {
                    displayTxt = "50";

                    GoldRefreshCostImage.gameObject.SetActive(true);
                    RefreshNewListing(displayTxt, Color.red);
                }
                else
                {
                    displayTxt = (refreshCount - 1) + "/" + maxRefresh;

                    RefreshNewListing(displayTxt, Color.white);
                }
            }
            else
            {
                GoldRefreshCostImage.gameObject.SetActive(true);

                int endVal = InventoryController.Inventory_Instance.gold - 50;

                if (endVal >= 0)
                {
                    InventoryController.Inventory_Instance.AddGold(-50);
                    string displayTxt = "50";
                    RefreshNewListing(displayTxt, Color.red);
                }
            }
        }

        public void RefreshNewListing(string text, Color color)
        {
            if (refreshCount > 0)
            {
                refreshCount--;
            }

            // Clear previous listing items
            foreach (Transform child in listing.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < 4; i++)
            {
                int randomRaceIndex = Random.Range(0, races.Count);
                BaseStats randomRace = races[randomRaceIndex];

                int randLevel = Random.Range(1, maxLevel);
                int randStar = Random.Range(0, maxStar);

                Agent_Data agent_data = new Agent_Data(randomRace, randLevel, randStar);
                agent_data.unit_Name = nameGenerator.GenerateRandomName(agent_data.GetAgentStats().UNIT_RACE);

                Agent_AI agent = new Agent_AI(agent_data);
                GameObject go = Instantiate(slotPrefab, listing.transform);

                RecruitCharacter recruitCharacter = go.GetComponent<RecruitCharacter>();
                recruitCharacter.Init(agent, i + 1, randStar);

                go.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => { UpdatePrice(); });
            }

            recruitBtn_Text.SetActive(true);
            priceBtn_info.SetActive(false);
            priceBtn_Text.text = "" + 0;

            refreshTxt.text = text;
            refreshTxt.color = color;
        }

        public void UpdatePrice()
        {
            int count = 0, totalPrice = 0;

            for (int i = 0; i < listing.transform.childCount; i++)
            {
                RecruitCharacter recruitCharacter = listing.transform.GetChild(i).GetComponent<RecruitCharacter>();

                if (recruitCharacter.GetComponent<Toggle>().isOn)
                {
                    count++;
                    totalPrice += recruitCharacter.price;
                }
            }

            if (count == 0)
            {
                recruitBtn_Text.SetActive(true);
                priceBtn_info.SetActive(false);
                return;
            }

            else
            {
                priceBtn_info.SetActive(true);
                recruitBtn_Text.SetActive(false);
                priceBtn_Text.text = totalPrice.ToString();

                if (totalPrice > InventoryController.Inventory_Instance.gold)
                {
                    recruitBtn.GetComponent<Button>().interactable = false;
                }
                else
                {
                    recruitBtn.GetComponent<Button>().interactable = true;
                }
            }
        }

        public List<RecruitCharacter> GetSelectedUnit()
        {
            List<RecruitCharacter> selectedUnit = new List<RecruitCharacter>();

            for (int i = 0; i < listing.transform.childCount; i++)
            {
                RecruitCharacter recruitCharacter = listing.transform.GetChild(i).GetComponent<RecruitCharacter>();

                if (recruitCharacter.GetComponent<Toggle>().isOn)
                {
                    selectedUnit.Add(recruitCharacter);
                }
            }

            return selectedUnit;
        }

        public int CalculatePrice()
        {
            List<RecruitCharacter> selectedUnit = GetSelectedUnit();
            int totalPrice = 0;

            foreach(RecruitCharacter character in selectedUnit)
            {
                totalPrice += character.price;
            }

            return -totalPrice;
        }

        public void BuyUnits()
        {
            InventoryController instance = InventoryController.Inventory_Instance;
            int currentGold = instance.gold;
            bool isCapped = instance.GetUnitInventory().Count >= instance.maxUnitSlot;

            if (CalculatePrice() <= currentGold && !isCapped)
            {
                instance.AddGold(CalculatePrice());

                List<RecruitCharacter> selectedUnit = GetSelectedUnit();
                foreach (RecruitCharacter character in selectedUnit)
                {
                    instance.AddUnit(character.agent_data);
                    character.GetComponent<Toggle>().interactable = false;
                    character.GetComponent<Toggle>().isOn = false;
                    character.recruitedText.gameObject.SetActive(true);
                }
            }
        }
    }
}
