using AI;
using AI.STATS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuildSystem : MonoBehaviour
{
    public TextMeshProUGUI charIndex, charClass, charLevel;
    public List<BaseStats> randomPool;
    public List<ClassSystem> classPool;
    public List<Agent_Data> recruitPool;
    public List<Button> recruitButtons;

    public void Awake()
    {
        foreach (BaseStats stats in randomPool)
        {
            int rand = Random.Range(0, classPool.Count);
            Agent_Data agent = new Agent_Data(stats, classPool[rand]);
            recruitPool.Add(agent);
        }
    }

    public void RefreshRecruit()
    {
        int count = 1;
        foreach(Button button in recruitButtons)
        {
            int rand = Random.Range(0, recruitPool.Count);
            Agent_Data data = recruitPool[rand];
            button.transform.GetChild(0).GetComponent<Image>().sprite = data.unit_icon;
            button.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "CLASS";
            button.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = count.ToString();
            button.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = data.GetAgentLevel().ToString();
            count++;
        }
    }

    public void Recruit(int cost)
    {
        if (InventoryController.Inventory_Instance.DeductGold(cost))
        {
            Debug.Log("Purchase Successful");
        }
        else
        {
            Debug.Log("Not enough gold");
        }
  
    }

}
