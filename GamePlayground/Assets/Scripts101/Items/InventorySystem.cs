using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySystem : MonoBehaviour
{
    private List<GameObject> inventory = new List<GameObject>();
    //private InventoryItem equippedItem;

    public void AddToInventory(GameObject lootItem)
    {
        inventory.Add(lootItem);
        lootItem.SetActive(false); // Disable in scene
    }

    public void UseItem(int index)
    {
        if (index >= 0 && index < inventory.Count)
        {
            GameObject item = inventory[index];
            // Use inventory item - equip or consume
        }
    }

    public void EquipItem()
    {

    }

    public void UnequipItem()
    {

    }

    //public void RemoveItemFromInventory(InventoryItem item)
    //{
    //    if(inventory.Contains(item))
    //    {
    //        inventory.Remove(item);
    //    }
    //}
    //}
}
