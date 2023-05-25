using TMPro;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float interactRange = 5f;
    private InventorySystem inventory;
    WorldMusicPlayer worldMusicPlayer;

    private void Awake()
    {
        worldMusicPlayer = FindObjectOfType<WorldMusicPlayer>();
    }

    private void Start()
    {
        inventory = FindObjectOfType<InventorySystem>();
        //inventory.inventoryText = FindObjectOfType<TMP_Text>();
        
        worldMusicPlayer.SetCharacterState(WorldMusicPlayer.CharacterState.Idle);
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.ChangeItem(false);
            Debug.Log("Change Item Back.");
        }

        // Use inventory item
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.UseItem();
            Debug.Log("Use Item.");

        }

        // Go forward an inventory item
        if (Input.GetKeyDown(KeyCode.Alpha3))
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
                    //Debug.Log("Loot item claimed.");
                    Debug.Log(lootItem.ToString() + " claimed.");

                    //InventoryItem inventoryItem = collider.GetComponent<InventoryItem>();
                    //if (inventoryItem != null)
                    //{
                        inventory.AddToInventory(collider.gameObject);
                        //Debug.Log(inventoryItem.ToString() + " picked up.");
                        Debug.Log(collider.gameObject.ToString() + " picked up.");
                        //Debug.Log("Loot item picked up!");
                    //}
                    break; // Stop checking other loot items
                }
            }
        }
    }
}