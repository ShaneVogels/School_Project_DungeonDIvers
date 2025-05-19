using UnityEngine;

public class DoorWithKey : MonoBehaviour
{
    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            TryOpenDoor();
        }
    }

    private void TryOpenDoor()
    {
        if (Inventory.Instance.HasDoorKey())
        {
            OpenDoor();

        }
        else
        {
            Debug.Log("You need a key to open this door.");
            // Optionally, you can provide feedback to the player indicating they need a key.
        }
    }

    private void OpenDoor()
    {
        // Delete the door GameObject when the player opens it
        Inventory.GetInstance().SetDoorKeyStatus(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
