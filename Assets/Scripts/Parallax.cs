
using UnityEngine;

public class SimpleParallax : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float imageWidth = 19f;

    private void Update()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.IsGameOver)
            return;

        transform.position +=
            Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= -imageWidth)
        {
            transform.position +=
                Vector3.right * imageWidth * 2f;
        }
    }
}
