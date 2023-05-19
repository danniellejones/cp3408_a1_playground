using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//[System.Serializable]

public class InventorySystem : MonoBehaviour
{
    //[SerializeField]
    public List<GameObject> inventory;

    // Display and navigate inventory items
    private int currentIndex = 0;
    public TMP_Text inventoryText;

    private void Awake()
    {
        inventory = new List<GameObject>();
    }

    public void AddToInventory(GameObject inventoryItem)
    {
        Debug.Log(inventoryItem.ToString() + "Just before add.");
        //InventoryItem itemComponent = inventoryItem.GetComponent<InventoryItem>();
        //if (itemComponent != null)
        if (inventoryItem != null)
        {
            inventory.Add(inventoryItem);
            Debug.Log(inventoryItem + "Loot item added to inventory.");
            //lootItem.gameObject.SetActive(false); // Disable in scene
            UpdateInventoryText();
        }
    }

    public void UseItem()
    {
        //if (currentIndex >= 0 && currentIndex < inventory.Count)
        //{
        //    InventoryItem item = inventory[currentIndex];
        //    InventoryItem itemComponent = item.GetComponent<InventoryItem>();
        //    if (itemComponent != null)
        //    {
        //        itemComponent.Use();
        //        Debug.Log("Loot item being used.");
        //        //RemoveItemFromInventory(item);
        //    }
        //}
    }

    // Change inventory item
    public void ChangeItem(bool next)
    {
        if (next)
        {
            currentIndex++;
            if (currentIndex >= inventory.Count)
                currentIndex = 0;
        }
        else
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = inventory.Count - 1;
        }
        UpdateInventoryText();
    }


    // Display inventory
    private void UpdateInventoryText()
    {
        if (inventoryText != null)
        {
            if (inventory.Count > 0)
            {
                string itemName = inventory[currentIndex].GetComponent<InventoryItem>().displayName;
                inventoryText.text = itemName;
            }
        }
    }


    public void RemoveItemFromInventory(InventoryItem itemToRemove)
    {
        inventory.RemoveAt(currentIndex);
        Destroy(itemToRemove);
        UpdateInventoryText();

    }
}
