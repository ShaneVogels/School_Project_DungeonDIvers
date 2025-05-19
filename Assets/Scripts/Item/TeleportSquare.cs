using UnityEngine;

public class TeleportSquare : MonoBehaviour
{
    public Transform teleportTarget; // The target location to teleport the player to

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportTarget.position;
        }
    }
}
