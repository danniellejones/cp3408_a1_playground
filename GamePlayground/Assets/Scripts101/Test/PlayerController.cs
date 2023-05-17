using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float interactRange = 2f;

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement.Normalize(); // Normalize diagonal movement

        transform.Translate(movement * movementSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUpLoot();
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
                    break; // Stop checking other loot items
                }
            }
        }
    }
}