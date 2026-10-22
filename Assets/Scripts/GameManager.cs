using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }


    private GameObject gameplayUI;
    
    void Start()
    {
        Instance = this;

        gameplayUI = GameObject.Find("GameplayUI");
        if (gameplayUI != null)
        {
            gameplayUI.SetActive(true);
    }
        }
        
        

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        if (IsGameOver) return;

        IsGameOver = true;

        gameplayUI.SetActive(false);
        UIManager.Instance.GameOver();

    }

    public void CheckWinCondition()
    {
        if (ScoreManager.Instance.currentScore >= ScoreManager.Instance.requiredScore && ScoreManager.Instance.currentCollectibles >= ScoreManager.Instance.requiredCollectibles)
        {
            SceneManager.LoadScene("VictoryScene");
            Debug.Log("Victory!");
        }

        else
        {
            SceneManager.LoadScene("DefeatScene");
            Debug.Log("Defeat!");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }   



}
