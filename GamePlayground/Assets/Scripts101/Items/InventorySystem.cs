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

    // Call on inventory item subclasses to use item selected
    public void UseItem()
    {
        if (currentIndex >= 0 && currentIndex < inventory.Count)
        {
            GameObject itemToUse = inventory[currentIndex];
            InventoryItem itemComponent = itemToUse.GetComponent<InventoryItem>();
            if (itemComponent != null)
            {
                itemComponent.Use();
                Debug.Log("Loot item being used.");
            }
        }
    }

    // Change inventory item
    public void ChangeItem(bool next)
    {
        Debug.Log("Current Index Starts at: " + currentIndex);
        if (inventory.Count > 0)  // Inventory is empty skip change
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
            Debug.Log("Current Index Changed to: " + currentIndex);
            UpdateInventoryText();
        }
    }


    // Update display of inventory item in UI
    private void UpdateInventoryText()
    {
        if (inventoryText != null)
        {
            if (inventory.Count > 0)
            {
                string itemName = inventory[currentIndex].GetComponent<InventoryItem>().displayName;
                inventoryText.text = itemName;
            }
            else
            {
                inventoryText.text = "Empty";
            }
        }
    }

    public void RemoveItemFromInventory(InventoryItem itemToRemove)
    {
        int indexOfItemToRemove = inventory.IndexOf(itemToRemove.gameObject);
        if (indexOfItemToRemove >= 0)
        {
            inventory.RemoveAt(indexOfItemToRemove);
            if (currentIndex >= inventory.Count)
            {
                currentIndex = Mathf.Max(0, inventory.Count - 1);
            }
            Destroy(itemToRemove.gameObject);
            UpdateInventoryText();
        }

    }

    public void EquipWeapon(WeaponItem weapon)
    {
        UnequipWeapon();
        inventory.Remove(weapon.gameObject);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            weapon.transform.SetParent(player.transform, false);  // Set weapon as child of player
            weapon.gameObject.SetActive(true);
        }
        UpdateInventoryText();
    }

    public void UnequipWeapon()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        WeaponItem equippedWeapon = player.GetComponentInChildren<WeaponItem>();
        if (equippedWeapon != null)
        {
            equippedWeapon.transform.SetParent(null, false);
            //InventorySystem inventorySystem = FindObjectOfType<InventorySystem>();
            //inventorySystem.AddToInventory(equippedWeapon.gameObject);
            inventory.Add(equippedWeapon.gameObject);
            equippedWeapon.gameObject.SetActive(false);
            Debug.Log(equippedWeapon.ToString() + " unequipped.");
            UpdateInventoryText();
        }
    }
}
