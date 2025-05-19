using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get the Inventory component from the Player GameObject
            Inventory inventory = collision.gameObject.GetComponent<Inventory>();
            if (inventory != null)
            {
                // Set the key status to true when the player picks up the key
                inventory.SetChestKeyStatus(true); // or SetDoorKeyStatus(true) for a door key
                Destroy(gameObject); // Destroy the key GameObject
            }
            else
            {
                Debug.LogError("Inventory component not found on the player GameObject.");
            }
        }
    }
}
