using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerHealth.TakeDamage(1);
        }

        Collectible collectible = collision.GetComponent<Collectible>();
        if (collectible != null)
        {
            switch (collectible.collectibleType)
            {
                case CollectibleType.Score:
                    ScoreManager.Instance.AddScore();
                    break;
                case CollectibleType.Collectible:
                    ScoreManager.Instance.AddCollectible();
                    break;
                case CollectibleType.ReverseControls:
                    PlayerController.Instance.ReverseControls();
                    break;
                case CollectibleType.Shield:
                    PlayerHealth.Instance.ActivateShield();
                    break;
                case CollectibleType.RestoreHealth:
                    PlayerHealth.Instance.RestoreHealth();
                    break;
            }

            Destroy(collision.gameObject);
        }
    }
}
