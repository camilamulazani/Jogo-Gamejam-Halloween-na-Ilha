using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int currentScore = 0;
    public int currentCollectibles = 0;

    public int requiredScore = 100;
    public int requiredCollectibles = 7;

    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        UpdateUI();
    }
    public void AddScore()
    {
        currentScore ++;
        UpdateUI();
    }

    public void AddCollectible()
    {
        currentCollectibles++;
        UpdateUI();
        if (currentCollectibles >= requiredCollectibles)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpdateScore(currentScore);
        UIManager.Instance.UpdateCollectibles(currentCollectibles);
    }
}
