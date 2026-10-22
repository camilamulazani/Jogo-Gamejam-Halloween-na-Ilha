using UnityEngine;

public enum CollectibleType {Score, Collectible, ReverseControls, Shield, RestoreHealth}

public class Collectible : MonoBehaviour
{
    public CollectibleType collectibleType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
