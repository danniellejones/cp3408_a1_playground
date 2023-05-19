using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to weapons
public class WeaponItem : InventoryItem
{
    public int damage;

    // Equip the weapon
    public override void Use()
    {
        base.Use();
        Debug.Log("Equipping Weapon.");
        InventorySystem inventorySystem = FindObjectOfType<InventorySystem>();

        // Equip this weapon
        inventorySystem.EquipWeapon(this);
    }
}
