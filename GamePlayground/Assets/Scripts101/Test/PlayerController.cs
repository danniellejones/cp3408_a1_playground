using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float interactRange = 5f;
    private InventorySystem inventory;

    private void Start()
    {
        inventory = FindObjectOfType<InventorySystem>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement.Normalize(); // Normalize diagonal movement

        transform.Translate(movement * movementSpeed * Time.deltaTime);

        // Pick up loot
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUpLoot();
        }

        // Go back an inventory item
        if (Input.GetKeyDown(KeyCode.Less))
        {
            inventory.ChangeItem(false);
            Debug.Log("Change Item Back.");
        }

        // Use inventory item
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.UseItem();
            Debug.Log("Use Item.");
        }

        // Go forward an inventory item
        if (Input.GetKeyDown(KeyCode.Greater))
        {
            inventory.ChangeItem(true);
            Debug.Log("Change Item Forward.");
        }
    }

    private void TryPickUpLoot()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Loot"))
            {
                LootItem lootItem = collider.GetComponent<LootItem>();
                if (lootItem != null && !lootItem.IsClaimed())
                {
                    lootItem.SetClaimed();
                    Debug.Log("Loot item picked up!");

                    InventoryItem inventoryItem = collider.GetComponent<InventoryItem>();
                    if (inventoryItem != null)
                    {
                        inventory.AddToInventory(inventoryItem);
                    }
                    break; // Stop checking other loot items
                }
            }
        }
    }
}