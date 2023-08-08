using AI;
using AI.STATS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IsekaiDungeon
{
    public class RecruitCharacter : MonoBehaviour
    {
        public Image characterSprite;
        public TextMeshProUGUI nameText, classText, levelText, indexText, recruitedText;
        public GameObject[] unitStar = new GameObject[5];
        public int star, level, price;
        public string className;
        public Agent_Data agent_data;

        public void Init(CharacterStats S , int starLevel)
        {

        }

        public void Init(Agent_AI agent, int index, int starLevel)
        {
            level = agent.AgentData.GetAgentLevel();
            star = agent.AgentData.awaken_level;
            characterSprite.sprite = agent.AgentData.unit_icon;

            nameText.text = agent.AgentData.unit_Name;
            classText.text = agent.AgentData.GetAgentStats().UNIT_RACE + " " + agent.AgentData.GetAgentClass().name;
            levelText.text = "Lvl " + agent.AgentData.GetAgentLevel().ToString();
            indexText.text = index.ToString();

            //Show Stars
            for(int i = 0; i < 5; i++) 
            {
                if(i < star)
                {
                    //Active Stars
                    unitStar[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    //Nonactive stars
                    unitStar[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            price = CalculatePrice();
            agent_data = agent.AgentData;
        }
        
        private int CalculatePrice()
        {
            int calcAmount;

            if (level <= 10)
            {
                calcAmount = (50 * level);
            }
            else if(level <= 15)
            {
                calcAmount = (150 * level);
            }
            else
            {
                calcAmount = (300 * level);
            }

            return calcAmount * (star + 1);
        }
    }
}
