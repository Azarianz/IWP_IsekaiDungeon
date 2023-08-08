using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Inventory_Instance;

    [SerializeField]
    private List<InventoryItem> item_inventory = new List<InventoryItem>();
    [SerializeField]
    private List<Agent_Data> unit_inventory = new List<Agent_Data>();
    public int maxItemSlot = 20, maxUnitSlot = 30;
    public int gold, science, stamina, maxStamina = 20, day = 1;

    public List<InventoryItem> GetItemInventory() {  return item_inventory; }
    public List<Agent_Data> GetUnitInventory() { return unit_inventory; }

    #region Item_Inventory

    private void Start()
    {
        if (Inventory_Instance != null & Inventory_Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Inventory_Instance = this;
            DontDestroyOnLoad(this);
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

    #region Add_Currencies (Gold, Science, Stamina, Day)
    public bool AddGold(int reward)
    {
        int finalVal = gold + reward;
        if (finalVal >= 0)
        {
            gold = finalVal;
            return true;
        }
        return false;
    }

    public bool AddScience(int cost)
    {
        int finalVal = science + cost;
        if (finalVal >= 0)
        {
            science = finalVal;
            return true;
        }
        return false;
    }

    public bool AddStamina(int val)
    {
        int finalVal = stamina + val;
        if (finalVal >= 0)
        {
            stamina = finalVal;
            return true;
        }

        return false;
    }

    public void NextDay()
    {
        day++;
        stamina = maxStamina;
    }
    #endregion

}
