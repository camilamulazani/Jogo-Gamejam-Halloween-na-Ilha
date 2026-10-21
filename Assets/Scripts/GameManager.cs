using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }


    private GameObject gameplayUI;
    public GameObject gameOverScreen; 
    void Start()
    {
        Instance = this;
        gameOverScreen.SetActive(false);
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
        gameOverScreen.SetActive(true);

    }
}
