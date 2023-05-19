using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//[System.Serializable]

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> inventory;

    // Display and navigate inventory items
    private int currentIndex = 0;
    public TMP_Text inventoryText;

    private void Awake()
    {
        inventory = new List<GameObject>();
    }

    public void AddToInventory(GameObject lootItem)
    {
        InventoryItem itemComponent = lootItem.GetComponent<InventoryItem>();
        if (itemComponent != null)
        {
            inventory.Add(lootItem);
            Debug.Log("Loot item added to inventory.");
            lootItem.SetActive(false); // Disable in scene
            UpdateInventoryText();
        }
    }

    public void UseItem(int index)
    {
        if (currentIndex >= 0 && currentIndex < inventory.Count)
        {
            GameObject item = inventory[index];
            InventoryItem itemComponent = item.GetComponent<InventoryItem>();
            if (itemComponent != null)
            {
                itemComponent.Use();
                Debug.Log("Loot item being used.");
                inventory.RemoveAt(index);
                Destroy(item);
                UpdateInventoryText();
            }
        }
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
    }


    // Display inventory
    private void UpdateInventoryText()
    {
        if (inventoryText != null)
        {
            if (inventory.Count > 0)
            {
                inventoryText.text = inventory[currentIndex].name;
            }
            else
            {
                inventoryText.text = "Inventory Empty";
            }
        }
    }

    //public void SetInventoryText(TMP_Text textComponent)
    //{
    //    inventoryText = textComponent;
    //    UpdateInventoryText();
    //}




    //public void RemoveItemFromInventory(InventoryItem item)
    //{
    //    if(inventory.Contains(item))
    //    {
    //        inventory.Remove(item);
    //    }
    //}
    //}
}
