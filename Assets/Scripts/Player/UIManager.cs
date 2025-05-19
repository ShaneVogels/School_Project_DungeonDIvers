using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Singleton instance
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    Debug.LogError("UIManager instance not found in the scene.");
                }
            }
            return instance;
        }
    }

    public GameObject chestKeyIcon;
    public GameObject doorKeyIcon;

    // Method to remove the chest key icon from the UI
    public void RemoveChestKeyIcon()
    {
        chestKeyIcon.SetActive(false);
    }

    // Method to remove the door key icon from the UI
    public void RemoveDoorKeyIcon()
    {
        doorKeyIcon.SetActive(false);
    }
}
