using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TMP_Text scoreCountText;
    public TMP_Text collectiblesCountText;
    void Start()
    {
        Instance = this;
    }
    public void UpdateScore(int score)
    {
        
        scoreCountText.text = "Pontuação: " + score;
    
    }

    public void UpdateCollectibles(int collectibles)
    {
        collectiblesCountText.text =  ScoreManager.Instance.currentCollectibles + "/" + ScoreManager.Instance.requiredCollectibles;
    }
}
