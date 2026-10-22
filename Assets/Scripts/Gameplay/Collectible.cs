using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        Score,
        Collectible
    }

    public CollectibleType collectibleType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (collectibleType == CollectibleType.Score)
            {
                ScoreManager.Instance.AddScore();
            }
            else if (collectibleType == CollectibleType.Collectible)
            {
                ScoreManager.Instance.AddCollectible();
            }

            Destroy(gameObject);
        }
    }
}
