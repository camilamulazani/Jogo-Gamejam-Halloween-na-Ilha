using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject gameOverScreen;
    public TMP_Text FinalscoreCountText;
    public TMP_Text FinalcollectiblesCountText;

    public TMP_Text scoreCountText;
    public TMP_Text collectiblesCountText;
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateScore(0);
        UpdateCollectibles(0);
        gameOverScreen.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        
        scoreCountText.text = "Pontuação: " + score;
    
    }

    public void UpdateCollectibles(int collectibles)
    {
        collectiblesCountText.text =  ScoreManager.Instance.currentCollectibles + "/" + ScoreManager.Instance.requiredCollectibles;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        FinalscoreCountText.text = "Pontuação: " + ScoreManager.Instance.currentScore;
        FinalcollectiblesCountText.text = "Itens coletados: " + ScoreManager.Instance.currentCollectibles + "/" + ScoreManager.Instance.requiredCollectibles;
    }
}
