using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for both weapons and consumable items

public class InventoryItem : MonoBehaviour
{
    // Common properities to all items
    public string displayName;
    public Sprite icon;
    public GameObject prefab;

   public virtual void Use()
    {

    }
}
