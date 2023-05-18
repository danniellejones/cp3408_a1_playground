using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : InventoryItem
{
    public int healthRestoreAmount;

    public override void Use()
    {
        base.Use();

        // Perform health restore

        // Destroy object and update inventory count

    }
}
