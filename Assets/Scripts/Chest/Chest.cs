using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Prefabs for items to drop
    public Transform[] itemSpawnPoints; // Points around the chest to spawn items
    public float itemSpeed = 5f; // Speed at which items move towards the player

    private Animator animator;
    private bool isPlayerInRange = false;
    private Transform playerTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No GameObject tagged as 'Player' found.");
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            TryOpenChest();
        }
    }

    private void TryOpenChest()
    {
        if (Inventory.Instance.HasChestKey())
        {
            OpenChest();
            Inventory.GetInstance().SetChestKeyStatus(false);
        }
        else
        {
            Debug.Log("You need a key to open this chest.");
            // Optionally, you can provide feedback to the player indicating they need a key.
        }
    }

    private void OpenChest()
    {
        // Trigger the chest opening animation
        animator.SetTrigger("Open");

        // Spawn items from the chest
        foreach (Transform spawnPoint in itemSpawnPoints)
        {
            SpawnItem(spawnPoint.position);
        }
    }

    private void SpawnItem(Vector3 spawnPosition)
    {
        if (itemPrefabs.Length > 0)
        {
            GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);

            // Move the item towards the player if playerTransform is not null
            if (playerTransform != null)
            {
                Vector3 direction = (playerTransform.position - spawnPosition).normalized;
                newItem.GetComponent<Rigidbody2D>().velocity = direction * itemSpeed;
            }
        }
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
