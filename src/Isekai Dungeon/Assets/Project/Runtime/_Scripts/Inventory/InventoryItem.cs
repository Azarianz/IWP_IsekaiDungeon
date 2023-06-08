using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData item_data;

    public InventoryItem(ItemData item)
    {
        item_data = item;
    }

}