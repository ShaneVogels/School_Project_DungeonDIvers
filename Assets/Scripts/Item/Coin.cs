using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.GetInstance().AddPoints(10);
            }
            else
            {
                Debug.LogError("ScoreManager instance not found.");
            }
            Destroy(gameObject);
        }
    }
}
