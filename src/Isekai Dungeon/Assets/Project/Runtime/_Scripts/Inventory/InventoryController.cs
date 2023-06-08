using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Inventory_Instance;

    private List<InventoryItem> item_inventory = new List<InventoryItem>();
    private List<Agent_Data> unit_inventory = new List<Agent_Data>();
    public int maxItemSlot = 20, maxUnitSlot = 30;
    private int gold = 0, science = 0;

    #region Item_Inventory

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Inventory_Instance != null & Inventory_Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Inventory_Instance = this; 
        }
    }

    public bool AddItem(ItemData itemData)
    {
        if(item_inventory.Count < maxItemSlot)
        {
            //Non-Stack Model
            InventoryItem newItem = new InventoryItem(itemData);
            item_inventory.Add(newItem);
            Debug.LogFormat("Item {0} Added!", newItem.item_data.name);
            return true;
        }

        Debug.Log("Item Inventory is full! Cannot add more items.");
        return false;
    }

    public bool RemoveItem(ItemData itemData)
    {
        if (item_inventory.Count <= 0)
            return false;

        //Non-Stack Model
        InventoryItem itemToRemove = item_inventory.Find(item => item.item_data.name == itemData.name);
        if (itemToRemove != null)
        {
            item_inventory.Remove(itemToRemove);
            Debug.LogFormat("Item {0} Removed!", itemToRemove.item_data.name);
            return true;
        }

        Debug.LogFormat("Item {0} does not exist!", itemToRemove.item_data.name);
        return false;
    }

    public List<InventoryItem> GetItemInventory()
    {
        return item_inventory;
    }

    public List<Agent_Data> GetUnitInventory()
    {
        return unit_inventory;
    }

    #endregion

    #region Unit_Inventory

    public bool AddUnit(Agent_Data unitData)
    {
        if (unit_inventory.Count < maxUnitSlot)
        {
            //Non-Stack Model
            unit_inventory.Add(unitData);
            return true;
        }
        return false;
    }

    public bool RemoveUnit(Agent_Data unitData)
    {
        if (unit_inventory.Count <= 0)
            return false;

        //Non-Stack Model
        Agent_Data unitToRemove = unit_inventory.Find(unit => unit.unitID == unitData.unitID);
        if (unitToRemove != null)
        {
            unit_inventory.Remove(unitToRemove);
            return true;
        }

        return false;
    }

    public bool RemoveUnit(int index)
    {
        if (unit_inventory.Count <= 0)
            return false;

        //Non-Stack Model
        Agent_Data unitToRemove = unit_inventory[index];
        if (unitToRemove != null)
        {
            unit_inventory.Remove(unitToRemove);
            return true;
        }

        return false;
    }

    #endregion

    public void RewardGold(int reward)
    {
        gold += reward;
    }

    public bool DeductGold(int cost)
    {
        if(gold >= cost)
        {
            gold -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeductScience(int cost)
    {
        science -= cost;
    }


}
