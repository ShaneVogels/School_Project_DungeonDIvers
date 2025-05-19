using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Singleton instance
    public static Inventory Instance { get; private set; }

    // Static variables to persist key statuses across scenes
    private static bool hasChestKey = false;
    private static bool hasDoorKey = false;

    public GameObject chestKeyIcon;
    public GameObject doorKeyIcon;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of Inventory exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //gameObject.SetActive(false);
            return;
        }

        // Ensure key icons reflect the current static key statuses
        chestKeyIcon.SetActive(hasChestKey);
        doorKeyIcon.SetActive(hasDoorKey);
    }

    // Method to check if the player has the chest key
    public bool HasChestKey()
    {
        return hasChestKey;
    }

    // Method to check if the player has the door key
    public bool HasDoorKey()
    {
        return hasDoorKey;
    }

    // Method to set the chest key status
    public void SetChestKeyStatus(bool status)
    {
        hasChestKey = status;
        chestKeyIcon.SetActive(status); // Show/hide chest key icon
    }

    // Method to set the door key status
    public void SetDoorKeyStatus(bool status)
    {
        hasDoorKey = status;
        doorKeyIcon.SetActive(status); // Show/hide door key icon
    }

    // Static method to get the Inventory instance
    public static Inventory GetInstance()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<Inventory>();
            if (Instance == null)
            {
                GameObject go = new GameObject("Inventory");
                Instance = go.AddComponent<Inventory>();
            }
        }
        return Instance;
    }
}
