using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AI;
using JetBrains.Annotations;

namespace IsekaiDungeon
{
    public class Placement_Inventory : MonoBehaviour
    {
        public GameObject unit_content, unitPrefab;
        public List<GameObject> unit_list;

        [SerializeField]
        private List<Agent_Data> unitInventory;

        [SerializeField]    //For Debugging
        private InventoryController inventoryController;

        public Unit_Viewer unitViewerScript;

        // Start is called before the first frame update
        void Start()
        {
            inventoryController = InventoryController.Inventory_Instance;

            RefreshUnitList();
        }

        private void UpdateUnitView()
        {
            unitInventory = inventoryController.GetUnitInventory();
        }

        public void RefreshUnitList()
        {
            UpdateUnitView();
            StartCoroutine(ViewUnitList());
        }

        IEnumerator ViewUnitList()
        {
            yield return new WaitForEndOfFrame(); // Wait for the end of the frame to ensure proper UI update

            foreach (GameObject unit in unit_list)
            {
                Destroy(unit);
            }
            unit_list.Clear();

            for (int i = 0; i < unitInventory.Count; i++)
            {
                Agent_Data data = unitInventory[i];

                GameObject go = Instantiate(unitPrefab, unit_content.transform);
                go.GetComponent<UnitSelect>().unit_data = unitInventory[i];

                go.transform.Find("CharacterIcon").GetComponent<Image>().sprite = unitInventory[i].unit_icon;
                string _unitName = unitInventory[i].unit_Name;
                go.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = _unitName.Substring(0, Mathf.Min(5, _unitName.Length));
                go.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = "LV " + unitInventory[i].GetAgentLevel().ToString();
                go.transform.Find("SlotIndex").GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();

                Button _button = go.transform.Find("InfoBtn").GetComponent<Button>();

                // Capture the correct value of 'i' in the lambda expression
                _button.onClick.AddListener(() => DisplayUnitStats(data));

                GameObject stars = go.transform.Find("Stars").gameObject;
                int star_rating = unitInventory[i].awaken_level;
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

                unit_list.Add(go);
            }
        }

        void DisplayUnitStats(Agent_Data data)
        {
            //Debug.Log("UNITSTAT_DISPLAY: " + unitViewerScript);
            unitViewerScript.unitstat_window.SetActive(true);
            unitViewerScript.QuickDisplay(data);
            Debug.Log("DATA: " + data);
        }
    }
}
