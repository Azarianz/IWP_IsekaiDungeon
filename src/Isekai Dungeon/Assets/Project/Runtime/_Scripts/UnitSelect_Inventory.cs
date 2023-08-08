using AI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IsekaiDungeon
{
    public class UnitSelect_Inventory : MonoBehaviour
    {
        private Agent_Data unit_data;

        public GameObject stars;
        public TextMeshProUGUI levelText, nameText;
        public Image icon;

        public Agent_Data GetUnitData() { return unit_data; }
        public void SetUnitData(Agent_Data data) 
        { 
            unit_data = data;

            icon.sprite = unit_data.unit_icon;

            string _unitName = unit_data.unit_Name;       
            nameText.text = _unitName.Substring(0, Mathf.Min(5, _unitName.Length)); ;
            
            levelText.text = "LV " + unit_data.GetAgentLevel().ToString();
            int star_rating = unit_data.awaken_level;

            for (int s = 0; s < 5; s++)
            {
                if (s < star_rating)
                {
                    //Active Stars
                    stars.transform.GetChild(s).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    //Nonactive stars
                    stars.transform.GetChild(s).GetChild(0).gameObject.SetActive(false);
                }
            }

            Button _button = GetComponent<Button>();
            _button.onClick.AddListener(() => DisplayUnitStats(data));
        }

        void DisplayUnitStats(Agent_Data data)
        {
            Unit_Viewer unitstat_display = GetComponentInParent<Unit_Viewer>();
            unitstat_display.unitstat_window.SetActive(true);
            unitstat_display.QuickDisplay(data);
        }
    }
}
