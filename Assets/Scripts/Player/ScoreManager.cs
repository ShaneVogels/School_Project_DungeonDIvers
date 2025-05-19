using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public ScoreSO scoreSO;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        // Check if an instance of ScoreManager already exists
        if (Instance == null)
        {
            // If not, set the Instance to this instance of ScoreManager
            Instance = this;
            // Load the score from ScoreSO
            LoadScore();
        }
        else
        {
            // If an instance already exists, destroy this game object to maintain a single instance
            //gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        // Update the score text when the game starts
        UpdateScoreText();
    }

    public void AddPoints(int points)
    {
        // Add points to the score and update the UI
        scoreSO.score += points;
        Debug.Log("Added points: " + points + ". New score: " + scoreSO.score);
        UpdateScoreText();
        SaveScore();
    }

    private void UpdateScoreText()
    {
        // Update the score text UI element
        if (scoreText != null)
        {
            scoreText.text = "Score: " + scoreSO.score;
            Debug.Log("Updated score text: " + scoreText.text);
            Debug.Log("TextMeshPro component active: " + scoreText.gameObject.activeSelf);
        }
        else
        {
            Debug.LogError("ScoreText is not assigned in the ScoreManager.");
        }
    }

    private void OnValidate()
    {
        // Update the score text in the editor when a value changes
        UpdateScoreText();
    }

    // Load the score from ScoreSO
    private void LoadScore()
    {
        if (scoreSO != null)
        {
            Debug.Log("Loaded score: " + scoreSO.score);
        }
        else
        {
            Debug.LogError("ScoreSO is not assigned in the ScoreManager.");
        }
    }

    // Save the score to ScoreSO
    private void SaveScore()
    {
        if (scoreSO != null)
        {
            // Assuming ScoreSO is serialized, you don't need to do anything special here
            // The data in ScoreSO will persist as it is part of the project assets
            Debug.Log("Saved score: " + scoreSO.score);
        }
        else
        {
            Debug.LogError("ScoreSO is not assigned in the ScoreManager.");
        }
    }

    // Static method to get the ScoreManager instance
    public static ScoreManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<ScoreManager>();
            if (Instance == null)
            {
                GameObject go = new GameObject("ScoreManager");
                Instance = go.AddComponent<ScoreManager>();
            }
        }
        return Instance;
    }
}
