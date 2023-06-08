using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer;
using AI;
using AI.STATS;

public class InventoryView : MonoBehaviour
{
    public GameObject item_content, unit_content, itemPrefab, unitPrefab;
    public List<GameObject> item_list, unit_list;

    [SerializeField]    //For Debugging
    private List<InventoryItem> itemInventory;

    [SerializeField]
    private List<Agent_Data> unitInventory;

    [SerializeField]    //For Debugging
    private InventoryController inventoryController;

    public InventoryItem debugItem1, debugItem2;
    public BaseStats debugStat1, debugStat2;

    private void Start()
    {
        inventoryController = InventoryController.Inventory_Instance;
        itemInventory = inventoryController.GetItemInventory();

        int childCount = item_content.transform.childCount;
        item_list.Clear();
        for(int i = 0; i < childCount; i++)
        {
            GameObject childTransform = item_content.transform.GetChild(i).gameObject;
            item_list.Add(childTransform);
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            inventoryController.AddItem(debugItem1.item_data);
            if (item_content.activeSelf)
            {
                RefreshItemList();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            inventoryController.AddItem(debugItem2.item_data);
            if (item_content.activeSelf)
            {
                RefreshItemList();
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            inventoryController.RemoveItem(debugItem1.item_data);

            if (item_content.activeSelf)
            {
                RefreshItemList();
            }
        }

        //if (Input.GetKeyUp(KeyCode.Alpha4))
        //{
        //    Agent_Data unitToAdd = new Agent_Data(debugStat1);
        //    inventoryController.AddUnit(unitToAdd);
        //    if (item_content.activeSelf)
        //    {
        //        RefreshUnitList();
        //    }
        //}
        //if (Input.GetKeyUp(KeyCode.Alpha5))
        //{
        //    Agent_Data unitToAdd = new Agent_Data(debugStat2);
        //    inventoryController.AddUnit(unitToAdd);
        //    if (item_content.activeSelf)
        //    {
        //        RefreshUnitList();
        //    }
        //}
        //if (Input.GetKeyUp(KeyCode.Alpha6))
        //{
        //    inventoryController.RemoveUnit(0);

        //    if (item_content.activeSelf)
        //    {
        //        RefreshUnitList();
        //    }
        //}
    }

    private void UpdateInventoryView()
    {
        itemInventory = inventoryController.GetItemInventory();
    }

    private void UpdateUnitView()
    {
        unitInventory = inventoryController.GetUnitInventory();
    }

    public void RefreshItemList()
    {
        UpdateInventoryView();
        StartCoroutine(ViewItemList());
    }

    IEnumerator ViewItemList()
    {
        yield return new WaitForEndOfFrame(); // Wait for the end of the frame to ensure proper UI update

        Debug.Log(item_list.Count);

        int size = item_list.Count;
        for (int i = 0; i < size; i++)
        {
            if (i < itemInventory.Count && itemInventory[i] != null)
            {
                item_list[i].transform.GetChild(0).gameObject.SetActive(true);
                item_list[i].transform.GetChild(0).gameObject.GetComponentInChildren<Image>().sprite = itemInventory[i].item_data.icon;
                item_list[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
            }
            else
            {
                item_list[i].transform.GetChild(0).gameObject.SetActive(false);
                item_list[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
            }
        }

        // Disable remaining item buttons
        for (int i = itemInventory.Count; i < size; i++)
        {
            item_list[i].transform.GetChild(0).gameObject.SetActive(false);
            item_list[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
        }
    }

    public void RefreshUnitList()
    {
        UpdateUnitView();
        StartCoroutine(ViewUnitList());
    }

    IEnumerator ViewUnitList()
    {
        yield return new WaitForEndOfFrame(); // Wait for the end of the frame to ensure proper UI update

        Debug.Log(item_list.Count);

        foreach(GameObject unit in unit_list)
        {
            Destroy(unit);
        }
        unit_list.Clear();

        for (int i = 0; i < unitInventory.Count; i++)
        {
            GameObject go = Instantiate(unitPrefab, unit_content.transform);
            go.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
            go.transform.GetChild(0).GetComponent<Image>().sprite = unitInventory[i].unit_icon;
            unit_list.Add(go);
        }
    }

}
