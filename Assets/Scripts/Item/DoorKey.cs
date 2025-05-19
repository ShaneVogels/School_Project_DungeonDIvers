using UnityEngine;

public class DoorKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Update the player's inventory to indicate that they have the door key
            Inventory.GetInstance().SetDoorKeyStatus(true);
            // Destroy the key GameObject
            Destroy(gameObject);
        }
    }
}
