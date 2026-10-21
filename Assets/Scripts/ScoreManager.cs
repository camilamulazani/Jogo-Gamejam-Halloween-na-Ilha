using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int currentScore = 0;
    public int currentCollectibles = 0;

    private int requiredScore = 100;
    public int requiredCollectibles = 7; 
    
    public void Start()
    {
        Instance = this;
        UpdateUI();
    }
    public void AddScore()
    {
        currentScore ++;
        UpdateUI();
        CheckWinCondition();
    }

    public void AddCollectible()
    {
        currentCollectibles++;
        UpdateUI();
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (currentScore >= requiredScore && currentCollectibles >= requiredCollectibles)
        {
            Debug.Log("Venceu uouiuuu :)");
            // TODO: IMPLEMENTAR BAGULHO DE VITORIA
        }

        if (currentScore <= requiredScore && currentCollectibles >= requiredCollectibles)
        {
            Debug.Log("Perdeu uouiuuu :(");
            // TODO: IMPLEMENTAR BAGULHO DE DERROTA
        }
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpdateScore(currentScore);
        UIManager.Instance.UpdateCollectibles(currentCollectibles);
    }
}
